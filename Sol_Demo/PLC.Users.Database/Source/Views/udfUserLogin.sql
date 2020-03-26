CREATE VIEW [Users].[udvUserLogin]
WITH SCHEMABINDING
	AS 
		SELECT 
			U.UserId,
			U.UserIdentity,
			U.FirstName,
			U.LastName,
			UL.UserName,
			UL.Salt,
			UL.Hash
		FROM 
			Users.tblUsers AS U WITH(NOLOCK)
		INNER JOIN
			Users.tblUsersLogin AS UL WITH(NOLOCK) 
		ON 
			U.UserId=UL.UserId
			
