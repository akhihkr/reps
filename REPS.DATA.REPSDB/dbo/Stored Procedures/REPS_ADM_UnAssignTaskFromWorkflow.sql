-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: UnAssign Task From Workflow
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UnAssignTaskFromWorkflow]
			@taskID int,
			@workflowID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @WorkflowTaskID int = 0;

	-- Build WorkflowTaskID
	SET @WorkflowTaskID = CAST(( CAST(@workflowID AS varchar) + CAST(@taskID AS varchar)) AS int)

	-- Delete Task assignment
	DELETE FROM [dbo].[WorkflowTask]
		  WHERE [WorkflowTaskID] = @WorkflowTaskID
	
	SET @rowCount =  @@ROWCOUNT	;
END

