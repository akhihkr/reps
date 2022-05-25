-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert data to workflow progress 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateDealStatusInWorkflowProgress]
				 @dealID int,
				 @workflowTaskID int,
				 @userID int
				,@Identity int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
			,null
			,null
			,@userID
			,GETDATE()
			,null
			,0)

	SET @Identity = SCOPE_IDENTITY()

END