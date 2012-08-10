CREATE TABLE [dbo].[tblMovies]
(
	MovieId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() RowGuidCol PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL,
	Duration INT NOT NULL,
	Director NVARCHAR(255) NOT NULL,
	Producer NVARCHAR(255) NOT NULL,
	[Type] NVARCHAR(MAX) NOT NULL,
	AgeRest NVARCHAR(25) NOT NULL,	--Is a NVARCHAR for situations like 13PG, etc.
	ExpectedAudience NVARCHAR(100) NOT NULL CHECK (ExpectedAudience = 'Mainstream' OR ExpectedAudience = 'Average' OR ExpectedAudience = 'Below-Average' OR ExpectedAudience = 'Specialist' OR ExpectedAudience = 'Non-Mainstream'),
	BBFC_Rate NVARCHAR (8) NULL,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
	NumViewers INT NULL,
	IsArchived BIT NOT NULL DEFAULT 0
)
