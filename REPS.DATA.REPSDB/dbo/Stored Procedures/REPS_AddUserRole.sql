-- =============================================
-- Author: Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert user role Info to UserRole table 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddUserRole]
			@UserID int,
			@RoleID int,
			@Identity int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[UserRole]
           (
			    [UserID]
			   ,[RoleID]
			   ,[IsActive]
			   ,[DateCreated]
			   ,[Deleted]
		   )
     VALUES
           (
			   @UserID
			   ,@RoleID
			   ,1
			   ,GETDATE()
			   ,0
		   )
		SET @Identity = SCOPE_IDENTITY()
END
