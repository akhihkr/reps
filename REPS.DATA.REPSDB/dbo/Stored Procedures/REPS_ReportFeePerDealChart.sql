-- =============================================
-- Author:		PravinaA
-- Create date: 27-Jan-2017
-- Description:	Get sum of all fees per deal type
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportFeePerDealChart]
	@UserId int =null,
	@EntityID int =null,
	@WorkflowTaskID int =null,
	@DealProcessTaskID int =null,
	@VariableTypeID int =null,
	@sum bigint=null,
	@StartDate Datetime =null,
	@EndDate Datetime =null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId AND Deleted = 0

--check if value is greater than 1
SELECT @sum= Sum(value)
	FROM
		(
			--generate sum of fees 
			SELECT Sum(isnull(cast(suma as float),0)) as value , label  FROM
			(	
				SELECT  T.DealID dealid, WP.TransactionID ,TD.[Value] suma, DT.[Description] label,CONVERT(VARCHAR(10), D.[DateCreated], 120) dateStart, U.UserID userID

				FROM [dbo].[WorkflowProgress] WP
					INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
					INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
					INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
					INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
					INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
					INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
					INNER JOIN [User] U on WP.[UserID] = U.[UserID]
				WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
					AND TD.[VariableTypeID] = @VariableTypeID --values
					AND (@DealProcessTaskID IS NULL OR D.DealProcessTaskID = @DealProcessTaskID )
					AND TD.Deleted = 0
					AND
					(@UserId IS NULL OR  D.UserID =  @UserId)
					/* for current company only */
					AND
					([EntityID] = @EntityID)

				GROUP BY T.DealID , WP.TransactionID ,TD.[Value],DT.[Description], D.[DateCreated], U.UserID
			)allps

			WHERE 
					/* compare dates */
					(@StartDate IS NULL OR dateStart >= @StartDate )
					AND 
					(@EndDate IS NULL OR dateStart <= @EndDate)
			GROUP BY label --label = dealtype description : Get sum of all fees per deal type
		)allSumPerFeeType

--excutes sql if value is more than 1
IF(@sum > 0)
	BEGIN
		--generate sum of fees 
		SELECT Sum(isnull(cast(suma as float),0)) as value , label  FROM
		(	
			SELECT  T.DealID dealid, WP.TransactionID ,TD.[Value] suma, DT.[Description] label,CONVERT(VARCHAR(10), D.[DateCreated], 120) dateStart, U.UserID userID

			FROM [dbo].[WorkflowProgress] WP
				INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
				INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
				INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
				INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
				INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
				INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
				INNER JOIN [User] U on WP.[UserID] = U.[UserID]
			WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
				AND TD.[VariableTypeID] = @VariableTypeID --values
				AND (@DealProcessTaskID IS NULL OR D.DealProcessTaskID = @DealProcessTaskID )
				AND TD.Deleted = 0
				AND
				(@UserId IS NULL OR  D.UserID =  @UserId)
				/* for current company only */
				AND
				([EntityID] = @EntityID)

			GROUP BY T.DealID , WP.TransactionID ,TD.[Value],DT.[Description], D.[DateCreated], U.UserID
		)allps

		WHERE 
				/* compare dates */
				(@StartDate IS NULL OR dateStart >= @StartDate )
				AND 
				(@EndDate IS NULL OR dateStart <= @EndDate)
		GROUP BY label --label = dealtype description : Get sum of all fees per deal type
	END


--ELSE
--BEGIN 
-- SELECT 0 AS value, NULL AS label
--END