create database ScroogeDb;

use ScroogeDb;

create table Users (
    Id int primary key identity,
    Name nvarchar(50) not null,
    Surname nvarchar(50) not null,
    BirthDate datetime2
)