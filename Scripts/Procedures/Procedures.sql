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
---------------------------Выборка всех товаров из магазина
	IF OBJECT_ID('[dbo].[usp_ShopSelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShopSelectAll] 
END 
GO
CREATE PROC [dbo].[usp_ShopSelectAll]
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Shop] 

	COMMIT
GO