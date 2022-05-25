-- =============================================
-- Author:		PravinaA
-- Create date:  27 Jan 2017
-- Description:	Get detail of workflow deal 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportWorkflowDealPerCountTable]
@UserId int = null, @WorkflowID int = null,@DealProcessTaskID int = null,@StatusID int = null,@EntityID int = null, @StartDate Date = null,@EndDate Date = null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 

	--Get all workflow and user detail
	SELECT TaskID,[Status],DealID,[Date Created],[Unique Reference],[Client Reference],[Created By] FROM
	(
		--Get Count per Task ID
		Select WT.[TaskID] , [TaskName] [Status], (D.DealID) ,WP.DateCreated[Date Created],D.[UniqueReference] [Unique Reference], D.[ClientReference] [Client Reference], U.[GivenName] + ' ' + U.[FamilyName] [Created By]
		
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
				AND (@StatusID IS NULL OR WT.[TaskID] = @StatusID)	--workflow status from workflow table
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
		GROUP BY WT.[TaskID],[TaskName] ,D.[DealID],WP.DateCreated,D.[UniqueReference],D.[ClientReference], U.[GivenName] + ' ' + U.[FamilyName]
	) 
	As allTaskCounts

	GROUP BY TaskID,[Status],DealID,[Date Created],[Unique Reference],[Client Reference],[Created By]
	order by TaskID,DealID
GO