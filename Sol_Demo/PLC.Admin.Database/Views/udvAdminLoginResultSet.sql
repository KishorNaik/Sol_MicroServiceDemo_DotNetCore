CREATE VIEW [Admin].[udvAdminLoginResultSet]
	AS 
	SELECT 
		A.AdminIdentity,
		A.FirstName,
		A.LastName,
		A.EmailId,
		A.Role,
		AL.Salt,
		AL.Hash,
		AL.UserName
	FROM 
		Admin.tblAdmin As A WITH(NOLOCK)
	INNER JOIN 
		Admin.tblAdminLogin AS AL WITH(NOLOCK)
	ON 
		A.AdminId=AL.AdminId

