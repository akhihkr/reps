-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all details for audit timeline
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAuditDetails_ByDealID]
	@DealID int = null
AS
	SELECT
		TA.[TaskName]
		,T.[TransactionID]
		,T.[DateCreated]
		,T.[DealID]
		,T.[TransactionTypeID]
		,TT.[Description]
		,T.[TransactionStatusID]
		,T.[Deleted]
		,U.[GivenName]
	FROM
		[dbo].[Transaction] T
	JOIN
		[dbo].[TransactionType] TT ON T.[TransactionTypeID] = TT.[TransactionTypeID]
	JOIN
		[dbo].[TransactionStatus] TS ON T.[TransactionStatusID] = TS.[TransactionStatusID]
	JOIN
		[dbo].[WorkflowProgress] WP ON T.[TransactionID] = WP.[TransactionID]
	JOIN
		[dbo].[WorkflowTask] WT ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]
	JOIN
		[dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
	JOIN
		[User] U on WP.[UserID] = U.[UserID]
	WHERE
		T.[DealID] = @DealID
	AND
		WP.[Deleted]=0
	GROUP BY
		TA.[TaskName]
		,U.[GivenName]
		,T.[TransactionID]
		,T.[DateCreated]
		,T.[DealID]
		,T.[TransactionTypeID]
		,TT.[Description]
		,T.[TransactionStatusID]
		,T.[Deleted]
	ORDER BY
		T.[DateCreated]  DESC