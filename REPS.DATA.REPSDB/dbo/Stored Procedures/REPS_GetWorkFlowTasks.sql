-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get all WorkflowTasks
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkFlowTasks]
		@WorkflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 

		WT.[WorkflowTaskGUID],T.[TaskGUID],WT.[WorkflowTaskID],
		WT.[WorkflowID],WT.[TaskID],T.[TaskName],T.TaskDisplayIcon,
		W.[WorkflowName],WT.[WorkflowTaskOrder],T.[TaskControl] ,T.TaskWorkflowID

		FROM [dbo].[WorkflowTask] WT
		INNER JOIN [dbo].[Workflow] W 
		ON WT.[WorkflowID] = W.[WorkflowID]
		INNER JOIN [dbo].[Task] T 
		ON WT.[TaskID]=T.[TaskID]

		WHERE W.[WorkflowID] = @WorkflowID
		AND W.[IsDeleted] = 0
END