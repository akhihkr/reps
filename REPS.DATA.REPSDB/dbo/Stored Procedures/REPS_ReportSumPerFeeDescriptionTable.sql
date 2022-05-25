-- =============================================
-- Author:		Pravina A
-- Create date: 27-Jan-2017
-- Description:Table : Get sum of all fees per fee type in table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportSumPerFeeDescriptionTable]
	@UserId int =null,
	@EntityID int =null,
	@WorkflowTaskID int =null,
	@FeeTypeID int =null,
	@ValueTypeID int =null,
	@FeeDescID int =null,
	@StartDate Datetime =null,
	@EndDate Datetime =null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 

	Select fee1.[Created By], fee1.[Email], fee1.[Fee Description], SUM(fee2.[Total Sum]) [Total Sum] from
	(
	  SELECT T.DealID [dealid], WP.TransactionID [TransactionID], FT.[Description] [Fee Description], U.[GivenName] + ' ' + U.[FamilyName][Created By],U.[Email][Email], D.[DateCreated][Date Created],FT.[FeeTypeID][FeeDescID]

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

	  GROUP BY FT.[Description],WP.TransactionID ,T.DealID,D.UserID, U.[GivenName] + ' ' + U.[FamilyName],D.[DateCreated],U.[Email],FT.[FeeTypeID]
  
	) fee1
	JOIN 
	(	 
	  SELECT T.DealID [dealid], WP.TransactionID [TransactionID], Sum(isnull(cast(TD.[Value] as float),0)) as [Total Sum], U.[GivenName] + ' ' + U.[FamilyName][Created By],U.[Email][Email],D.[DateCreated][Date Created]
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

	  GROUP BY T.DealID , WP.TransactionID ,TD.[Value], D.[DateCreated],D.UserID,U.[GivenName] + ' ' + U.[FamilyName],U.[Email]
	)fee2

	ON fee1.TransactionID =fee2.TransactionID
	WHERE   
			/* compare dates */
			(@StartDate IS NULL OR fee2.[Date Created] >= @StartDate )
			AND 
			(@EndDate IS NULL OR fee2.[Date Created] <= @EndDate)
			AND 
			(@FeeDescID IS NULL OR fee1.[FeeDescID] = @FeeDescID)
	GROUP BY fee1.[Fee Description],fee1.[Email],fee1.[Created By],fee1.[FeeDescID]