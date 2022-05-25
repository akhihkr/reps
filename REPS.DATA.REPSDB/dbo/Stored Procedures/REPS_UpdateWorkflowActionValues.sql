-- =============================================
-- Author: Pravina Barosah
-- Create date: 15 - FEB - 2017
-- Description:	Save Action values for Edit View
-- =============================================
CREATE PROCEDURE [dbo].[REPS_UpdateWorkflowActionValues]
		@transactionID int,
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
			   (@transactionID
			   ,@workflowActionVarID
			   ,@variableTypeID
			   ,@value
			   ,GETDATE()
			   ,NULL
			   ,0)

	SET @rowCount = SCOPE_IDENTITY();
END