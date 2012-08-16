CREATE PROCEDURE [dbo].[CreateUser]
	@name NVARCHAR(MAX),
	@surname NVARCHAR(MAX),
	@adminLevel INT,
	@contactNumber NVARCHAR(25),
	@password NVARCHAR(MAX),
	@loginName NVARCHAR(MAX),
	@dateOfBirth DATETIME,
	@active BIT
AS
	INSERT INTO tblUsers (UserName,UserSurname,AdminLevel,ContactNumber,LoginPassword,LoginUsername,UserDoB,bActive)
	VALUES (@name,@surname,@adminLevel,@contactNumber,@password,@loginName,@dateOfBirth,@active)
