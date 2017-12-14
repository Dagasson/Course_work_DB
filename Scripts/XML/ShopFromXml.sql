Use Course_work

delete from [Shop];

DBCC CHECKIDENT ('[Shop]', RESEED, 0);

IF OBJECT_ID('[dbo].[insertShopFromXml]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[insertShopFromXml] 
END 
GO
CREATE PROC [dbo].[insertShopFromXml] 
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

		INSERT INTO Shop(Category,Name,Cost,dateOfIncoming,info) 
			SELECT 
			C3.value('Category[1]', 'nvarchar(32)') AS Category,
			C3.value('Name[1]', 'nvarchar(32)') AS Name,
			C3.value('Cost[1]', 'money') AS Cost,
			C3.value('dateOfIncoming[1]', 'date') AS dateOfIncoming,
			C3.value('info[1]', 'nvarchar(100)') AS info
			FROM @xml.nodes('Root/Order') AS T3(C3) 
	COMMIT;
	set @count2 = (select count(*) from Shop)
	return @count2 - @count1;
end;
GO

select * from Shop;
exec [dbo].[insertShopFromXml] @path = 'E:\Study\3-1\DB\Курсач\Course_work\Shop_XML.xml'


