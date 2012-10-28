CREATE PROCEDURE [dbo].[ScheduleMovieScreening]
	@MovieId UNIQUEIDENTIFIER,
	@ScreenId UNIQUEIDENTIFIER,
	@StartScreen DATETIME, 
	@EndScreen DATETIME
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
		IF NOT EXISTS (SELECT * 
					   FROM tlnkScreenMovie 
					   WHERE (MovieId = @MovieId AND 
					          ScreenId = @ScreenId AND 
							  ((StartScreen BETWEEN @StartScreen AND @EndScreen) OR
							   (EndScreen BETWEEN @StartScreen AND @EndScreen))))
		BEGIN
			INSERT INTO tlnkScreenMovie (MovieId, ScreenId, StartScreen, EndScreen)
			VALUES (@MovieId, @ScreenId, @StartScreen, @EndScreen)
		END
		ELSE
		BEGIN
			RAISERROR('Cannot schedule over an existing screening.', 16, 16)
		END
	END