-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Remove Task
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_DeleteTask]
			@workflowTaskID int,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
 	-- Delete Task
	DELETE FROM [dbo].[WorkflowTask]
		  WHERE [WorkflowTaskID] = @workflowTaskID

END	
SET @rowCount=  @@ROWCOUNT	

