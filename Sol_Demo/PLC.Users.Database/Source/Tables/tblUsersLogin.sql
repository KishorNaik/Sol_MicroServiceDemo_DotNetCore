CREATE TABLE [Users].[tblUsersLogin]
(
	[UserId] NUmeric(18,0) NOT NULL PRIMARY KEY,
	UserName varchar(100),
	Password Varchar(100),
	Salt Varchar(MAX),
	Hash Varchar(MAX)
)
