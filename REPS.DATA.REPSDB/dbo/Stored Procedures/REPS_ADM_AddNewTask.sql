-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Task to Task Table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddNewTask]
			@workflowID int,
			@taskName varchar(max),
			@taskOrder int,
			@icon varchar(max),
			@rowCount INT OUTPUT,
			@Identity INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @LastTaskID int = 0,
			@NewTaskID int = 0,
			@CurrentTaskID int = 0;

	-- Get last taskID
	SELECT TOP 1 @LastTaskID = [TaskID]
			FROM [dbo].[Task]
		ORDER BY [TaskID] DESC

	SET @NewTaskID = @LastTaskID + 1;

	-- Create New Task
	INSERT INTO [dbo].[Task]
			   ([TaskGUID]
			   ,[TaskID]
			   ,[TaskName]
			   ,[TaskDisplayIcon]
			   ,[TaskControl]
			   ,[TaskWorkflowID]
			   ,[IsDeleted]
			   ,[IsWorkflowTask])
		 VALUES
			   (NEWID()
			   ,@NewTaskID
			   ,@taskName
			   ,@icon
			   ,NULL
			   ,NULL
			   ,0
			   ,1)

	SET @Identity = @NewTaskID;

   	-- Get last Display Order
	DECLARE @LastDisplayOrder int = 0;
	SELECT @LastDisplayOrder = (
							SELECT TOP 1 [WorkflowTaskOrder]
							  FROM [dbo].[WorkflowTask]
							  WHERE [WorkflowID] = @workflowID
							  ORDER BY [WorkflowTaskOrder] DESC
						  )

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
			   ,CAST(( CAST(@workflowID AS varchar) + CAST(@NewTaskID AS varchar)) AS int)
			   ,@workflowID
			   ,@NewTaskID
			   ,(@LastDisplayOrder + 1)
			   ,0)

	SET @rowCount = SCOPE_IDENTITY();
END