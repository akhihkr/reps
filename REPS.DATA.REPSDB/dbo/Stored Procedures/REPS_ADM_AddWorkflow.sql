-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Workflow details
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddWorkflow]
			@workflowName varchar(max),
			@workflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @LastTaskID int = 0;
	DECLARE @NewTaskID int = 0;
	DECLARE @NewTaskWorkflowID varchar(20);

	-- Get last Task ID from Table
	SELECT @LastTaskID = (
							SELECT TOP 1 [TaskID]
							  FROM [dbo].[Task]
							  ORDER BY [TaskID] DESC
						  )

	SET @NewTaskID = @LastTaskID + 1; -- Increment to get New Task ID
	SET @NewTaskWorkflowID = CAST(@workflowID AS varchar) + CAST(@NewTaskID AS VARCHAR);     -- Build New Task Workflow ID


	-- Insert Task
	INSERT INTO [dbo].[Task]
			   ([TaskGUID]
			   ,[TaskID]
			   ,[TaskName]
			   ,[TaskDisplayIcon]
			   ,[TaskControl]
			   ,[TaskWorkflowID])
		 VALUES
			   ( NEWID()
			   ,@NewTaskID
			   ,@workflowName
			   ,null
			   ,null
			   , CAST(@NewTaskWorkflowID AS INT))

	-- Insert Workflow 
	INSERT INTO [dbo].[Workflow]
			   ([WorkflowGUID]
			   ,[WorkflowID]
			   ,[WorkflowName])
		 VALUES
			   (NEWID()
			   ,CAST(@NewTaskWorkflowID AS INT)
			   ,(@workflowName + '_Workflow'))

	-- Get last Display Order
	DECLARE @LastDisplayOrder int = 0;
	SELECT @LastDisplayOrder = (
							SELECT TOP 1 [WorkflowTaskOrder]
							  FROM [dbo].[WorkflowTask]
							  WHERE [WorkflowID] = @workflowID
							  ORDER BY [WorkflowTaskOrder] DESC
						  )

	-- Insert WorkflowTask
	INSERT INTO [dbo].[WorkflowTask]
			   ([WorkflowTaskGUID]
			   ,[WorkflowTaskID]
			   ,[WorkflowID]
			   ,[TaskID]
			   ,[WorkflowTaskOrder]
			   ,[IsDeleted])
		 VALUES
			   (NEWID()
			   ,CAST(('2' + CAST(@NewTaskID AS varchar)) AS INT)
			   ,@workflowID
			   ,@NewTaskID
			   ,(@LastDisplayOrder + 1)
			   ,0)

END