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

--Create default movie data.
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Transformers')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], AgeRest, ExpectedAudience, BBFC_Rate,NumViewers,IsArchived, YearProduced)
	VALUES ('Transformers','144', 'Michael Bay', 'DreamWorks SKG','Action,Sci-Fi,Thriller', '13PG', 'Average', '7.2', 0, 0, 2007 )
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Transformers: Revenge of the Fallen')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], AgeRest, ExpectedAudience, BBFC_Rate,NumViewers,IsArchived, YearProduced)
	VALUES ('Transformers: Revenge of the Fallen','150', 'Michael Bay', 'DreamWorks SKG','Action,Adventure,Sci-Fi', '13PG', 'Below-Average', '5.9', 0, 0, 2009)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'The Avengers')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], AgeRest, ExpectedAudience, BBFC_Rate,NumViewers,IsArchived, YearProduced)
	VALUES ('The Avengers','143', 'Joss Whedon', 'Marvel Studios','Action,Adventure,Sci-Fi', '13PG', 'Mainstream', '8.6', 0, 0, 2012)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Batman Begins')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], AgeRest, ExpectedAudience, BBFC_Rate,NumViewers,IsArchived,YearProduced)
	VALUES ('Batman Begins','140', 'Christopher Nolan', 'Warner Bros. Pictures','Action,Adventure,Drama', '13PG', 'Mainstream', '8.3', 0, 0, 2005)
END
IF NOT EXISTS (SELECT * FROM tblMovies WHERE Name = 'Batman & Robin')
BEGIN
	INSERT INTO tblMovies (Name, Duration, Director, Producer, [Type], AgeRest, ExpectedAudience, BBFC_Rate,NumViewers,IsArchived, YearProduced)
	VALUES ('Batman & Robin','125', 'Joel Schumacher', 'Warner Bros. Pictures','Action,Crime,Fantasy', '13PG', 'Below-Average', '3.6', 0, 1, 1949 )
END
