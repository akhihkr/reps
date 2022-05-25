-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all Tasks
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetAllTasksPerProcess]
			@processID int,
			@workflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [TaskID], [TaskName], MAX(Assigned) Assigned
	FROM
	-- Get all Tasks for Process
		 ( SELECT T.TaskID,
			   T.TaskName,
			   '0' AS 'Assigned'
		  FROM [dbo].[WorkflowTask] WT
		  INNER JOIN [dbo].[Task] T
		  ON WT.TaskID = T.TaskID
		  WHERE WT.WorkflowID IN
		  (
  			SELECT 

				T.[TaskWorkflowID]

				FROM [dbo].[WorkflowTask] WT
				INNER JOIN [dbo].[Workflow] W 
				ON WT.[WorkflowID] = W.[WorkflowID]
				INNER JOIN [dbo].[Task] T 
				ON WT.[TaskID]=T.[TaskID]
			WHERE W.[WorkflowID] = @processID
			AND	  W.[IsDeleted] = 0	
			)


		UNION
		(
		-- Get Tasks assgined to Workflow
  			SELECT 
				T.TaskID,
				T.TaskName,
				'1' AS 'Assigned'
				FROM [dbo].[WorkflowTask] WT
				INNER JOIN [dbo].[Workflow] W 
				ON WT.[WorkflowID] = W.[WorkflowID]
				INNER JOIN [dbo].[Task] T 
				ON WT.[TaskID]=T.[TaskID]
			WHERE W.[WorkflowID] = @workflowID
			AND	  W.[IsDeleted] = 0	)
		UNION
		(
			SELECT 
			T.TaskID,
			T.TaskName,
			'0' AS 'Assigned'
			FROM
				[dbo].[Task] T
			WHERE
				T.[IsWorkflowTask] = 1
			AND
				T.[IsDeleted] = 0
			AND
				T.[TaskID] NOT IN
				(
					SELECT
						T.TaskID
					FROM
						[dbo].[WorkflowTask] WT
					WHERE 
						WT.[TaskID] = T.[TaskID]
				)
		)
	)
		AS T GROUP BY  TaskID, [TaskName]
		ORDER BY TaskID

END