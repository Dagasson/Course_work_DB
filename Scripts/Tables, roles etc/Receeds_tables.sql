Use Course_work;--���������� id
DBCC CHECKIDENT ('[Shop]', RESEED, 0);
DBCC CHECKIDENT ('[productOnOrder]', RESEED, 0);
DBCC CHECKIDENT ('[Delivery]', RESEED, 0);
DBCC CHECKIDENT ('[Orders]', RESEED, 0);
DBCC CHECKIDENT ('[Shipper]', RESEED, 0);
DBCC CHECKIDENT ('[Users]', RESEED, 0);