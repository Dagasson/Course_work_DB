use master;

drop LOGIN [IS_user];
drop LOGIN [IS_admin];

drop user [user];
drop user [admin];
use master;
CREATE LOGIN [IS_user] WITH PASSWORD=N'IS_user',
 DEFAULT_DATABASE=[Course_work], DEFAULT_LANGUAGE=[русский], 
 CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [IS_user] ENABLE
GO
CREATE LOGIN [IS_admin] WITH PASSWORD=N'IS_admin',
 DEFAULT_DATABASE=[Course_work], DEFAULT_LANGUAGE=[русский], 
 CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [IS_admin] ENABLE
GO
use Course_work;
CREATE USER [user] FOR LOGIN [IS_user];  
GO  
CREATE USER [admin] FOR LOGIN [IS_admin];  
GO  