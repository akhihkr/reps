-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get all Workflow Progress
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkflowProgress]
		@DealID int = null
AS
		SELECT TaskName,WP.DateCreated AS [DateCreated], U.GivenName
			FROM [WorkflowProgress] WP
			JOIN [dbo].[WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
			JOIN [Task] T ON WT.TaskID = T.TaskID
			JOIN [User] U on WP.[UserID] = U.[UserID]
			WHERE DealID=@DealID AND WP.Deleted=0
			GROUP BY TaskName,U.GivenName,WP.DateCreated
			ORDER BY DateCreated  DESC
GO