create table paybayservice.Markets (
	MaketId int identity(1,1) primary key not null,
	MarketName nvarchar(100) not null,
	Address nvarchar(200) not null,
	Phone varchar(12),
	Image varbinary(max)
);

create table paybayservice.UserType (	
	TypeId int identity(1,1) not null primary key,
	TypeName varchar(20)
);

create table paybayservice.Users (
	UserId int identity(1,1) not null primary key,
	Name nvarchar(50) not null,
	Birthday date,
	Email nvarchar(30),
	Phone varchar(12),
	Gender bit default 0 not null,
	Address nvarchar(200),
	Avatar varbinary(max),
	Username varchar(30),
	Pass varbinary(max),
	TypeID int not null,	
);

create table paybayservice.Stores (
	StoreId int identity(1,1) not null primary key,
	StoreName nvarchar(100) not null,
	KiotNo varchar(8),
	Image varbinary(max),
	Phone varchar(12),
	MarketID int not null,
	OwnerID int not null ,
);

create table paybayservice.Products (
	ProductId int identity(1,1) primary key not null,
	ProductName nvarchar(100) not null,
	Image varbinary(max),
	UnitPrice float not null,
	NumberOf int default 0 not null,
	Unit nvarchar(20),
	StoreID int not null,
);

create table paybayservice.Bills (
	BillId int identity(1,1) primary key not null,
	CreatedDate date not null,
	StoreID int not null,
	--constraint FK_StoreBill foreign key (StoreID) references paybayservice.STORES ,
	TotalPrice float,
	ReducedPrice float,
	UserID int not null,
	--constraint FK_UserBill foreign key (UserID) references paybayservice.USERS 
);

create table paybayservice.DetailBill (
	Id int identity(1,1) not null primary key,
	BillID int not null,
	--constraint FK_DT_B foreign key (BillID) references paybayservice.Bills,
	ProductID int not null,
	--constraint FK_DT_P foreign key (ProductID) references paybayservice.PRODUCTS,
	NumberOf int,
	UnitPrice float,
	Unit nvarchar(20),
	--primary key(BillID,ProductID)
);

create table paybayservice.SaleInfo (
	SaleId int identity(1,1) primary key not null,
	Title nvarchar(200) not null,
	Image varbinary(max),
	Describes nvarchar(max),
	StartDate date,
	EndDate date,
	StoreID int not null,
	--constraint FK_S_S foreign key (StoreID) references paybayservice.STORES,
	isRequired bit not null default 0
);

create table paybayservice.Comments (
	Id int identity(1,1) primary key not null,
	CommentDate date,
	CommentTime time,
	StoreID int not null,
	--constraint FK_S_C foreign key (StoreID) references paybayservice.STORES ,
	UserID int not null,
	--constraint FK_U_C foreign key (UserID) references paybayservice.USERS ,
	Content nvarchar(max)
);

create table paybayservice.RevenueStatistic (
	Id int identity(1,1) not null primary key,
	StoreID int not null,
	--constraint FK_S_R foreign key (StoreID) references paybayservice.STORES,
	BillID int not null,
	--constraint FK_B_R foreign key (BillID) references paybayservice.BILLS,
	Total float,
	CreatedDate date
);

create table paybayservice.ProductStatistic (
	Id int identity(1,1) not null primary key,
	BillID int not null,
	--constraint FK_B_P foreign key (BillID) references paybayservice.BILLS,
	ProductID int not null,
	--constraint FK_P_P foreign key (ProductID) references paybayservice.PRODUCTS,
	NumberOf int,
	UnitPrice float,
	Unit nvarchar(20),
	SaleDate date
);

create table TodoItem (
	Id varchar(10) primary key,
	Text nvarchar(10),
	Complete bit
);

