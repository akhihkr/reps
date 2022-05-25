-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Update Workflow Name
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateWorkflowName]
			@workflowName varchar(max),
			@taskID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DECLARE @WorkflowID int = 0,
		    @UpdtCount int = 0;

	SELECT @WorkflowID = [TaskWorkflowID]
		FROM [dbo].[Task]
		WHERE [TaskID] = @taskID

	UPDATE [dbo].[Task]
		SET [TaskName] = @workflowName
		WHERE [TaskID] = @taskID

	SET @UpdtCount = @UpdtCount + @@ROWCOUNT;

	UPDATE [dbo].[Workflow]
		SET [WorkflowName] = (@workflowName + '_Workflow')
		WHERE [WorkflowID] = @WorkflowID

	SET @UpdtCount = @UpdtCount + @@ROWCOUNT;

	IF ( @UpdtCount >= 2 )
		SET @rowCount = @UpdtCount;
	ELSE
		SET @rowCount = 0;
END