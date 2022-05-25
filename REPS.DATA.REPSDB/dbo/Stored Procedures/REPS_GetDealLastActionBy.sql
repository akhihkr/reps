-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Last Action By
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDealLastActionBy]
	@DealID int = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT TOP 1
		U.[GivenName],T.[DateCreated],TA.TaskName,WT.WorkflowTaskID
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
		U.[GivenName]	,T.[DateCreated],TA.TaskName,WT.WorkflowTaskID	
	ORDER BY
		T.[DateCreated]  DESC

END