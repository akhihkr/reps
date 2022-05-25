-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all Actions
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetAllActions]
			@workflowTaskID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  SELECT [WorkflowActionID], [DisplayName], MAX(Assigned) Assigned
		FROM
		-- Get all Tasks for Process
			 ( 
			SELECT [ID] AS 'WorkflowActionID',
				   [DisplayName],
				   '0' AS 'Assigned'
			  FROM [dbo].[WorkflowAction]
			  WHERE [ID] NOT IN (43)   -- Remove Fees
			  AND [IsDeleted] = 0
			UNION
			(
			-- Get Tasks assgined to Workflow
			SELECT 
					WM.[WorkflowActionID],
					WA.[DisplayName],
					'1' AS 'Assigned'
				FROM [dbo].[WorkflowTask] WT
				INNER JOIN [dbo].[Task] T 
				ON WT.[TaskID]=T.[TaskID]
				INNER JOIN [dbo].[WorkflowActionMap] WM
				ON WM.[WorkflowTaskID] = WT.[WorkflowTaskID]
				INNER JOIN [dbo].WorkflowAction WA
				ON WA.[ID] = WM.[WorkflowActionID]


			WHERE WT.WorkflowTaskID = @workflowTaskID
			AND WA.[IsActive] = 1)

				)
			AS T GROUP BY  WorkflowActionID, [DisplayName]
			ORDER BY WorkflowActionID
END
