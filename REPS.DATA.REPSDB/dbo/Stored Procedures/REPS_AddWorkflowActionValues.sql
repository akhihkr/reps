-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Action values for Add View
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_AddWorkflowActionValues]
		@dealID int,
		@userID int,
		@transactionID int,
		@WorkflowTaskID int,
		@workflowActionVarID int,
		@variableTypeID int,
		@value varchar(max),
		@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO [dbo].[TransactionDetail]
			   ([TransactionID]
			   ,[WorkflowActionVarID]
			   ,[VariableTypeID]
			   ,[Value]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[Deleted])
		 VALUES
			   ( @transactionID
			   , @WorkflowActionVarID
			   ,@VariableTypeID
			   ,@Value
			   ,GETDATE()
			   ,null
			   ,0)

	SET @rowCount = SCOPE_IDENTITY();
END