DECLARE @pathName NVARCHAR(512) 
SET @pathName = 'E:\BackupDB\db_backup_' + Convert(varchar(8), GETDATE(), 112) + '.bak' 
BACKUP DATABASE [Course_work] TO  DISK = @pathName WITH NOFORMAT, NOINIT,  NAME = N'db_backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
