-- =============================================
-- Author:	Pravina Barosah
-- Create date: 27 Jan 2017
-- Description: get report workflow status
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportWorkflowStatus]
	--initial variables
		@WorkflowID int  =null
	--end initial variables

AS
  	Select * FROM
	(
	 --Get all workflow and user detail
	 SELECT T.[TaskID] [ReportsId], T.[TaskName] [Description],W.[WorkflowID] [workflowID],WP.Deleted

	 FROM [dbo].[WorkflowProgress] WP
	 INNER JOIN [dbo].[WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
	 INNER JOIN [dbo].[Workflow] W  ON WT.[WorkflowID] = W.[WorkflowID]
	 INNER JOIN [dbo].[Task] T ON WT.[TaskID]=T.[TaskID]
	 INNER JOIN [dbo].[Deal] DL ON WP.[DealID] = DL.[DealID]
	 INNER JOIN [User] U on WP.[UserID] = U.[UserID]
	 --end get detail of workflow status  
	  ) As allStatus

	WHERE 	(@WorkflowID IS NULL OR  [workflowID] = @WorkflowID)
			AND [Deleted]=0

	GROUP BY [Description],[ReportsId], [workflowID],[Deleted]
	ORDER BY [Description]  
GO
