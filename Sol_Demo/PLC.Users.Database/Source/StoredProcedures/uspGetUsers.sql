CREATE PROCEDURE [Users].[uspGetUsers]
	@Command Varchar(100)=NULL,

	@UserIdentity Varchar(100)=NULL,

	@FirstName Varchar(100)=NULL,
	@LastName Varchar(100)=NULL,

	@MobileNo Varchar(10)=NULL,
	@EmailId Varchar(100)=NULL,

	@UserName Varchar(100)=NULL,
	@Password Varchar(100)=NULL

WITH RECOMPILE
AS

	BEGIN
		
		SET NOCOUNT ON;
		DECLARE @ErrorMessage Varchar(MAX);

		IF @Command='Login-Credentails'
			BEGIN
				
				BEGIN TRY 
					BEGIN TRANSACTION

						SELECT 
							UL.UserIdentity,
							UL.FirstName,
							UL.LastName,
							'' As 'Split1',
							UL.UserName,
							UL.Salt,
							UL.Hash
						FROM 
							Users.udvUserLogin AS  UL
						WHERE
							UL.UserName=@UserName

					
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
