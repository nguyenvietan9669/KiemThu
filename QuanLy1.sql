create database Quanlynhahang
use Quanlynhahang
--Food
--Table
--Foodcategory
--Account
--Bill
--BillInfo
create table TableFood
(
    ID INT IDENTITY PRIMARY KEY,
    name nvarchar(100) not null,
    status nvarchar(100)not null default N'Trống'
)
create table Account
(
    ID INT IDENTITY PRIMARY KEY not null,
    DisplayName nvarchar(100) not null,
    userName nvarchar(100) not null,
    PassWord nvarchar(100) default 0,
    type int not null default 0 --1 admin 0 nhân viên
)
create table Foodcategory
(
    ID INT IDENTITY PRIMARY KEY,
    name nvarchar(100)
)
create table Food
(
    ID INT IDENTITY PRIMARY KEY,
    name nvarchar(150) not null,
    idcategory int not null,
    gia float not null,
    foreign key (idcategory) references	dbo.Foodcategory(ID)
)
CREATE Table Bill
(
     ID INT IDENTITY PRIMARY KEY,
     NgayVao date not null default getdate(),
     NgayRa date,
     idTableFood int not null,
     status int not null default 0,--1 đã thanh toán, 0 chưa thanh toán 
     foreign key (idTableFood ) references	dbo.TableFood(ID)
)
CREATE Table Billinfo
(
     ID INT IDENTITY PRIMARY KEY,
     idbill int not null,
     idfood int not null,
     count int not null default 0
     foreign key(idbill ) references dbo.Bill (ID),
     foreign key(idfood ) references dbo.Food (ID)
     
)
Insert INTO dbo.Account
(
       userName,
       DisplayName,
       PassWord,
       type
)
values
(
      'NVA',
      'NVA',
      '123',
      0
)
Insert INTO dbo.Account
(
       userName,
       DisplayName,
       PassWord,
       type
)
values
(
      'Admin',
      'Admin',
      '123',
      1
)
use Quanlynhahang
select * from dbo.Account
select * from dbo.Account Where userName ='NVA' AND PassWord ='123'


go
create Proc USP_Login
@userName nvarchar(100),@passWord nvarchar(100)
AS
Begin
   select * from dbo.Account where userName = @userName	AND PassWord =	@passWord
END
go
Declare @i int =0
while @i <=24
begin
insert dbo.TableFood (name)Values (N'Bàn' + CAST(@i as nvarchar(100)))
set @i=@i+1;
end
go 
create Proc USP_GetTableList
as select * from dbo.TableFood
go 

go

exec dbo.USP_GetTableList
go

--Thêm loại món ăn
Insert dbo.Foodcategory(name)
values(N'Hải Sản')
Insert dbo.Foodcategory(name)
values(N'Nông Sản')
Insert dbo.Foodcategory(name)
values(N'Tráng miệng')
Insert dbo.Foodcategory(name)
values(N'Đặc Sản')
Insert dbo.Foodcategory(name)
values(N'Nước')
--Món ăn

--Hải sản
Insert  dbo.Food(name,idcategory,gia)
values (N'Ốc hương tự nhiên Nha Trang',1,200000)
Insert dbo.Food(name,idcategory,gia)
values (N'Tôm hùm Bình Ba',1,150000)
Insert dbo.Food(name,idcategory,gia)
values (N'Cua Năm Căn Cà Mau',1,170000)
Insert dbo.Food(name,idcategory,gia)
values (N'Ghẹ',1,100000)
Insert dbo.Food(name,idcategory,gia)
values (N' "Mực nháy” Cửa Lò',1,80000)
Insert dbo.Food(name,idcategory,gia)
values (N'Cá ngừ đại dương',1,150000)
Insert dbo.Food(name,idcategory,gia)
values (N' Bào ngư Bạch Long Vỹ ',1,200000)
Insert dbo.Food(name,idcategory,gia)
values (N'Mực một nắng Phan Thiết',1,100000)
Insert dbo.Food(name,idcategory,gia)
values (N'Ốc vú nàng Côn Đảo',1,120000)
Insert dbo.Food(name,idcategory,gia)
values (N'Tôm tít',1,100000)
go
use Quanlynhahang
Insert dbo.Food(name,idcategory,gia)
values (N'Vú dê nướng sữa',2,500000)
Insert dbo.Food(name,idcategory,gia)
values (N'Heo nướng',2,350000)
Insert dbo.Food(name,idcategory,gia)
values (N'Gà nướng mật ong',2,200000)
Insert dbo.Food(name,idcategory,gia)
values (N'Thịt heo nướng mật ong',2,300000)
--tráng miệng
go
use Quanlynhahang
Insert dbo.Food(name,idcategory,gia)
values (N'Bánh kem',3,200000)
Insert dbo.Food(name,idcategory,gia)
values (N'Chè hạt sen',3,150000)
Insert dbo.Food(name,idcategory,gia)
values (N'Nho Mỹ',3,300000)
Insert dbo.Food(name,idcategory,gia)
values (N'Bánh sandwich sữa dừa',3,150000)
Insert dbo.Food(name,idcategory,gia)
values (N'Bánh bò nướng nhân dừa',3,250000)
Insert dbo.Food(name,idcategory,gia)
values (N'Bánh su kem',3,350000)
--đặc sản 
Insert dbo.Food(name,idcategory,gia)
values (N'Cá rô mè kho thủy liễu',4,450000)
Insert dbo.Food(name,idcategory,gia)
values (N'Dê núi Ninh Bình',4,350000)
Insert dbo.Food(name,idcategory,gia)
values (N'Tôm tít rang me',4,600000)
Insert dbo.Food(name,idcategory,gia)
values (N'Thịt trâu gác bếp',4,500000)
Insert dbo.Food(name,idcategory,gia)
values (N'Thịt nai',4,750000)
Insert dbo.Food(name,idcategory,gia)
values (N'Chuột cao lãnh',4,450000)
--nước
Insert dbo.Food(name,idcategory,gia)
values (N'7Up',5,20000)
Insert dbo.Food(name,idcategory,gia)
values (N'Tiger',5,350000)
Insert dbo.Food(name,idcategory,gia)
values (N'Heniken',5,350000)
Insert dbo.Food(name,idcategory,gia)
values (N'Strong Bow',5,350000)
Insert dbo.Food(name,idcategory,gia)
values (N'CoCa',5,200000)
Insert dbo.Food(name,idcategory,gia)
values (N'Cam ép',5,200000)
--Thêm bill
Insert dbo.Bill(NgayVao,NgayRa,idTableFood,status)
values(GETDATE(),null,1,0)
Insert dbo.Bill(NgayVao,NgayRa,idTableFood,status)
values(GETDATE(),null,2,0)
Insert dbo.Bill(NgayVao,NgayRa,idTableFood,status)
values(GETDATE(),null,3,0)
Insert dbo.Bill(NgayVao,NgayRa,idTableFood,status)
values(GETDATE(),GETDATE(),2,1)
Insert dbo.Bill(NgayVao,NgayRa,idTableFood,status)
values(GETDATE(),null,4,0)
--thêm billifo
go

Insert dbo.Billinfo(idbill,idfood,count)
values(1,1,2)
Insert dbo.Billinfo(idbill,idfood,count)
values(1,3,4)
Insert dbo.Billinfo(idbill,idfood,count)
values(1,5,2)
Insert dbo.Billinfo(idbill,idfood,count)
values(2,1,2)
Insert dbo.Billinfo(idbill,idfood,count)
values(2,6,2)
Insert dbo.Billinfo(idbill,idfood,count)
values(3,5,2)

Select * from dbo.Foodcategory
Select * from dbo.Bill
Select * from dbo.Billinfo
Select * from dbo.Food
Select * from dbo.Bill where idTableFood =3 And status =1
Select f.name,bi.count,f.gia,f.gia*bi.count As totalPrice from dbo.Billinfo As bi,dbo.Bill As b,dbo.Food As f 
where bi.idbill = b.ID and bi.idfood = f.ID and b.idTableFood = 2
go

alter proc USP_InsertBill
@idtable int 
as 
begin 
insert dbo.Bill(NgayVao,
                NgayRa,
                idTableFood, 
                status,
                discount)
  values (getdate(),
  null,
  @idtable,
  0,
  0
  )  
  end  
  go
  alter proc USP_InsertBillinfo
  @idbill int , @idfood int,@count int
  as
  begin 
  Declare @isExitSBillInfo int 
  Declare @foodCount int = 1
  select @isExitSBillInfo = ID , @foodCount =b.count 
  from dbo.Billinfo as b 
  where idbill = @idbill and idfood = @idfood
  if(@isExitSBillInfo > 0)
  begin 
  declare @newcount int = @foodCount + @count 
     if(@newcount > 0)
      update dbo.Billinfo set count =@foodCount + @count where idfood = @idfood
     else 
      delete dbo.Billinfo where idbill= @idbill And idfood = @idfood
  end
  else 
  begin
  insert dbo.Billinfo(idbill,idfood,count)
  values (@idbill,@idfood,@count)
  end 
  end
  go
 
  delete dbo.Billinfo
  delete dbo.Food
  delete dbo.Bill
  Create trigger UTG_updateBillInfo
  on dbo.Billinfo for insert, update
  as 
  begin 
  declare @idBill int 
   select @idBill= idbill From Inserted
  declare @idTable int
  select @idTable = idTableFood From dbo.Bill where ID = @idBill And status =0
  update dbo.TableFood set status = N'Có người' where ID = @idTable
  end 
  go
  use Quanlynhahang
  alter trigger UTG_UpdateBill
  on dbo.Bill for insert, update
  as 
  begin 
      declare @idBill int
      select @idBill = ID From Inserted
      declare @idTable int
      select @idTable = idTableFood From dbo.Bill where ID = @idBill 
      declare @count int 
      select @count = COUNT(*) from dbo.Bill where idTableFood = @idTable and status =0
      if(@count=0)
         update dbo.TableFood set status = N'Trống' where ID = @idTable
  end
  go
  use Quanlynhahang
  alter table dbo.Bill
  add discount int 
  
  update dbo.Bill set discount =0 
  select * from dbo.Food
  go
  alter proc USP_SwitchTable
  @idTable1 int,@idTable2 int
  as 
  begin 
  declare  @idFirstBill int
  declare @idSeconrdBill int
  declare  @isFisrtTablEmty int = 1
  declare @isSeconrdTableEmty int = 1
  
      Select @idSeconrdBill = ID from dbo.Bill where idTableFood =@idTable2 And status =0
      Select @idFirstBill = ID  from dbo.Bill where idTableFood =@idTable1 And status =0
      if(@idFirstBill is Null)
      begin
         insert into dbo.Bill(NgayVao,
         NgayRa,
         idTableFood,
         status)
         values(GETDATE(),null,@idTable1,0)
         select @idFirstBill = MAX(ID) from dbo.Bill where idTableFood =@idTable1 And status =0
      end
      select @isFisrtTablEmty = COUNT(*) from dbo.Billinfo where idbill = @idFirstBill
       if(@idSeconrdBill is Null)
      begin
         insert into dbo.Bill(NgayVao,
         NgayRa,
         idTableFood,
         status)
         values(GETDATE(),null,@idTable2,0)
         select @idSeconrdBill = MAX(ID) from dbo.Bill where idTableFood =@idTable2 And status =0
         
      end
      select @isSeconrdTableEmty = COUNT(*) from dbo.Billinfo where  idbill = @idSeconrdBill
      
      
      select ID into IDBillInfoTable from dbo.Billinfo where idbill = @idSeconrdBill
      update dbo.Billinfo set idbill = @idSeconrdBill where idbill = @idFirstBill
      update dbo.Billinfo set idbill = @idFirstBill where ID in (select * from IDBillInfoTable )
      drop table IDBillInfoTable
      if(@isFisrtTablEmty=0)
      update dbo.TableFood set status =N'Trống' where ID = @idTable2
      if(@isSeconrdTableEmty=0)
      update dbo.TableFood set status =N'Trống' where ID = @idTable1
  end 
  go
  alter Proc USP_GetListBillByDate
  @CheckIn date, @CheckOut date
  select t.name as [Tên bàn ],b.totalPrice as [Tổng tiền ],NgayVao as [Ngày Vào],NgayRa as [Ngày Ra ],discount as [Giảm giá]
  from dbo.Bill as b,dbo.TableFood as t
  where NgayVao >= @CheckIn and NgayRa <= @CheckOut and b.status = 1
  and t.ID = b.idTableFood 
  end 
  go
create proc USP_updateAccount
@userName nvarchar(100),@disPlayName nvarchar(100),@password nvarchar(100),@newPassword nvarchar(100)
as begin 
 declare @isRightPass int 
 select @isRightPass = Count (*) from dbo.Account  where userName = @userName and PassWord = @password
 if(@isRightPass=1)
 begin
    if(@newPassword = null or @newPassword = ' ')
    begin 
        update dbo.Account set DisplayName = @disPlayName where userName = @userName
    end 
    else 
    update dbo.Account set DisplayName = @disPlayName,PassWord = @newPassword where userName = @userName
 end
end 
go 
create trigger UTG_DelteBillInfo
on dbo.BillInfo for delete 
as 
begin 
    declare @idBillInFo int 
    declare @idbill int 
    select  @idBillInFo = ID,@idbill = deleted.idbill from deleted 
    declare @idTable int 
    select @idTable = idTableFood  from dbo.Bill where ID= @idbill
    declare @count int = 0
    select @count = COUNT(*) from dbo.Billinfo as bi,dbo.Bill as b where b.ID =bi.idbill and bi.ID = @idbill and b.status =0
    if(@count = 0)
    begin 
    update dbo.TableFood set status = N'Trống ' where ID = @idTable
    end 
end
select * From dbo.Bill