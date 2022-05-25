-- =============================================
-- Author:	Kenny
-- Create date: 22 March 2017
-- Description:	Get all Payment
--				fields saved values for Edit View
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetEditPaymentFields]
		@dealID int,
		@transactionID int,
		@workflowActionID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	
			TD.[TransactionDetailGUID],
			WP.[WorkflowTaskID], 
			WAV.[WorkflowActionID],
			TD.[TransactionID],
			TD.[TransactionDetailID],
			TD.[WorkflowActionVarID],
			WAV.[WorkflowVariableID],
			WAV.IsRequired,
			TD.[Value]
		FROM [dbo].[WorkflowProgress] WP
		INNER JOIN [dbo].[Transaction] TR
		ON WP.TransactionID = TR.TransactionID
		INNER JOIN  [dbo].[TransactionDetail] TD
		ON TR.TransactionID = TD.TransactionID
		INNER JOIN  [dbo].[WorkflowActionVariable] WAV
		ON WAV.ID = TD.WorkflowActionVarID

		WHERE WP.DealID = @dealID
		AND  WAV.WorkflowActionID = @workflowActionID
		AND  TD.[TransactionID] = @transactionID
		--AND  WP.WorkflowTaskID = @taskID
		AND	 TD.Deleted = 0

		GROUP BY 
			TD.[TransactionDetailGUID],
			WP.[WorkflowTaskID], 
			WAV.[WorkflowActionID],
			TD.[TransactionID],
			TD.[TransactionDetailID],
			TD.[WorkflowActionVarID],
			WAV.[WorkflowVariableID],
			WAV.IsRequired,
			TD.[Value]
END