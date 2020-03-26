CREATE PROCEDURE [Course].[uspGetCourse]
	
	@Command Varchar(100)=NULL,
	@CourseIdentity Varchar(100)=NULL,

	@CourseName Varchar(100)=NULL

WITH RECOMPILE
AS

	BEGIN
		
		SET NOCOUNT ON;
		DECLARE @ErrorMessage Varchar(MAX);

		IF @Command='Get-CourseList'
			BEGIN
				
				BEGIN TRY 
					BEGIN TRANSACTION

						SELECT 
							C.CourseIdentity,
							C.CourseName
						FROM 
							Course.tblCourse AS C WITH(NOLOCK)
					
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
