alter proc [paybayservice].[sp_DelMarket] --super admin
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

select * from paybayservice.Markets

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

create proc [paybayservice].[sp_AddProduct]
@Name nvarchar(100),
@Image varbinary(max),
@UnitPrice float,
@NumberOf int,
@Unit nvarchar(20),
@StoreID int
as
	begin tran addProduct		
		insert into paybayservice.PRODUCTS values(@Name,@Image,@UnitPrice,@NumberOf,@Unit,@StoreID)		
		if(@@ERROR > 0)
		begin
			select 0 as ErrCode, 'Can not add product!' as ErrMsg
			rollback tran
		end
	commit

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

create proc paybayservice.sp_AddUser
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
			insert into paybayservice.USERS(Name,Birthday,Email,Phone,Gender,Address,Avatar,Username,Pass,TypeID)
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
	
alter proc paybayservice.sp_UpdateUser
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

create proc paybayservice.sp_DelUser --spuer admin permission
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

create proc paybayservice.sp_AddComment
@Date date,
@Time time(7),
@StoreID int,
@Content nvarchar(max),
@UserID int
as
	begin tran insertComment
		insert into paybayservice.COMMENTS(CommentDate,CommentTime,StoreID,Content,UserID) values (@Date,@Time,@StoreID,@Content,@UserID)
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Comment not successful!' as ErrMsg
		end
	commit

create proc paybayservice.sp_DelComment --permission Store Owner
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

create proc paybayservice.sp_AddSaleInfo --permission store owner
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

create proc paybayservice.sp_UpdateSaleInfo --permission store owner
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

create proc paybayservice.sp_DelSaleInfo --permission store owner
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

alter proc [paybayservice].[sp_InsertDetailBill] --permission user
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

ALTER proc [paybayservice].[sp_DelDetailBill]
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

insert into paybayservice.Users values('Pham Quang Huy','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)
insert into paybayservice.Users values('Do Thanh Nam','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)
insert into paybayservice.Users values('Pham Quang Huy','2016-10-23','monpham2310@gmail.com','01268673096',1,'Bien Hoa',NULL,'monadmin',pwdencrypt('monadmin'),1)

alter proc paybayservice.sp_AddUserType 'Mon'
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

create proc paybayservice.sp_UpdateUserType
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
	
alter proc paybayservice.sp_ViewCommentOfStore
@StoreID int
as
	select Id,CommentDate,CommentTime,StoreID,a.UserID,Name,Content
	from paybayservice.Comments a inner join paybayservice.Users b on a.UserID=b.UserID
	where StoreID=@StoreID

alter proc paybayservice.sp_GetSaleInfoOfStore
@StoreID int,
@isRequired bit
as
	select SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from paybayservice.SaleInfo a inner join paybayservice.Stores b on a.StoreID=b.StoreID
	where a.StoreID=@StoreID and isRequired = @isRequired

alter proc paybayservice.sp_GetStoreOfMarket 
@MarketID int
as
	select StoreId,StoreName,KiotNo,Image,Phone,MarketID,OwnerID,Rate
	from paybayservice.Stores
	where MarketID=@MarketID

alter proc paybayservice.sp_GetAllSaleInfo
as
	select SaleId,Title,a.Image,Describes,StartDate,EndDate,a.StoreID,StoreName,isRequired
	from paybayservice.SaleInfo a inner join paybayservice.Stores b on a.StoreID=b.StoreID
	where isRequired = 1

create proc paybayservice.sp_GetProductOfStore
@StoreID int
as
	select ProductId,ProductName,Image,UnitPrice,NumberOf,Unit,StoreID,SalePrice
	from paybayservice.Products
	where StoreID=@StoreID

create proc paybayservice.sp_UserLogin
@Username nvarchar(30),
@Pass varbinary(max)
as
	select UserID,Name,Birthday,Email,Phone,Gender,Address,Avatar,TypeID,Username
	from paybayservice.Users
	where Username = @Username and Pass = @Pass

create proc paybayservice.sp_GetDetailBill
@BillID int
as
	select Id,BillID,ProductID,NumberOf,UnitPrice,Unit
	from paybayservice.DetailBill
	where BillID = @BillID

create proc paybayservice.sp_GetBillOfStore
@StoreID int
as
	select BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice,UserID,isShiped
	from Bills
	where StoreID = @StoreID

create proc paybayservice.sp_GetBillOfUser
@UserID int
as
	select BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice,UserID,isShiped
	from Bills
	where UserID = @UserID

create proc paybayservice.sp_UpdateLike
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

create proc paybayservice.sp_AllowShowHomePage
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

create proc paybayservice.sp_UpdateNumOfProduct
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

create proc paybayservice.sp_GetNewProduct
as
	declare @justDate date
	set @justDate = (select max(ImportDate) from paybayservice.Products)
	select ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,b.MarketID,MarketName,SalePrice
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID left join paybayservice.Markets c on b.MarketID=c.MarketID
	where ImportDate = @justDate

create proc paybayservice.sp_GetSaleProduct
as
	select ProductId,ProductName,a.Image,UnitPrice,NumberOf,Unit,a.StoreID,StoreName,b.MarketID,MarketName,SalePrice
	from paybayservice.Products a join paybayservice.Stores b on a.StoreID=b.StoreID left join paybayservice.Markets c on b.MarketID=c.MarketID
	where SalePrice <> 0

alter proc paybayservice.sp_GetBestSaleProduct
as		
	select top 10 a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	from paybayservice.ProductStatistic a join paybayservice.Products b on a.ProductID=b.ProductID join paybayservice.Stores c
		 on c.StoreID=b.StoreID join paybayservice.Markets d on c.MarketID=d.MarketID
	group by a.ProductId,ProductName,b.Image,a.UnitPrice,b.NumberOf,a.Unit,b.StoreID,StoreName,c.MarketID,MarketName,SalePrice
	order by sum(a.NumberOf) DESC

create proc paybayservice.sp_GetMaxProductId
as
	select max(ProductId)
	from paybayservice.Products

create proc paybayservice.sp_GetMaxSaleId
as
	select max(SaleId)
	from paybayservice.SalesInfo

create proc paybayservice.sp_GetImageSale
@isRequired bit
as
	select Image,SasQuery
	from paybayservice.SaleInfo
	where isRequired = @isRequired
	

