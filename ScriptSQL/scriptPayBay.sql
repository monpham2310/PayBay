alter proc [viethung_paybayservice].[sp_DelMarket] --super admin
@MarketId int
as
	begin tran delMarket
		delete from viethung_paybayservice.Markets where MarketId=@MarketId
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete market!' as ErrMsg
			rollback tran
		end
	commit

select * from viethung_paybayservice.Markets

create proc [viethung_paybayservice].[sp_UpdateMarket]
@ID int,
@Name nvarchar(100),
@Address nvarchar(200),
@Phone varchar(12),
@Image varbinary(max)
as
	begin tran updateMarket
		update viethung_paybayservice.MARKETS
		set MarketName=@Name,Address=@Address,Phone=@Phone,Image=@Image
		where MarketId=@ID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update market!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_AddMarket]
@Name nvarchar(100),
@Address nvarchar(200),
@Phone varchar(12),
@Image varbinary(max)
as
	begin tran addMarket		
		insert into viethung_paybayservice.MARKETS values(@Name,@Address,@Phone,@Image)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add market!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_AddStore]
@Name nvarchar(100),
@KiotNo varchar(8),
@Image varbinary(max),
@Phone varchar(12),
@MarketID int,
@OwnerID int
as
	begin tran addStore		
		insert into viethung_paybayservice.STORES values(@Name,@KiotNo,@Image,@Phone,@MarketID,@OwnerID)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add store!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_UpdateStore]
@ID int,
@Name nvarchar(100),
@KiotNo varchar(8),
@Image varbinary(max),
@Phone varchar(12),
@MarketID int,
@OwnerID int
as
	begin tran updateStore
		update viethung_paybayservice.STORES
		set StoreName=@Name,KiotNo=@KiotNo,Image=@Image,Phone=@Phone,MarketID=@MarketID
		where StoreId=@ID and OwnerID=@OwnerID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update store!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_DelStore] --Super Admin
@ID int
as
	begin tran delStore
		delete from viethung_paybayservice.STORES where StoreId=@ID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete store!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_AddProduct]
@Name nvarchar(100),
@Image varbinary(max),
@UnitPrice float,
@NumberOf int,
@Unit nvarchar(20),
@StoreID int
as
	begin tran addProduct		
		insert into viethung_paybayservice.PRODUCTS values(@Name,@Image,@UnitPrice,@NumberOf,@Unit,@StoreID)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add product!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_UpdateProduct]
@ID int,
@Name nvarchar(100),
@Image varbinary(max),
@UnitPrice float,
@NumberOf int,
@Unit nvarchar(20),
@StoreID int
as
	begin tran updateProduct
		update viethung_paybayservice.PRODUCTS
		set ProductName=@Name,Image=@Image,UnitPrice=@UnitPrice,NumberOf=@NumberOf,Unit=@Unit
		where ProductId=@ID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not update product!' as ErrMsg
			rollback tran
		end
	commit

create proc [viethung_paybayservice].[sp_DelProduct]
@ID int,
@StoreID int
as
	begin tran delProduct
		delete from viethung_paybayservice.PRODUCTS where ProductId=@ID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode,'Can not delete product!' as ErrMsg
			rollback tran
		end
	commit

create proc viethung_paybayservice.sp_AddUser
@Name nvarchar(100),
@Birthday date,
@Email nvarchar(100),
@Phone varchar(12),
@Gender bit,
@Address nvarchar(200),
@Avatar varbinary(max),
@Username nvarchar(50),
@Pass nvarchar(50),
@TypeID int
as	
	if not exists (select 1 from USERS where Username=@Username and Email=@Email)
	begin
		begin tran insertUser
			insert into viethung_paybayservice.USERS(Name,Birthday,Email,Phone,Gender,Address,Avatar,Username,Pass,TypeID)
			values (@Name,@Birthday,@Email,@Phone,@Gender,@Address,@Avatar,@Username,PWDENCRYPT(@Pass),@TypeID)
			if(@@ERROR > 0)
			begin				
				rollback tran
				select 0 as ErrCode,'Add User not successful!' as ErrMsg
			end
			else
				select 1 as ErrCode,'Add User successful!' as ErrMsg
		commit
	end
	else
		select 0 as ErrCode,'Username and email had already registered!Please try again!' as ErrMsg
	
alter proc viethung_paybayservice.sp_UpdateUser
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
		update viethung_paybayservice.USERS
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

create proc viethung_paybayservice.sp_DelUser --spuer admin permission
@UserID int
as
	begin tran delUser
		delete from viethung_paybayservice.USERS where UserId=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete user not successful!' as ErrMsg
		end
	commit

create proc viethung_paybayservice.sp_AddComment
@Date date,
@Time time(7),
@StoreID int,
@Content nvarchar(max),
@UserID int
as
	begin tran insertComment
		insert into viethung_paybayservice.COMMENTS(CommentDate,CommentTime,StoreID,Content,UserID) values (@Date,@Time,@StoreID,@Content,@UserID)
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Comment not successful!' as ErrMsg
		end
	commit

create proc viethung_paybayservice.sp_DelComment --permission Store Owner
@ID int
as
	begin tran delComment
		delete from viethung_paybayservice.COMMENTS where Id=@ID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Cannot delete this comment!' as ErrMsg
		end
	commit

create proc viethung_paybayservice.sp_AddSaleInfo --permission store owner
@Title nvarchar(100),
@Image varbinary(max),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID int
as
	begin tran addSales
		insert into viethung_paybayservice.SALES(Title,Image,Describes,StartDate,EndDate,StoreID) values (@Title,@Image,@Description,@StartDate,@EndDate,@StoreID)
		if(@@ERROR > 0)
		begin
			rollback tran 
			select 0 as ErrCode,'Add sale info not successfull!' as ErrMsg
		end
	commit

create proc viethung_paybayservice.sp_UpdateSaleInfo --permission store owner
@SaleID int,
@Title nvarchar(100),
@Image varbinary(200),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID int
as
	begin tran updateSaleInfo
		update viethung_paybayservice.SALES
		set Title=@Title,Image=@Image,Description=@Description,StartDate=@StartDate,EndDate=@EndDate
		where SaleId=@SaleID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Update sale info not successfull!' as ErrMsg
		end
	commit

create proc viethung_paybayservice.sp_DelSaleInfo --permission store owner
@SaleID int,
@StoreID int
as
	begin tran delSale
		delete from viethung_paybayservice.SALESINFO where SaleId=@SaleID and StoreID=@StoreID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Delete sale info not successfull!' as ErrMsg
		end
	commit

create proc [viethung_paybayservice].[sp_UpdateBill]
@BillID int,
@ReducedPrice float,
@StoreID int,
@UserID int
as
	begin tran updatePrice
		update viethung_paybayservice.BILLS
		set ReducedPrice=@ReducedPrice,TotalPrice-=@ReducedPrice
		where BillId=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update bill not successfull!' as ErrMsg
		end
	commit

create proc [viethung_paybayservice].[sp_DelBill]
@BillID int,
@StoreID int,
@UserID int
as
	begin tran delBill
		delete from viethung_paybayservice.BILLS where BillId=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete bill not successfull!' as ErrMsg
		end
	commit

alter proc [viethung_paybayservice].[sp_InsertDetailBill] --permission user
@BillID int,
@ProductID int,
@NumberOf int
as
	declare @UnitPrice float,@Unit nvarchar(20),@numOfPro int
	set @UnitPrice=(select UnitPrice from viethung_paybayservice.PRODUCTS where ProductId=@ProductID)
	set @Unit=(select Unit from viethung_paybayservice.PRODUCTS where ProductId=@ProductID)
	set @numOfPro = (select NumberOf from viethung_paybayservice.Products where ProductID=@ProductID)
	if exists (select 1 from viethung_paybayservice.BILLS where BillId=@BillID)
	begin
		if(@numOfPro >= @NumberOf)
		begin
			if not exists (select 1 from viethung_paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
			begin
				begin tran addDetail
					insert into viethung_paybayservice.DETAILBILL(BillID,ProductID,NumberOf,UnitPrice,Unit) values (@BillID,@ProductID,@NumberOf,@UnitPrice,@Unit)

					update viethung_paybayservice.Products
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
				set @numOld = (select NumberOf from viethung_paybayservice.DetailBill where BillID=@BillID and ProductID=@ProductID)
				begin tran updateDetail
					update viethung_paybayservice.DETAILBILL
					set NumberOf=@NumberOf
					where BillID=@BillID and ProductID=@ProductID

					update viethung_paybayservice.Products
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
		from viethung_paybayservice.DetailBill
		where BillID = @BillID	
	end

ALTER proc [viethung_paybayservice].[sp_DelDetailBill]
@BillID int,
@ProductID int
as
	if exists (select 1 from viethung_paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
	begin
		begin tran delDetailBill
			if(@ProductID <> 0)
				delete from viethung_paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID
			else
				delete from viethung_paybayservice.DETAILBILL where BillID=@BillID

			if(@@ERROR > 0)
			begin
				rollback tran
				select 1 as ErrCode,'Delete detail bill successful!' as ErrMsg
			end
		commit
	end

insert into viethung_paybayservice.Users values('Pham Quang Huy','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)
insert into viethung_paybayservice.Users values('Do Thanh Nam','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)
insert into viethung_paybayservice.Users values('Pham Quang Huy','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)

alter proc viethung_paybayservice.sp_AddUserType 'Mon'
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

create proc viethung_paybayservice.sp_UpdateUserType
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
	
alter proc viethung_paybayservice.sp_ViewCommentOfStore
@StoreID int
as
	select Id,CommentDate,CommentTime,StoreID,a.UserID,Name,Content
	from viethung_paybayservice.Comments a inner join viethung_paybayservice.Users b on a.UserID=b.UserID
	where StoreID=@StoreID

alter proc viethung_paybayservice.sp_GetSaleInfoOfStore
@StoreID int,
@isRequired bit
as
	select SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from viethung_paybayservice.SaleInfo a inner join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where a.StoreID=@StoreID and isRequired = @isRequired

alter proc viethung_paybayservice.sp_GetStoreOfMarket 
@MarketID int
as
	select StoreId,StoreName,KiotNo,Image,Phone,MarketID,OwnerID,Rate
	from viethung_paybayservice.Stores
	where MarketID=@MarketID

alter proc viethung_paybayservice.sp_GetAllSaleInfo
as
	select SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from viethung_paybayservice.SaleInfo a inner join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = 1

create proc viethung_paybayservice.sp_GetProductOfStore
@StoreID int
as
	select ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,SalePrice
	from viethung_paybayservice.Products
	where StoreID=@StoreID

create proc viethung_paybayservice.sp_UserLogin
@Username nvarchar(30),
@Pass varbinary(max)
as
	select UserID,Name,Birthday,Email,Phone,Gender,Address,Avatar,TypeID,Username
	from viethung_paybayservice.Users
	where Username = @Username and Pass = @Pass

create proc viethung_paybayservice.sp_GetDetailBill
@BillID int
as
	select Id,BillID,ProductID,NumberOf,UnitPrice,Unit
	from viethung_paybayservice.DetailBill
	where BillID = @BillID

create proc viethung_paybayservice.sp_GetBillOfStore
@StoreID int
as
	select BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice,UserID,isShiped
	from Bills
	where StoreID = @StoreID

create proc viethung_paybayservice.sp_GetBillOfUser
@UserID int
as
	select BillID,CreatedDate,a.StoreID,StoreName,TotalPrice,ReducedPrice,UserID,Note,ShipMethod,TradeTerm,AgreeredShippingDate,ShippingDate
	from viethung_paybayservice.Bills a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where UserID = @UserID

create proc viethung_paybayservice.sp_UpdateLike
@StoreID int,
@NumberOf int
as
	begin tran updateLike
		update viethung_paybayservice.Stores
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

create proc viethung_paybayservice.sp_AllowShowHomePage
@saleId int,
@isRequired bit
as
	begin tran updateRequired
		update viethung_paybayservice.SaleInfo
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

create proc viethung_paybayservice.sp_UpdateNumOfProduct
@ProductID int,
@NumberOf int,
@ImportDate date
as
	begin tran import
		update viethung_paybayservice.Products
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

create proc viethung_paybayservice.sp_GetNewProduct
as
	declare @justDate date
	set @justDate = (select max(ImportDate) from viethung_paybayservice.Products)
	select ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,b.MarketID,MarketName,SalePrice
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID left join viethung_paybayservice.Markets c on b.MarketID=c.MarketID
	where ImportDate = @justDate

create proc viethung_paybayservice.sp_GetSaleProduct
as
	select ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,b.MarketID,MarketName,SalePrice
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID left join viethung_paybayservice.Markets c on b.MarketID=c.MarketID
	where SalePrice <> 0

alter proc viethung_paybayservice.sp_GetBestSaleProduct
as		
	select top 10 a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	from viethung_paybayservice.ProductStatistic a join viethung_paybayservice.Products b on a.ProductID=b.ProductID join viethung_paybayservice.Stores c
		 on c.StoreID=b.StoreID join viethung_paybayservice.Markets d on c.MarketID=d.MarketID
	group by a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	order by sum(a.NumberOf) DESC

create proc viethung_paybayservice.sp_GetMaxProductId
as
	select max(ProductId)
	from viethung_paybayservice.Products

create proc viethung_paybayservice.sp_GetMaxSaleId
as
	select max(SaleId)
	from viethung_paybayservice.SalesInfo

create proc viethung_paybayservice.sp_GetImageSale
@isRequired bit
as
	select Image,SasQuery
	from viethung_paybayservice.SaleInfo
	where isRequired = @isRequired
	
create proc viethung_paybayservice.sp_GetMaxMarketId
as
	select max(MarketId)
	from viethung_paybayservice.Markets

create proc viethung_paybayservice.sp_GetMaxUserId
as
	select max(UserId)
	from viethung_paybayservice.Users

create proc viethung_paybayservice.sp_GetMaxStoreId
as
	select max(StoreID)
	from viethung_paybayservice.Stores



alter proc viethung_paybayservice.sp_GetMarketWithName --0,'Ba'
@MarketId int,
@MarketName nvarchar(100)
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from viethung_paybayservice.Markets
	where MarketId > @MarketId and MarketName like N'%'+@MarketName+N'%'

alter proc viethung_paybayservice.sp_FindStore --'Ba',1,-1
@StoreName nvarchar(100),
@MarketID int,
@StoreId int
as
	select top 12 StoreId,StoreName,KiotNo,Image,a.Phone,MarketID,OwnerID,Username,a.SasQuery,Rate
	from viethung_paybayservice.Stores a join viethung_paybayservice.Users b on a.OwnerID=b.UserId
	where MarketID=@MarketID and StoreId>@StoreId and StoreName like N'%'+@StoreName+N'%'

alter proc viethung_paybayservice.sp_GetMoreMarket --0
@MarketId int
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from viethung_paybayservice.Markets
	where MarketId > @MarketId


alter proc viethung_paybayservice.sp_UserRate
@UserId int,
@StoreId int,
@Rate float
as
	begin tran postRate
		if not exists (select 1 from viethung_paybayservice.StatisticRating where UserID=@UserId and StoreID=@StoreId)
		begin
			insert into viethung_paybayservice.StatisticRating values(@UserId,@StoreId,@Rate)
		end
		else
		begin
			update viethung_paybayservice.StatisticRating
			set RateOfUser = @Rate
			where UserID=@UserId and StoreID=@StoreId
		end			
		declare @Rated int,@count int
		select @count=count(*),@Rated=sum(RateOfUser) from viethung_paybayservice.StatisticRating where StoreId = 1
		
		update viethung_paybayservice.Stores
		set Rate = @Rated/@count
		where StoreId = @StoreId

		select StoreId,Rate from viethung_paybayservice.Stores where StoreId = @StoreId
	commit

alter proc viethung_paybayservice.sp_LoadAllSale --0,1
@SaleId int,
@isRequired bit
as
if(@SaleId <> -1)
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId < @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
end
else
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc
end

alter proc viethung_paybayservice.sp_GetMoreProduct --0
@ProductId int
as
if(@ProductId <> -1)
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID	
	where ProductId < @ProductId
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID	
	order by ProductId desc
end

alter proc viethung_paybayservice.sp_GetProductWithName --'B'
@ProductId int,
@ProductName nvarchar(100)
as
if(@ProductId <> -1)
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId < @ProductId and ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc
end

create proc viethung_paybayservice.sp_LoadNewSale
@SaleId int,
@isRequired bit
as
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId > @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
	order by SaleId desc

alter proc viethung_paybayservice.sp_LoadNewProduct
@ProductId int
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId > @ProductId
	order by ProductId desc

create proc viethung_paybayservice.sp_LoadNewProductWithName
@ProductId int,
@ProductName nvarchar(100)
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where ProductId > @ProductId and ProductName like N'%'+@ProductName+N'%'
	order by ProductId desc

create proc viethung_paybayservice.sp_LoadNewSaleWithTitle
@SaleId int,
@SaleName nvarchar(200),
@isRequired bit
as
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId > @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate())
			and Title like N'%'+@SaleName+N'%'
	order by SaleId desc

alter proc viethung_paybayservice.sp_LoadAllSaleWithTitle --0,'G',1
@SaleId int,
@SaleName nvarchar(200),
@isRequired bit
as
if(@SaleId <> -1)
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where SaleId < @SaleId and isRequired = @isRequired and EndDate >= convert(date,getdate()) and Title like N'%'+@SaleName+N'%'
	order by SaleId desc
end
else
begin
	select top 5 SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = @isRequired and EndDate >= convert(date,getdate()) and Title like N'%'+@SaleName+N'%'
	order by SaleId desc
end

create proc viethung_paybayservice.sp_GetNewMarketWithName
@MarketId int,
@MarketName nvarchar(100)
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from viethung_paybayservice.Markets
	where MarketId > @MarketId and MarketName like N'%'+@MarketName+N'%'
	order by MarketId desc

create proc viethung_paybayservice.sp_GetNewMarket
@MarketId int
as
	select top 5 MarketId,MarketName,Address,Phone,Image,SasQuery,Longitute,Latitute
	from viethung_paybayservice.Markets
	where MarketId > @MarketId
	order by MarketId desc

create proc viethung_paybayservice.sp_GetNewProductOfStore
@StoreId int,
@ProductId int
as
	select top 5 row_number() over (order by (select 1)) as STT, ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,ImportDate,SalePrice,SasQuery
	from viethung_paybayservice.Products
	where StoreID=@StoreID and ProductId > @ProductId
	order by ProductId desc

alter proc viethung_paybayservice.sp_ViewNewCmtOfStore 1,-1
@StoreID int,
@CommentId int
as
	declare @table table (Id int,CommentDate datetime,StoreID int,UserID int,Username nvarchar(30),Avatar nvarchar(max),Content nvarchar(max),Rated float)
	declare @Id int,@CommentDate datetime,@Store int,@UserID int,@Username nvarchar(30),@Avatar nvarchar(max),@Content nvarchar(max)
	declare cCursor cursor for select top 5 Id,CommentDate,StoreID,a.UserID,b.Username,b.Avatar,Content
								from viethung_paybayservice.Comments a inner join viethung_paybayservice.Users b on a.UserID=b.UserID
								where StoreID=@StoreID and Id > @CommentId
								order by Id desc
	open cCursor
	fetch next from cCursor into @Id,@CommentDate,@Store,@UserID,@Username,@Avatar,@Content
	while @@fetch_status = 0
	begin
		declare @Rated float
		set @Rated = (select RateOfUser from viethung_paybayservice.StatisticRating where UserID = @UserID and StoreID = @StoreID)
		insert into @table select @Id,@CommentDate,@Store,@UserID,@Username,@Avatar,@Content,@Rated
		fetch next from cCursor into @Id,@CommentDate,@Store,@UserID,@Username,@Avatar,@Content
	end
	close cCursor
	deallocate cCursor
	select Id,CommentDate,StoreID,UserID,Username,Avatar,Content,Rated from @table as VariableTable

create proc viethung_paybayservice.sp_GetStarRated 8,1
@UserId int,
@StoreId int
as
	select ID,UserID,StoreID,RateOfUser
	from viethung_paybayservice.StatisticRating
	where UserID=@UserId and StoreID=@StoreId

create proc viethung_paybayservice.sp_ResetPassword
@Email nvarchar(30),
@Pass varbinary(max)
as
	begin tran resetPass
		if exists (select 1 from viethung_paybayservice.Users where Email=@Email)
		begin
			update viethung_paybayservice.Users
			set Pass=@Pass
			where Email=@Email
		end
		if(@@Error > 0)
			rollback tran
	commit


alter proc viethung_paybayservice.sp_GetMoreMessageDetail
@ID int,
@MessageID int
as
	if(@ID = -1)
	begin
		select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
		from viethung_paybayservice.InboxDetail a join viethung_paybayservice.MessageInbox b on a.MessageId=b.MessageId
				join viethung_paybayservice.Users c on b.UserId=c.UserId 
		where a.MessageID = @MessageID
		order by ID desc
	end
	else
	begin
		select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
		from viethung_paybayservice.InboxDetail a join viethung_paybayservice.MessageInbox b on a.MessageId=b.MessageId
				join viethung_paybayservice.Users c on b.UserId=c.UserId 
		where a.MessageID = @MessageID and ID < @ID 
		order by ID desc
	end

alter proc viethung_paybayservice.sp_GetNewMessageDetail
@ID int,
@MessageID int
as
	select top 5 ID,a.MessageId,b.UserId,Username,Avatar,Content,InboxDate
	from viethung_paybayservice.InboxDetail a join viethung_paybayservice.MessageInbox b on a.MessageId=b.MessageId
			join viethung_paybayservice.Users c on b.UserId=c.UserId 
	where a.MessageID = @MessageID and ID > @ID 
	order by ID desc

ALTER proc [viethung_paybayservice].[sp_GetMessageOfStore] --52,8
@MessageID int,
@OwnerID int
as	
	if(@MessageID = -1)
	begin
		select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from (
			select top 8 MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
			from viethung_paybayservice.MessageInbox a join viethung_paybayservice.Users b on a.OwnerID=b.UserID
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
			from viethung_paybayservice.MessageInbox a join viethung_paybayservice.Users b on a.OwnerID=b.UserID
			where  MessageID < @MessageID and (OwnerID = @OwnerID or a.UserID = @OwnerID)
			order by MessageID desc
		) a
		order by MessageID asc
	end

alter proc viethung_paybayservice.sp_GetMoreNewMessage --10,9
@MessageID int,
@OwnerID int
as
	select MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
		from (
			select top 8 MessageID,a.UserID,OwnerID,Username,Avatar,InboxDate,Content
			from viethung_paybayservice.MessageInbox a join viethung_paybayservice.Users b on a.OwnerID=b.UserID
			where MessageID > @MessageID and (OwnerID = @OwnerID or a.UserID = @OwnerID)
			order by MessageID desc
		) a
		order by MessageID asc

alter proc viethung_paybayservice.sp_InboxHistory --9
@OwnerID int
as
	declare @UserID int,@UserName nvarchar(30),@Avatar nvarchar(max),@InboxDate datetime
	declare @tbl table (UserID int,UserName nvarchar(30),Avatar nvarchar(max),InboxDate datetime)
	declare cUser cursor for select distinct UserID
								from viethung_paybayservice.MessageInbox
								where OwnerID = @OwnerID
								order by UserID desc
	open cUser
	fetch next from cUser into @UserID
	while @@fetch_status = 0
	begin
		select @UserName=Username,@Avatar=Avatar
		from viethung_paybayservice.Users a
		where a.UserID=@UserID

		select top 1 @InboxDate=InboxDate
		from viethung_paybayservice.MessageInbox
		where UserID = @UserID
		order by InboxDate desc

		insert into @tbl select @UserID,@UserName,@Avatar,@InboxDate
		fetch next from cUser into @UserID
	end
	close cUser
	deallocate cUser
	select UserID,UserName,Avatar,InboxDate from @tbl as temp

alter schema viethung_paybayservice transfer viethung_paybayservice.Bills
alter schema viethung_paybayservice transfer viethung_paybayservice.Comments
alter schema viethung_paybayservice transfer viethung_paybayservice.DetailBill
alter schema viethung_paybayservice transfer viethung_paybayservice.Markets
alter schema viethung_paybayservice transfer viethung_paybayservice.MessageInbox
alter schema viethung_paybayservice transfer viethung_paybayservice.Products
alter schema viethung_paybayservice transfer viethung_paybayservice.ProductStatistic
alter schema viethung_paybayservice transfer viethung_paybayservice.RevenueStatistic
alter schema viethung_paybayservice transfer viethung_paybayservice.SaleInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.StatisticRating
alter schema viethung_paybayservice transfer viethung_paybayservice.Stores
alter schema viethung_paybayservice transfer viethung_paybayservice.Users
alter schema viethung_paybayservice transfer viethung_paybayservice.UserType

alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddComment
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddMsgDetail
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddNewMessage
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddSaleInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AddUserType
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_AllowShowHomePage
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelComment
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelDetailBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelSaleInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_DelUser
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_FindNewStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_FindStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetAllSaleInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetBestSaleProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetDetailBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetImageSale
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMarketWithName
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMaxId
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMessageOfStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMoreMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMoreMessageDetail
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMoreNewMessage
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetMoreProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewMarketWithName
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewMessageDetail
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewProductOfStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetNewStoreOfMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetOwnerInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetProductOfStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetProductWithName
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetSaleInfoOfStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetSaleProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetStarRated
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_GetStoreOfMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_InboxHistory
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_InsertDetailBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadAllSale
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadAllSaleWithTitle
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadNewProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadNewProductWithName
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadNewSale
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_LoadNewSaleWithTitle
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_ResetPassword
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateBill
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateMarket
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateNumOfProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateProduct
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateSaleInfo
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UpdateUserType
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UserLogin
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_UserRate
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_ViewCommentOfStore
alter schema viethung_paybayservice transfer viethung_paybayservice.sp_ViewNewCmtOfStore

create proc viethung_paybayservice.sp_GetStoreOfOwner
@OwnerID int
as
	select StoreId,StoreName,KiotNo,Image,Phone,MarketID,OwnerID,SasQuery,Rate
	from viethung_paybayservice.STORES
	where OwnerID = @OwnerID

create proc viethung_paybayservice.sp_GetAllMarket
as
	select MarketID,MarketName
	from viethung_paybayservice.Markets
	
---------------------------------------------------------

create proc viethung_paybayservice.sp_GetProductOfOwner
@OwnerID int,
@ProductID int
as
if(@ProductID = -1)
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where OwnerID = @OwnerID
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where OwnerID = @OwnerID and ProductID < @ProductID
	order by ProductId desc
end

create proc viethung_paybayservice.sp_GetMoreProductOfOwner
@OwnerID int,
@ProductID int
as
	select top 5 ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,ImportDate,SalePrice,a.SasQuery
	from viethung_paybayservice.Products a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where OwnerID = @OwnerID and ProductID > @ProductID
	order by ProductId desc

---------------------------------------------
alter proc [viethung_paybayservice].[sp_AddBill]
@CreatedDate date,
@StoreID int,
@TotalPrice float,
@ReducedPrice float,
@UserID int,
@Note nvarchar(200),
@ShipMethod nvarchar(100),
@TradeTerm nvarchar(100),
@Agree nvarchar(100),
@ShipDate datetime
as
	begin tran addBill
		insert into viethung_paybayservice.BILLS values(@CreatedDate,@StoreID,
												@TotalPrice,@ReducedPrice,@UserID,@Note,@ShipMethod,@TradeTerm,@Agree,@ShipDate)
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Add bill not successfull!' as ErrMsg
		end
		select Username,Avatar,(select OwnerID from viethung_paybayservice.Stores where StoreID = @StoreID) as OwnerID
		from viethung_paybayservice.Users  
		where UserID = @UserID		
	commit
	
create proc viethung_paybayservice.sp_GetSaleOfOwner
@SaleID int,
@OwnerID int
as
	if(@SaleID = -1)
	begin
		select top 5 SaleId,Title,Describes,StartDate,EndDate,a.StoreID,a.Image,a.SasQuery
		from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
		where OwnerID = @OwnerID
		order by SaleId desc
	end
	else
	begin
		select top 5 SaleId,Title,Describes,StartDate,EndDate,a.StoreID,a.Image,a.SasQuery
		from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
		where OwnerID = @OwnerID and SaleId < @SaleID
		order by SaleId desc
	end
	
create proc viethung_paybayservice.sp_GetMoreSaleOfOwner
@SaleID int,
@OwnerID int
as
	select top 5 SaleId,Title,Describes,StartDate,EndDate,a.StoreID,a.Image,a.SasQuery
	from viethung_paybayservice.SaleInfo a join viethung_paybayservice.Stores b on a.StoreID=b.StoreID
	where OwnerID = @OwnerID and SaleId > @SaleID
	order by SaleId desc

create proc viethung_paybayservice.sp_GetNewMechandise
@ProductID int
as
if(@ProductID = -1)
begin
	select top 5 ProductId,ProductName,Image
	from viethung_paybayservice.Products
	order by ProductId desc
end
else
begin
	select top 5 ProductId,ProductName,Image
	from viethung_paybayservice.Products
	where ProductId < @ProductId
	order by ProductId desc
end

create proc viethung_paybayservice.sp_GetMoreNewMechandise
@ProductID int
as
	select top 5 ProductId,ProductName,Image
	from viethung_paybayservice.Products
	where ProductId > @ProductId
	order by ProductId desc

create proc viethung_paybayservice.sp_GetUserJustOrder
@StoreID int,
@UserID int
as
	select Username,Avatar,(select OwnerID from viethung_paybayservice.Stores where StoreID = @StoreID) as OwnerID
	from viethung_paybayservice.Users
	where UserID = @UserID

alter proc viethung_paybayservice.sp_GetNearbyMarketList --10.846483,106.682361
@Latitute float,
@Longitute float
as	
	declare @marketId int,@marketName nvarchar(100),@address nvarchar(200),@phone varchar(12),@image nvarchar(max),@sasquery nvarchar(max),@lat float,@long float,@opentime time,@closetime time
	declare @table table (MarketId int,MarketName nvarchar(100),Address nvarchar(200), Phone varchar(12),Image nvarchar(max),SasQuery nvarchar(max),Latitute float,Longitute float,OpenTime time,CloseTime time,Distance float)
	declare cCursor cursor for select MarketId,MarketName,Address,Phone,Image,SasQuery,Latitute,Longitute,OpenTime,CloseTime
							   from viethung_paybayservice.Markets	
	open cCursor 
	fetch next from cCursor into @marketId,@marketName,@address,@phone,@image,@sasquery,@lat,@long,@opentime,@closetime
	while @@fetch_status = 0
	begin
		declare @distance float
		set @distance = viethung_paybayservice.fnCalcDistanceKM(@Latitute,@lat,@Longitute,@long)
						
		insert into @table select @marketId,@marketName,@address,@phone,@image,@sasquery,@lat,@long,@opentime,@closetime,@distance

		fetch next from cCursor into @marketId,@marketName,@address,@phone,@image,@sasquery,@lat,@long,@opentime,@closetime
	end
	close cCursor
	deallocate cCursor
	select MarketId,MarketName,Address,Phone,Image,SasQuery,Latitute,Longitute,OpenTime,CloseTime,Distance
	from @table as tbl
	order by Distance