create proc sp_AddUser
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
	if not exists (select Username from USERS where Username=@Username)
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
		select 0 as ErrCode,'Username had already exists!Please try again!' as ErrMsg
	
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

create proc sp_DelUser
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
@Content nvarchar(max)
as
	begin tran insertComment
		insert into COMMENTS(CommentDate,CommentTime,StoreID,Content) values (@Date,@Time,@StoreID,@Content)
		if(@@ERROR > 0)
		begin
			rollback tran
			select 0 as ErrCode,'Comment not successful!' as ErrMsg
		end
	commit

create proc sp_DelComment
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

create proc sp_AddSaleInfo
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

create proc sp_UpdateSaleInfo
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

create proc sp_DelSaleInfo
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

create proc sp_AddBill
@BillID varchar(10),
@CreatedDate date,
@StoreID varchar(10),
@TotalPrice float,
@ReducedPrice float
as
	begin tran addBill
		insert into BILL(BillID,CreatedDate,StoreID,TotalPrice,ReducedPrice) values(@BillID,@CreatedDate,@StoreID,@TotalPrice,@ReducedPrice)
		if(@@ERROR > 0)
		begin 
			rollback tran
			select 0 as ErrCode,'Add bill not successfull!' as ErrMsg
		end
	commit



select * from BILL