-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Update Task
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateTask]
			@workflowTaskID int,
			@taskID int,
			@taskName varchar(max),
			@taskOrder int,
			@icon varchar(max),
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	DECLARE @UpdtCount int = 0;

	UPDATE [dbo].[WorkflowTask]
	   SET [WorkflowTaskOrder] = @taskOrder
	 WHERE [WorkflowTaskID] = @workflowTaskID

	 SET @UpdtCount = @UpdtCount + @@ROWCOUNT;

	UPDATE [dbo].[Task]
	   SET [TaskName] = @taskName
		  ,[TaskDisplayIcon] = @icon
	 WHERE [TaskID] = @taskID

	 SET @UpdtCount = @UpdtCount + @@ROWCOUNT;

	IF ( @UpdtCount >= 2 )
		SET @rowCount = @UpdtCount;
	ELSE
		SET @rowCount = 0;
END