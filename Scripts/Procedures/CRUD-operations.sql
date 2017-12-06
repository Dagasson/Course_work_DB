USE [Course_work];
GO

IF OBJECT_ID('[dbo].[usp_DeliverySelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_DeliverySelect] 
END 
GO
CREATE PROC [dbo].[usp_DeliverySelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [Name], [dateOfIncoming], [Cost], [amount], [idOfShipper] 
	FROM   [dbo].[Delivery] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
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
	
	INSERT INTO [dbo].[Delivery] ([Name], [dateOfIncoming], [Cost], [amount], [idOfShipper])
	SELECT @Name, @dateOfIncoming, @Cost, @amount, @idOfShipper
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Name], [dateOfIncoming], [Cost], [amount], [idOfShipper]
	FROM   [dbo].[Delivery]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_DeliveryUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_DeliveryUpdate] 
END 
GO
CREATE PROC [dbo].[usp_DeliveryUpdate] 
    @id int,
    @Name nvarchar(32),
    @dateOfIncoming date,
    @Cost money,
    @amount int,
    @idOfShipper int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Delivery]
	SET    [Name] = @Name, [dateOfIncoming] = @dateOfIncoming, [Cost] = @Cost, [amount] = @amount, [idOfShipper] = @idOfShipper
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Name], [dateOfIncoming], [Cost], [amount], [idOfShipper]
	FROM   [dbo].[Delivery]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_DeliveryDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_DeliveryDelete] 
END 
GO
CREATE PROC [dbo].[usp_DeliveryDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Delivery]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[usp_OrdersSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersSelect] 
END 
GO
CREATE PROC [dbo].[usp_OrdersSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [idOfUser], [Summ], [dateOfAssembly] 
	FROM   [dbo].[Orders] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_OrdersInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersInsert] 
END 
GO
CREATE PROC [dbo].[usp_OrdersInsert] 
    @idOfUser int,
    @Summ money,
    @dateOfAssembly date
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Orders] ([idOfUser], [Summ], [dateOfAssembly])
	SELECT @idOfUser, @Summ, @dateOfAssembly
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfUser], [Summ], [dateOfAssembly]
	FROM   [dbo].[Orders]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_OrdersUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersUpdate] 
END 
GO
CREATE PROC [dbo].[usp_OrdersUpdate] 
    @id int,
    @idOfUser int,
    @Summ money,
    @dateOfAssembly date
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Orders]
	SET    [idOfUser] = @idOfUser, [Summ] = @Summ, [dateOfAssembly] = @dateOfAssembly
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfUser], [Summ], [dateOfAssembly]
	FROM   [dbo].[Orders]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_OrdersDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersDelete] 
END 
GO
CREATE PROC [dbo].[usp_OrdersDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Orders]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[usp_productOnOrderSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_productOnOrderSelect] 
END 
GO
CREATE PROC [dbo].[usp_productOnOrderSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [idOfOrder], [idOfProduct], [amount] 
	FROM   [dbo].[productOnOrder] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
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
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfOrder], [idOfProduct], [amount]
	FROM   [dbo].[productOnOrder]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_productOnOrderUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_productOnOrderUpdate] 
END 
GO
CREATE PROC [dbo].[usp_productOnOrderUpdate] 
    @id int,
    @idOfOrder int,
    @idOfProduct int,
    @amount int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[productOnOrder]
	SET    [idOfOrder] = @idOfOrder, [idOfProduct] = @idOfProduct, [amount] = @amount
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [idOfOrder], [idOfProduct], [amount]
	FROM   [dbo].[productOnOrder]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_productOnOrderDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_productOnOrderDelete] 
END 
GO
CREATE PROC [dbo].[usp_productOnOrderDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[productOnOrder]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[usp_ShipperSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShipperSelect] 
END 
GO
CREATE PROC [dbo].[usp_ShipperSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [Name], [numberOfCards] 
	FROM   [dbo].[Shipper] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShipperInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShipperInsert] 
END 
GO
CREATE PROC [dbo].[usp_ShipperInsert] 
    @Name nvarchar(32),
    @numberOfCards int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Shipper] ([Name], [numberOfCards])
	SELECT @Name, @numberOfCards
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Name], [numberOfCards]
	FROM   [dbo].[Shipper]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShipperUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShipperUpdate] 
END 
GO
CREATE PROC [dbo].[usp_ShipperUpdate] 
    @id int,
    @Name nvarchar(32),
    @numberOfCards int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Shipper]
	SET    [Name] = @Name, [numberOfCards] = @numberOfCards
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Name], [numberOfCards]
	FROM   [dbo].[Shipper]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShipperDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShipperDelete] 
END 
GO
CREATE PROC [dbo].[usp_ShipperDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Shipper]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[usp_ShopSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopSelect] 
END 
GO
CREATE PROC [dbo].[usp_ShopSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [Category], [Name], [Cost], [onStorage], [dateOfIncoming], [hmOnStorage], [info] 
	FROM   [dbo].[Shop] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShopInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopInsert] 
END 
GO
CREATE PROC [dbo].[usp_ShopInsert] 
    @Category nvarchar(32),
    @Name nvarchar(32),
    @Cost money,
    @onStorage nvarchar(3),
    @dateOfIncoming date,
    @hmOnStorage int = NULL,
    @info nvarchar(100) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Shop] ([Category], [Name], [Cost], [onStorage], [dateOfIncoming], [hmOnStorage], [info])
	SELECT @Category, @Name, @Cost, @onStorage, @dateOfIncoming, @hmOnStorage, @info
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Category], [Name], [Cost], [onStorage], [dateOfIncoming], [hmOnStorage], [info]
	FROM   [dbo].[Shop]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShopUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopUpdate] 
END 
GO
CREATE PROC [dbo].[usp_ShopUpdate] 
    @id int,
    @Category nvarchar(32),
    @Name nvarchar(32),
    @Cost money,
    @onStorage nvarchar(3),
    @dateOfIncoming date,
    @hmOnStorage int = NULL,
    @info nvarchar(100) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Shop]
	SET    [Category] = @Category, [Name] = @Name, [Cost] = @Cost, [onStorage] = @onStorage, [dateOfIncoming] = @dateOfIncoming, [hmOnStorage] = @hmOnStorage, [info] = @info
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Category], [Name], [Cost], [onStorage], [dateOfIncoming], [hmOnStorage], [info]
	FROM   [dbo].[Shop]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_ShopDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopDelete] 
END 
GO
CREATE PROC [dbo].[usp_ShopDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Shop]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[usp_UsersSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersSelect] 
END 
GO
CREATE PROC [dbo].[usp_UsersSelect] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [id], [Login], [Password], [Balance], [isAdmin] 
	FROM   [dbo].[Users] 
	WHERE  ([id] = @id OR @id IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_UsersInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersInsert] 
END 
GO
CREATE PROC [dbo].[usp_UsersInsert] 
    @Login nvarchar(16),
    @Password nvarchar(16),
    @Balance money = NULL,
    @isAdmin int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Users] ([Login], [Password], [Balance], [isAdmin])
	SELECT @Login, @Password, @Balance, @isAdmin
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Login], [Password], [Balance], [isAdmin]
	FROM   [dbo].[Users]
	WHERE  [id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_UsersUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersUpdate] 
END 
GO
CREATE PROC [dbo].[usp_UsersUpdate] 
    @id int,
    @Login nvarchar(16),
    @Password nvarchar(16),
    @Balance money = NULL,
    @isAdmin int = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Users]
	SET    [Login] = @Login, [Password] = @Password, [Balance] = @Balance, [isAdmin] = @isAdmin
	WHERE  [id] = @id
	
	-- Begin Return Select <- do not remove
	SELECT [id], [Login], [Password], [Balance], [isAdmin]
	FROM   [dbo].[Users]
	WHERE  [id] = @id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_UsersDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersDelete] 
END 
GO
CREATE PROC [dbo].[usp_UsersDelete] 
    @id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Users]
	WHERE  [id] = @id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

