create table Users --������������
(id int identity(1,1) primary key,--�� ������������
Login nvarchar(16) unique not null, --�����
Password nvarchar(16) not null, --������
idOfCards int unique, --����� �����
isAdmin int default 0 --����� ��
);

create table Shop --�������
(
id int identity(1,1) primary key, --�� ������
Category nvarchar(32) not null, --���������
Name nvarchar(32) not null unique, --������������
Cost money not null, --���������
dateOfIncoming date not null, --���� ��������
info nvarchar(100) --����������
);

create table Shipper --��������
(
	id int identity(1,1) primary key,
	Name nvarchar(32) not null,--������������
	numberOfCards int unique not null --����� �����
);

Create table Delivery--��������
(
	id int identity(1,1) primary key,
	Name nvarchar(32) not null,--������������ ������
	dateOfIncoming date not null,--���� ��������
	Cost money not null,--��������� ����� ������� ������
	amount int not null,--����������
	idOfShipper int
);

create table productOnOrder --����� � ������
(
	id int identity(1,1) primary key,
	idOfOrder int not null, --�� ������
	idOfProduct int not null, --������������ ������
	amount int not null --����������
);

create table Orders--������
(
	id int identity(1,1) primary key,--�� ������
	idOfUser int not null,--�� ������������
	Summ money not null,--�����
	dateOfAssembly date not null--���� ������
);
create table Storage--�����
(id int identity(1,1) primary key,
nameOfProduct nvarchar(32) unique, --������������ ������
hmOnStorage int default 0 --���������� �� ������
);
