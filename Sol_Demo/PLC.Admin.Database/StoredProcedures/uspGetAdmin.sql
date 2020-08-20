CREATE PROCEDURE [Admin].[uspGetAdmin]
	@Command Varchar(100)=NULL,

	--@AdminIdentity Varchar(100)=NULL,
	--@FirstName Varchar(100)=NULL,
	--@LastName Varchar(100)=NULL,

	--@EmailId Varchar(100)=NULL,

	--@Role Varchar(100)=NULL,

	@UserName Varchar(100)=NULL
	--@Password Varchar(100)=NULL,

	--@Salt Varchar(MAX)=NULL,
	--@Hash Varchar(MAX)=NULL

WITH RECOMPILE
AS

	BEGIN
		
		SET NOCOUNT ON;
		DECLARE @ErrorMessage Varchar(MAX);
		DECLARE @AdminId NUmeric(18,0)=NULL;
		DECLARE @TransactionId Varchar(100)=NULL;
		DECLARE @Flag bit=0;

		IF @Command='Get-Admin'
			BEGIN
				
				BEGIN TRY

					BEGIN TRANSACTION

						SELECT 
							A.AdminIdentity,
							A.FirstName,
							A.LastName,
							A.EmailId,
							A.Role,
							AL.UserName
						FROM 
							tblAdmin As A WITH(NOLOCK)
						INNER JOIN 
							tblAdminLogin AS AL WITH(NOLOCK)
						ON 
							A.AdminId=AL.AdminId

					COMMIT TRANSACTION

				END TRY

				BEGIN CATCH 

					SET @ErrorMessage=ERROR_MESSAGE();
					RAISERROR(@ErrorMessage,16,1);
					ROLLBACK TRANSACTION

				END CATCH

			END
		ELSE IF @Command='Admin-Login'
			BEGIN
				
				BEGIN TRY

					BEGIN TRANSACTION

						SELECT 
							*
						FROM 
							Admin.udvAdminLoginResultSet AS ALRS WITH(NOLOCK)
						WHERE
							ALRS.UserName=@UserName

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