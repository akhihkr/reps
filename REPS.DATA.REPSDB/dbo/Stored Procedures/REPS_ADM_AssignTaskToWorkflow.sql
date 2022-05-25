-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Assign Task To Workflow
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AssignTaskToWorkflow]
			@taskID int,
			@workflowID int,
			@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

 	-- Get last Display Order
	DECLARE @LastDisplayOrder int = 0;
	SELECT @LastDisplayOrder = (
							SELECT TOP 1 [WorkflowTaskOrder]
							  FROM [dbo].[WorkflowTask]
							  WHERE [WorkflowID] = @workflowID
							  ORDER BY [WorkflowTaskOrder] DESC
						  )

	-- IF null => 1st task to be added, set lastdisplayorder to 0
	IF	(@LastDisplayOrder is null)
	BEGIN
		SET @LastDisplayOrder = 0
	END

	-- Assign Task
	INSERT INTO [dbo].[WorkflowTask]
			   ([WorkflowTaskGUID]
			   ,[WorkflowTaskID]
			   ,[WorkflowID]
			   ,[TaskID]
			   ,[WorkflowTaskOrder]
			   ,[IsDeleted])
		 VALUES
			   (NEWID()
			   ,CAST(( CAST(@workflowID AS varchar) + CAST(@taskID AS varchar)) AS int)
			   ,@workflowID
			   ,@taskID
			   ,(@LastDisplayOrder + 1)
			   ,0)

	SET @rowCount = SCOPE_IDENTITY();
END
