-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get Task Actions
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetWorkFlowTasksActionsByTaskID]
					@TaskID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT 

			WT.[WorkflowTaskID],
			WT.[TaskID],T.[TaskName],
			WA.[ID] AS "WorkflowActionID",WA.[DisplayName],
			WT.[WorkflowTaskOrder],T.[TaskControl]

			FROM [dbo].[WorkflowTask] WT
			INNER JOIN [dbo].[Task] T 
			ON WT.[TaskID]=T.[TaskID]
			INNER JOIN [dbo].[WorkflowActionMap] WM
			ON WM.[WorkflowTaskID] = WT.[WorkflowTaskID]
			INNER JOIN [dbo].WorkflowAction WA
			ON WA.[ID] = WM.[WorkflowActionID]


		WHERE WT.WorkflowTaskID = @TaskID
		AND WA.[IsActive] = 1
END