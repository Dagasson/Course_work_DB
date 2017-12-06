create table Users --пользователи
(id int identity(1,1) primary key,--ид пользователЯ
Login nvarchar(16) unique not null, --логин
Password nvarchar(16) not null, --пароль
Balance money default 0, --баланс
isAdmin int default 0 --админ ли
);

create table Shop --магазин
(
id int identity(1,1) primary key, --ид товара
Category nvarchar(32) not null, --категориЯ
Name nvarchar(32) not null unique, --наименование
Cost money not null, --стоимость
onStorage nvarchar(3) not null,--есть ли в наличии
dateOfIncoming date not null, --дата поставки
hmOnStorage int, --количество на складе
info nvarchar(100) --информациЯ
);

create table Shipper --продавец
(
	id int identity(1,1) primary key,
	Name nvarchar(32) not null,--наименование
	numberOfCards int not null --номер карты
);

Create table Delivery
(
	id int identity(1,1) primary key,
	Name nvarchar(32) not null,
	dateOfIncoming date not null,--дата поставки
	Cost money not null,--стоимость одной единицы товара
	amount int not null,--количество
	idOfShipper int
);

create table productOnOrder --товар в заказе
(
	id int identity(1,1) primary key,
	idOfOrder int not null, --ид заказа
	idOfProduct int not null, --наименование товара
	amount int not null --количество
);

create table Orders
(
	id int identity(1,1) primary key,--ид заказа
	idOfUser int not null,--ид пользователЯ
	Summ money not null,--сумма
	dateOfAssembly date not null--дата сборки
);

