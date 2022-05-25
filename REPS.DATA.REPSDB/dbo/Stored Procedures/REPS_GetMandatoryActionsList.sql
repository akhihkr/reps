-- =============================================
-- Author: Pravina Barosah
-- Create date: 20 FEB 2017
-- Description:	Get mandatory Actions list	
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetMandatoryActionsList]
			@workflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	-- Get all mandatory Actions list
			SELECT 

				WT.[WorkflowTaskID],
				WT.[TaskID],T.[TaskName],
				WA.[ID] AS "WorkflowActionID",WA.[DisplayName],
				WT.[WorkflowTaskOrder]

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
END