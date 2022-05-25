-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get all Workflow Tasks Actions
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkFlowTasksActions]
		@TaskID int,
		@DealID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT WorkflowActionID,max(tick) Tick,[DisplayName]
		FROM 
		(
		SELECT 

			WT.[WorkflowTaskID],
			WT.[TaskID],T.[TaskName],
			WA.[ID] AS "WorkflowActionID",WA.[DisplayName],
			WT.[WorkflowTaskOrder],T.[TaskControl] ,'1' AS Tick

			FROM [dbo].[WorkflowTask] WT
			INNER JOIN [dbo].[Task] T 
			ON WT.[TaskID]=T.[TaskID]
			INNER JOIN [dbo].[WorkflowActionMap] WM
			ON WM.[WorkflowTaskID] = WT.[WorkflowTaskID]
			INNER JOIN [dbo].WorkflowAction WA
			ON WA.[ID] = WM.[WorkflowActionID]


		WHERE WT.WorkflowTaskID = @TaskID
		AND WA.[IsActive] = 1

		AND 
		(
		 WA.[ID] in (
		SELECT WAV.WorkflowActionID
			FROM [dbo].[WorkflowProgress] WP
			INNER JOIN [dbo].[Transaction] TR
			ON WP.TransactionID = TR.TransactionID
			INNER JOIN  [dbo].[TransactionDetail] TD
			ON TR.TransactionID = TD.TransactionID
			INNER JOIN  [dbo].[WorkflowActionVariable] WAV
			ON WAV.ID = TD.WorkflowActionVarID

		WHERE WP.DealID = @DealID
		AND WP.Deleted = 0
		AND TR.Deleted = 0
		AND TD.Deleted = 0)

		)
		UNION

		SELECT 

			WT.[WorkflowTaskID],
			WT.[TaskID],T.[TaskName],
			WA.[ID] AS "WorkflowActionID",WA.[DisplayName],
			WT.[WorkflowTaskOrder],T.[TaskControl] ,'0' AS Tick

			FROM [dbo].[WorkflowTask] WT
			INNER JOIN [dbo].[Task] T 
			ON WT.[TaskID]=T.[TaskID]
			INNER JOIN [dbo].[WorkflowActionMap] WM
			ON WM.[WorkflowTaskID] = WT.[WorkflowTaskID]
			INNER JOIN [dbo].WorkflowAction WA
			ON WA.[ID] = WM.[WorkflowActionID]


		WHERE WT.WorkflowTaskID = @TaskID
		AND WA.[IsActive] = 1

		) AS T  GROUP BY WorkflowActionID,[DisplayName]
		ORDER BY WorkflowActionID

END