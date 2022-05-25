-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Archive Workflow Actions
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ArchiveWorkflowActionsByTransactionID]
			@oldtransactionID int,
			@workflowactionvarId int,
			@dealID int,
			@transactionTypeID int,
			@transactionStatusID int,
			@userID int,
			@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @WorkflowTaskID int;

	SELECT @WorkflowTaskID = WorkflowTaskID
	FROM [dbo].WorkflowProgress
	WHERE [TransactionID] = @oldtransactionID

	UPDATE [dbo].[TransactionDetail]
		SET   [DateModified] = GETDATE(),
		      [Deleted] = 1
		--WHERE [TransactionID] = @oldtransactionID
		WHERE [WorkflowActionVarID] = @workflowactionvarId
		AND   [VariableTypeID] <> 5 ;             --  do not archive files : * file shall not be removed unless the user edit it and remove the file

	DECLARE @NewTransactionID int;

	INSERT INTO [dbo].[Transaction]
			   ([DealID]
			   ,[TransactionTypeID]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[TransactionStatusID]
			   ,[Deleted])
		 VALUES
			   (@dealID
			   ,@transactionTypeID		
			   ,GETDATE()
			   ,null
			   ,@transactionStatusID	
			   ,0);

	 SELECT @NewTransactionID = SCOPE_IDENTITY();

	 	-- Replace New transaction ID by New  for files
		UPDATE [dbo].[TransactionDetail]
		SET   [DateModified] = GETDATE(),
		      [TransactionID] = @NewTransactionID
		WHERE [TransactionID] = @oldtransactionID
		AND   [VariableTypeID] = 5 ;             -- files

	 -- Create WorkflowProgress
	 	INSERT INTO [dbo].[WorkflowProgress]
			   ([WorkflowProgressGUID]
			   ,[DealID]
			   ,[WorkflowTaskID]
			   ,[TransactionID]
			   ,[WorkflowParentID]
			   ,[UserID]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[Deleted])
		 VALUES
			   (NEWID()
			   ,@dealID
			   ,@WorkflowTaskID
			   ,@NewTransactionID
			   ,null
			   ,@userID
			   ,GETDATE()
			   ,null
			   ,0)
		SET @rowCount = @NewTransactionID;
END