CREATE TABLE [Admin].[tblAdmin]
(
	[AdminId] NUmeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AdminIdentity Varchar(100) UNIQUE,
	FirstName Varchar(100),
	LastName Varchar(100),
	EmailId Varchar(100) UNIQUE,
	Role Varchar(100),
	IsDelete bit,
	CreatedDate DateTime,
	ModifiedDate DateTime
)
