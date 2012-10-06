CREATE PROCEDURE [dbo].[GetMovieList]
AS
	SELECT
		MovieId
		, Created
		, Name
		, Duration
		, YearProduced
		, Director
		, Producer
		, [Type]
		, [Description]
		, ExpectedAudience
		, BBFC_Rate
		, IsArchived
		, ISNULL(CAST(DistributorId AS NVARCHAR(255)), '') AS DistributorId
	FROM tblMovies
	ORDER BY Name