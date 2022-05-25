-- =============================================
-- Author:	Kenny
-- Create date: 22 March 2017
-- Description:	Insert Payment Transation
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddPaymentTransaction]
		@dealID int,
		@transactionTypeID int,
		@transactionStatusID int,
		@workflowTaskID int,
		@userID int,
		@transactionID int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
			   ,@transactionStatusID	--4
			   ,0);

	 SET @transactionID = SCOPE_IDENTITY();

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
END

