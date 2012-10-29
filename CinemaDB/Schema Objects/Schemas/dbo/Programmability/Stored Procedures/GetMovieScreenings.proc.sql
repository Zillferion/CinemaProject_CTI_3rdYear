CREATE PROCEDURE [dbo].[GetMovieScreenings]
	@StartScreen DATETIME, 
	@EndScreen DATETIME
AS
	IF @StartScreen = NULL OR
	   @EndScreen = NULL
	BEGIN
		RAISERROR('Invalid parameter(s).', 16, 16)
	END
	ELSE
	BEGIN
		SELECT tblMovies.Name, tblScreen.ScreenId, tblScreen.ScreenNum, tblScreen.Seats, tlnkScreenMovie.StartScreen, tlnkScreenMovie.EndScreen 
		FROM tblMovies, tblScreen, tlnkScreenMovie
		WHERE tlnkScreenMovie.MovieId = tblMovies.MovieId AND
			  tlnkScreenMovie.ScreenId = tblScreen.ScreenId AND
			  tlnkScreenMovie.StartScreen >= @StartScreen AND
			  tlnkScreenMovie.EndScreen <= @EndScreen
		ORDER BY tlnkScreenMovie.StartScreen, tblScreen.ScreenNum, tblMovies.Name
	END
