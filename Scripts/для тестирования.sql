use Course_work

 drop index [Shop].ordersIndex 
 
 select Cost from Shop
create index ordersIndex 
on [dbo].[Shop](Cost asc)
select Cost from Shop

delete from Shop where Category Like 'cat%'