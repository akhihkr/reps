-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Change - Deals for workflow only 
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDealSumByTask]
	@UserID int=null,
	@WorkflowID int=null,
	@EntityID int=null

AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--end of fetch EnetityID from user database by user session 


	SELECT TaskID,TaskName,MAX(DealCount) DealCount FROM
	(
		--Get Count per Task ID
		Select WT.[TaskID] , [TaskName] , Count(DealID) DealCount from [dbo].[WorkflowProgress] WP
		INNER JOIN [dbo].[WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
		INNER JOIN [Task] T ON WT.TaskID = T.TaskID

		WHERE  WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.WorkflowProgressID) WorkflowProgressID
			FROM [dbo].[WorkflowProgress]  WP
			INNER JOIN [dbo].[WorkflowTask] WT ON WP.WorkflowTaskID = WT.WorkflowTaskID
			INNER JOIN [User] U on WP.[UserID] = U.[UserID]
			WHERE
				WP.Deleted=0
				AND (@UserID IS NULL OR U.[UserID] =@UserID)
				AND WT.WorkflowID = @WorkflowID
				/* for current company only */
				AND
				([EntityID] = @EntityID)
			GROUP BY DealID
		)
		GROUP BY WT.[TaskID],[TaskName] 

	UNION

		--Get All Count per Task ID
		SELECT 
		WT.[TaskID],T.[TaskName],'0' DealCount

		FROM [dbo].[WorkflowTask] WT
		INNER JOIN [dbo].[Workflow] W 	ON WT.[WorkflowID] = W.[WorkflowID]
		INNER JOIN [dbo].[Task] T 	ON WT.[TaskID]=T.[TaskID]
		WHERE W.[WorkflowID] = @WorkflowID
	) 
	As allTaskCounts
	GROUP BY TaskID,TaskName
	Order By TaskID