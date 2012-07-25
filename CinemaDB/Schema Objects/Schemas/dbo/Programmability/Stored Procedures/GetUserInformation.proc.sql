CREATE PROCEDURE [dbo].[GetUserInformation]
	@username NVARCHAR(255),
	@password NVARCHAR(255)
AS
	--If the username exists in the database continue, else throw error.
	IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username)
	BEGIN
		--If username and password combination exists in the database continue, else throw error.
		IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username AND LoginPassword = @password)
		BEGIN
			--Checks that the user is active.
			IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username AND LoginPassword = @password AND bActive = 1)
			BEGIN
				--Get the user details where the username and password are correct.
				SELECT *
				FROM tblUsers
				WHERE LoginUsername = @username AND LoginPassword = @password
			END
			ELSE
			BEGIN
				SELECT 'That acount has been de-activated!'
			END
		END
		ELSE
		BEGIN
			SELECT 'Entered username and password do not match!'
		END
	END
	ELSE
	BEGIN
		SELECT 'That username is invalid!'
	END