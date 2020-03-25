CREATE VIEW [Users].[udfUserCommunication]
WITH SCHEMABINDING
	AS 
		SELECT 
			U.UserIdentity,
			U.FirstName,
			U.LastName,
			UC.EmailId,
			UC.MobileNo
		FROM 
			Users.tblUsers AS U WITH(NOLOCK)
		INNER JOIN
			Users.tblUserCommunication AS UC WITH(NOLOCK) 
		ON 
			U.UserId=UC.UserId
			
