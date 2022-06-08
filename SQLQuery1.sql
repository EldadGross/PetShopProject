use PetShop
go

create table Category(
categoryId int identity(1,1) primary key,
[Name] nvarchar (200) not null
)
go

create table Animal(
AnimalId INT identity(1,1) primary key,
[Name] nvarchar(200) not null,
birthDate date null,
[descraption]nvarchar(max) null,
PictureUrl nvarchar(max),
categoryId int foreign key references Category(CategoryId)
)
go

create table comment(
CommentId int identity(1,1) primary key,
CommentText nvarchar(max) not null,
AnimalId int foreign key references Animal(AnimalId)
)
go
