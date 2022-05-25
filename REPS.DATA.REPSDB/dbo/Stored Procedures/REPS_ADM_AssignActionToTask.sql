-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Assign Action To Task
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AssignActionToTask]
			@workflowActionID int,
			@workflowTaskID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

 	-- Get last Display Order
	INSERT INTO [dbo].[WorkflowActionMap]
			   ([WorkflowActionID]
			   ,[WorkflowTaskID]
			   ,[AllowAttachments]
			   ,[ParticipantTypeID]
			   ,[DateCreated]
			   ,[CreatedByUserID]
			   ,[IsDeleted])
		 VALUES
			   (@workflowActionID
			   ,@workflowTaskID
			   ,1
			   ,NULL
			   ,GETDATE()
			   ,NULL
			   ,0)

	SET @rowCount = SCOPE_IDENTITY();
END