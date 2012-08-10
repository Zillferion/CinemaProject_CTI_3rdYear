CREATE PROCEDURE [dbo].[GetUserLists]
	@adminLevel INT
AS
	IF @adminLevel = 2
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
		ORDER BY [UserName] + ' ' + [UserSurname]
	END
	ELSE IF @adminLevel = 1
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
		WHERE AdminLevel <> 2 AND [bActive] = 1
		ORDER BY [UserName] + ' ' + [UserSurname]
	END
	ELSE
	BEGIN
		RAISERROR ('You do not have permission to see this page.', 1, 1)
	END