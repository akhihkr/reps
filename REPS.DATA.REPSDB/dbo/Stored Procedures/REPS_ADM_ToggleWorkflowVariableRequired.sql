-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Toggle Workflow Variable Required
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_ToggleWorkflowVariableRequired]
			@workflowVariableID int,
			@actionID int,
			@toggle bit,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF ( @toggle = 1 )
		BEGIN
			UPDATE [dbo].[WorkflowActionVariable]
			   SET [IsRequired] = 1
			   WHERE [WorkflowActionID] = @actionID
				AND   [WorkflowVariableID] = @workflowVariableID;

				SET @rowCount = @@ROWCOUNT;
		END
	ELSE
		BEGIN
			UPDATE [dbo].[WorkflowActionVariable]
			   SET [IsRequired] = 0
			   WHERE [WorkflowActionID] = @actionID
				AND   [WorkflowVariableID] = @workflowVariableID;

				SET @rowCount = @@ROWCOUNT;
		END
END