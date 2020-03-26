﻿CREATE VIEW [Users].[udvUserCommunication]
WITH SCHEMABINDING
	AS 
		SELECT 
			U.UserId,
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
			
