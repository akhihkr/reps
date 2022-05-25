-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get all Workflow Tasks Actions 
--				fields saved values for Edit View
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkFlowActionsEditFieldsValues]
		@dealID int,
		@taskID int,
		@workflowActionID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT		A.[WorkflowTaskID], 
				A.[WorkflowActionID],
				E.[TransactionID],
				E.[TransactionDetailGUID],
				A.[WorkflowActionVarID],
				A.[WorkflowVariableID],
				E.[Value],
				A.[IsRequired]
	FROM
	(	
		SELECT 
			WT.[WorkflowTaskID],
			WT.[TaskID],T.[TaskName],
			WA.[ID] AS "WorkflowActionID",WA.[DisplayName] AS "WorkflowActionDisplayName",
			WT.[WorkflowTaskOrder],T.[TaskControl],
			WV.[ID] AS "WorkflowVariableID",WV.[DisplayName] AS "WorkflowVariableDisplayName",WAV.[ID] AS "WorkflowActionVarID",
			WAV.IsRequired

			FROM [dbo].[WorkflowTask] WT
			INNER JOIN [dbo].[Task] T 
			ON WT.[TaskID]=T.[TaskID]
			INNER JOIN [dbo].[WorkflowActionMap] WM
			ON WM.[WorkflowTaskID] = WT.[WorkflowTaskID]
			INNER JOIN [dbo].WorkflowAction WA
			ON WA.[ID] = WM.[WorkflowActionID]
			INNER JOIN [WorkflowActionVariable] WAV
			ON WA.[ID] = WAV.[WorkflowActionID]
			INNER JOIN [dbo].WorkflowVariable WV
			ON  WV.[ID] = WAV.[WorkflowVariableID]
		WHERE 
		(@taskID IS NULL OR WT.WorkflowTaskID = @taskID)
		AND WA.[IsActive] = 1
		AND WA.ID = @workflowActionID
		AND WAV.[IsDeleted] = 0
		) AS A 

	LEFT JOIN 
	(
		SELECT	WP.[WorkflowTaskID], 
				WAV.[WorkflowActionID],
				TD.[TransactionID],
				TD.[WorkflowActionVarID],
				WAV.[WorkflowVariableID],
				TD.[Value],
				TD.[TransactionDetailGUID]
			FROM [dbo].WorkflowTask WT
			INNER JOIN [dbo].[WorkflowProgress] WP
			ON WT.WorkflowTaskID = WP.WorkflowTaskID
			INNER JOIN [dbo].[Transaction] TR
			ON WP.TransactionID = TR.TransactionID
			INNER JOIN  [dbo].[TransactionDetail] TD
			ON TR.TransactionID = TD.TransactionID
			INNER JOIN  [dbo].[WorkflowActionVariable] WAV
			ON WAV.ID = TD.WorkflowActionVarID

			WHERE WP.DealID = @dealID
			AND  WAV.WorkflowActionID = @workflowActionID
			AND  (@taskID IS NULL OR WT.WorkflowTaskID = @taskID)
			AND	 TD.Deleted = 0
			AND WAV.[IsDeleted] = 0

			GROUP BY WP.[WorkflowTaskID], 
			WAV.[WorkflowActionID],
			TD.[TransactionID],
			TD.[WorkflowActionVarID],
			WAV.[WorkflowVariableID],
			TD.[Value],
			TD.[TransactionDetailGUID]  
	) AS E

		ON 
			E.WorkflowActionVarID = A.WorkflowActionVarID
END