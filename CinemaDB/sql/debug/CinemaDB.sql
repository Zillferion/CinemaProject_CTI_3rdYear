/*
Deployment script for CinemaDB
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "CinemaDB"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"

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

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target SHOGUN-PC\SQLEXPRESS. The database for which this script was built, CinemaDB, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'SHOGUN-PC\SQLEXPRESS')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'SHOGUN-PC\SQLEXPRESS',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]

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
PRINT N'Dropping DF__tmp_ms_xx__Movie__440B1D61...';


GO
ALTER TABLE [dbo].[tblMovies] DROP CONSTRAINT [DF__tmp_ms_xx__Movie__440B1D61];


GO
PRINT N'Dropping DF__tmp_ms_xx__Creat__44FF419A...';


GO
ALTER TABLE [dbo].[tblMovies] DROP CONSTRAINT [DF__tmp_ms_xx__Creat__44FF419A];


GO
PRINT N'Dropping DF__tmp_ms_xx__IsArc__45F365D3...';


GO
ALTER TABLE [dbo].[tblMovies] DROP CONSTRAINT [DF__tmp_ms_xx__IsArc__45F365D3];


GO
PRINT N'Dropping FK__tlnkMovie__Movie__4D94879B...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor] DROP CONSTRAINT [FK__tlnkMovie__Movie__4D94879B];


GO
PRINT N'Dropping FK__tlnkScree__Movie__4E88ABD4...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie] DROP CONSTRAINT [FK__tlnkScree__Movie__4E88ABD4];


GO
PRINT N'Dropping CK__tblMovies__BBFC___5165187F...';


GO
ALTER TABLE [dbo].[tblMovies] DROP CONSTRAINT [CK__tblMovies__BBFC___5165187F];


GO
PRINT N'Starting rebuilding table [dbo].[tblMovies]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_tblMovies] (
    [MovieId]          UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() ROWGUIDCOL NOT NULL,
    [Created]          DATETIME         DEFAULT GETDATE() NOT NULL,
    [Name]             NVARCHAR (255)   NOT NULL,
    [Duration]         INT              NOT NULL,
    [YearProduced]     INT              NOT NULL,
    [Director]         NVARCHAR (255)   NOT NULL,
    [Producer]         NVARCHAR (255)   NOT NULL,
    [Type]             NVARCHAR (MAX)   NOT NULL,
    [Description]      TEXT             DEFAULT '' NOT NULL,
    [ExpectedAudience] NVARCHAR (100)   NOT NULL,
    [BBFC_Rate]        NVARCHAR (8)     NOT NULL,
    [IsArchived]       BIT              DEFAULT 0 NOT NULL,
    PRIMARY KEY CLUSTERED ([MovieId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[tblMovies])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_tblMovies] ([MovieId], [Created], [Name], [Duration], [YearProduced], [Director], [Producer], [Type], [ExpectedAudience], [BBFC_Rate], [IsArchived])
        SELECT   [MovieId],
                 [Created],
                 [Name],
                 [Duration],
                 [YearProduced],
                 [Director],
                 [Producer],
                 [Type],
                 [ExpectedAudience],
                 [BBFC_Rate],
                 [IsArchived]
        FROM     [dbo].[tblMovies]
        ORDER BY [MovieId] ASC;
    END

DROP TABLE [dbo].[tblMovies];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_tblMovies]', N'tblMovies';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating On column: MovieId...';


GO
ALTER TABLE [dbo].[tlnkMovieDistributor] WITH NOCHECK
    ADD FOREIGN KEY ([MovieId]) REFERENCES [dbo].[tblMovies] ([MovieId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating On column: MovieId...';


GO
ALTER TABLE [dbo].[tlnkScreenMovie] WITH NOCHECK
    ADD FOREIGN KEY ([MovieId]) REFERENCES [dbo].[tblMovies] ([MovieId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


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
EXECUTE (N'DBCC CHECKCONSTRAINTS (N''[dbo].[tblMovies]'')
    WITH NO_INFOMSGS');

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occured while verifying constraints on table [dbo].[tblMovies]', 16, 127);
    END


GO
SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
