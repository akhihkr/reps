-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: update New Role
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateRole]
			@roleID int,
			@roleName VARCHAR(MAX),
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE [dbo].[Role]
	   SET [Description] = IsNull(@roleName, [Description])
		  ,[DateModified] = GETDATE()
	 WHERE [RoleID] = @roleID
	 
END
SET @rowCount=  @@ROWCOUNT