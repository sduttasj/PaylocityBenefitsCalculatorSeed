CREATE DATABASE sample;

CREATE TABLE [sample].[dbo].[Employee] (
    Id int PRIMARY KEY,
    FirstName varchar(50) NULL,
    LastName varchar(50) NULL,
    Salary decimal(18,2) NOT NULL,
    DateOfBirth datetime NOT NULL,
 );
CREATE TABLE [sample].[dbo].[Dependent]( 
Id int PRIMARY KEY,
FirstName varchar(50) NULL,
LastName varchar(50) NULL,
EmployeeId int NOT NULL FOREIGN KEY REFERENCES Employee(Id),
DateOfBirth datetime NOT NULL, 
Relationship varchar(50) NOT NULL);
