-- =============================================
-- Author:		PravinaA
-- Create date: 27-Jan-2017
-- Description:	Chart Report : Get sum of all fees per fee type
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportSumPerFeeDescriptionChart] 
	@UserId int =null,
	@EntityID int =null,
	@WorkflowTaskID int  =null,
	@FeeTypeID int =null,
	@ValueTypeID int =null,
	@FeeDescID int =null,
	@sum bigint=null,
	@StartDate Datetime =null,
	@EndDate Datetime =null
AS

	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--check if sum is greater than 1
	SELECT @sum = SUM(value) 
		FROM
		(
			Select   fee1.label, SUM(fee2.[value]) [value] from
			(
			  SELECT  WP.TransactionID, FT.[Description] label,FT.[FeeTypeID][FeeDescID]
			  FROM [dbo].[WorkflowProgress] WP
				   INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
				   INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
				   INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
				   INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
				   INNER JOIN [dbo].[FeeType] FT ON  FT.[FeeTypeID] =  TD.[Value]
				   INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
				   INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
				   INNER JOIN [User] U on WP.[UserID] = U.[UserID]
			  WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
					AND TD.[VariableTypeID] = @FeeTypeID ---fees type
					AND TD.Deleted = 0
					AND
					(@UserId IS NULL OR  D.UserID =  @UserId)
					/* for current company only */
					AND
					([EntityID] = @EntityID)
			  GROUP BY FT.[Description],WP.TransactionID,FT.[FeeTypeID]
			) fee1
			JOIN 
			(
			  SELECT   WP.TransactionID [TransactionID], Sum(isnull(cast(TD.[Value] as float),0)) [value] ,D.UserID [userID], D.[DateCreated][startDate]
			  FROM [dbo].[WorkflowProgress] WP
				   INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
				   INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
				   INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
				   INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
				   INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
				   INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
				   INNER JOIN [User] U on WP.[UserID] = U.[UserID]
			   WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
				   AND TD.[VariableTypeID] = @ValueTypeID --values
				   AND TD.Deleted = 0
					AND
					(@UserId IS NULL OR  D.UserID =  @UserId)
					/* for current company only */
					AND
					([EntityID] = @EntityID)

			  GROUP BY  WP.TransactionID ,TD.[Value], D.[DateCreated],D.UserID
			)fee2
			ON fee1.TransactionID =fee2.TransactionID
			WHERE   
					/* compare dates */
					(@StartDate IS NULL OR fee2.startDate >= @StartDate )
					AND 
					(@EndDate IS NULL OR fee2.startDate <= @EndDate)
					AND 
					(@FeeDescID IS NULL OR fee1.[FeeDescID] = @FeeDescID)
			GROUP BY fee1.label,fee1.[FeeDescID]
			)mergeFeeType
--execute store procedure if greater than one
IF(@sum > 0)
	BEGIN
		Select   fee1.label, SUM(fee2.[value]) [value] from
		(
		  SELECT  WP.TransactionID, FT.[Description] label,FT.[FeeTypeID][FeeDescID]

		  FROM [dbo].[WorkflowProgress] WP

			   INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
			   INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
			   INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
			   INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
			   INNER JOIN [dbo].[FeeType] FT ON  FT.[FeeTypeID] =  TD.[Value]
			   INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
			   INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
			   INNER JOIN [User] U on WP.[UserID] = U.[UserID]

		  WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
				AND TD.[VariableTypeID] = @FeeTypeID ---fees type
				AND TD.Deleted = 0
				AND
				(@UserId IS NULL OR  D.UserID =  @UserId)
				/* for current company only */
				AND
				([EntityID] = @EntityID)

		  GROUP BY FT.[Description],WP.TransactionID,FT.[FeeTypeID]
  
		) fee1
		JOIN 
		(
		  SELECT   WP.TransactionID [TransactionID], Sum(isnull(cast(TD.[Value] as float),0)) [value] ,D.UserID [userID], D.[DateCreated][startDate]

		  FROM [dbo].[WorkflowProgress] WP

			   INNER JOIN [dbo].[Transaction] T ON WP.TransactionID = T.TransactionID
			   INNER JOIN [dbo].[WorkflowTask] WT ON WT.WorkflowTaskID = WP.WorkflowTaskID
			   INNER JOIN [dbo].[Task] TA ON WT.[TaskID] = TA.[TaskID]
			   INNER JOIN [dbo].[TransactionDetail] TD ON  T.[TransactionID] = TD.[TransactionID]
			   INNER JOIN [dbo].[Deal] D ON D.DealID = T.DealID 
			   INNER JOIN [dbo].[DealType] DT ON DT.DealTypeID = D.DealTypeID  
			   INNER JOIN [User] U on WP.[UserID] = U.[UserID]

		   WHERE WP.WorkflowTaskID = @WorkflowTaskID  --Fees
			   AND TD.[VariableTypeID] = @ValueTypeID --values
			   AND TD.Deleted = 0
				AND
				(@UserId IS NULL OR  D.UserID =  @UserId)
				/* for current company only */
				AND
				([EntityID] = @EntityID)

		  GROUP BY  WP.TransactionID ,TD.[Value], D.[DateCreated],D.UserID
		)fee2
		ON fee1.TransactionID =fee2.TransactionID
		WHERE   
				/* compare dates */
				(@StartDate IS NULL OR fee2.startDate >= @StartDate )
				AND 
				(@EndDate IS NULL OR fee2.startDate <= @EndDate)
				AND 
				(@FeeDescID IS NULL OR fee1.[FeeDescID] = @FeeDescID)
		GROUP BY fee1.label,fee1.[FeeDescID]
	END