USE [Course_work]
GO
/****** Object:  StoredProcedure [dbo].[usp_UserSelectByLogin]    Script Date: 07.12.2017 22:14:14 ******/
-------------------------Выборка Пользователя по логину.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_UserSelectByLogin] 
    @login nvarchar(16),
	@password nvarchar(16)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	declare @adm int;
	select @adm=[isAdmin]from Users where Login=@login and Password=@password;

	if (@adm!=0)
	select * from Users where Login=@login and Password=@password;
	return @adm;
	COMMIT

	--------------------Выбор заказов пользователя
	IF OBJECT_ID('[dbo].[usp_SelectUsersOrders]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_SelectUsersOrders] 
END 
GO
CREATE PROC [dbo].[usp_SelectUsersOrders]
@idOfUser int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Orders] where [idOfUser]=@idOfUser;
	COMMIT
GO	
	
	--------------------Выбор актуальных заказов конкретного пользователя
	IF OBJECT_ID('[dbo].[usp_SelectCurrentOrders]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_SelectCurrentOrders] 
END 
GO
CREATE PROC [dbo].[usp_SelectCurrentOrders]
@idOfUser int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Orders] where (([dateOfAssembly]>=CONVERT(date, SYSDATETIME())) and ([idOfUser]=@idOfUser))
	COMMIT
GO

--------------------Выборка продуктов по номеру заказа.
	IF OBJECT_ID('[dbo].[usp_SelectproductByOrdersId]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_SelectproductByOrdersId] 
END 
GO
CREATE PROC [dbo].[usp_SelectproductByOrdersId]
@idOfOrder int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT Name, Category, amount, dateOfIncoming FROM   [dbo].[productOnOrder] inner join [dbo].[Shop] on productOnOrder.idOfProduct=Shop.id 
	where (productOnOrder.idOfOrder=@idOfOrder)
	COMMIT
GO

---------------------------Получить стоимость заказа
	IF OBJECT_ID('[dbo].[usp_getSummOfOrder]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_getSummOfOrder] 
END 
GO
CREATE PROC [dbo].[usp_getSummOfOrder]
@idOfOrder int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
declare @currSum money;
create table #t (summ money);
	SELECT @currSum=SUM(productOnOrder.amount*Shop.Cost) FROM   [dbo].[productOnOrder], [dbo].Shop where (Shop.id=productOnOrder.idOfProduct) and (productOnOrder.idOfOrder=@idOfOrder)
	
	
	insert into #t values (@currSum);
	if(@currSum!=0)
	select * from #t;
	COMMIT
	return @currSum;
	
GO


------------------------------Обновить сумму заказа
IF OBJECT_ID('[dbo].[usp_OrdersSumUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersSumUpdate] 
END 
GO
CREATE PROC [dbo].[usp_OrdersSumUpdate] 
    @id int,
    @Summ money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Orders]
	SET    [Summ] = @Summ
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfUser], [Summ], [dateOfAssembly]
	FROM   [dbo].[Orders]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO

------------проверка даты прибытия
IF OBJECT_ID('[dbo].[usp_OrdersDateUpdate] ') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersDateUpdate] 
END 
GO
CREATE PROC [dbo].[usp_OrdersDateUpdate] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	declare @maxdate date;
	
	select @maxdate=max(dateOfIncoming) from [dbo].Shop inner join [dbo].productOnOrder on Shop.id=productOnOrder.idOfProduct 
	where productOnOrder.idOfOrder=@id;


	UPDATE [dbo].[Orders]
	SET    [dateOfAssembly]=@maxdate
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfUser], [Summ], [dateOfAssembly]
	FROM   [dbo].[Orders]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO

---------------------Получить товары в диапазоне цен
IF OBJECT_ID('[dbo].[usp_getProductByCost] ') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_getProductByCost] 
END 
GO
CREATE PROC [dbo].[usp_getProductByCost] 
    @fromSum money,
	@toSum money
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	if(@toSum!=0 and @toSum>=@fromSum)
	select * from [dbo].Shop where Shop.Cost between @fromSum and @toSum;
	else if(@toSum=0 and @fromSum=0) select * from [dbo].Shop;

	COMMIT
GO

--------------------------Назначить/убрать администратора
IF OBJECT_ID('[dbo].[usp_UsersIsAdminUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersIsAdminUpdate] 
END 
GO
CREATE PROC [dbo].[usp_UsersIsAdminUpdate] 
    @id int,
    @isAdmin int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Users]
	SET    [isAdmin] = @isAdmin
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT * FROM   [dbo].[Users] WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO

----------------------Обычный insert, но меняющий данные даты в магазине.

USE [Course_work];
GO
IF OBJECT_ID('[dbo].[usp_DeliveryInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_DeliveryInsert] 
END 
GO
CREATE PROC [dbo].[usp_DeliveryInsert] 
    @Name nvarchar(32),
    @dateOfIncoming date,
    @Cost money,
    @amount int,
    @idOfShipper int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	declare @shopDate Date;
	INSERT INTO [dbo].[Delivery] ([Name], [dateOfIncoming], [Cost], [amount], [idOfShipper])
	SELECT @Name, @dateOfIncoming, @Cost, @amount, @idOfShipper;
	select @shopDate=Shop.dateOfIncoming from Shop where Shop.Name=@Name;
	Update Storage set hmOnStorage+=@amount where @Name=Storage.NameOfProduct;
	if(@dateOfIncoming>@shopDate)
	Update Shop set dateOfIncoming=@dateOfIncoming where Shop.Name=@Name;
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Name], [dateOfIncoming], [Cost], [amount], [idOfShipper]
	FROM   [dbo].[Delivery]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO

--------------------------------------------Вставка товара по заказу с изъястием из склада.

IF OBJECT_ID('[dbo].[usp_productOnOrderInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_productOnOrderInsert] 
END 
GO
CREATE PROC [dbo].[usp_productOnOrderInsert] 
    @idOfOrder int,
    @idOfProduct int,
    @amount int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[productOnOrder] ([idOfOrder], [idOfProduct], [amount])
	SELECT @idOfOrder, @idOfProduct, @amount
	declare @currAmount int;
	declare @nameOfProd nvarchar(32);
	select @currAmount=hmOnStorage, @nameOfProd=Storage.NameOfProduct from Storage, Shop 
	where @idOfProduct=Shop.id and Shop.Name=Storage.NameOfProduct;
	
	if(@currAmount-@amount<0)
	Update Storage set hmOnStorage=0 where nameOfProduct=@nameOfProd;
	If(@currAmount-@amount>=0)
	Update Storage set hmOnStorage=(@currAmount-@amount) where nameOfProduct=@nameOfProd;
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfOrder], [idOfProduct], [amount]
	FROM   [dbo].[productOnOrder]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO

-----------------------------------При добавлении товара в магазин добавляем его же в Склад.
IF OBJECT_ID('[dbo].[usp_ShopInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopInsert] 
END 
GO
CREATE PROC [dbo].[usp_ShopInsert] 
    @Category nvarchar(32),
    @Name nvarchar(32),
    @Cost money,
    @dateOfIncoming date,
    @info nvarchar(100) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Shop] ([Category], [Name], [Cost], [dateOfIncoming], [info])
	SELECT @Category, @Name, @Cost, @dateOfIncoming, @info;
	
	exec [dbo].[usp_StorageInsert] @nameOfProduct=@Name, @hmOnStorage=0;
	-- Begin Return Select <- do not remove
	SELECT [id], [Category], [Name], [Cost], [dateOfIncoming], [info]
	FROM   [dbo].[Shop]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO





