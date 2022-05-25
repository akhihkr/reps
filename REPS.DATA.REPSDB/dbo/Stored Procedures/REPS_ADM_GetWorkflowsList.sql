-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get Workflows List
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetWorkflowsList]
			@workflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  	SELECT 

		WT.[WorkflowTaskGUID],WT.[WorkflowTaskID],T.[TaskGUID],
		WT.[WorkflowID],WT.[TaskID],T.[TaskName],T.TaskDisplayIcon,
		W.[WorkflowName],WT.[WorkflowTaskOrder],T.[TaskControl],T.[TaskWorkflowID]

		FROM [dbo].[WorkflowTask] WT
		INNER JOIN [dbo].[Workflow] W 
		ON WT.[WorkflowID] = W.[WorkflowID]
		INNER JOIN [dbo].[Task] T 
		ON WT.[TaskID]=T.[TaskID]
		WHERE W.[WorkflowID] = @WorkflowID
		AND	  W.[IsDeleted] = 0		  
		AND	  T.[IsDeleted] = 0
		ORDER BY
		WT.[WorkflowTaskOrder] ASC
END