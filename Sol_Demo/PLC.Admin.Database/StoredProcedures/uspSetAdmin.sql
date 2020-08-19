CREATE PROCEDURE [Admin].[uspSetAdmin]
	@Command Varchar(100)=NULL,

	@AdminIdentity Varchar(100)=NULL,
	@FirstName Varchar(100)=NULL,
	@LastName Varchar(100)=NULL,

	@EmailId Varchar(100)=NULL,

	@Role Varchar(100)=NULL,

	@UserName Varchar(100)=NULL,
	@Password Varchar(100)=NULL,

	@Salt Varchar(MAX)=NULL,
	@Hash Varchar(MAX)=NULL

WITH RECOMPILE
AS

	BEGIN
		
		SET NOCOUNT ON;
		DECLARE @ErrorMessage Varchar(MAX);
		DECLARE @AdminId NUmeric(18,0)=NULL;
		DECLARE @TransactionId Varchar(100)=NULL;
		DECLARE @Flag bit=0;

		IF @Command='Add-Admin'
			BEGIN
				
				BEGIN TRY

					BEGIN TRANSACTION

						SET @Flag=Admin.udfIsAdminExists(@AdminIdentity,@EmailId);

						IF @Flag=0
							BEGIN

								SET @TransactionId=NEWID();

								INSERT INTO [Admin].tblAdmin 
								(
									AdminIdentity,
									FirstName,
									LastName,
									EmailId,
									Role,
									TransactionId,
									IsCommit,
									IsDelete,
									CreatedDate
								)
								VALUES
								(
									NEWID(),
									@FirstName,
									@LastName,
									@EmailId,
									@Role,
									@TransactionId,
									1,
									0,
									GETDATE()
								)

								SET @AdminId=@@IDENTITY

								INSERT INTO [Admin].tblAdminLogin
								(
									AdminId,
									UserName,
									Password,
									Salt,
									Hash,
									TransactionId,
									IsCommit,
									IsDelete,
									CreatedDate
								)
								VALUES
								(
									@AdminId,
									@EmailId,
									@Password,
									@Salt,
									@Hash,
									@TransactionId,
									1,
									0,
									GETDATE()
								)

								SELECT 'Insert' AS 'Message';

							END
						ELSE
							BEGIN
								SELECT 'Admin already exists' as 'Message' ;
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