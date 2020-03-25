CREATE FUNCTION [Users].[udfIsEmailIdOrMobileExists]
(
	@MobileNo Varchar(50)=NULL,
	@EmailId Varchar(100)=NULL,
	@UserIdentity Varchar(100)=NULL
)

RETURNS BIT
AS
BEGIN
	
	DECLARE @Flag bit
	
	SET @UserIdentity=ISNULL(@UserIdentity,'');

	IF EXISTS(
					SELECT 
						1
					FROM 
						Users.udfUserCommunication AS UC WITH(NOLOCK)
					WHERE
							(
									UC.MobileNo=@MobileNo
								OR 
									UC.EmailId=@EmailId
							)
						AND
							UC.UserIdentity<>@UserIdentity
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
