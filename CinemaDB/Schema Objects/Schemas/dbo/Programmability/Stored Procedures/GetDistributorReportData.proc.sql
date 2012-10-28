CREATE PROCEDURE [dbo].[getDistributorReportData]
@name NVARCHAR(255)
AS
	SELECT
	"Distributor" = D.Name
	, "Movie Name" = M.Name
	, "Booked Seats" = SM.BookedNum
	, "Is Archived" = CASE
	WHEN M.IsArchived = 1 THEN 'Archived'
	ELSE 'Active'
	  END
	FROM tblDistributors AS D
	JOIN tblMovies AS M ON D.DistributorGuid = M.DistributorId
	JOIN tlnkScreenMovie AS SM ON M.MovieId = SM.MovieId
	JOIN tblScreen AS S ON SM.ScreenId = S.ScreenId
	WHERE
	D.Name = @name
	AND D.ReceiveMontly = 1
	AND SM.Created BETWEEN DATEADD(M, -1, GETDATE()) AND GETDATE()
	UNION
	SELECT
	"Distributor" = D.Name
	, "Movie Name" = M.Name
	, "Booked Seats" = SM.BookedNum
	, "Is Archived" = CASE
	WHEN M.IsArchived = 1 THEN 'Archived'
	ELSE 'Active'
	  END
	FROM tblDistributors AS D
	JOIN tblMovies AS M ON D.DistributorGuid = M.DistributorId
	JOIN tlnkScreenMovie AS SM ON M.MovieId = SM.MovieId
	JOIN tblScreen AS S ON SM.ScreenId = S.ScreenId
	WHERE
	D.Name = @name
	AND D.ReceiveQuaterly = 1
	AND SM.Created BETWEEN DATEADD(M, -4, GETDATE()) AND GETDATE()
	UNION
	SELECT
	"Distributor" = D.Name
	, "Movie Name" = M.Name
	, "Booked Seats" = SM.BookedNum
	, "Is Archived" = CASE
	WHEN M.IsArchived = 1 THEN 'Archived'
	ELSE 'Active'
	  END
	FROM tblDistributors AS D
	JOIN tblMovies AS M ON D.DistributorGuid = M.DistributorId
	JOIN tlnkScreenMovie AS SM ON M.MovieId = SM.MovieId
	JOIN tblScreen AS S ON SM.ScreenId = S.ScreenId
	WHERE
	D.Name = @name
	AND D.ReceiveHalfYearly = 1
	AND SM.Created BETWEEN DATEADD(M, -6, GETDATE()) AND GETDATE()
	UNION
	SELECT
	"Distributor" = D.Name
	, "Movie Name" = M.Name
	, "Booked Seats" = SM.BookedNum
	, "Is Archived" = CASE
	WHEN M.IsArchived = 1 THEN 'Archived'
	ELSE 'Active'
	  END
	FROM tblDistributors AS D
	JOIN tblMovies AS M ON D.DistributorGuid = M.DistributorId
	JOIN tlnkScreenMovie AS SM ON M.MovieId = SM.MovieId
	JOIN tblScreen AS S ON SM.ScreenId = S.ScreenId
	WHERE
	D.Name = @name
	AND D.ReceiveYearly = 1
	AND SM.Created BETWEEN DATEADD(YEAR, -1, GETDATE()) AND GETDATE()
