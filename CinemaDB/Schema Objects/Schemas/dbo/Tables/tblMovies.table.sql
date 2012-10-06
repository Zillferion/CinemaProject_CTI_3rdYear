CREATE TABLE [dbo].[tblMovies]
(
	MovieId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() RowGuidCol PRIMARY KEY,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
	Name NVARCHAR(255) NOT NULL,
	Duration INT NOT NULL,
	YearProduced INT NOT NULL,
	Director NVARCHAR(255) NOT NULL,
	Producer NVARCHAR(255) NOT NULL,
	[Type] NVARCHAR(MAX) NOT NULL,
	[Description] TEXT NOT NULL DEFAULT '',
	ExpectedAudience NVARCHAR(100) NOT NULL CHECK (ExpectedAudience = 'Mainstream' OR ExpectedAudience = 'Average' OR ExpectedAudience = 'Below-Average' OR ExpectedAudience = 'Specialist' OR ExpectedAudience = 'Non-Mainstream'),
	BBFC_Rate NVARCHAR (8) NOT NULL CHECK (BBFC_Rate='U' OR BBFC_Rate = 'PG' OR BBFC_Rate = '12' OR BBFC_Rate = '12A' OR BBFC_Rate = '15' OR BBFC_Rate = '18' OR BBFC_Rate = 'R18'),
	IsArchived BIT NOT NULL DEFAULT 0,
	DistributorId UNIQUEIDENTIFIER NULL REFERENCES [tblDistributors](DistributorGuid)
)
