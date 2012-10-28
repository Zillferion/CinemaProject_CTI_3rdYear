CREATE PROCEDURE [dbo].[GetScreenList]
AS
	SELECT ScreenId, ScreenNum, Seats 
	FROM tblScreen
	ORDER BY Seats DESC
RETURN 0