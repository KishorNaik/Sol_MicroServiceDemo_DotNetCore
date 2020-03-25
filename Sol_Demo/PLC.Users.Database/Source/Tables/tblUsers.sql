CREATE TABLE [Users].[tblUsers]
(
	[UserId] Numeric(18,0) IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	UserIdentity Varchar(100) NOT NULL,
	FirstName Varchar(50),
	LastName Varchar(50)
)
