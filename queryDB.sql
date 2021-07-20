create database [PetServices];

use [PetServices];

create table account(
	username nvarchar(30) primary key,
	[password] nvarchar(50),
	user_role int,
	name nvarchar(50),
	phone int,
	email nvarchar(50),
	gender bit,
	secret_question nvarchar(100),
	secret_answer nvarchar(100)
);

insert into account values ('admin', 'Admin1234', 0, 'Mr.Artemis', 01234632543, 'example1234@gmail.com', 0, 'What is your dogs name?', 'Misa');

select * from account;

delete from account where username='mra';
select * from account;

create table [services] (
	service_id int primary key,
	[service_name] nvarchar(100),
	price float
)

insert into [services] values(1, 'Ear Cleaning', 15);
insert into [services] values(2, 'Clipping Fur', 15);
insert into [services] values(3, 'Brushing', 15);
insert into [services] values(4, 'Nail Trim', 15);
insert into [services] values(5, 'Grooming Program', 15);

select * from [services];

create table appointment (
	id int primary key identity(1,1),
	username nvarchar(30) foreign key references account(username),
	dog_name nvarchar(50),
	dog_kind nvarchar(100),
	dateAppointed date,
	[service_id] int foreign key references [services](service_id),
	[message] nvarchar(1000)
);

select * from appointment;