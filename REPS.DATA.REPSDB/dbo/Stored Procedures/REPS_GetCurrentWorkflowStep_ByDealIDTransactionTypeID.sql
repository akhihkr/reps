-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Current Workflow
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCurrentWorkflowStep_ByDealIDTransactionTypeID]
 @DealID INT = null, @WorkflowID int = null, @userID int = null, @EntityID int = null, @StartDate int = null,@EndDate int = null

AS
BEGIN
	/* SET NOCOUNT ON added to prevent extra result sets from*/
	/* interfering with SELECT statements.*/
	SET NOCOUNT ON;

	DECLARE @CounterCheck INT;


	/*fetch EnetityID from user database by user session */
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 

	/*check if any transaction of workflow type existed */
	SET @CounterCheck = 
	(
		SELECT COUNT(TaskName)
				FROM [WorkflowProgress] WP
				INNER JOIN [WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
				INNER JOIN [Transaction] T ON WP.TransactionID = T.TransactionID		 
				INNER JOIN [Task] TA ON WT.TaskID = TA.TaskID
				INNER JOIN [User] U on WP.[UserID] = U.[UserID]
				INNER JOIN [Deal] D ON D.DealID = T.DealID 

			WHERE WP.DealID=@DealID 	
			AND WP.Deleted=0
			AND (WT.WorkflowID = @WorkflowID) --3000 workflow
			AND (D.UserID IN ( SELECT [UserID] FROM [dbo].[User] WHERE [EntityID] = @EntityID)) 
			AND
			(	
				/* compare dates */
				@StartDate IS NULL OR  D.DateCreated>= @StartDate 
				AND 
				@EndDate IS NULL OR  D.DateCreated<= @EndDate 
			) 
	)
	/*if transaction of workflow type exist, fetch lastest insert data by desc*/
	IF (@CounterCheck <> 0)
		BEGIN
			SELECT TaskName,MAX(WP.DateCreated) DateCreated, U.GivenName,T.TransactionTypeID, WP.WorkflowTaskID ,D.UserID
				FROM [WorkflowProgress] WP
				INNER JOIN [WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
				INNER JOIN [Transaction] T ON WP.TransactionID = T.TransactionID				
				INNER JOIN [Task] TA ON WT.TaskID = TA.TaskID
				INNER JOIN [User] U on WP.[UserID] = U.[UserID]
				INNER JOIN [Deal] D ON D.DealID = T.DealID 

				WHERE WP.DealID=@DealID 	
				AND WP.Deleted=0
				AND (WT.WorkflowID = @WorkflowID )--3000 workflow
				AND (D.UserID IN ( SELECT [UserID] FROM [dbo].[User] WHERE [EntityID] = @EntityID)) 
				AND
				(	
					/* compare dates */
					@StartDate IS NULL OR  D.DateCreated>= @StartDate 
					AND 
					@EndDate IS NULL OR  D.DateCreated<= @EndDate 
				) 
				GROUP BY TaskName,U.GivenName,T.TransactionTypeID, WP.WorkflowTaskID ,D.UserID,WP.DateCreated
				order by DateCreated  DESC
		END
	/*otherwise set workflow as Initiate Action -- by default*/
	ELSE
		BEGIN
			SELECT '' AS [TaskName], 0 AS [TaskID]
		END
END