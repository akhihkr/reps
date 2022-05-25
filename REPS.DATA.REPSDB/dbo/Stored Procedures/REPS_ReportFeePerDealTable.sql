-- =============================================
-- Author:		PravinaA
-- Create date: 11 May 2017
-- Description:	Get sum of all fees per deal type -Group by user
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportFeePerDealTable]
	@UserId int =null,
	@EntityID int =null,
	@WorkflowTaskID int  =null,
	@DealProcessTaskID int =null,
	@VariableTypeID int =null,
	@StartDate Datetime =null,
	@EndDate Datetime =null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId AND Deleted = 0 

	--generate sum of fees 
	SELECT Sum(isnull(cast(suma as float),0)) as [Total Fees], [Deal Type],[Created By] ,[Contact Number],Email  FROM
	(
		SELECT  TD.[Value] [suma], DT.[Description] [Deal Type], U.[GivenName] + ' ' + U.[FamilyName][Created By],U.[Telephone][Contact Number], U.[Email] Email, D.[DateCreated] [Date Created]

		FROM [dbo].[WorkflowProgress] WP
			INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
			INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
			INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
			INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
			INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
			INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  

			INNER JOIN [User] U on D.[UserID] = U.[UserID]
		WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
			AND TD.[VariableTypeID] = @VariableTypeID --values
			AND (@DealProcessTaskID IS NULL OR D.DealProcessTaskID = @DealProcessTaskID )
			AND TD.Deleted = 0
			AND
			(@UserId IS NULL OR  D.UserID =  @UserId)
			/* for current company only */
			AND
			([EntityID] = @EntityID)

		GROUP BY T.DealID , WP.TransactionID ,TD.[Value],DT.[Description], D.[DateCreated], D.[UserID],U.[GivenName] + ' ' + U.[FamilyName],U.[Telephone], U.[Email] 
	)allps

	WHERE 
			/* compare dates */
			(@StartDate IS NULL OR CONVERT(VARCHAR(10), [Date Created], 120) >= @StartDate )
			AND 
			(@EndDate IS NULL OR CONVERT(VARCHAR(10), [Date Created], 120) <= @EndDate)

	GROUP BY [Deal Type] ,[Created By],[Contact Number], Email   --label = dealtype description : Get sum of all fees per deal type