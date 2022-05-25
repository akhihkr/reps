-- =============================================
-- Author:	Kenny
-- Create date: 22 March 2017
-- Description:	Get Payment list
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetPaymentList]
		@dealID int,
		@workflowTaskID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
			TD.[TransactionDetailGUID],
			TD.[TransactionID],
			TD.[TransactionDetailID],
			TD.[VariableTypeID],
			TD.[Value],
			TD.[DateCreated]
	FROM
		[dbo].[WorkflowProgress] WP
	INNER JOIN
		[dbo].[Transaction] T
	ON
		WP.TransactionID = T.TransactionID
	INNER JOIN
		[dbo].[WorkflowTask] WT
	ON
		WT.WorkflowTaskID = WP.WorkflowTaskID
	INNER JOIN
		[dbo].[Task] TA
	ON
		WT.[TaskID] = TA.[TaskID]
	INNER JOIN
		[dbo].[TransactionDetail] TD
	ON 
		T.[TransactionID] = TD.[TransactionID]
	WHERE
		WP.DealID = @dealID --AND T.TransactionTypeID = 12
		AND WP.WorkflowTaskID = @workflowTaskID
	AND TD.Deleted = 0
	GROUP BY
			TD.[TransactionDetailGUID],
			TD.[TransactionID],
			TD.[TransactionDetailID],
			TD.[VariableTypeID],
			TD.[Value],
			TD.[DateCreated],
			WP.WorkflowTaskID
	ORDER BY
		WP.WorkflowTaskID,
		TD.[TransactionID],
		TD.[VariableTypeID]
END