create table MARKETS (
	MarketID varchar(10) primary key not null,
	MarketName nvarchar(100) not null,
	Address nvarchar(200) not null,
	Phone int,
	Image nvarchar(max)
);

create table USERTYPE (
	TypeID int primary key not null,
	TypeName varchar(20)
);

create table USERS (
	UserID varchar(10) primary key not null,
	Name nvarchar(50) not null,
	Birthday date,
	Email nvarchar(30),
	Phone int,
	Gender bit default 0 not null,
	Address nvarchar(200),
	Avatar nvarchar(max),
	Username varchar(30),
	Pass varbinary(max),
	TypeID int foreign key references USERTYPE not null
);

create table STORES(
	StoreID varchar(10) primary key,
	StoreName nvarchar(100) not null,
	KiotNo varchar(8),
	Image nvarchar(max),
	Phone int,
	MarketID varchar(10) not null foreign key references MARKETS,
	OwnerID varchar(10) not null foreign key references USERS
);

create table PRODUCTS (
	ProductID varchar(10) primary key not null,
	ProductName nvarchar(100) not null,
	Image nvarchar(max),
	UnitPrice float not null,
	NumberOf int default 0 not null,
	Unit nvarchar(20),
	StoreID varchar(10) foreign key references STORES not null
);

create table BILL (
	BillID varchar(10) primary key not null,
	CreatedDate date not null,
	StoreID varchar(10) foreign key references STORES not null,
	TotalPrice float,
	ReducedPrice float,
);

create table DETAILBILL (
	BillID varchar(10) foreign key references BILL not null,
	ProductID varchar(10) foreign key references PRODUCTS not null,
	NumberOf int,
	UnitPrice float,
	Unit nvarchar(20),
	primary key(BillID,ProductID)
);

create table SALESINFO (
	SaleID int identity(1,1) primary key not null,
	Title nvarchar(200) not null,
	Image nvarchar(max),
	Description nvarchar(max),
	StartDate date,
	EndDate date,
	StoreID varchar(10) foreign key references STORES not null
);

create table COMMENTS (
	ID int identity(1,1) primary key not null,
	CommentDate date,
	CommentTime time,
	StoreID varchar(10) foreign key references STORES not null
);

create table REVENUESTATISTIC (
	StoreID varchar(10) foreign key references STORES not null,
	BillID varchar(10) foreign key references BILL not null,
	Total float,
	CreatedDate date,
	primary key(StoreID,BillID)
);

create table PRODUCTSTATISTIC (
	BillID varchar(10) foreign key references BILL not null,
	ProductID varchar(10) foreign key references PRODUCTS not null,
	NumberOf int,
	UnitPrice float,
	Unit nvarchar(20),
	SaleDate date,
	primary key(BillID,ProductID)
);

select * from USERS