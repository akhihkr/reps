-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Toggle Workflow Variable
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_ToggleWorkflowVariable]
			@workflowVariableID int,
			@actionID int,
			@toggle bit,
			@rowCount INT OUTPUT 
AS
SET NOCOUNT OFF -- enable rowcount return 
    -- Insert statements for procedure here
	
	DECLARE @counterCheck INT;
	
	IF ( @toggle = 1 )
		BEGIN
			SET @counterCheck = (
				SELECT
					COUNT([WorkflowActionID])
				FROM
					[dbo].[WorkflowActionVariable]
				WHERE
					[WorkflowActionID] = @actionID
				AND
					[WorkflowVariableID] = @workflowVariableID
			)

			IF (@CounterCheck <> 0)
				BEGIN
					UPDATE
						[dbo].[WorkflowActionVariable]
					SET
						[IsDeleted] = 0
					WHERE
						[WorkflowActionID] = @actionID
					AND
						[WorkflowVariableID] = @workflowVariableID
				END
			ELSE
				BEGIN
					INSERT INTO [dbo].[WorkflowActionVariable]
						   ([WorkflowActionID]
						   ,[WorkflowVariableID]
						   ,[IsRequired]
						   ,[IsDeleted])
					 VALUES
						   (@actionID
						   ,@workflowVariableID
						   ,0
						   ,0);
				END
		END
	ELSE
		BEGIN
			UPDATE
				[dbo].[WorkflowActionVariable]
			SET
				[IsDeleted] = 1
			WHERE
				[WorkflowActionID] = @actionID
			AND
				[WorkflowVariableID] = @workflowVariableID
		END

	SET @rowCount=  @@ROWCOUNT	