/*
Deployment script for CinemaDB
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "CinemaDB"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[tblDistributors]...';


GO
CREATE TABLE [dbo].[tblDistributors] (
    [DistributorGuid]   UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Created]           DATETIME         NOT NULL,
    [Name]              NVARCHAR (255)   NOT NULL,
    [IsActive]          BIT              NOT NULL,
    [Email]             NVARCHAR (255)   NOT NULL,
    [ContactNumber]     NVARCHAR (50)    NOT NULL,
    [PhysicalAddress]   NVARCHAR (255)   NULL,
    [ReceiveMontly]     BIT              NOT NULL,
    [ReceiveQuaterly]   BIT              NOT NULL,
    [ReceiveHalfYearly] BIT              NOT NULL,
    [ReceiveYearly]     BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([DistributorGuid] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[tblMovies]...';


GO
CREATE TABLE [dbo].[tblMovies] (
    [MovieId]          UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Created]          DATETIME         NOT NULL,
    [Name]             NVARCHAR (255)   NOT NULL,
    [Duration]         INT              NOT NULL,
    [YearProduced]     INT              NOT NULL,
    [Director]         NVARCHAR (255)   NOT NULL,
    [Producer]         NVARCHAR (255)   NOT NULL,
    [Type]             NVARCHAR (MAX)   NOT NULL,
    [Description]      TEXT             NOT NULL,
    [ExpectedAudience] NVARCHAR (100)   NOT NULL,
    [BBFC_Rate]        NVARCHAR (8)     NOT NULL,
    [IsArchived]       BIT              NOT NULL,
    [DistributorId]    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([MovieId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[tblScreen]...';


GO
CREATE TABLE [dbo].[tblScreen] (
    [ScreenId]  UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [ScreenNum] INT              NOT NULL,
    [Seats]     INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([ScreenId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[tblUsers]...';


GO
CREATE TABLE [dbo].[tblUsers] (
    [UserGuid]      UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Created]       DATETIME         NOT NULL,
    [UserName]      NVARCHAR (255)   NOT NULL,
    [UserSurname]   NVARCHAR (255)   NOT NULL,
    [UserDoB]       DATE             NOT NULL,
    [ContactNumber] NVARCHAR (20)    NULL,
    [LoginUsername] NVARCHAR (255)   NOT NULL,
    [LoginPassword] NVARCHAR (255)   NOT NULL,
    [bActive]       BIT              NOT NULL,
    [AdminLevel]    INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([UserGuid] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[tlnkMovieDistributor]...';


GO
CREATE TABLE [dbo].[tlnkMovieDistributor] (
    [linkId]        UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [MovieId]       UNIQUEIDENTIFIER NOT NULL,
    [DistributorId] UNIQUEIDENTIFIER NOT NULL,
    [Created]       DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([linkId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[tlnkScreenMovie]...';


GO
CREATE TABLE [dbo].[tlnkScreenMovie] (
    [linkId]      UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [Created]     DATETIME         NOT NULL,
    [MovieId]     UNIQUEIDENTIFIER NOT NULL,
    [ScreenId]    UNIQUEIDENTIFIER NOT NULL,
    [StartScreen] DATETIME         NOT NULL,
    [EndScreen]   DATETIME         NOT NULL,
    [BookedNum]   INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([linkId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating On column: DistributorGuid...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT NEWSEQUENTIALID() FOR [DistributorGuid];


GO
PRINT N'Creating On column: Created...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating On column: IsActive...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating On column: ReceiveMontly...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT 0 FOR [ReceiveMontly];


GO
PRINT N'Creating On column: ReceiveQuaterly...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT 0 FOR [ReceiveQuaterly];


GO
PRINT N'Creating On column: ReceiveHalfYearly...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT 0 FOR [ReceiveHalfYearly];


GO
PRINT N'Creating On column: ReceiveYearly...';


GO
ALTER TABLE [dbo].[tblDistributors]
    ADD DEFAULT 0 FOR [ReceiveYearly];


GO
PRINT N'Creating On column: MovieId...';


GO
ALTER TABLE [dbo].[tblMovies]
    ADD DEFAULT NEWSEQUENTIALID() FOR [MovieId];


GO
PRINT N'Creating On column: Created...';


GO
ALTER TABLE [dbo].[tblMovies]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating On column: Description...';


GO
ALTER TABLE [dbo].[tblMovies]
    ADD DEFAULT '' FOR [Description];


GO
PRINT N'Creating On column: IsArchived...';


GO
ALTER TABLE [dbo].[tblMovies]
    ADD DEFAULT 0 FOR [IsArchived];


GO
PRINT N'Creating On column: ScreenId...';


GO
ALTER TABLE [dbo].[tblScreen]
    ADD DEFAULT NEWSEQUENTIALID() FOR [ScreenId];


GO
PRINT N'Creating On column: UserGuid...';


GO
ALTER TABLE [dbo].[tblUsers]
    ADD DEFAULT NEWSEQUENTIALID() FOR [UserGuid];


GO
PRINT N'Creating On column: Created...';


GO
ALTER TABLE [dbo].[tblUsers]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating On column: bActive...';


GO
ALTER TABLE [dbo].[tblUsers]
    ADD DEFAULT 1 FOR [bActive];


GO
PRINT N'Creating On column: AdminLevel...';


GO
ALTER TABLE [dbo].[tblUsers]
    ADD DEFAULT 0 FOR [AdminLevel];


GO
PRINT N'Creating On column: linkId...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor]
    ADD DEFAULT NEWSEQUENTIALID() FOR [linkId];


GO
PRINT N'Creating On column: Created...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating On column: linkId...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie]
    ADD DEFAULT NEWSEQUENTIALID() FOR [linkId];


GO
PRINT N'Creating On column: Created...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating On column: BookedNum...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie]
    ADD DEFAULT 0 FOR [BookedNum];


GO
PRINT N'Creating On column: DistributorId...';


GO
ALTER TABLE [dbo].[tblMovies] WITH NOCHECK
    ADD FOREIGN KEY ([DistributorId]) REFERENCES [dbo].[tblDistributors] ([DistributorGuid]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: MovieId...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor] WITH NOCHECK
    ADD FOREIGN KEY ([MovieId]) REFERENCES [dbo].[tblMovies] ([MovieId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: DistributorId...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor] WITH NOCHECK
    ADD FOREIGN KEY ([DistributorId]) REFERENCES [dbo].[tblDistributors] ([DistributorGuid]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: MovieId...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie] WITH NOCHECK
    ADD FOREIGN KEY ([MovieId]) REFERENCES [dbo].[tblMovies] ([MovieId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: ScreenId...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie] WITH NOCHECK
    ADD FOREIGN KEY ([ScreenId]) REFERENCES [dbo].[tblScreen] ([ScreenId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: ExpectedAudience...';


GO
ALTER TABLE [dbo].[tblMovies] WITH NOCHECK
    ADD CHECK (ExpectedAudience = 'Mainstream' OR ExpectedAudience = 'Average' OR ExpectedAudience = 'Below-Average' OR ExpectedAudience = 'Specialist' OR ExpectedAudience = 'Non-Mainstream');


GO
PRINT N'Creating On column: BBFC_Rate...';


GO
ALTER TABLE [dbo].[tblMovies] WITH NOCHECK
    ADD CHECK (BBFC_Rate='U' OR BBFC_Rate = 'PG' OR BBFC_Rate = '12' OR BBFC_Rate = '12A' OR BBFC_Rate = '15' OR BBFC_Rate = '18' OR BBFC_Rate = 'R18');


GO
PRINT N'Creating On column: AdminLevel...';


GO
ALTER TABLE [dbo].[tblUsers] WITH NOCHECK
    ADD CHECK (AdminLevel = 0 OR AdminLevel = 1 OR AdminLevel = 2 OR AdminLevel = 3);


GO
PRINT N'Creating [dbo].[ArchiveNewMovie]...';


GO
CREATE PROCEDURE [dbo].[ArchiveNewMovie]
	@param1 int = 0, 
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[CreateMovie]...';


GO
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
GO
PRINT N'Creating [dbo].[CreateUpdateDistributor]...';


GO
CREATE PROCEDURE [dbo].[CreateUpdateDistributor]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@active BIT,
	@email NVARCHAR(255),
	@contactNumber NVARCHAR(50),
	@address NVARCHAR(255),
	@monthly BIT,
	@quaterly BIT,
	@halfYearly BIT,
	@yearly BIT

AS
	DECLARE @canContinue BIT = 1
	--Checks that the name is not already in use.
	IF EXISTS (SELECT * FROM tblDistributors WHERE Name = @name AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Name not available.', 16, 16)
		SET @canContinue = 0
	END
	--Checks that the contact number is not already in use.
	IF EXISTS (SELECT * FROM tblDistributors WHERE ContactNumber = @contactNumber AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Contact number not available.', 16, 16)
		SET @canContinue = 0
	END
	--Checks that the email address does not already exist.
	IF EXISTS (SELECT * FROM tblDistributors WHERE Email = @email AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Email Address not available.', 16, 16)
		SET @canContinue = 0
	END

	IF @canContinue = 1
	BEGIN
		IF EXISTS (SELECT * FROM tblDistributors WHERE DistributorGuid = @id)
	BEGIN
		UPDATE tblDistributors
		SET Name = @name,
			IsActive = @active,
			Email = @email,
			ContactNumber = @contactNumber,
			PhysicalAddress = @address,
			ReceiveMontly = @monthly,
			ReceiveQuaterly = @quaterly,
			ReceiveHalfYearly = @halfYearly,
			ReceiveYearly = @yearly
		WHERE DistributorGuid = @id
	END
	ELSE
	BEGIN
		INSERT INTO tblDistributors (DistributorGuid, Name, IsActive, Email, ContactNumber, PhysicalAddress, ReceiveMontly, ReceiveQuaterly, ReceiveHalfYearly, ReceiveYearly)
		VALUES (@id, @name, @active, @email, @contactNumber, @address, @monthly, @quaterly, @halfYearly, @yearly)
	END
	END
GO
PRINT N'Creating [dbo].[CreateUser]...';


GO
CREATE PROCEDURE [dbo].[CreateUser]
	@name NVARCHAR(MAX),
	@surname NVARCHAR(MAX),
	@adminLevel INT,
	@contactNumber NVARCHAR(25),
	@password NVARCHAR(MAX),
	@loginName NVARCHAR(MAX),
	@dateOfBirth DATETIME,
	@active BIT
AS
	INSERT INTO tblUsers (UserName,UserSurname,AdminLevel,ContactNumber,LoginPassword,LoginUsername,UserDoB,bActive)
	VALUES (@name,@surname,@adminLevel,@contactNumber,@password,@loginName,@dateOfBirth,@active)
GO
PRINT N'Creating [dbo].[GetDistributorList]...';


GO
CREATE PROCEDURE [dbo].[GetDistributorList]
AS
	SELECT *
	FROM tblDistributors
	ORDER BY Name
GO
PRINT N'Creating [dbo].[getDistributorReportData]...';


GO
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
GO
PRINT N'Creating [dbo].[GetMovieList]...';


GO
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
GO
PRINT N'Creating [dbo].[GetMovieScreenings]...';


GO
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
GO
PRINT N'Creating [dbo].[GetScreenList]...';


GO
CREATE PROCEDURE [dbo].[GetScreenList]
AS
	SELECT ScreenId, ScreenNum, Seats 
	FROM tblScreen
	ORDER BY Seats DESC
RETURN 0
GO
PRINT N'Creating [dbo].[GetUserInformation]...';


GO
CREATE PROCEDURE [dbo].[GetUserInformation]
	@username NVARCHAR(255),
	@password NVARCHAR(255)
AS
	--If the username exists in the database continue, else throw error.
	IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username)
	BEGIN
		--If username and password combination exists in the database continue, else throw error.
		IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username AND LoginPassword = @password)
		BEGIN
			--Checks that the user is active.
			IF EXISTS (SELECT * FROM tblUsers WHERE LoginUsername = @username AND LoginPassword = @password AND bActive = 1)
			BEGIN
				--Get the user details where the username and password are correct.
				SELECT *
				FROM tblUsers
				WHERE LoginUsername = @username AND LoginPassword = @password
			END
			ELSE
			BEGIN
				SELECT 'That acount has been de-activated!'
			END
		END
		ELSE
		BEGIN
			SELECT 'Entered username and password do not match!'
		END
	END
	ELSE
	BEGIN
		SELECT 'That username is invalid!'
	END
GO
PRINT N'Creating [dbo].[GetUserList]...';


GO
CREATE PROCEDURE [dbo].[GetUserList]
	@adminLevel INT
AS
	IF @adminLevel = 2
	BEGIN
		SELECT [UserGuid]
			  ,[Created]
			  ,[UserName]
			  ,[UserSurname]
			  ,[UserDoB]
			  ,[ContactNumber]
			  ,[LoginUsername]
			  ,[LoginPassword]
			  ,[bActive]
			  ,[AdminLevel]
		FROM tblUsers
		ORDER BY [UserName] + ' ' + [UserSurname]
	END
	ELSE IF @adminLevel = 1
	BEGIN
		SELECT [UserGuid]
			  ,[Created]
			  ,[UserName]
			  ,[UserSurname]
			  ,[UserDoB]
			  ,[ContactNumber]
			  ,[LoginUsername]
			  ,[LoginPassword]
			  ,[bActive]
			  ,[AdminLevel]
		FROM tblUsers
		WHERE AdminLevel <> 2 AND [bActive] = 1
		ORDER BY [UserName] + ' ' + [UserSurname]
	END
	ELSE
	BEGIN
		RAISERROR ('You do not have permission to see this page.', 1, 1)
	END
GO
PRINT N'Creating [dbo].[LinkMovieDistributor]...';


GO
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
GO
PRINT N'Creating [dbo].[ScheduleMovieScreening]...';


GO
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
GO
PRINT N'Creating [dbo].[SetBookingNum]...';


GO
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
GO
PRINT N'Creating [dbo].[UpdateMovie]...';


GO
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
GO
PRINT N'Creating [dbo].[UpdateUser]...';


GO
CREATE PROCEDURE [dbo].[UpdateUser]
	@userGuid UNIQUEIDENTIFIER,
	@name NVARCHAR(MAX),
	@surname NVARCHAR(MAX),
	@adminLevel INT,
	@contactNumber NVARCHAR(25),
	@password NVARCHAR(MAX),
	@loginName NVARCHAR(MAX),
	@dateOfBirth DATETIME,
	@active BIT
AS
	UPDATE tblUsers
	SET UserName = @name,
		UserSurname = @surname,
		AdminLevel = @adminLevel,
		ContactNumber = @contactNumber,
		LoginPassword = @password,
		LoginUsername = @loginName,
		UserDoB = @dateOfBirth,
		bActive = @active
	WHERE UserGuid = @userGuid
GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--Creates the default admin user.
IF NOT EXISTS (SELECT * FROM tblUsers WHERE UserName = 'Admin')
BEGIN
	INSERT INTO tblUsers (UserName, UserSurname, UserDoB, AdminLevel, LoginUsername, LoginPassword)
	VALUES ('Admin', '', GETDATE(), 2, 'Admin', 'p@ssw0rd!')
END
IF NOT EXISTS (SELECT * FROM tblUsers WHERE UserName = 'Eddie')
BEGIN
	INSERT INTO tblUsers (UserName, UserSurname, UserDoB, AdminLevel, LoginUsername, LoginPassword)
	VALUES ('Eddie', 'Skinner', GETDATE(), 1, 'Chasso', 'chasso')
END
IF NOT EXISTS (SELECT * FROM tblUsers WHERE UserName = 'James')
BEGIN
	INSERT INTO tblUsers (UserName, UserSurname, UserDoB, AdminLevel, LoginUsername, LoginPassword)
	VALUES ('James', 'Chen', GETDATE(), 0, 'Shogun', 'shogun')
END

--Create default movie data.
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Transformers')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], ExpectedAudience, BBFC_Rate,IsArchived, YearProduced)
	VALUES ('Transformers','144', 'Michael Bay', 'DreamWorks SKG','Action,Sci-Fi,Thriller', 'Average', '12', 0, 2007 )
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Transformers: Revenge of the Fallen')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], ExpectedAudience, BBFC_Rate,IsArchived, YearProduced)
	VALUES ('Transformers: Revenge of the Fallen','150', 'Michael Bay', 'DreamWorks SKG','Action,Adventure,Sci-Fi', 'Below-Average', '12', 0, 2009)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'The Avengers')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], ExpectedAudience, BBFC_Rate,IsArchived, YearProduced)
	VALUES ('The Avengers','143', 'Joss Whedon', 'Marvel Studios','Action,Adventure,Sci-Fi', 'Mainstream', '12A', 0, 2012)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Batman Begins')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], ExpectedAudience, BBFC_Rate,IsArchived,YearProduced)
	VALUES ('Batman Begins','140', 'Christopher Nolan', 'Warner Bros. Pictures','Action,Adventure,Drama', 'Mainstream', '15', 0, 2005)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Batman & Robin')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], ExpectedAudience, BBFC_Rate,IsArchived, YearProduced)
	VALUES ('Batman & Robin','125', 'Joel Schumacher', 'Warner Bros. Pictures','Action,Crime,Fantasy', 'Below-Average', 'PG', 1, 1949 )
END

--Default screens
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 1)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (1, 1000)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 2)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (2, 1000)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 3)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (3, 500)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 4)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (4, 500)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 5)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (5, 500)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 6)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (6, 500)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 7)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (7, 200)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 8)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (8, 200)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 9)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (9, 200)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 10)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (10, 200)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 11)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (11, 100)
END
IF NOT EXISTS (SELECT * FROM tblScreen WHERE ScreenNum = 12)
BEGIN
	INSERT INTO tblScreen (ScreenNum, Seats)
	VALUES (12, 100)
END

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO

GO
CREATE TABLE [#__checkStatus] (
    [Table]      NVARCHAR (270),
    [Constraint] NVARCHAR (270),
    [Where]      NVARCHAR (MAX)
);

SET NOCOUNT ON;


GO
INSERT INTO [#__checkStatus]
EXECUTE (N'DBCC CHECKCONSTRAINTS (N''[dbo].[tblMovies]'')
    WITH NO_INFOMSGS');

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occured while verifying constraints on table [dbo].[tblMovies]', 16, 127);
    END


GO
INSERT INTO [#__checkStatus]
EXECUTE (N'DBCC CHECKCONSTRAINTS (N''[dbo].[tlnkMovieDistributor]'')
    WITH NO_INFOMSGS');

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occured while verifying constraints on table [dbo].[tlnkMovieDistributor]', 16, 127);
    END


GO
INSERT INTO [#__checkStatus]
EXECUTE (N'DBCC CHECKCONSTRAINTS (N''[dbo].[tlnkScreenMovie]'')
    WITH NO_INFOMSGS');

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occured while verifying constraints on table [dbo].[tlnkScreenMovie]', 16, 127);
    END


GO
INSERT INTO [#__checkStatus]
EXECUTE (N'DBCC CHECKCONSTRAINTS (N''[dbo].[tblUsers]'')
    WITH NO_INFOMSGS');

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occured while verifying constraints on table [dbo].[tblUsers]', 16, 127);
    END


GO
SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
