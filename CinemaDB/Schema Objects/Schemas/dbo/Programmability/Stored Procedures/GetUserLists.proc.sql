CREATE PROCEDURE [dbo].[GetUserLists]
	@adminLevel INT
AS
	IF @adminLevel = 1
	BEGIN
		SELECT [UserGuid]
			  ,[Created]
			  ,[UserName]
			  ,[UserSurname]
			  ,[UserDoB]
			  ,[ContactNumber]
			  ,[LoginUsername]
			  ,[LoginPassword]
			  ,[bActive]
			  ,[AdminLevel]
		FROM tblUsers
	END
	ELSE IF @adminLevel = 2
	BEGIN
		SELECT [UserGuid]
			  ,[Created]
			  ,[UserName]
			  ,[UserSurname]
			  ,[UserDoB]
			  ,[ContactNumber]
			  ,[LoginUsername]
			  ,[LoginPassword]
			  ,[bActive]
			  ,[AdminLevel]
		FROM tblUsers
		WHERE AdminLevel <> 1 AND [bActive] = 1
	END
	ELSE
	BEGIN
		RAISERROR ('You do not have permission to see this page.', 1, 1)
	END