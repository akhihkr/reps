-- =============================================
-- Author: Pravina Barosah
-- Create date:13 FEB 2017
-- Description: Add Workflow New Transaction
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddWorkflowActionNewTransaction]
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
	DECLARE @TransactionID int;

	INSERT INTO [dbo].[Transaction]
			   ([DealID]
			   ,[TransactionTypeID]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[TransactionStatusID]
			   ,[Deleted])
		 VALUES
			   (@dealID
			   ,@transactionTypeID		--3
			   ,GETDATE()
			   ,null
			   ,@transactionStatusID	--4
			   ,0);

	SELECT @TransactionID = SCOPE_IDENTITY();

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
			,@TransactionID
			,null
			,@userID
			,GETDATE()
			,null
			,0)

	--SELECT @TransactionID;
	SET @rowCount = @TransactionID
END