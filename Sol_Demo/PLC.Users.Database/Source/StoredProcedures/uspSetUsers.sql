CREATE PROCEDURE [Users].[uspSetUsers]
	
	@Command Varchar(100)=NULL,

	@UserIdentity Varchar(100)=NULL,

	@FirstName Varchar(100)=NULL,
	@LastName Varchar(100)=NULL,

	@MobileNo Varchar(10)=NULL,
	@EmailId Varchar(100)=NULL,

	@UserName Varchar(100)=NULL,
	@Password Varchar(100)=NULL,

	@Salt Varchar(MAX)=NULL,
	@Hash Varchar(MAX)=NULL

WITH RECOMPILE
AS
	BEGIN
		
		SET NOCOUNT ON;
		DECLARE @ErrorMessage Varchar(MAX);
		DECLARE @UserId NUmeric(18,0)
		DECLARE @Flag bit

		IF @Command='Add-Users'
			BEGIN
				
				BEGIN TRY 
					BEGIN TRANSACTION

						SET @Flag=Users.udfIsEmailIdOrMobileExists(@MobileNo,@EmailId,@UserIdentity);

						IF(@Flag=0)
							BEGIN

								SELECT 'Data-Inserted' AS 'Message'

								INSERT INTO Users.tblUsers
								(
									UserIdentity,
									FirstName,
									LastName
								)
								VALUES
								(
									NEWID(),
									@FirstName,
									@LastName
								)

								SET @UserId=@@IDENTITY;

								INSERT INTO Users.tblUserCommunication
								(
									UserId,
									MobileNo,
									EmailId
								)
								VALUES
								(
									@UserId,
									@MobileNo,
									@EmailId
								)

								INSERT INTO Users.tblUsersLogin
								(
									UserId,
									UserName,
									Password,
									Salt,
									Hash
								)
								VALUES
								(
									@UserId,
									@UserName,
									@Password,
									@Salt,
									@Hash
								)

							END
						ELSE
							BEGIN
								SELECT 'MobileNo or Email Id is Exists' AS 'Message'
							END

								
					COMMIT TRANSACTION
				END TRY 

				BEGIN CATCH
					SET @ErrorMessage=ERROR_MESSAGE();
					RAISERROR(@ErrorMessage,16,1);
					ROLLBACK TRANSACTION

				END CATCH

			END

	END
GO	

