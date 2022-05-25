-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Completed Actions By WorkflowID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetCompletedActionsByWorkflowID]
			@dealID int,
			@workflowID int

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	-- Get all completed mandatory Actions
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


			WHERE WT.WorkflowID = @workflowID
			AND WA.[IsActive] = 1
			AND WM.[IsMandatory] = 1

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

			WHERE WP.DealID = @dealID
			AND WP.Deleted = 0
			AND TR.Deleted = 0
			AND TD.Deleted = 0)

			)
END