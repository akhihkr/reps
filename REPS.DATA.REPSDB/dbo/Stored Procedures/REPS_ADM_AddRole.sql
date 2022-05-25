-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Role to Role Table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddRole]
			@roleName VARCHAR(MAX),
			@Identity int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Role]
			   ([Description]
			   ,[DateCreated]
			   ,[Deleted])
		 VALUES
			   (@roleName
			   ,GETDATE()
			   ,0)
	 SET @Identity = SCOPE_IDENTITY()	 
END