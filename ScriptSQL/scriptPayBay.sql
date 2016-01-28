alter proc sp_AddUser
@UserID varchar(10),
@Name nvarchar(100),
@Birthday date,
@Email nvarchar(100),
@Phone varchar(12),
@Gender bit,
@Address nvarchar(200),
@Avatar nvarchar(max),
@Username nvarchar(50),
@Pass nvarchar(50),
@TypeID int
as	
	if not exists (select 1 from USERS where Username=@Username and Email=@Email)
	begin
		begin tran insertUser
			insert into USERS(UserID,Name,Birthday,Email,Phone,Gender,Address,Avatar,Username,Pass,TypeID)
			values (@UserID,@Name,@Birthday,@Email,@Phone,@Gender,@Address,@Avatar,@Username,PWDENCRYPT(@Pass),@TypeID)
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
	
create proc sp_UpdateUser
@UserID varchar(10),
@Name nvarchar(100),
@Birthday date,
@Email nvarchar(100),
@Phone varchar(12),
@Gender bit,
@Address nvarchar(200),
@Avatar nvarchar(max),
@Username nvarchar(50),
@Pass nvarchar(50),
@TypeID int
as
	begin tran updateUser
		update USERS
		set Name=@Name,Birthday=@Birthday,Email=@Email,Phone=@Phone,Gender=@Gender,Address=@Address,Avatar=@Avatar,Username=@Username,Pass=PWDENCRYPT(@Pass),TypeID=@TypeID
		where UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update user not successful!' as ErrMsg
		end
	commit

alter proc sp_DelUser --spuer admin permission
@UserID varchar(10)
as
	begin tran delUser
		delete from USERS where UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete user not successful!' as ErrMsg
		end
	commit

alter proc sp_AddComment
@Date date,
@Time time(7),
@StoreID varchar(10),
@Content nvarchar(max),
@UserID varchar(10)
as
	begin tran insertComment
		insert into COMMENTS(CommentDate,CommentTime,StoreID,Content,UserID) values (@Date,@Time,@StoreID,@Content,@UserID)
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Comment not successful!' as ErrMsg
		end
	commit

create proc sp_DelComment --permission Store Owner
@ID int
as
	begin tran delComment
		delete from COMMENTS where ID=@ID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Cannot delete this comment!' as ErrMsg
		end
	commit

create proc sp_AddSaleInfo --permission store owner
@SaleID varchar(10),
@Title nvarchar(100),
@Image nvarchar(200),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID varchar(10)
as
	begin tran addSales
		insert into SALES(SaleID,Title,Image,Description,StartDate,EndDate,StoreID) values (@SaleID,@Title,@Image,@Description,@StartDate,@EndDate,@StoreID)
		if(@@ERROR > 0)
		begin
			rollback tran 
			select 0 as ErrCode,'Add sale info not successfull!' as ErrMsg
		end
	commit

create proc sp_UpdateSaleInfo --permission store owner
@SaleID varchar(10),
@Title nvarchar(100),
@Image nvarchar(200),
@Description nvarchar(max),
@StartDate date,
@EndDate date,
@StoreID varchar(10)
as
	begin tran updateSaleInfo
		update SALES
		set Title=@Title,Image=@Image,Description=@Description,StartDate=@StartDate,EndDate=@EndDate,StoreID=@StoreID
		where SaleID=@SaleID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Update sale info not successfull!' as ErrMsg
		end
	commit

create proc sp_DelSaleInfo --permission store owner
@SaleID varchar(10)
as
	begin tran delSale
		delete from SALESINFO where SaleID=@SaleID
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Delete sale info not successfull!' as ErrMsg
		end
	commit

ALTER proc [dbo].[sp_AddBill]
@BillID varchar(10),
@CreatedDate date,
@StoreID varchar(10),
@UserID varchar(10)
as
	begin tran addBill
		insert into BILL(BillID,CreatedDate,StoreID,UserID) values(@BillID,@CreatedDate,@StoreID,@UserID)
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Add bill not successfull!' as ErrMsg
		end
	commit

ALTER proc [dbo].[sp_UpdateBill]
@BillID varchar(10),
@ReducedPrice float,
@StoreID varchar(10),
@UserID varchar(10)
as
	begin tran updatePrice
		update BILL
		set ReducedPrice=@ReducedPrice,TotalPrice-=@ReducedPrice
		where BillID=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Update bill not successfull!' as ErrMsg
		end
	commit

ALTER proc [dbo].[sp_DelBill]
@BillID varchar(10),
@StoreID varchar(10),
@UserID varchar(10)
as
	begin tran delBill
		delete from BILL where BillID=@BillID and StoreID=@StoreID and UserID=@UserID
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Delete bill not successfull!' as ErrMsg
		end
	commit

ALTER proc [dbo].[sp_InsertDetailBill] --permission user
@BillID varchar(10),
@ProductID varchar(10),
@NumberOf int
as
	declare @UnitPrice float,@Unit nvarchar(20)
	set @UnitPrice=(select UnitPrice from PRODUCTS where ProductID=@ProductID)
	set @Unit=(select Unit from PRODUCTS where ProductID=@ProductID)
	if exists (select 1 from BILL where BillID=@BillID)
	begin
		if not exists (select 1 from DETAILBILL where BillID=@BillID and ProductID=@ProductID)
		begin
			begin tran addDetail
				insert into DETAILBILL(BillID,ProductID,NumberOf,UnitPrice,Unit) values (@BillID,@ProductID,@NumberOf,@UnitPrice,@Unit)
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
				update DETAILBILL
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
