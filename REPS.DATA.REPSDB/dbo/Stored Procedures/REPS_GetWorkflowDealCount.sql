-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Deal Count Per Workflow
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkflowDealCount]
     @UserID int=null,
     @WorkflowID int=null,
     @DealProcessTaskID int = null,  
     @EntityID int=null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--end of fetch EnetityID from user database by user session 

	SELECT TaskGUID,TaskID,TaskName,MAX(DealCount) DealCount FROM
	(
		--Get Count per Task ID
		Select T.[TaskGUID][TaskGUID],  WT.[TaskID] , T.[TaskName] , Count(D.DealID) DealCount 
		
		FROM [dbo].[WorkflowProgress] WP

			INNER JOIN (
				Select [WorkflowTaskID],[TaskID] 
				from [WorkflowTask]
			)WT ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]

			INNER JOIN (
				Select [TaskID] ,[TaskGUID],[TaskName]
				from [Task]
			) T ON WT.[TaskID] = T.[TaskID]

			INNER JOIN (
				Select [DealID],[DealProcessTaskID] from [Deal]
				) D 
			ON WP.[DealID] = D.[DealID]

		WHERE  WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.WorkflowProgressID) WorkflowProgressID

			FROM [dbo].[WorkflowProgress]  WP

			INNER JOIN (
				Select [WorkflowTaskID],[TaskID],[WorkflowID] 
				from [dbo].[WorkflowTask]
			) WT ON WP.WorkflowTaskID = WT.WorkflowTaskID

			INNER JOIN (
				Select [UserID],[EntityID] from [User]
				) U ON WP.[UserID] = U.[UserID]

			WHERE
				WP.[Deleted]=0		
				AND (@UserId IS NULL OR  U.[UserID] =  @UserId) --get data per user if @UserId is not null							
				AND (@WorkflowID IS NULL OR WT.[WorkflowID] = @WorkflowID)	--workflow status from workflow table
				AND (@DealProcessTaskID IS NULL OR  D.[DealProcessTaskID] =  @DealProcessTaskID) --deal status from deal table
				/* for current company only */
				AND ([EntityID] = @EntityID)
			GROUP BY WP.[DealID]
		)
		GROUP BY T.[TaskGUID],WT.[TaskID],T.[TaskName] 

	UNION ALL

		--Get All Count per Task ID
		SELECT  T.[TaskGUID],WT.[TaskID],T.[TaskName],'0' DealCount

		FROM [dbo].[WorkflowTask] WT

		INNER JOIN (
			Select [WorkflowID] 
			from [dbo].[Workflow]
		) W ON WT.[WorkflowID] = W.[WorkflowID]

		INNER JOIN (
			Select [TaskID],[TaskName],[TaskGUID] 
			from [dbo].[Task]
		) T	ON WT.[TaskID]=T.[TaskID]

		WHERE W.[WorkflowID] = @WorkflowID
	) 
	As allTaskCounts

	GROUP BY TaskGUID,TaskID,TaskName
	Order By TaskID