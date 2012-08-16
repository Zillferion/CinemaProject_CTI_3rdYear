CREATE PROCEDURE [dbo].[UpdateMovie]
	@movieGuid UNIQUEIDENTIFIER, 
	@name NVARCHAR(255),
	@type NVARCHAR(255),
	@directors NVARCHAR(255),
	@producers NVARCHAR(255),
	@duration INT,
	@expectedAudience NVARCHAR(255),
	@rating NVARCHAR(255),
	@description NVARCHAR(MAX),
	@archived BIT,
	@year INT
AS
	UPDATE tblMovies
	SET Name = @name,
		[Type] = @type,
		Director = @directors,
		Producer = @producers,
		Duration = @duration,
		ExpectedAudience = @expectedAudience,
		BBFC_Rate = @rating,
		[Description] = @description,
		IsArchived = @archived,
		YearProduced = @year
	WHERE MovieId = @movieGuid