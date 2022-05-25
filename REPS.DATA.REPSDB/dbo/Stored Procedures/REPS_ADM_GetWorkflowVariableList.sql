-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get Workflow Variable list
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetWorkflowVariableList]
			@actionID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   SELECT RES1.[ID], RES1.[DisplayName], WAV2.[IsRequired], RES1.[Assigned]
	   FROM
	   (
  		  SELECT [ID], [DisplayName], MAX(Assigned) Assigned
			FROM
			-- Get all Tasks for Process
				 ( 
					SELECT WV.[VariableTypeID] AS 'ID',
						   WV.[DisplayName],
						   '1' AS 'Assigned'
					  FROM [dbo].[WorkflowActionVariable] WAV
					  INNER JOIN [dbo].[WorkflowVariable] WV
					  ON WAV.WorkflowVariableID = WV.[VariableTypeID]
					  WHERE WAV.[WorkflowActionID] = @actionID
					  AND   WAV.[IsDeleted] = 0
					  AND	WV.[IsDeleted] = 0
				UNION
				(
				-- Get Tasks assgined to Workflow
					SELECT  [VariableTypeID] AS 'ID'
							,[DisplayName]
							,'0' AS 'Assigned'
					FROM [dbo].[WorkflowVariable]
					WHERE [IsDeleted] = 0
					AND   [ID] <> 4
				)

					)
				AS T GROUP BY  ID, [DisplayName]
				--ORDER BY ID

				) RES1

		 LEFT JOIN [dbo].[WorkflowActionVariable] WAV2
		 ON WAV2.[WorkflowVariableID] = RES1.[ID]
		 AND WAV2.[WorkflowActionID] = @actionID

		 ORDER BY RES1.[ID]
END