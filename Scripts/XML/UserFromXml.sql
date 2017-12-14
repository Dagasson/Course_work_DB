Use Course_work

delete from [Users];

DBCC CHECKIDENT ('[Users]', RESEED, 0);

IF OBJECT_ID('[dbo].[insertUsersFromXml]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[insertUsersFromXml] 
END 
GO
CREATE PROC [dbo].[insertUsersFromXml] 
	@path nvarchar(256)
AS 
begin
	SET NOCOUNT ON  --отлючить вывод кол-ва обработанных строк
	SET XACT_ABORT ON  --ролбэк транзакции и прекращение процедуры
	declare @count1 int=0;
	declare @count2 int=0;
	set @count1 = (select count(*) from Shop)
		
	BEGIN TRAN
		declare @results table (x xml)			--таблица для рерзультата прочтения файла xml
		declare @sql nvarchar(300)=
				   'SELECT 
			CAST(REPLACE(CAST(x AS VARCHAR(MAX)), ''encoding="utf-16"'', ''encoding="utf-8"'') AS XML)
			FROM OPENROWSET(BULK '''+@path+''', SINGLE_BLOB) AS T(x)'; --чтение xml 
 
		INSERT INTO @results EXEC (@sql) -- -> результат в поле таблицы @results

		declare @xml XML = (SELECT  TOP 1 x from  @results);	-- поле с xml таблицы в переменную типа xml

		INSERT INTO Users(Login,Password,idOfCards,isAdmin) 
			SELECT 
			C3.value('Login[1]', 'nvarchar(16)') AS Login,
			C3.value('Password[1]', 'nvarchar(16)') AS Password,
			C3.value('idOfCards[1]', 'int') AS idOfCards,
			C3.value('isAdmin[1]', 'int') AS isAdmin
			FROM @xml.nodes('Root/Users') AS T3(C3) 
	COMMIT;
	set @count2 = (select count(*) from Shop)
	return @count2 - @count1;
end;
GO


select * from Users;
exec [dbo].[insertUsersFromXml] @path = 'E:\Study\3-1\DB\Курсач\Course_work\Users_XML.xml'
