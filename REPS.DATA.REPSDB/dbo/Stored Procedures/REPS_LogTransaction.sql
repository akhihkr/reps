-- =============================================
-- Author: Pravina Barosah
-- Create date: 11 Jan 2017
-- Description:	Insert Log Fee Transation
-- =============================================
CREATE PROCEDURE [dbo].[REPS_LogTransaction]
				 @dealID int,
				 @transactionTypeID int,
				 @transactionStatusID int,
				 @workflowTaskID int,
				 @userID int
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
			   ,@transactionTypeID	
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

	SELECT @TransactionID;
END
