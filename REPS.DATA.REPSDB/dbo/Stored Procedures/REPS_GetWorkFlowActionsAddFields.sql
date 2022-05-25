-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get all Workflow Tasks Actions 
--				fields for Add View
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkFlowActionsAddFields]
		@taskID int,
		@workflowActionID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
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
	WHERE WT.WorkflowTaskID = @taskID
	AND WA.[IsActive] = 1
	AND WA.ID = @workflowActionID
	AND WAV.[IsDeleted] = 0

END