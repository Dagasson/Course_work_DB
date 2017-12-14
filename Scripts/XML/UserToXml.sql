EXEC master.dbo.sp_configure 'show advanced options', 1;							--allow= instance tuning 
RECONFIGURE;
EXEC master.dbo.sp_configure 'xp_cmdshell', 1;										--start xp_cmdshell for bcp operates
RECONFIGURE;

----------------------------------------Write Books+Authors to XML-----------------------
IF OBJECT_ID('[dbo].[exportUsersToXML]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[exportUsersToXML] 
END 
GO
CREATE PROC [dbo].[exportUsersToXML] 
	@path nvarchar(256)																	--path to file
AS 
begin
	BEGIN TRAN
		declare @sql nvarchar(500)=
				  'bcp "SELECT * '+								--select 
		'FROM  [dbo].[Users] '+																										--select
		'  FOR XML PATH(''Users''), ROOT(''Root'')" queryout "'+@path+'"  -S Ashenvale  -d Course_work   -w -T ';     --once_item_name/ root_name/ path to file/ server path/ warchar + T
		EXEC xp_cmdshell @sql;
	COMMIT;
end;
GO

exec [dbo].[exportUsersToXML] @path = 'E:\Study\3-1\DB\Курсач\Course_work\Users_XML.xml'