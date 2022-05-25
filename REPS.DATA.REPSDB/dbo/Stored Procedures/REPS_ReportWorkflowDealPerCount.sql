-- =============================================
-- Author:		Pravina Barosah
-- Create date: 23-08-2016
-- Description:	Report : Workflow Per Deal Count
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportWorkflowDealPerCount]
	--declare variables
	@UserID int = null, @WorkflowID int = null,@DealProcessTaskID int = null,@EntityID int = null,@TaskID int = null, @sum bigint =null , @StartDate Date = null,@EndDate Date = null

AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--end of fetch EnetityID from user database by user session 

--calculate total sum of workflow
SELECT @sum = sum(value)
FROM
(
 
	SELECT TaskID,[label],MAX([value]) [value] FROM
	(
		--Get Count per Task ID
		Select WT.[TaskID] , [TaskName] [label], Count(D.DealID) [value]
		
		FROM [dbo].[WorkflowProgress] WP

			INNER JOIN [dbo].[WorkflowTask] WT 
			ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]

			INNER JOIN [Task] T 
			ON WT.[TaskID] = T.[TaskID]

			INNER JOIN [Deal] D 
			ON WP.[DealID] = D.[DealID]

			INNER JOIN [User] U 
			ON WP.[UserID] = U.[UserID]

		WHERE  WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.WorkflowProgressID) WorkflowProgressID

			FROM [dbo].[WorkflowProgress]  WP

			INNER JOIN [dbo].[WorkflowTask] WT 
			ON WP.WorkflowTaskID = WT.WorkflowTaskID

			WHERE
				WP.[Deleted]=0		
				AND (@UserId IS NULL OR  U.[UserID] =  @UserId) --get data per user if @UserId is not null							
				AND (@WorkflowID IS NULL OR WT.[WorkflowID] = @WorkflowID)	--workflow status from workflow table
				AND (@DealProcessTaskID IS NULL OR  D.[DealProcessTaskID] =  @DealProcessTaskID) --deal status from deal table
				/* for current company only */
				AND ([EntityID] = @EntityID)
				/* compare dates */
				AND 
				(@StartDate IS NULL OR Convert(DATE,D.[DateCreated]) >= @StartDate )
				AND 
				(@EndDate IS NULL OR Convert(DATE,D.[DateCreated]) <= @EndDate) 

			GROUP BY WP.[DealID]
			)
			GROUP BY WT.[TaskID],[TaskName] 

	UNION

		--Get All Count per Task ID
		SELECT  WT.[TaskID],T.[TaskName],'0' DealCount

		FROM [dbo].[WorkflowTask] WT

		INNER JOIN [dbo].[Workflow] W 	
		ON WT.[WorkflowID] = W.[WorkflowID]

		INNER JOIN [dbo].[Task] T 	
		ON WT.[TaskID]=T.[TaskID]

		WHERE W.[WorkflowID] = @WorkflowID
		AND (WT.[TaskID] != @TaskID)
	) 
	As allTaskCounts

	GROUP BY TaskID,[label]

	--Order By TaskID
 

) as sumValueTask
	GROUP BY value

 --execute the sql if value is not 0
 if(@sum > 0)
 BEGIN 
	SELECT TaskID,[label],MAX([value]) [value] FROM
	(
		--Get Count per Task ID
		Select WT.[TaskID] , [TaskName] [label], Count(D.DealID) [value]
		
		FROM [dbo].[WorkflowProgress] WP

			INNER JOIN [dbo].[WorkflowTask] WT 
			ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]

			INNER JOIN [Task] T 
			ON WT.[TaskID] = T.[TaskID]

			INNER JOIN [Deal] D 
			ON WP.[DealID] = D.[DealID]

			INNER JOIN [User] U 
			ON WP.[UserID] = U.[UserID]

		WHERE  WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.WorkflowProgressID) WorkflowProgressID

			FROM [dbo].[WorkflowProgress]  WP

			INNER JOIN [dbo].[WorkflowTask] WT 
			ON WP.WorkflowTaskID = WT.WorkflowTaskID

			WHERE
				WP.[Deleted]=0		
				AND (@UserId IS NULL OR  U.[UserID] =  @UserId) --get data per user if @UserId is not null							
				AND (@WorkflowID IS NULL OR WT.[WorkflowID] = @WorkflowID)	--workflow status from workflow table
				AND (@DealProcessTaskID IS NULL OR  D.[DealProcessTaskID] =  @DealProcessTaskID) --deal status from deal table
				/* for current company only */
				AND ([EntityID] = @EntityID)
				/* compare dates */
				AND 
				(@StartDate IS NULL OR Convert(DATE,D.[DateCreated]) >= @StartDate )
				AND 
				(@EndDate IS NULL OR Convert(DATE,D.[DateCreated]) <= @EndDate) 

			GROUP BY WP.[DealID]
			)
			GROUP BY WT.[TaskID],[TaskName] 

	UNION

		--Get All Count per Task ID
		SELECT  WT.[TaskID],T.[TaskName],'0' DealCount

		FROM [dbo].[WorkflowTask] WT

		INNER JOIN [dbo].[Workflow] W 	
		ON WT.[WorkflowID] = W.[WorkflowID]

		INNER JOIN [dbo].[Task] T 	
		ON WT.[TaskID]=T.[TaskID]

		WHERE W.[WorkflowID] = @WorkflowID
		AND (WT.[TaskID] != @TaskID)
	) 
	As allTaskCounts

	GROUP BY TaskID,[label]

	Order By TaskID
 END
GO