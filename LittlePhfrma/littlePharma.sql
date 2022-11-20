USE [master]
GO

/****** Object:  Database [LittlePharma]    Script Date: 18.11.2022 13:39:26 ******/
CREATE DATABASE [LittlePharma]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LittlePharma', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LittlePharma.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LittlePharma_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LittlePharma_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO


-------------------------------------------------------------------------------------
USE LittlePharma
go

CREATE TABLE Product (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED, Name NVARCHAR(255))

CREATE TABLE Pharmacy (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED, Name NVARCHAR(255), Address NVARCHAR(255), Phone NVARCHAR(100))

CREATE TABLE Stock (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
PharmacyId INT NOT NULL FOREIGN KEY REFERENCES dbo.Pharmacy(Id) ON DELETE CASCADE,
Name NVARCHAR(255))

CREATE TABLE Supplying (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
ProductId INT NOT NULL FOREIGN KEY REFERENCES dbo.Product(Id) ON DELETE CASCADE,
StockId INT NOT NULL FOREIGN KEY REFERENCES dbo.Stock(Id) ON DELETE CASCADE,
Amount INT NOT null)
go

CREATE OR ALTER PROCEDURE AddProduct (@name nvarchar(255))
AS
BEGIN
INSERT INTO dbo.Product(Name)
VALUES (@name)
SELECT SCOPE_IDENTITY()
END
GO

CREATE OR ALTER PROCEDURE DelProduct (@Id INT)
AS
BEGIN
DELETE FROM dbo.Product WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE AddPharmacy (@name nvarchar(255), @address NVARCHAR(255), @phone NVARCHAR(100))
AS
BEGIN
INSERT INTO dbo.Pharmacy(Name, Address, Phone)
VALUES (@name, @address, @phone)
SELECT SCOPE_IDENTITY()
END
GO

CREATE OR ALTER PROCEDURE DelPharmacy (@id INT)
AS
BEGIN
DELETE FROM dbo.Pharmacy WHERE Id = @id
END
GO

CREATE OR ALTER PROCEDURE AddStock (@pharmacyId INT, @name NVARCHAR(255))
AS
BEGIN
INSERT INTO dbo.Stock(PharmacyId, Name)
VALUES (@pharmacyId, @name)
SELECT SCOPE_IDENTITY()
END
GO

CREATE OR ALTER PROCEDURE DelStock (@id INT)
AS
BEGIN
DELETE FROM dbo.Stock WHERE Id = @id    
END
GO

CREATE OR ALTER PROCEDURE AddSupplying (@productId INT, @stockId INT, @amount INT)
AS
BEGIN
INSERT INTO dbo.Supplying(ProductId, StockId, Amount)
VALUES (@productId, @stockId, @amount)
SELECT SCOPE_IDENTITY()
END
GO

CREATE OR ALTER PROCEDURE DelSupplying (@id INT)
AS
BEGIN
DELETE FROM dbo.Supplying WHERE Id = @id
END
go

CREATE OR ALTER PROCEDURE GetProducts (@pharmacyId INT)
AS
BEGIN
SELECT pr.Name, SUM(sp.Amount) AS Amount  
FROM dbo.Pharmacy p
LEFT JOIN dbo.Stock s ON p.Id = s.PharmacyId
LEFT JOIN dbo.Supplying sp ON s.Id = sp.StockId
LEFT JOIN dbo.Product pr ON sp.ProductId = pr.Id
WHERE p.Id = @pharmacyId
GROUP BY pr.Name
END
GO

CREATE OR ALTER PROCEDURE GetProduct
AS
BEGIN
SELECT Id ,
       Name FROM dbo.Product
end
GO

CREATE OR ALTER PROCEDURE GetPharmacy
AS
BEGIN
SELECT Id,
       Name FROM dbo.Pharmacy
END
go

CREATE OR ALTER PROCEDURE GetStock
AS
BEGIN
SELECT Id,
       Name FROM dbo.Stock
END
go

CREATE OR ALTER PROCEDURE GetSupplying
AS
BEGIN
SELECT Id,
      Amount FROM dbo.Supplying
END
go


