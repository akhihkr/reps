-- =============================================
-- Author:	Abhinav
-- Create date: 29 June 2016
-- Description:	Insert Payment values for Add View
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddPayment]
		@dealID int,
		@transactionID int,
		@userID int,
		@WorkflowTaskID int,
		@workflowActionVarID int,
		@variableTypeID int,
		@value varchar(max),
        @identity int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

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
			   , @workflowActionVarID
			   ,@variableTypeID
			   ,@value
			   ,GETDATE()
			   ,null
			   ,0)
			   
    SET @identity = SCOPE_IDENTITY()
END