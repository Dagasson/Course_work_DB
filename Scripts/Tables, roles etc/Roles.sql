use Course_work;
--Будет допиливаться
DROP ROLE [Course_work_user];

CREATE ROLE [Course_work_user];

GRANT EXECUTE ON [dbo].[usp_DeliverySelect]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_OrdersSelect]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_productOnOrderSelect]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_ShipperSelect]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_ShopSelect]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_ShopSelectAll]  TO [Course_work_user];
GRANT EXECUTE ON [dbo].[usp_UsersSelect]  TO [Course_work_user];
go


sp_addrolemember 'Course_work_user', 'user';  

GRANT EXECUTE ON SCHEMA::[dbo] TO [admin]