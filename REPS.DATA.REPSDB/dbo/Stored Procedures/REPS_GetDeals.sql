-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Change - Deals for workflow only 
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDeals]
	@UserID int  =null,
	@TaskID int=null, 
	@ClientReference varchar(1000)=null, 
	@WorkflowID int=null,
	@DealProcessTaskID int = null,
	@DealId int = null, 
	@UniqueReference varchar(1000)=null,
	@DealTypeId int = null,	
	@EntityID int =null,
	@Start int=null,
	@End int =null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId  AND Deleted = 0
	--end of fetch EnetityID from user database by user session 

	--execute get deal store procedure
	--- if case = view all deal for workflow we select by DealProcessTaskID
	IF @DealProcessTaskID is NUll

		SELECT  [TaskID],[Status],[DealID],[UniqueReference],[ClientReference],[DateCreated],[DealTypeID],[DealType],[DealProcessTaskID],[UserID], COUNT([Participants]) [Participants] FROM
		(
		--Get Count per Task ID
		Select  ROW_NUMBER() OVER(ORDER BY WP.[DealID] )  Num,(WT.[TaskID]) , [TaskName] AS [Status] ,WP.[DealID],D.[DateCreated],D.[ClientReference],D.[DealTypeID],
		DT.[Description] DealType,D.[UniqueReference],
		(Select [ParticipantID] from [Participant] WHERE [Participant].[ParticipantID] = PT.[ParticipantID] AND [Deleted] =0 ) [Participants]		 
		,D.DealProcessTaskID,D.[UserID] [UserID]
		FROM [dbo].[WorkflowProgress] WP

		INNER JOIN (
			Select [WorkflowTaskID],[TaskID] 
			from [dbo].[WorkflowTask]
		)WT ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]

		INNER JOIN (
			Select [TaskID],[TaskName] 
			from [dbo].[Task]
		)T ON WT.[TaskID] = T.[TaskID]
		
		INNER JOIN (
			Select [DealID],[DealTypeID],[Deleted],[DateCreated],[ClientReference],[UniqueReference],[DealProcessTaskID],[UserID] 
			from [dbo].[Deal]
		)D on WP.[DealID] = D.[DealID]

		INNER JOIN (
			Select [DealTypeID],[Description] 
			from [dbo].[DealType]
		) DT ON D.DealTypeID = DT.DealTypeID

		LEFT JOIN (
			Select [ParticipantID],[DealID] 
			from [dbo].[Participant]
		) PT
		

		ON D.[DealID] = PT.[DealID]

		WHERE  
		WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.[WorkflowProgressID]) WorkflowProgressID
			FROM [dbo].[WorkflowProgress]  WP 
			INNER JOIN (
				Select [WorkflowTaskID],[WorkflowID],[TaskID] 
				from [WorkflowTask]
			) WT ON WP.WorkflowTaskID = WT.WorkflowTaskID

			INNER JOIN (
				Select [UserID],[EntityID] 
				from [User]
			) U on WP.[UserID] = U.[UserID]

			LEFT JOIN (
				Select [DealTypeID]
				from [dbo].[DealType]
			)DT 

			ON D.[DealTypeID] = DT.[DealTypeID]		

			WHERE
				WP.[Deleted]=0			
				AND (@WorkflowID IS NULL OR WT.[WorkflowID] = @WorkflowID)	
				AND (@TaskID is NULL OR [TaskID] =@TaskID)
				/* for current company only */
				AND ([EntityID] = @EntityID)
			GROUP BY WP.[DealID]
		)
		AND	D.[Deleted] =0
		GROUP BY WT.[TaskID],[TaskName],WP.DealID,D.[DateCreated],ParticipantID,ClientReference,D.[DealTypeID],DT.[Description],[UniqueReference],[DealProcessTaskID],D.[UserID]
		) AS DealsPerTask

		WHERE 
		 (@UserId IS NULL OR  [UserID] =  @UserId) 
		 AND
		(	@TaskID IS NULL OR [TaskID] = @TaskID	)
		AND
		((@UniqueReference IS null OR @UniqueReference = '') OR [UniqueReference]= @UniqueReference	)
		AND
		(	@ClientReference IS NULL OR [ClientReference]= @ClientReference	)
		AND
		(	@DealId IS NULL OR [DealID]= @DealId	)
		AND
		(	@DealTypeId IS NULL OR [DealTypeID]= @DealTypeId	)
		AND
		(	@Start IS NULL OR Num>= @Start	)
		AND
		(	@End IS NULL OR Num<= @End	)

		GROUP BY [TaskID],[Status],DealID,[DateCreated],ClientReference,[DealTypeID],[DealType],[UniqueReference],[DealProcessTaskID],[UserID]
			ORDER BY [DateCreated] DESC
	
	ELSE 	--- if case = view all deal for specifiec action like initiate etc

		SELECT  [TaskID],[Status],[DealID],[UniqueReference],[ClientReference],[DateCreated],[DealTypeID],[DealType],[DealProcessTaskID],[UserID], COUNT([Participants]) [Participants] FROM
		(
		--Get Count per Task ID
		Select  ROW_NUMBER() OVER(ORDER BY WP.[DealID] )  Num,(WT.[TaskID]) , [TaskName] AS [Status] ,WP.[DealID],D.[DateCreated],D.[ClientReference],D.[DealTypeID],
		DT.[Description] DealType,D.[UniqueReference], 
		(Select [ParticipantID] from [Participant] WHERE [Participant].[ParticipantID] = PT.[ParticipantID] AND [Deleted] =0 ) [Participants]
		,D.DealProcessTaskID,D.[UserID] [UserID]
		FROM [dbo].[WorkflowProgress] WP

		INNER JOIN (
			Select [WorkflowTaskID],[TaskID] from [dbo].[WorkflowTask]
		)WT ON WP.[WorkflowTaskID] = WT.[WorkflowTaskID]

		INNER JOIN (
			Select [TaskID],[TaskName] 
			from [dbo].[Task]
		)T ON WT.[TaskID] = T.[TaskID]
		
		INNER JOIN (
			Select [DealID],[DealTypeID],[Deleted],[DateCreated],[ClientReference],[UniqueReference],[DealProcessTaskID],[UserID] 
			from [dbo].[Deal]
		)D on WP.[DealID] = D.[DealID]

		INNER JOIN (
			Select [DealTypeID],[Description] 
			from [dbo].[DealType]
		) DT ON D.DealTypeID = DT.DealTypeID

		LEFT JOIN (
			Select [ParticipantID],[DealID] 
			from [dbo].[Participant]
		) PT
		ON D.[DealID] = PT.[DealID]

		WHERE  
		WorkflowProgressID IN
		(
			-- Get max workflow progressid Per Deal
			SELECT  MAX(WP.[WorkflowProgressID]) WorkflowProgressID
			FROM [dbo].[WorkflowProgress]  WP
			INNER JOIN (
				Select [WorkflowTaskID],[WorkflowID],[TaskID] from [WorkflowTask]
			) WT ON WP.WorkflowTaskID = WT.WorkflowTaskID

			INNER JOIN (
				Select [UserID],[EntityID] from [User]
			) U on WP.[UserID] = U.[UserID]

			LEFT JOIN (
				Select [DealTypeID]
				from [dbo].[DealType]
			)DT 
			ON D.[DealTypeID] = DT.[DealTypeID]		

			WHERE
				WP.[Deleted]=0 
				AND (@WorkflowID IS NULL OR WT.[WorkflowID] = @WorkflowID)	
				AND (@DealProcessTaskID IS NULL OR  [DealProcessTaskID] =  @DealProcessTaskID) 
				/* for current company only */
				AND
				([EntityID] = @EntityID)
			GROUP BY WP.[DealID]
		)
		AND	D.[Deleted] =0
		GROUP BY WT.[TaskID],[TaskName],WP.DealID,D.[DateCreated],ParticipantID,ClientReference,D.[DealTypeID],DT.[Description],[UniqueReference],[DealProcessTaskID],D.[UserID]
		) AS DealsPerTask

		WHERE 
		 (@UserId IS NULL OR  [UserID] =  @UserId) 
		 AND
		(	@TaskID IS NULL OR [TaskID] = @TaskID	)
		AND
		((@UniqueReference IS null OR @UniqueReference = '') OR [UniqueReference]= @UniqueReference	)
		AND
		(	@ClientReference IS NULL OR [ClientReference]= @ClientReference	)
		AND
		(	@DealId IS NULL OR [DealID]= @DealId	)
		AND
		(	@DealTypeId IS NULL OR [DealTypeID]= @DealTypeId	)
		AND
		(	@Start IS NULL OR Num>= @Start	)
		AND
		(	@End IS NULL OR Num<= @End	)

		GROUP BY [TaskID],[Status],DealID,[DateCreated],ClientReference,[DealTypeID],[DealType],[UniqueReference],[DealProcessTaskID],[UserID]
			ORDER BY [DateCreated] DESC

	--end of execute get deal store procedure