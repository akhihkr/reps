-- =============================================
-- Author:	Kenny
-- Create date: 22 March 2017
-- Description:	Archive Payment and Create New 
--				Transaction/Progress
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ArchivePaymentByTransactionID]
			@oldtransactionID int,
			@dealID int,
			@transactionTypeID int,
			@transactionStatusID int,
			@workflowTaskID int,
			@userID int,
			@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[TransactionDetail]
		SET   [DateModified] = GETDATE(),
			  [Deleted] = 1
		WHERE [TransactionID] = @oldtransactionID


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
			   ,@workflowTaskID
			   ,@NewTransactionID
			   ,null
			   ,@userID
			   ,GETDATE()
			   ,null
			   ,0)

		--SELECT @NewTransactionID;
		SET @rowCount = @NewTransactionID;
END