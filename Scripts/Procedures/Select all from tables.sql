USE [Course_work];
GO

IF OBJECT_ID('[dbo].[usp_DeliverySelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_DeliverySelectAll] 
END 
GO
CREATE PROC [dbo].[usp_DeliverySelectAll] 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Delivery]

	COMMIT
GO


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

IF OBJECT_ID('[dbo].[usp_OrdersSelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_OrdersSelectAll] 
END 
GO
CREATE PROC [dbo].[usp_OrdersSelectAll]
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Orders] 
	COMMIT
GO

IF OBJECT_ID('[dbo].[usp_productOnOrderSelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_productOnOrderSelectAll] 
END 
GO
CREATE PROC [dbo].[usp_productOnOrderSelectAll] 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[productOnOrder]

	COMMIT
GO


IF OBJECT_ID('[dbo].[usp_ShipperSelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_ShipperSelectAll] 
END 
GO
CREATE PROC [dbo].[usp_ShipperSelectAll]
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Shipper] 
	
	COMMIT
GO


IF OBJECT_ID('[dbo].[usp_UsersSelectAll]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_UsersSelectAll] 
END 
GO
CREATE PROC [dbo].[usp_UsersSelectAll] 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * FROM   [dbo].[Users] 

	COMMIT
GO