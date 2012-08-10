CREATE PROCEDURE [dbo].[UpdateUser]
	@userGuid UNIQUEIDENTIFIER,
	@name NVARCHAR(MAX),
	@surname NVARCHAR(MAX),
	@adminLevel INT,
	@contactNumber NVARCHAR(25),
	@password NVARCHAR(MAX),
	@loginName NVARCHAR(MAX),
	@dateOfBirth DATETIME,
	@active BIT
AS
	UPDATE tblUsers
	SET UserName = @name,
		UserSurname = @surname,
		AdminLevel = @adminLevel,
		ContactNumber = @contactNumber,
		LoginPassword = @password,
		LoginUsername = @loginName,
		UserDoB = @dateOfBirth,
		bActive = @active
	WHERE UserGuid = @userGuid