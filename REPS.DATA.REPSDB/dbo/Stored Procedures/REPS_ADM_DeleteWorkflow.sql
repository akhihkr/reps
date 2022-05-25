-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Delete Workflow
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_DeleteWorkflow]
			@workflowID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Workflow]
	   SET [IsDeleted] = 1
	 WHERE [WorkflowID] = @workflowID

	UPDATE [dbo].[Task]
	   SET [IsDeleted] = 1
	 WHERE TaskWorkflowID = @workflowID	  
	
	DECLARE @WorkflowTaskId int = 0;

	SELECT @WorkflowTaskId = WT.[WorkflowTaskID]
	  FROM [dbo].[WorkflowTask] WT
	  INNER JOIN [dbo].[Task] T
	  ON WT.[TaskID] = T.[TaskID]
	  WHERE T.[TaskWorkflowID] = @workflowID	
	  
	UPDATE [dbo].[WorkflowTask]
	   SET [IsDeleted] = 1
	 WHERE [WorkflowTaskID] = @WorkflowTaskId

	 SET @rowCount=  @@ROWCOUNT	
END