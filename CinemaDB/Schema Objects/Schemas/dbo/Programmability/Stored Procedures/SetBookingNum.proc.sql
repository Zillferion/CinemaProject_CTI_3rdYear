CREATE PROCEDURE [dbo].[SetBookingNum]
	@MovieId UNIQUEIDENTIFIER,
	@ScreenId UNIQUEIDENTIFIER,
	@StartScreen DATETIME, 
	@EndScreen DATETIME,
	@BookedNum INT
AS
	IF @MovieId = NULL OR 
	   @ScreenId = NULL OR
	   @StartScreen = NULL OR
	   @EndScreen = NULL
	BEGIN
		RAISERROR('Invalid parameter(s).', 16, 16)
	END
	ELSE
	BEGIN
		UPDATE tlnkScreenMovie 
		SET BookedNum = @BookedNum 
		WHERE MovieId = @MovieId AND
		      ScreenId = @ScreenId AND 
			  StartScreen = @StartScreen AND 
			  EndScreen = @EndScreen
	END
RETURN 0







