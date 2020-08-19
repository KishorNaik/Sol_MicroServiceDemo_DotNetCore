CREATE FUNCTION [Admin].[udfIsAdminExists]
(
	@AdminIdentity Varchar(100),
	@EmailId Varchar(100)
)
RETURNS BIT
AS
BEGIN
	
	DECLARE @Flag Bit=0;

	SET @AdminIdentity=ISNULL(@AdminIdentity,'');
	

	IF EXISTS(
			
			SELECT 
				1
			FROM 
				Admin.tblAdmin AS A WITH(NOLOCK)
			WHERE
				(
					A.EmailId=@EmailId
				)
				AND
				A.AdminIdentity<>@AdminIdentity
		
		)
		BEGIN
			SET @Flag=1
		END
	ELSE 
		BEGIN
			SET @Flag=0
		END 

	RETURN @Flag;

END
