﻿CREATE TABLE [dbo].[tblUsers]
(
	UserGuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID() RowGuidCol PRIMARY KEY,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
	UserName NVARCHAR(255) NOT NULL,
	UserSurname NVARCHAR(255) NOT NULL,
	UserDoB DATE NOT NULL,
	ContactNumber NVARCHAR(20) NULL,
	LoginUsername NVARCHAR(255) NOT NULL,
	LoginPassword NVARCHAR(255) NOT NULL,
	bActive BIT NOT NULL DEFAULT 1,
	AdminLevel INT NOT NULL DEFAULT 0 CHECK (AdminLevel = 0 OR AdminLevel = 1 OR AdminLevel = 2 OR AdminLevel = 3 ) --0=Normal employee. 1=Manager. 2=Admin.  3=Distributor
)
