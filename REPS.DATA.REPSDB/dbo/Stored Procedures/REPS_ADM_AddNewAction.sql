-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Action details of admin 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddNewAction]
			@actionName varchar(max),
			@workflowTaskID int,
			@rowCount INT OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @ActionID int = 0;

	INSERT INTO [dbo].[WorkflowAction]
			   ([DisplayName]
			   ,[IsActive]
			   ,[IsDeleted])
		 VALUES
			   (@actionName
			   ,1
			   ,0)

	SET @ActionID = SCOPE_IDENTITY();
	SET @rowCount = @ActionID;

	INSERT INTO [dbo].[WorkflowActionMap]
			   ([WorkflowActionID]
			   ,[WorkflowTaskID]
			   ,[AllowAttachments]
			   ,[ParticipantTypeID]
			   ,[DateCreated]
			   ,[CreatedByUserID]
			   ,[IsDeleted])
		 VALUES
			   (@ActionID
			   ,@workflowTaskID
			   ,1
			   ,NULL
			   ,GETDATE()
			   ,NULL
			   ,0)
END
