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
	
create proc paybayservice.sp_UpdateUser
@UserID int,
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
	begin tran updateUser
		update paybayservice.USERS
		set Name=@Name,Birthday=@Birthday,Email=@Email,Phone=@Phone,Gender=@Gender,Address=@Address,Avatar=@Avatar,Username=@Username,Pass=PWDENCRYPT(@Pass),TypeID=@TypeID
		where UserId=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update user not successful!' as ErrMsg
		end
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

create proc [paybayservice].[sp_InsertDetailBill] --permission user
@BillID int,
@ProductID int,
@NumberOf int
as
	declare @UnitPrice float,@Unit nvarchar(20)
	set @UnitPrice=(select UnitPrice from PRODUCTS where ProductId=@ProductID)
	set @Unit=(select Unit from PRODUCTS where ProductId=@ProductID)
	if exists (select 1 from paybayservice.BILLS where BillId=@BillID)
	begin
		if not exists (select 1 from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
		begin
			begin tran addDetail
				insert into paybayservice.DETAILBILL(BillID,ProductID,NumberOf,UnitPrice,Unit) values (@BillID,@ProductID,@NumberOf,@UnitPrice,@Unit)
				if(@@ERROR > 0)
				begin
					rollback tran
					select 0 as ErrCode,'add detail bill not successfull!' as ErrMsg
				end
			commit
		end
		else
		begin
			begin tran updateDetail
				update paybayservice.DETAILBILL
				set NumberOf=@NumberOf
				where BillID=@BillID and ProductID=@ProductID
				if(@@ERROR > 0)
				begin
					rollback tran
					select 0 as ErrCode,'Update bill not successfull!' as ErrMsg
				end
			commit
		end	
	end

create proc paybayservice.sp_DelDetailBill
@BillID int,
@ProductID int
as
	if exists (select 1 from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID)
	begin
		begin tran delDetailBill
			delete from paybayservice.DETAILBILL where BillID=@BillID and ProductID=@ProductID
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