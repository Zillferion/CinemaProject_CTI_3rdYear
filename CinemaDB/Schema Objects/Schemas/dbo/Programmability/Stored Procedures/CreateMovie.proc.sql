CREATE PROCEDURE [dbo].[CreateMovie]
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
	INSERT INTO tblMovies (Name, [Type], Director, Producer, Duration, ExpectedAudience, BBFC_Rate, [Description], IsArchived, YearProduced)
	VALUES(@name, @type,@directors,@producers,@duration,@expectedAudience,@rating,@description,@archived,@year)