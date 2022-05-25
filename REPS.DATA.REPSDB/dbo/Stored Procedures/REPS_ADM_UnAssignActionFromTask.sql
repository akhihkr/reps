-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: UnAssign Action To Task
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UnAssignActionFromTask]
			@workflowActionID int,
			@workflowTaskID int,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    -- Insert statements for procedure here
	DELETE FROM [dbo].[WorkflowActionMap]
		  WHERE [WorkflowActionID] = @workflowActionID
		  AND   [WorkflowTaskID] = @workflowTaskID

END
SET @rowCount=  @@ROWCOUNT	