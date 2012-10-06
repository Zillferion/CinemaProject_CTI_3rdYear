CREATE PROCEDURE [dbo].[LinkMovieDistributor]
	@movieName NVARCHAR(255), 
	@distributorGuid UNIQUEIDENTIFIER
AS
	IF EXISTS(SELECT * FROM tblMovies WHERE Name = @movieName AND IsArchived = 0)
	BEGIN
		IF EXISTS (SELECT * FROM tblMovies WHERE Name = @movieName AND IsArchived = 0 AND DistributorId IS NULL)
		BEGIN
			UPDATE tblMovies
			SET DistributorId = @distributorGuid
			WHERE Name = @movieName
		END
		ELSE
		BEGIN
			RAISERROR ('Movie already linked to another distributor.', 16, 16)
		END
	END
	ELSE
	BEGIN
		RAISERROR('Movie is no longer active.', 16, 16)
	END