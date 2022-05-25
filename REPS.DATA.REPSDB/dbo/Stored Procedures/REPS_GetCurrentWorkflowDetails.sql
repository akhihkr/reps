-- ===========================================
-- Author:Pravina Barosah
-- Create date: 17 April 2017
-- Description:	Get current Workflow Tasks
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetCurrentWorkflowDetails]

		 @taskID int,@taskWorkflowID int --variables
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  	SELECT 

		WT.[WorkflowTaskGUID],WT.[WorkflowTaskID],
		WT.[WorkflowID],WT.[TaskID],T.[TaskName],T.TaskDisplayIcon,
		W.[WorkflowName],WT.[WorkflowTaskOrder],T.[TaskControl],T.[TaskWorkflowID]

		FROM [dbo].[WorkflowTask] WT
		INNER JOIN [dbo].[Workflow] W 
		ON WT.[WorkflowID] = W.[WorkflowID]
		INNER JOIN [dbo].[Task] T 
		ON WT.[TaskID]=T.[TaskID]

	WHERE 
	(@taskID IS NULL OR WT.[TaskID] = @taskID)
	AND(@taskWorkflowID IS NULL OR T.[TaskWorkflowID] = @taskWorkflowID)
	AND(W.[IsDeleted] = 0)  
	AND(T.[IsDeleted] = 0)
END	
GO
