USE [master]
GO
/****** Object:  Database [PayBayDatabase]    Script Date: 3/22/2016 11:09:43 AM ******/
CREATE DATABASE [PayBayDatabase]
GO
ALTER DATABASE [PayBayDatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PayBayDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PayBayDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PayBayDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PayBayDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PayBayDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PayBayDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [PayBayDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PayBayDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PayBayDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PayBayDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PayBayDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PayBayDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PayBayDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PayBayDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PayBayDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PayBayDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PayBayDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PayBayDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PayBayDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PayBayDatabase] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [PayBayDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PayBayDatabase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PayBayDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PayBayDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [PayBayDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [PayBayDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PayBayDatabase] SET DB_CHAINING OFF 
GO
USE [PayBayDatabase]
GO
/****** Object:  User [uAIkhYcpFaLogin_paybayserviceUser]    Script Date: 3/22/2016 11:09:43 AM ******/
CREATE USER [uAIkhYcpFaLogin_paybayserviceUser] FOR LOGIN [uAIkhYcpFaLogin_paybayservice] WITH DEFAULT_SCHEMA=[paybayservice]
GO
/****** Object:  Schema [paybayservice]    Script Date: 3/22/2016 11:09:44 AM ******/
CREATE SCHEMA [paybayservice]
GO
/****** Object:  Table [paybayservice].[Bills]    Script Date: 3/22/2016 11:09:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[Bills](
	[BillId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[StoreID] [int] NOT NULL,
	[TotalPrice] [float] NULL,
	[ReducedPrice] [float] NULL,
	[UserID] [int] NOT NULL,
	[Note] [nvarchar](30) NULL,
	[ShipMethod] [nvarchar](50) NULL,
	[TradeTerm] [nvarchar](10) NULL,
	[AgreeredShippingDate] [nvarchar](100) NULL,
	[ShippingDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[Comments]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentDate] [datetime] NULL,
	[StoreID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[DetailBill]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[DetailBill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[NumberOf] [int] NULL,
	[UnitPrice] [float] NULL,
	[Unit] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[Markets]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [paybayservice].[Markets](
	[MarketId] [int] IDENTITY(1,1) NOT NULL,
	[MarketName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](12) NULL,
	[Image] [nvarchar](max) NULL,
	[SasQuery] [nvarchar](max) NULL,
	[Latitute] [float] NULL CONSTRAINT [DF__Markets__Longitu__4CC05EF3]  DEFAULT ((0)),
	[Longitute] [float] NULL CONSTRAINT [DF__Markets__Latitut__4DB4832C]  DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[MarketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [paybayservice].[MessageInbox]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[MessageInbox](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[OwnerID] [int] NULL,
	[Content] [nvarchar](255) NULL,
	[InboxDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[Products]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[UnitPrice] [float] NOT NULL,
	[NumberOf] [int] NOT NULL DEFAULT ((0)),
	[Unit] [nvarchar](20) NULL,
	[StoreID] [int] NOT NULL,
	[ImportDate] [date] NULL,
	[SalePrice] [float] NOT NULL DEFAULT ((0)),
	[SasQuery] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[ProductStatistic]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[ProductStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[NumberOf] [int] NULL,
	[UnitPrice] [float] NULL,
	[Unit] [nvarchar](20) NULL,
	[SaleDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[RevenueStatistic]    Script Date: 3/22/2016 11:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[RevenueStatistic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NOT NULL,
	[BillID] [int] NOT NULL,
	[Total] [float] NULL,
	[CreatedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[SaleInfo]    Script Date: 3/22/2016 11:09:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[SaleInfo](
	[SaleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Describes] [nvarchar](max) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[StoreID] [int] NOT NULL,
	[isRequired] [bit] NOT NULL DEFAULT ((0)),
	[SasQuery] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[StatisticRating]    Script Date: 3/22/2016 11:09:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [paybayservice].[StatisticRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[StoreID] [int] NULL,
	[RateOfUser] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [paybayservice].[Stores]    Script Date: 3/22/2016 11:09:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [paybayservice].[Stores](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](100) NOT NULL,
	[KiotNo] [varchar](8) NULL,
	[Image] [nvarchar](max) NULL,
	[Phone] [varchar](12) NULL,
	[MarketID] [int] NOT NULL,
	[OwnerID] [int] NOT NULL,
	[Rate] [float] NULL,
	[SasQuery] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [paybayservice].[Users]    Script Date: 3/22/2016 11:09:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [paybayservice].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Birthday] [date] NULL,
	[Email] [nvarchar](30) NULL,
	[Phone] [varchar](12) NULL,
	[Gender] [bit] NOT NULL DEFAULT ((0)),
	[Address] [nvarchar](200) NULL,
	[Avatar] [nvarchar](max) NULL,
	[TypeID] [int] NOT NULL,
	[Username] [nvarchar](30) NULL,
	[Pass] [varbinary](max) NULL,
	[SasQuery] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [paybayservice].[UserType]    Script Date: 3/22/2016 11:09:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [paybayservice].[UserType](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [paybayservice].[Bills] ON 

INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (1, CAST(N'2016-02-11' AS Date), 1, 80000, 0, 1, N'Đến lấy', NULL, NULL, NULL, NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (3, CAST(N'2016-03-04' AS Date), 1, 300000, 0, 9, NULL, N'Land Transportation', N'Cash', N'Ship 2 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (4, CAST(N'2016-03-04' AS Date), 1, 175000, 0, 9, NULL, N'Express', N'Cash', N'Ship 2 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (5, CAST(N'2016-03-04' AS Date), 1, 128000, 0, 9, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (6, CAST(N'2016-03-04' AS Date), 1, 180000, 0, 9, NULL, N'Express', N'Cash', N'Ship 2 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (7, CAST(N'2016-03-04' AS Date), 1, 15000, 0, 9, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (8, CAST(N'2016-03-04' AS Date), 1, 16000, 0, 9, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (9, CAST(N'2016-03-04' AS Date), 1, 9700, 300, 9, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (10, CAST(N'2016-03-04' AS Date), 1, 90000, 0, 8, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (11, CAST(N'2016-03-04' AS Date), 1, 70000, 15000, 10, NULL, N'Sea Freight', N'Cash', N'Ship 1 day(s) after supplier receives initial payment', NULL)
INSERT [paybayservice].[Bills] ([BillId], [CreatedDate], [StoreID], [TotalPrice], [ReducedPrice], [UserID], [Note], [ShipMethod], [TradeTerm], [AgreeredShippingDate], [ShippingDate]) VALUES (12, CAST(N'2016-03-04' AS Date), 1, 60000, 15000, 10, NULL, N'Land Transportation', N'Cash', N'Ship 2 day(s) after supplier receives initial payment', NULL)
SET IDENTITY_INSERT [paybayservice].[Bills] OFF
SET IDENTITY_INSERT [paybayservice].[Comments] ON 

INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (1, CAST(N'2016-02-25 17:00:00.000' AS DateTime), 1, 9, N'It''s a good')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (2, CAST(N'2016-02-25 18:00:00.000' AS DateTime), 1, 8, N'very good')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (3, CAST(N'2016-02-25 18:30:00.000' AS DateTime), 1, 7, N'so good!')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (5, CAST(N'2016-02-25 23:17:07.000' AS DateTime), 1, 7, N'oh yeah')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (6, CAST(N'2016-02-25 23:50:41.000' AS DateTime), 1, 8, N'I''m gay!')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (7, CAST(N'2016-02-25 23:52:34.000' AS DateTime), 1, 9, N'Mon is handsome!')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (8, CAST(N'2016-02-26 22:14:07.000' AS DateTime), 1, 9, N'abcd')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (9, CAST(N'2016-02-27 01:02:23.000' AS DateTime), 8, 8, N'sjdkflsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (10, CAST(N'2016-02-27 01:14:05.000' AS DateTime), 8, 7, N'fsdgfdg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (11, CAST(N'2016-02-27 01:14:59.000' AS DateTime), 8, 7, N'dfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (12, CAST(N'2016-02-27 01:19:17.000' AS DateTime), 8, 7, N'dfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (13, CAST(N'2016-02-27 01:28:15.000' AS DateTime), 1, 7, N'sdgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (14, CAST(N'2016-02-27 01:38:37.000' AS DateTime), 3, 8, N'dsfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (15, CAST(N'2016-02-27 05:52:45.000' AS DateTime), 8, 9, N'gdfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (16, CAST(N'2016-02-27 06:02:47.000' AS DateTime), 8, 9, N'sdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (17, CAST(N'2016-02-27 18:26:15.000' AS DateTime), 7, 9, N'sdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (18, CAST(N'2016-02-28 09:42:17.000' AS DateTime), 6, 7, N'sdfsdfsd')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (19, CAST(N'2016-02-28 09:42:36.000' AS DateTime), 6, 7, N'sdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (20, CAST(N'2016-02-28 09:43:11.000' AS DateTime), 3, 7, N'fsdgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (21, CAST(N'2016-02-28 09:47:04.000' AS DateTime), 1, 7, N'sdfsdfdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (22, CAST(N'2016-02-28 09:53:23.000' AS DateTime), 1, 9, N'ryrtyrty')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (23, CAST(N'2016-02-28 09:53:35.000' AS DateTime), 1, 9, N'sdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (24, CAST(N'2016-02-28 09:54:11.000' AS DateTime), 8, 9, N'Quá ngon')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (25, CAST(N'2016-02-28 16:56:05.000' AS DateTime), 2, 9, N'Error')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (26, CAST(N'2016-02-28 16:56:08.000' AS DateTime), 2, 9, N'Love')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (27, CAST(N'2016-02-28 16:56:22.000' AS DateTime), 2, 9, N'Tâm <3')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (28, CAST(N'2016-02-28 23:55:45.000' AS DateTime), 8, 9, N'sdfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (29, CAST(N'2016-02-28 23:55:58.000' AS DateTime), 8, 9, N'huy')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (30, CAST(N'2016-02-28 17:47:09.000' AS DateTime), 1, 9, N'dfmgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (31, CAST(N'2016-02-28 17:47:18.000' AS DateTime), 1, 9, N'mon')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (32, CAST(N'2016-02-28 18:18:17.000' AS DateTime), 1, 8, N'Tam')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (33, CAST(N'2016-02-28 22:29:30.000' AS DateTime), 1, 9, N'alo')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (34, CAST(N'2016-02-28 22:34:53.000' AS DateTime), 8, 9, N'test comment')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (35, CAST(N'2016-02-29 05:57:45.000' AS DateTime), 4, 8, N'push')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (36, CAST(N'2016-02-29 21:29:37.000' AS DateTime), 1, 8, N'ahihi')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (37, CAST(N'2016-03-02 01:44:16.000' AS DateTime), 1, 9, N'test rating succeeded ')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (38, CAST(N'2016-03-02 01:49:01.000' AS DateTime), 1, 9, N'sdj mon sdsd')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (39, CAST(N'2016-03-02 01:50:55.000' AS DateTime), 1, 9, N'dfgfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (40, CAST(N'2016-03-02 05:58:45.000' AS DateTime), 8, 7, N'sdfdsf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (41, CAST(N'2016-03-02 06:03:55.000' AS DateTime), 8, 7, N'Nam  co ho')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (42, CAST(N'2016-03-02 21:55:09.000' AS DateTime), 4, 7, N'push')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (43, CAST(N'2016-03-02 21:57:27.000' AS DateTime), 4, 7, N'asfsdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (44, CAST(N'2016-03-02 22:07:20.000' AS DateTime), 3, 7, N'Tam ngon')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (45, CAST(N'2016-03-02 22:07:58.000' AS DateTime), 3, 7, N'sdfsdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (46, CAST(N'2016-03-02 22:53:48.000' AS DateTime), 3, 9, N'fsdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (47, CAST(N'2016-03-02 22:59:49.000' AS DateTime), 4, 9, N'dfgdfgfdg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (48, CAST(N'2016-03-02 23:01:08.000' AS DateTime), 4, 9, N'ay da')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (49, CAST(N'2016-03-04 01:34:32.000' AS DateTime), 1, 7, N'test')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (50, CAST(N'2016-03-04 06:09:38.000' AS DateTime), 4, 9, N'hula')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (51, CAST(N'2016-03-04 06:11:50.000' AS DateTime), 4, 9, N'alo')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (52, CAST(N'2016-03-05 02:46:14.000' AS DateTime), 8, 10, N'a')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (53, CAST(N'2016-03-05 02:46:17.000' AS DateTime), 8, 10, N'')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (54, CAST(N'2016-03-05 02:46:17.000' AS DateTime), 8, 10, N'')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (55, CAST(N'2016-03-05 04:13:49.000' AS DateTime), 1, 10, N'a')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (56, CAST(N'2016-03-14 00:19:44.000' AS DateTime), 8, 9, N'sdfkl;sdkf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (57, CAST(N'2016-03-14 00:24:09.000' AS DateTime), 8, 9, N'sdgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (58, CAST(N'2016-03-14 00:30:42.000' AS DateTime), 8, 9, N'test ')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (59, CAST(N'2016-03-14 00:34:45.000' AS DateTime), 8, 9, N'alo')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (60, CAST(N'2016-03-14 00:52:14.000' AS DateTime), 8, 9, N'boo roi')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (61, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 8, 9, N'jkhkjsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (62, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 8, 9, N'gdfgfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (63, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 8, 9, N'sdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (64, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 4, 9, N'alo test')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (65, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 4, 9, N'test Oauth')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (66, CAST(N'2016-03-14 00:00:00.000' AS DateTime), 4, 9, N'dfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (67, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'test')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (68, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 4, 9, N'asdasd')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (69, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'push')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (70, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'haiz')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (71, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'fsdfsdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (72, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'asdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (73, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 4, 9, N'asdasdgfdgdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (74, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 5, 9, N'so sad')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (75, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 3, 9, N'so fun')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (76, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 6, 9, N'OMG')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (77, CAST(N'2016-03-15 00:00:00.000' AS DateTime), 6, 9, N'oijlmklkjlk')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (78, CAST(N'2016-03-17 00:00:00.000' AS DateTime), 4, 9, N'gdfgdfghcbcvbc')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (79, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 4, 8, N'sdfsdfcvbcvb')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (80, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 4, 9, N'sdfsdg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (81, CAST(N'2016-03-18 00:00:00.000' AS DateTime), 5, 8, N'dfgdf')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (82, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'ytyery')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (83, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'etyertytry')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (84, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'eyer')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (85, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'etyerty')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (86, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'ertyery')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (87, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 4, 7, N'ertyrtye')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (88, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 1, 9, N'gdfgdfg')
INSERT [paybayservice].[Comments] ([Id], [CommentDate], [StoreID], [UserID], [Content]) VALUES (89, CAST(N'2016-03-20 00:00:00.000' AS DateTime), 1, 9, N'abcdx')
SET IDENTITY_INSERT [paybayservice].[Comments] OFF
SET IDENTITY_INSERT [paybayservice].[DetailBill] ON 

INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (1, 1, 1, 8, 10000, N'Hộp')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (2, 1, 3, 1, 400000, N'Cái')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (3, 3, 35, 10, 30000, N'Bông')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (4, 4, 34, 10, 10000, N'bich')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (5, 4, 33, 5, 15000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (6, 5, 29, 8, 16000, N'Hop')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (7, 6, 32, 3, 60000, N'Hop')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (8, 7, 33, 1, 15000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (9, 8, 30, 1, 16000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (10, 9, 34, 1, 10000, N'bich')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (11, 10, 35, 2, 30000, N'Bông')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (12, 10, 34, 3, 10000, N'bich')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (13, 11, 33, 1, 15000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (14, 11, 32, 1, 60000, N'Hop')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (15, 11, 31, 1, 10000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (16, 12, 33, 1, 15000, N'Cai')
INSERT [paybayservice].[DetailBill] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit]) VALUES (17, 12, 32, 1, 60000, N'Hop')
SET IDENTITY_INSERT [paybayservice].[DetailBill] OFF
SET IDENTITY_INSERT [paybayservice].[Markets] ON 

INSERT [paybayservice].[Markets] ([MarketId], [MarketName], [Address], [Phone], [Image], [SasQuery], [Latitute], [Longitute]) VALUES (2, N'Bà Chiểu', N'Quận Bình Thạnh', N'987654321', NULL, NULL, 10.801577, 106.698854)
INSERT [paybayservice].[Markets] ([MarketId], [MarketName], [Address], [Phone], [Image], [SasQuery], [Latitute], [Longitute]) VALUES (3, N'Tam Hòa', N'Tam Hòa,BH', N'51321854', N'https://paybaystorage.blob.core.windows.net/products/banh0.jpg', N'?sv=2015-04-05&sr=c&sig=sTAoIbwfxleq2cpmA6VrXJKrGnBwVPYYJuWHIPbNjKU%3D&se=2016-02-15T08%3A35%3A10Z&sp=rw', 10.942236, 106.864148)
INSERT [paybayservice].[Markets] ([MarketId], [MarketName], [Address], [Phone], [Image], [SasQuery], [Latitute], [Longitute]) VALUES (4, N'Chợ Lớn', N'Cholon, Vietnam', N'978315313', N'https://paybaystorage.blob.core.windows.net/products/test0.jpg', N'?sv=2015-04-05&sr=c&sig=qLHL%2BdTLDmVLZ0dUytho1LVQYYPsVMIDVOVaQoxCBUg%3D&se=2016-02-15T10%3A42%3A58Z&sp=rw', 10.74978, 106.651066)
INSERT [paybayservice].[Markets] ([MarketId], [MarketName], [Address], [Phone], [Image], [SasQuery], [Latitute], [Longitute]) VALUES (10, N'Ben Thanh', N'Lê Lợi, Ho Chi Minh City, Tp. Hồ Chí Minh', N'0123456789', N'https://paybaystorage.blob.core.windows.net/markets/benthanh10.jpg', N'?sv=2015-04-05&sr=c&sig=rovJ6XZKQiuo5OAFFtmwHbYuKpQmzTlbsaSPYNuAbxk%3D&se=2016-02-27T06%3A45%3A39Z&sp=rw', 10.772432, 106.69805)
INSERT [paybayservice].[Markets] ([MarketId], [MarketName], [Address], [Phone], [Image], [SasQuery], [Latitute], [Longitute]) VALUES (11, N'Tan An', N'Tan An, BH', N'0987654321', N'https://paybaystorage.blob.core.windows.net/markets/tanan11.jpg', N'?sv=2015-04-05&sr=c&sig=RQZq9yg0gk606clPNmW6VPVNJv4dhHthLx7Hd1pIrx4%3D&se=2016-02-27T07%3A01%3A32Z&sp=rw', 10.686303, 107.74862)
SET IDENTITY_INSERT [paybayservice].[Markets] OFF
SET IDENTITY_INSERT [paybayservice].[MessageInbox] ON 

INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (4, 8, 9, N'sdfgdfgdfg', CAST(N'2016-03-21 15:10:41.550' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (5, 8, 9, N'sgdfghbvbnvbn', CAST(N'2016-03-21 15:19:09.657' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (6, 8, 9, N'alo', CAST(N'2016-03-21 15:29:27.443' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (7, 8, 9, N'alo', CAST(N'2016-03-21 15:31:27.860' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (8, 8, 9, N'alo ', CAST(N'2016-03-21 15:40:43.490' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (9, 8, 9, N'alo', CAST(N'2016-03-21 08:42:28.763' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (10, 8, 9, N'fdgdfgdfgfd', CAST(N'2016-03-21 08:42:50.807' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (11, 8, 9, N'dfgdfgdfg', CAST(N'2016-03-21 08:43:11.187' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (12, 8, 9, N'alo Tam', CAST(N'2016-03-21 08:53:19.003' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (13, 8, 9, N'alo Tam', CAST(N'2016-03-21 08:56:59.813' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (14, 8, 9, N'alo ', CAST(N'2016-03-21 08:57:37.417' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (15, 8, 9, N'alo alo', CAST(N'2016-03-21 09:01:02.907' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (16, 8, 9, N'alo alo', CAST(N'2016-03-21 09:08:13.407' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (17, 8, 9, N'alo alo', CAST(N'2016-03-21 09:25:14.677' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (18, 8, 9, N'alo alo', CAST(N'2016-03-21 09:32:45.057' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (19, 8, 9, N'alo alo', CAST(N'2016-03-21 09:43:53.340' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (20, 8, 9, N'alo alo', CAST(N'2016-03-21 09:44:46.203' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (21, 8, 9, N'alo alo', CAST(N'2016-03-21 09:48:44.500' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (22, 8, 9, N'afsdfsdfsdf', CAST(N'2016-03-21 09:53:30.913' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (23, 7, 9, N'all alo', CAST(N'2016-03-21 13:16:19.410' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (24, 7, 9, N'fsdfsdf', CAST(N'2016-03-21 13:18:17.590' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (25, 8, 9, N'sdfsdf', CAST(N'2016-03-21 13:39:08.633' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (26, 8, 9, N'kjhkjhkjhkj', CAST(N'2016-03-21 13:47:15.853' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (27, 8, 9, N'asdasfd', CAST(N'2016-03-21 13:49:18.593' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (28, 8, 9, N'asdasdasd', CAST(N'2016-03-21 13:53:06.183' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (29, 8, 9, N'asdfsdfsdf', CAST(N'2016-03-21 13:54:04.250' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (30, 8, 8, N'coool', CAST(N'2016-03-21 13:54:12.350' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (31, 8, 8, N'huy gay', CAST(N'2016-03-21 13:54:29.307' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (32, 8, 8, N'huy la` thang` gay', CAST(N'2016-03-21 13:54:35.117' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (33, 8, 9, N'sdfsdf', CAST(N'2016-03-21 14:00:20.027' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (34, 8, 9, N'sdfgdfgdfg', CAST(N'2016-03-21 14:02:30.767' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (35, 8, 8, N'ddsagsdgds', CAST(N'2016-03-21 14:02:34.810' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (36, 8, 9, N'all alo', CAST(N'2016-03-21 14:06:45.253' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (37, 9, 8, N'ahihi do ngoc', CAST(N'2016-03-21 14:06:50.947' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (38, 9, 8, N'chat ne` thay ko gay oi ? :v :v', CAST(N'2016-03-21 14:07:03.663' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (39, 8, 9, N'Tam co h', CAST(N'2016-03-21 14:07:24.410' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (40, 9, 8, N'ko thay push noti cua ong :v', CAST(N'2016-03-21 14:07:34.413' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (41, 8, 9, N'lau Lau no bi nghen', CAST(N'2016-03-21 14:07:56.823' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (42, 8, 9, N'lat hoi RA 1 loat', CAST(N'2016-03-21 14:08:07.700' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (43, 9, 8, N'ngon :3', CAST(N'2016-03-21 14:08:20.023' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (44, 8, 9, N'haha', CAST(N'2016-03-21 14:08:33.683' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (45, 9, 8, N':3 :3 :3', CAST(N'2016-03-21 14:08:46.600' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (46, 9, 8, N'xong nghi khoe :v :v :v', CAST(N'2016-03-21 14:08:52.590' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (47, 8, 9, N'Tam ngon bay :v', CAST(N'2016-03-21 14:20:01.017' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (48, 9, 8, N':3 :3', CAST(N'2016-03-21 14:22:19.343' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (50, 8, 9, N'sdfsdfsdf', CAST(N'2016-03-21 14:29:26.967' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (51, 9, 8, N'ahihihi', CAST(N'2016-03-21 14:29:33.110' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (52, 9, 8, N'thay ko hu'' hu''', CAST(N'2016-03-21 14:29:48.057' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (53, 8, 9, N'that ', CAST(N'2016-03-21 14:29:58.407' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (54, 8, 9, N'ok', CAST(N'2016-03-21 14:30:06.410' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (56, 9, 8, N':3 :3', CAST(N'2016-03-21 14:30:38.943' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (57, 8, 9, N'send code ', CAST(N'2016-03-21 14:30:46.030' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (58, 8, 9, N'send project de', CAST(N'2016-03-21 14:30:57.077' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (59, 8, 9, N'asdsdf', CAST(N'2016-03-21 14:45:46.520' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (60, 8, 9, N'sdfjkljkl', CAST(N'2016-03-21 23:55:05.867' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (61, 8, 9, N'fdgdfg', CAST(N'2016-03-22 00:03:52.147' AS DateTime))
INSERT [paybayservice].[MessageInbox] ([MessageID], [UserID], [OwnerID], [Content], [InboxDate]) VALUES (62, 8, 9, N'sdgdfbcvbcvbv', CAST(N'2016-03-22 00:04:02.250' AS DateTime))
SET IDENTITY_INSERT [paybayservice].[MessageInbox] OFF
SET IDENTITY_INSERT [paybayservice].[Products] ON 

INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (28, N'uioiouio', N'https://paybaystorage.blob.core.windows.net/products/uioiouio0.jpg', 15000, 38, N'cai', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=0fKU9z2rruS2yFrodHzhUFGklij8ZtTRUmp0ksdVq1k%3D&se=2016-02-15T07%3A42%3A58Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (29, N'Sua', N'https://paybaystorage.blob.core.windows.net/products/sua0.jpg', 16000, 37, N'Hop', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=a3nU4VYZvA%2FkR9dqCeIYG%2F5HWMccBzV9YpoqT9xXUkA%3D&se=2016-02-15T08%3A15%3A42Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (30, N'Banh', N'https://paybaystorage.blob.core.windows.net/products/banh0.jpg', 16000, 34, N'Cai', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=sTAoIbwfxleq2cpmA6VrXJKrGnBwVPYYJuWHIPbNjKU%3D&se=2016-02-15T08%3A35%3A10Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (31, N'fghfgh', N'https://paybaystorage.blob.core.windows.net/products/fghfgh0.jpg', 10000, 35, N'Cai', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=ijRmRaQGYivxiEjPidmp14s960D8fvRC3Uuq7xxU4RM%3D&se=2016-02-15T08%3A40%3A37Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (32, N'Chocolate', N'https://paybaystorage.blob.core.windows.net/products/chocolate0.jpg', 60000, 35, N'Hop', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=RaQ9Ku9%2BslpY8nNxEw9lAC66g8EJ0pVnHKhxM8hy4%2F0%3D&se=2016-02-15T08%3A42%3A23Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (33, N'Test', N'https://paybaystorage.blob.core.windows.net/products/test0.jpg', 15000, 34, N'Cai', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=qLHL%2BdTLDmVLZ0dUytho1LVQYYPsVMIDVOVaQoxCBUg%3D&se=2016-02-15T10%3A42%3A58Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (34, N'keo', N'https://paybaystorage.blob.core.windows.net/products/keo0.jpg', 10000, 32, N'bich', 1, CAST(N'2016-02-14' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=RDmlmZ3mEgOqmcT02uGZaU%2BbeuLfybT%2BTne6EiYTX5E%3D&se=2016-02-15T15%3A34%3A47Z&sp=rw')
INSERT [paybayservice].[Products] ([ProductId], [ProductName], [Image], [UnitPrice], [NumberOf], [Unit], [StoreID], [ImportDate], [SalePrice], [SasQuery]) VALUES (35, N'Hoa', N'https://paybaystorage.blob.core.windows.net/sales/guitronyeuthuong12.jpg', 30000, 44, N'Bông', 1, CAST(N'2016-02-15' AS Date), 0, N'?sv=2015-04-05&sr=c&sig=BgodxRin5ZFsUrmhg7razJb0rnPkl2nUXKKCAsQYE8Q%3D&se=2016-02-27T14%3A22%3A45Z&sp=rw')
SET IDENTITY_INSERT [paybayservice].[Products] OFF
SET IDENTITY_INSERT [paybayservice].[ProductStatistic] ON 

INSERT [paybayservice].[ProductStatistic] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit], [SaleDate]) VALUES (1, 1, 1, 8, 10000, N'Hộp', CAST(N'2016-02-11' AS Date))
INSERT [paybayservice].[ProductStatistic] ([Id], [BillID], [ProductID], [NumberOf], [UnitPrice], [Unit], [SaleDate]) VALUES (2, 1, 3, 1, 400000, N'Cái', CAST(N'2016-02-11' AS Date))
SET IDENTITY_INSERT [paybayservice].[ProductStatistic] OFF
SET IDENTITY_INSERT [paybayservice].[SaleInfo] ON 

INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (1, N'Sale', N'https://paybaystorage.blob.core.windows.net/products/sua0.jpg', N'giảm tất cả', CAST(N'2016-11-09' AS Date), CAST(N'2016-02-09' AS Date), 1, 1, N'?sv=2015-04-05&sr=c&sig=a3nU4VYZvA%2FkR9dqCeIYG%2F5HWMccBzV9YpoqT9xXUkA%3D&se=2016-02-15T08%3A15%3A42Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (3, N'Giảm giá bánh', N'https://paybaystorage.blob.core.windows.net/products/banh0.jpg', N'khuyến mãi', CAST(N'2016-11-01' AS Date), CAST(N'2016-03-29' AS Date), 1, 1, N'?sv=2015-04-05&sr=c&sig=sTAoIbwfxleq2cpmA6VrXJKrGnBwVPYYJuWHIPbNjKU%3D&se=2016-02-15T08%3A35%3A10Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (4, N'Valentine', N'https://paybaystorage.blob.core.windows.net/products/chocolate0.jpg', N'Giảm giá chocolate', CAST(N'2016-12-12' AS Date), CAST(N'2016-03-14' AS Date), 1, 1, N'?sv=2015-04-05&sr=c&sig=RaQ9Ku9%2BslpY8nNxEw9lAC66g8EJ0pVnHKhxM8hy4%2F0%3D&se=2016-02-15T08%3A42%3A23Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (5, N'Thiếu nhi', N'https://paybaystorage.blob.core.windows.net/products/keo0.jpg', N'Khuyến mãi kẹo', CAST(N'2016-12-01' AS Date), CAST(N'2016-03-29' AS Date), 1, 1, N'?sv=2015-04-05&sr=c&sig=RDmlmZ3mEgOqmcT02uGZaU%2BbeuLfybT%2BTne6EiYTX5E%3D&se=2016-02-15T15%3A34%3A47Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (9, N'KM Onion', N'https://paybaystorage.blob.core.windows.net/products/uioiouio0.jpg', N'khuyến mãi bánh', CAST(N'2016-02-11' AS Date), CAST(N'2016-03-25' AS Date), 1, 1, N'?sv=2015-04-05&sr=c&sig=0fKU9z2rruS2yFrodHzhUFGklij8ZtTRUmp0ksdVq1k%3D&se=2016-02-15T07%3A42%3A58Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (10, N'Gui Tron Yeu Thuong', N'https://paybaystorage.blob.core.windows.net/sales/guitronyeuthuong12.jpg', N'Qua hap Dan Cho nu', CAST(N'2016-02-26' AS Date), CAST(N'2016-03-26' AS Date), 8, 1, N'?sv=2015-04-05&sr=c&sig=BgodxRin5ZFsUrmhg7razJb0rnPkl2nUXKKCAsQYE8Q%3D&se=2016-02-27T14%3A22%3A45Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (11, N'Daredevil', N'https://paybaystorage.blob.core.windows.net/sales/daredevil12.jpg', N'Ra rap', CAST(N'2016-02-26' AS Date), CAST(N'2016-03-26' AS Date), 8, 1, N'?sv=2015-04-05&sr=c&sig=AoLU5iGKckub4yGA3C8zLoBH4lqnlp3aNmABz9jhzEw%3D&se=2016-02-27T14%3A33%3A20Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (12, N'The Flash', N'https://paybaystorage.blob.core.windows.net/sales/theflash12.jpg', N'Coming soon', CAST(N'2016-02-21' AS Date), CAST(N'2016-03-26' AS Date), 8, 1, N'?sv=2015-04-05&sr=c&sig=H0i9nRg3GL7CewgzhLJWMd7k6NEHsRnkikBi%2F2qkxtA%3D&se=2016-02-27T14%3A34%3A58Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (13, N'Uncharted', N'https://paybaystorage.blob.core.windows.net/sales/uncharted12.jpg', N'Khoi chieu', CAST(N'2016-01-21' AS Date), CAST(N'2016-03-20' AS Date), 8, 1, N'?sv=2015-04-05&sr=c&sig=5SR2xYYakOq3WLsrOzl1kXds7I3AUxUQXppCEyrlrso%3D&se=2016-02-27T14%3A43%3A06Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (14, N'PhantomPain', N'https://paybaystorage.blob.core.windows.net/sales/phantompain12.jpg', N'Phat hanh', CAST(N'2015-12-21' AS Date), CAST(N'2016-03-20' AS Date), 8, 1, N'?sv=2015-04-05&sr=c&sig=g6CxlFarymomGY4ff%2FDlAnNmaHX1PPEO1zZOUR5OC4w%3D&se=2016-02-27T14%3A45%3A05Z&sp=rw')
INSERT [paybayservice].[SaleInfo] ([SaleId], [Title], [Image], [Describes], [StartDate], [EndDate], [StoreID], [isRequired], [SasQuery]) VALUES (17, N'Game Uncharted', N'https://paybaystorage.blob.core.windows.net/sales/uncharted12.jpg', N'Phát hành game', CAST(N'2016-01-30' AS Date), CAST(N'2016-03-03' AS Date), 7, 1, N'?sv=2015-04-05&sr=c&sig=5SR2xYYakOq3WLsrOzl1kXds7I3AUxUQXppCEyrlrso%3D&se=2016-02-27T14%3A43%3A06Z&sp=rw')
SET IDENTITY_INSERT [paybayservice].[SaleInfo] OFF
SET IDENTITY_INSERT [paybayservice].[StatisticRating] ON 

INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (1, 1, 1, 3)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (2, 7, 1, 5)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (3, 8, 1, 4)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (4, 9, 1, 2)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (5, 8, 8, 3)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (6, 9, 3, 3)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (7, 9, 4, 3)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (8, 7, 4, 3)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (9, 10, 7, 1)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (10, 10, 8, 5)
INSERT [paybayservice].[StatisticRating] ([ID], [UserID], [StoreID], [RateOfUser]) VALUES (11, 10, 1, 5)
SET IDENTITY_INSERT [paybayservice].[StatisticRating] OFF
SET IDENTITY_INSERT [paybayservice].[Stores] ON 

INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (1, N'Bà Bảy', N'A1', NULL, N'546556123', 10, 1, 3, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (2, N'Bà Mười', N'A3', NULL, N'258369147', 2, 7, 2.5, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (3, N'Bà Sáu', N'A4', NULL, N'413215465', 3, 8, 3, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (4, N'Bà Hai', N'B2', NULL, N'785454564', 4, 9, 3, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (5, N'Cô Ba', N'B7', NULL, N'545645642', 4, 9, 5, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (6, N'Cô Hai', N'B1', NULL, N'456213213', 3, 8, 2, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (7, N'Cô Sáu', N'B3', NULL, N'456781231', 2, 7, 3, NULL)
INSERT [paybayservice].[Stores] ([StoreId], [StoreName], [KiotNo], [Image], [Phone], [MarketID], [OwnerID], [Rate], [SasQuery]) VALUES (8, N'Anh Ba', N'B4', NULL, N'785123133', 10, 1, 3, NULL)
SET IDENTITY_INSERT [paybayservice].[Stores] OFF
SET IDENTITY_INSERT [paybayservice].[Users] ON 

INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (1, CAST(N'2016-10-22' AS Date), N'monpham@gmail.com', N'01268673096', 1, N'Bien Hoa', NULL, 1, N'Admin', 0x6162636458595A313233, NULL)
INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (7, CAST(N'1994-02-19' AS Date), N'xellosp@gmail.com', N'0123456789', 1, N'Quan 9,TP.Hcm', N'https://paybaystorage.blob.core.windows.net/users/dothanhnam7.jpg', 1, N'Do Thanh Nam', 0x6E616D313233, N'?sv=2015-04-05&sr=c&sig=vksIvjPOOKPTDxvo7qWD3M0er8mWLY0bnfYFymXbKYo%3D&se=2016-02-20T11%3A20%3A11Z&sp=rw')
INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (8, CAST(N'1994-05-21' AS Date), N'darkomega9408@gmail.com', N'987654321', 1, N'Quan Tan Binh', N'https://paybaystorage.blob.core.windows.net/users/nguyenthanhtam8.jpg', 1, N'Nguyen Thanh Tam', 0x74616D313233, N'?sv=2015-04-05&sr=c&sig=Aol6j5DKmN51AyPo9YV5C6u4qvLPzRv3TXaOTlTSWII%3D&se=2016-02-22T07%3A59%3A54Z&sp=rw')
INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (9, CAST(N'1994-10-23' AS Date), N'monpham2310@gmail.com', N'01268673096', 1, N'Tam Hoa,Bien Hoa', N'https://paybaystorage.blob.core.windows.net/users/phanquanghuy9.jpg', 1, N'Phan Quang Huy', 0x6162636458595A313233, N'?sv=2015-04-05&sr=c&sig=kfKNf7aYNg6Bqj2pDuQDq%2BvYkZse6ikM4pQayO7vERE%3D&se=2016-02-22T08%3A49%3A38Z&sp=rw')
INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (10, CAST(N'1996-03-04' AS Date), N'viethung.msp@outlook.com', N'0938175143', 1, N'9/7B Quang Trung Gvap', N'https://paybaystorage.blob.core.windows.net/users/hungdoan10.jpg', 2, N'Hung Doan', 0x766839356B696E67, N'?sv=2015-04-05&sr=c&sig=p%2F8K7zIpt6YSYz2A%2B6%2Fk%2BrgCdPH5hp4Hrc8rlsR%2Fukg%3D&se=2016-03-05T14%3A57%3A00Z&sp=rw')
INSERT [paybayservice].[Users] ([UserId], [Birthday], [Email], [Phone], [Gender], [Address], [Avatar], [TypeID], [Username], [Pass], [SasQuery]) VALUES (11, CAST(N'1996-03-04' AS Date), N'viethung.msp@outlook.com', N'0938175143', 1, N'9/7B Quang Trung Gvap', N'https://paybaystorage.blob.core.windows.net/users/hungdoan11.jpg', 2, N'Hung Doan', 0x766839356B696E67, N'?sv=2015-04-05&sr=c&sig=nz9eP3TJDshMEUxU7d99vm5NpIJBuk49ZSPocoVPvcc%3D&se=2016-03-05T14%3A57%3A37Z&sp=rw')
SET IDENTITY_INSERT [paybayservice].[Users] OFF
SET IDENTITY_INSERT [paybayservice].[UserType] ON 

INSERT [paybayservice].[UserType] ([TypeId], [TypeName]) VALUES (1, N'Administrator')
INSERT [paybayservice].[UserType] ([TypeId], [TypeName]) VALUES (2, N'Store Owner')
INSERT [paybayservice].[UserType] ([TypeId], [TypeName]) VALUES (3, N'User')
SET IDENTITY_INSERT [paybayservice].[UserType] OFF
/****** Object:  StoredProcedure [paybayservice].[sp_AddBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_AddBill]
@CreatedDate date,
@StoreID int,
@UserID int
as
	begin tran addBill
		insert into paybayservice.BILLS(CreatedDate,StoreID,UserID) values(@CreatedDate,@StoreID,@UserID)
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Add bill not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddComment]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_AddComment]
@Date date,
@StoreID int,
@UserID int,
@Content nvarchar(max)
as
	begin tran insertComment
		insert into paybayservice.COMMENTS(CommentDate,StoreID,UserID,Content) values (@Date,@StoreID,@UserID,@Content)
		if(@@ERROR > 0)
		begin
			rollback tran			
		end
		else
		begin			
				select top 4 Id,CommentDate,StoreID,a.UserID,b.Username,b.Avatar,Content,(select RateOfUser from paybayservice.StatisticRating c where c.UserID=a.UserID and StoreID = @StoreID) as Rated
				from paybayservice.Comments a inner join paybayservice.Users b on a.UserID=b.UserID
				where StoreID=@StoreID
				order by Id desc
					
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_AddMarket]
@Name nvarchar(100),
@Address nvarchar(200),
@Phone varchar(12),
@Image varbinary(max)
as
	begin tran addMarket		
		insert into paybayservice.MARKETS values(@Name,@Address,@Phone,@Image)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add market!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddMsgDetail]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_AddMsgDetail]
@MessageID int,
@InboxDate datetime,
@Content nvarchar(max)
as
	begin tran addDetail
		insert into paybayservice.InboxDetail values (@MessageID,@InboxDate,@Content)
		if(@@error > 0)
			rollback tran
		select a.UserID,OwnerID,ID,b.MessageID,Username,Avatar,InboxDate,Content
		from paybayservice.MessageInbox a join paybayservice.InboxDetail b on a.MessageID=b.MessageID
			join paybayservice.Users c on c.UserID=a.OwnerID
		where b.MessageID = @MessageID and InboxDate = @InboxDate
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddNewMessage]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_AddNewMessage] --9,9
@UserID int,
@OwnerID int,
@Content nvarchar(255),
@InboxDate datetime
as
	begin tran addNewMsg	
		insert into paybayservice.MessageInbox values (@UserID,@OwnerID,@Content,@InboxDate)		
		if(@@error > 0)
			rollback tran
		select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from paybayservice.MessageInbox a join paybayservice.Users b on a.OwnerID = b.UserID
		where a.UserID = @UserID and OwnerID = @OwnerID and InboxDate = @InboxDate		
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_AddProduct]
@Name nvarchar(100),
@Image varbinary(max),
@UnitPrice float,
@NumberOf int,
@Unit nvarchar(20),
@StoreID int,
@ImportDate date,
@SalePrice float
as
	begin tran addProduct		
		insert into paybayservice.PRODUCTS values(@Name,@Image,@UnitPrice,@NumberOf,@Unit,@StoreID,@ImportDate,@SalePrice)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add product!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddSaleInfo]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_AddSaleInfo] --permission store owner
@Title nvarchar(100),
@Image varbinary(max),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID int
as
	begin tran addSales
		insert into paybayservice.SALES(Title,Image,Describes,StartDate,EndDate,StoreID) values (@Title,@Image,@Description,@StartDate,@EndDate,@StoreID)
		if(@@ERROR > 0)
		begin
			rollback tran 
			select 0 as ErrCode,'Add sale info not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_AddStore]
@Name nvarchar(100),
@KiotNo varchar(8),
@Image varbinary(max),
@Phone varchar(12),
@MarketID int,
@OwnerID int
as
	begin tran addStore		
		insert into paybayservice.STORES values(@Name,@KiotNo,@Image,@Phone,@MarketID,@OwnerID)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add store!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AddUserType]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_AddUserType]
@typeName varchar(20)
as
	if not exists (select 1 from UserType where TypeName = @typeName)
	begin
	begin tran addType
		insert into UserType values(@typeName)
		if(@@error > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Add is not successful!' as ErrMsg
		end
		else
			select 1 as ErrCode,'Add is successful!' as ErrMsg
	commit
	end
	else
		select 0 as ErrCode,'Name had already exists!' as ErrMsg
GO
/****** Object:  StoredProcedure [paybayservice].[sp_AllowShowHomePage]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_AllowShowHomePage]
@saleId int,
@isRequired bit
as
	begin tran updateRequired
		update paybayservice.SaleInfo
		set isRequired = @isRequired
		where SaleId = @SaleId
		if(@@error > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Request is not successful!' as ErrMsg
		end
		else
		begin
			select 1 as ErrCode,'Request is successful!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelBill]
@BillID int,
@StoreID int,
@UserID int
as
	begin tran delBill
		delete from paybayservice.BILLS where BillId=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete bill not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelComment]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelComment] --permission Store Owner
@ID int
as
	begin tran delComment
		delete from paybayservice.COMMENTS where Id=@ID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Cannot delete this comment!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelDetailBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_DelDetailBill]
@BillID int,
@ProductID int
as
	if exists (select 1 from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
	begin
		begin tran delDetailBill
			if(@ProductID <> 0)
				delete from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID
			else
				delete from paybayservice.DETAILBILL where BillID=@BillID

			if(@@ERROR > 0)
			begin
				rollback tran
				select 1 as ErrCode,'Delete detail bill successful!' as ErrMsg
			end
		commit
	end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_DelMarket] --super admin
@MarketId int
as
	begin tran delMarket
		delete from paybayservice.Markets where MarketId=@MarketId
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete market!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelProduct]
@ID int,
@StoreID int
as
	begin tran delProduct
		delete from paybayservice.PRODUCTS where ProductId=@ID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete product!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelSaleInfo]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelSaleInfo] --permission store owner
@SaleID int,
@StoreID int
as
	begin tran delSale
		delete from paybayservice.SALESINFO where SaleId=@SaleID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Delete sale info not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelStore] --Super Admin
@ID int
as
	begin tran delStore
		delete from paybayservice.STORES where StoreId=@ID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete store!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_DelUser]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_DelUser] --spuer admin permission
@UserID int
as
	begin tran delUser
		delete from paybayservice.USERS where UserId=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete user not successful!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_FindNewStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_FindNewStore]
@StoreName nvarchar(100),
@MarketID int,
@StoreId int
as
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,Avatar,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreId>@StoreId and StoreName like N'%'+@StoreName+N'%'
	order by StoreId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_FindStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_FindStore]
@StoreName nvarchar(100),
@MarketID int,
@StoreId int
as
if(@StoreId <> -1)
begin
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,Avatar,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreId<@StoreId and StoreName like N'%'+@StoreName+N'%'
	order by StoreId desc
end
else
begin
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,Avatar,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreName like N'%'+@StoreName+N'%'
	order by StoreId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetAllSaleInfo]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetAllSaleInfo]
as
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from paybayservice.SaleInfo a inner join paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = 1
	order by SaleId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetBestSaleProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetBestSaleProduct]
as		
	select top 10 a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	from paybayservice.ProductStatistic a join paybayservice.Products b on a.ProductID=b.ProductID join paybayservice.Stores c
		 on c.StoreID=b.StoreID join paybayservice.Markets d on c.MarketID=d.MarketID
	group by a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	order by sum(a.NumberOf) DESC
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetBillOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetBillOfStore]
@StoreID int
as
	select BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice,UserID,isShiped
	from Bills
	where StoreID = @StoreID
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetBillOfUser]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetBillOfUser]
@UserID int
as
	select BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice,UserID,isShiped
	from Bills
	where UserID = @UserID
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetDetailBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetDetailBill]
@BillID int
as
	select Id,BillID,ProductID,NumberOf,UnitPrice,Unit
	from paybayservice.DetailBill
	where BillID = @BillID
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetImageSale]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetImageSale]
@isRequired bit
as
	select top 5 Image,SasQuery
	from paybayservice.SaleInfo
	where isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMarketWithName]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMarketWithName] --0,'Ba'
@MarketId int,
@MarketName nvarchar(100)
as
if(@MarketId <> -1)
begin
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets
	where MarketId < @MarketId and MarketName like N'%'+@MarketName+N'%'
	order by MarketId desc
end
else
begin
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets
	where MarketName like N'%'+@MarketName+N'%'
	order by MarketId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMaxId]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMaxId]
@table nvarchar(50)
as
	select ident_current(@table) as id
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMessageOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMessageOfStore] --52,9
@MessageID int,
@OwnerID int
as	
	if(@MessageID = -1)
	begin
		select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from (
			select top 8 MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
			from paybayservice.MessageInbox a join paybayservice.Users b on a.OwnerID=b.UserID
			where OwnerID = @OwnerID or a.UserID = @OwnerID
			order by MessageID desc
		) a
		order by MessageID asc
	end
	else
	begin
		select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from (
			select top 8 MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
			from paybayservice.MessageInbox a join paybayservice.Users b on a.OwnerID=b.UserID
			where  MessageID < @MessageID and (OwnerID = @OwnerID or a.UserID = @OwnerID)
			order by MessageID desc
		) a
		order by MessageID asc
	end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMoreMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMoreMarket] --0
@MarketId int
as
if(@MarketId <> -1)
begin
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets
	where MarketId < @MarketId
	order by MarketId desc
end
else
begin
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets	
	order by MarketId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMoreMessageDetail]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMoreMessageDetail]
@ID int,
@MessageID int
as
	if(@ID = -1)
	begin
		select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
		from paybayservice.InboxDetail a join paybayservice.MessageInbox b on a.MessageId=b.MessageId
				join paybayservice.Users c on b.UserId=c.UserId 
		where a.MessageID = @MessageID
		order by ID desc
	end
	else
	begin
		select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
		from paybayservice.InboxDetail a join paybayservice.MessageInbox b on a.MessageId=b.MessageId
				join paybayservice.Users c on b.UserId=c.UserId 
		where a.MessageID = @MessageID and ID < @ID 
		order by ID desc
	end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMoreNewMessage]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMoreNewMessage] --10,9
@MessageID int,
@OwnerID int
as
	select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from (
			select top 8 MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
			from paybayservice.MessageInbox a join paybayservice.Users b on a.OwnerID=b.UserID
			where MessageID > @MessageID and (OwnerID = @OwnerID or a.UserID = @OwnerID)
			order by MessageID desc
		) a
		order by MessageID asc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetMoreProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetMoreProduct] --0
@ProductId int
as
if(@ProductId <> -1)
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID	
	where ProductId < @ProductId
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID	
	order by ProductId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetNewMarket]
@MarketId int
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets
	where MarketId > @MarketId
	order by MarketId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewMarketWithName]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetNewMarketWithName]
@MarketId int,
@MarketName nvarchar(100)
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from paybayservice.Markets
	where MarketId > @MarketId and MarketName like N'%'+@MarketName+N'%'
	order by MarketId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewMessageDetail]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetNewMessageDetail]
@ID int,
@MessageID int
as
	select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
	from paybayservice.InboxDetail a join paybayservice.MessageInbox b on a.MessageId=b.MessageId
			join paybayservice.Users c on b.UserId=c.UserId 
	where a.MessageID = @MessageID and ID > @ID 
	order by ID desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetNewProduct]
@ProductId int
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId > @ProductId
	order by ProductId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewProductOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetNewProductOfStore]
@StoreId int,
@ProductId int
as
	select top 5 row_number() over (order by (select 1)) as STT, ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,ImportDate,SalePrice,SasQuery
	from paybayservice.Products
	where StoreID=@StoreID and ProductId > @ProductId
	order by ProductId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetNewStoreOfMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetNewStoreOfMarket]
@MarketID int,
@StoreID int
as
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreId > @StoreID
	order by StoreID desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetOwnerInfo]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetOwnerInfo] --4,9
@StoreID int,
@UserID int
as
	declare @ownerID int
	select @ownerID=OwnerID from paybayservice.Stores where StoreId = @StoreID	
	declare @avar nvarchar(max), @name nvarchar(30)
	select @name=Username,@avar=Avatar from paybayservice.Users where UserId=@UserID
	select @ownerID as OwnerID,@name as UserName,@avar as Avatar
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetProductOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetProductOfStore] --1,-1
@StoreID int,
@ProductId int
as
if(@ProductId <> -1)
begin
	select top 5 row_number() over (order by (select 1)) as STT, ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,ImportDate,SalePrice,SasQuery
	from paybayservice.Products
	where StoreID=@StoreID and ProductId < @ProductId
	order by ProductId desc
end
else
begin
	select top 5 row_number() over (order by (select 1)) as STT, ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,ImportDate,SalePrice,SasQuery
	from paybayservice.Products
	where StoreID=@StoreID
	order by ProductId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetProductWithName]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetProductWithName] --'B'
@ProductId int,
@ProductName nvarchar(100)
as
if(@ProductId <> -1)
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId < @ProductId and ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetSaleInfoOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetSaleInfoOfStore]
@StoreID int,
@isRequired bit
as
	select SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from paybayservice.SaleInfo a inner join paybayservice.Stores b on a.StoreID=b.StoreID
	where a.StoreID=@StoreID and isRequired = @isRequired
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetSaleProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetSaleProduct]
as
	select ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,b.MarketID,MarketName,SalePrice
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID left join paybayservice.Markets c on b.MarketID=c.MarketID
	where SalePrice <> 0
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetStarRated]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_GetStarRated]
@UserId int,
@StoreId int
as
	select ID,UserID,StoreID,RateOfUser
	from paybayservice.StatisticRating
	where UserID=@UserId and StoreID=@StoreId
GO
/****** Object:  StoredProcedure [paybayservice].[sp_GetStoreOfMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_GetStoreOfMarket] 
@MarketID int,
@StoreID int
as
if(@StoreID <> -1)
begin
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreId < @StoreID
	order by StoreID desc
end
else
begin
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,a.SasQuery,Rate
	from paybayservice.Stores a join paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID
	order by StoreID desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_InboxHistory]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_InboxHistory] --9
@OwnerID int
as
	declare @UserID int,@UserName nvarchar(30),@Avatar nvarchar(max),@InboxDate datetime
	declare @tbl table (UserID int,UserName nvarchar(30),Avatar nvarchar(max),InboxDate datetime)
	declare cUser cursor for select distinct UserID
								from paybayservice.MessageInbox
								where OwnerID = @OwnerID
								order by UserID desc
	open cUser
	fetch next from cUser into @UserID
	while @@fetch_status = 0
	begin
		select @UserName=Username,@Avatar=Avatar
		from paybayservice.Users a
		where a.UserID=@UserID

		select top 1 @InboxDate=InboxDate
		from paybayservice.MessageInbox
		where UserID = @UserID
		order by InboxDate desc

		insert into @tbl select @UserID,@UserName,@Avatar,@InboxDate
		fetch next from cUser into @UserID
	end
	close cUser
	deallocate cUser
	select UserID,UserName,Avatar,InboxDate from @tbl as temp
GO
/****** Object:  StoredProcedure [paybayservice].[sp_InsertDetailBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_InsertDetailBill] --permission user
@BillID int,
@ProductID int,
@NumberOf int
as
	declare @UnitPrice float,@Unit nvarchar(20),@numOfPro int
	set @UnitPrice=(select UnitPrice from paybayservice.PRODUCTS where ProductId=@ProductID)
	set @Unit=(select Unit from paybayservice.PRODUCTS where ProductId=@ProductID)
	set @numOfPro = (select NumberOf from paybayservice.Products where ProductID=@ProductID)
	if exists (select 1 from paybayservice.BILLS where BillId=@BillID)
	begin
		if(@numOfPro >= @NumberOf)
		begin
			if not exists (select 1 from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
			begin
				begin tran addDetail
					insert into paybayservice.DETAILBILL(BillID,ProductID,NumberOf,UnitPrice,Unit) values (@BillID,@ProductID,@NumberOf,@UnitPrice,@Unit)

					update paybayservice.Products
					set NumberOf -= @NumberOf
					where ProductID = @ProductID
					if(@@ERROR > 0)
					begin
						rollback tran
						select 0 as ErrCode,'add detail bill not successfull!' as ErrMsg
						return
					end				
				commit						
			end
			else
			begin
				declare @numOld int
				set @numOld = (select NumberOf from paybayservice.DetailBill where BillID=@BillID and ProductID=@ProductID)
				begin tran updateDetail
					update paybayservice.DETAILBILL
					set NumberOf=@NumberOf
					where BillID=@BillID and ProductID=@ProductID

					update paybayservice.Products
					set NumberOf = (NumberOf+@numOld)-@NumberOf
					where ProductID = @ProductID
					if(@@ERROR > 0)
					begin
						rollback tran
						select 0 as ErrCode,'Update bill not successfull!' as ErrMsg
						return
					end
				commit				
			end			
		end
		else
		begin
			select 0 as ErrCode,'This product is out!' as ErrMsg
			return
		end	
		select Id,BillID,ProductID,NumberOf,UnitPrice,Unit
		from paybayservice.DetailBill
		where BillID = @BillID	
	end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadAllSale]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_LoadAllSale] --0,1
@SaleId int,
@isRequired bit
as
if(@SaleId <> -1)
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId < @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
end
else
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadAllSaleWithTitle]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_LoadAllSaleWithTitle] --0,'G',1
@SaleId int,
@SaleName nvarchar(200),
@isRequired bit
as
if(@SaleId <> -1)
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId < @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate()) and Title like N'%'+@SaleName+N'%'
	order by SaleId desc
end
else
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = @isRequired and EndDate >= convert(date,getdate()) and Title like N'%'+@SaleName+N'%'
	order by SaleId desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadNewProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_LoadNewProduct]
@ProductId int
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId > @ProductId
	order by ProductId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadNewProductWithName]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_LoadNewProductWithName]
@ProductId int,
@ProductName nvarchar(100)
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId > @ProductId and ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadNewSale]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_LoadNewSale]
@SaleId int,
@isRequired bit
as
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId > @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_LoadNewSaleWithTitle]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_LoadNewSaleWithTitle]
@SaleId int,
@SaleName nvarchar(200),
@isRequired bit
as
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from paybayservice.SaleInfo a join paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId > @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
			and Title like N'%'+@SaleName+N'%'
	order by SaleId desc
GO
/****** Object:  StoredProcedure [paybayservice].[sp_ResetPassword]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_ResetPassword]
@Email nvarchar(30),
@Pass varbinary(max)
as
	begin tran resetPass
		if exists (select 1 from paybayservice.Users where Email=@Email)
		begin
			update paybayservice.Users
			set Pass=@Pass
			where Email=@Email
		end
		if(@@Error > 0)
			rollback tran
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateBill]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateBill]
@BillID int,
@ReducedPrice float,
@StoreID int,
@UserID int
as
	begin tran updatePrice
		update paybayservice.BILLS
		set ReducedPrice=@ReducedPrice,TotalPrice-=@ReducedPrice
		where BillId=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update bill not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateLike]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateLike]
@StoreID int,
@NumberOf int
as
	begin tran updateLike
		update paybayservice.Stores
		set NumOfLike = @NumberOf
		where StoreID = @StoreID
		if(@@error > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Like is not successful!' as ErrMsg
		end
		else
		begin
			select 1 as ErrCode,'Like is successful!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateMarket]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateMarket]
@ID int,
@Name nvarchar(100),
@Address nvarchar(200),
@Phone varchar(12),
@Image varbinary(max)
as
	begin tran updateMarket
		update paybayservice.MARKETS
		set MarketName=@Name,Address=@Address,Phone=@Phone,Image=@Image
		where MarketId=@ID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update market!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateNumOfProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateNumOfProduct]
@ProductID int,
@NumberOf int,
@ImportDate date
as
	begin tran import
		update paybayservice.Products
		set NumberOf = @NumberOf, ImportDate = @ImportDate
		where ProductID = @ProductID
		if(@@error > 0)
		begin
			rollback tran
		end
		else
		begin
			select 1 as ErrCode, 'Update is successful!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateProduct]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateProduct]
@ID int,
@Name nvarchar(100),
@Image varbinary(max),
@UnitPrice float,
@NumberOf int,
@Unit nvarchar(20),
@StoreID int
as
	begin tran updateProduct
		update paybayservice.PRODUCTS
		set ProductName=@Name,Image=@Image,UnitPrice=@UnitPrice,NumberOf=@NumberOf,Unit=@Unit
		where ProductId=@ID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update product!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateSaleInfo]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateSaleInfo] --permission store owner
@SaleID int,
@Title nvarchar(100),
@Image varbinary(200),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID int
as
	begin tran updateSaleInfo
		update paybayservice.SALES
		set Title=@Title,Image=@Image,Description=@Description,StartDate=@StartDate,EndDate=@EndDate
		where SaleId=@SaleID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Update sale info not successfull!' as ErrMsg
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateStore]
@ID int,
@Name nvarchar(100),
@KiotNo varchar(8),
@Image varbinary(max),
@Phone varchar(12),
@MarketID int,
@OwnerID int
as
	begin tran updateStore
		update paybayservice.STORES
		set StoreName=@Name,KiotNo=@KiotNo,Image=@Image,Phone=@Phone,MarketID=@MarketID
		where StoreId=@ID and OwnerID=@OwnerID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update store!' as ErrMsg
			rollback tran
		end
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateUser]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_UpdateUser]
@UserID int,
@Name nvarchar(100),
@Birthday date,
@Phone varchar(12),
@Gender bit,
@Address nvarchar(200),
@Avatar varbinary(max),
@Pass nvarchar(50),
@TypeID int
as
	begin tran updateUser
		update paybayservice.USERS
		set Name=@Name,Birthday=@Birthday,Phone=@Phone,Gender=@Gender,Address=@Address,Avatar=@Avatar,Pass=PWDENCRYPT(@Pass),TypeID=@TypeID
		where UserId=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update user is not successful!' as ErrMsg
		end
		else
			select 1 as ErrCode,'Update user is successful!' as ErrMsg
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UpdateUserType]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [paybayservice].[sp_UpdateUserType]
@typeId int,
@typeName varchar(20)
as
	begin tran updateType
		update UserType
		set TypeName=@typeName
		where TypeID=@typeId
		if(@@error > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update is not successful!' as ErrMsg
		end
		else
			select 1 as ErrCode,'Update is successful!' as ErrMsg
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UserLogin]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_UserLogin]
@Email nvarchar(30),
@Pass varbinary(max)
as
if exists (select 1 from paybayservice.Users where Email=@Email and Pass=@Pass)
begin
	select UserID,Birthday,Email,Phone,Gender,Address,Avatar,TypeID,Username,Pass,SasQuery
	from paybayservice.Users
	where Email = @Email and Pass = @Pass
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_UserRate]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_UserRate]
@UserId int,
@StoreId int,
@Rate float
as
	begin tran postRate
		if not exists (select 1 from paybayservice.StatisticRating where UserID=@UserId and StoreID=@StoreId)
		begin
			insert into paybayservice.StatisticRating values(@UserId,@StoreId,@Rate)
		end
		else
		begin
			update paybayservice.StatisticRating
			set RateOfUser = @Rate
			where UserID=@UserId and StoreID=@StoreId
		end			
		declare @Rated int,@count int
		select @count=count(*),@Rated=sum(RateOfUser) from paybayservice.StatisticRating where StoreId = 1
		
		update paybayservice.Stores
		set Rate = @Rated/@count
		where StoreId = @StoreId

		select StoreId,Rate from paybayservice.Stores where StoreId = @StoreId
	commit
GO
/****** Object:  StoredProcedure [paybayservice].[sp_ViewCommentOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_ViewCommentOfStore] --1
@StoreID int,
@CommentId int
as
if(@CommentId <> -1)
begin
	select top 4 Id,CommentDate,StoreID,a.UserID,b.Username,b.Avatar,Content,(select RateOfUser from paybayservice.StatisticRating c where c.UserID=a.UserID and StoreID = @StoreID) as Rated
	from paybayservice.Comments a inner join paybayservice.Users b on a.UserID=b.UserID
	where StoreID=@StoreID and Id < @CommentId
	order by Id desc
end
else
begin
	select top 4 Id,CommentDate,StoreID,a.UserID,b.Username,b.Avatar,Content,(select RateOfUser from paybayservice.StatisticRating c where c.UserID=a.UserID and StoreID = @StoreID) as Rated
	from paybayservice.Comments a inner join paybayservice.Users b on a.UserID=b.UserID
	where StoreID=@StoreID
	order by Id desc
end
GO
/****** Object:  StoredProcedure [paybayservice].[sp_ViewNewCmtOfStore]    Script Date: 3/22/2016 11:09:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [paybayservice].[sp_ViewNewCmtOfStore] --1,-1
@StoreID int,
@CommentId int
as
	select top 4 Id,CommentDate,StoreID,a.UserID,b.Username,b.Avatar,Content,(select RateOfUser from paybayservice.StatisticRating c where c.UserID=a.UserID and StoreID = @StoreID) as Rated
	from paybayservice.Comments a inner join paybayservice.Users b on a.UserID=b.UserID
	where StoreID=@StoreID and Id > @CommentId
	order by Id desc
GO
USE [master]
GO
ALTER DATABASE [PayBayDatabase] SET  READ_WRITE 
GO
