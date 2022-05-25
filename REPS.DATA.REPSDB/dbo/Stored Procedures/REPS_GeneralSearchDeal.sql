-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: GET DEAL BY SEARCH 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GeneralSearchDeal]
		--varaibles
		@SearchResult varchar(1000)=null,
		@checkboxResult varchar(100) = null,
		@UserId int = null,  
		@EntityID int =null
		--end of varaibles
AS
	SET NOCOUNT ON;

	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--end of fetch EnetityID from user database by user session 

	--if checkbox result is deal
	IF(@checkboxResult = 'deal')
		BEGIN
				SELECT * FROM
				(
					SELECT   DISTINCT(de.[DealID])
									,de.[UniqueReference]
									,de.[ClientReference]
									,de.[DealTypeID]
									,de.[WorkflowTaskID]
									,de.[DateCreated]
									,de.[DateModified]
									,de.[Deleted]
									,u.[EntityID]

 		
							FROM  [dbo].[Deal] de  
									LEFT JOIN [dbo].[User] u   
									ON de.[UserID] = u.[UserID]
							WHERE 
							(de.[UserID] = @UserId	OR  @UserId IS NULL) 	
							AND
							(@EntityID IS NULL OR @EntityID = u.[EntityID])
				) dealDetails
			  WHERE 
				(
					([ClientReference] LIKE '%'+@SearchResult+'%')
					OR
					([UniqueReference] LIKE '%'+@SearchResult+'%')
				)
				AND
				[Deleted] =0	

			ORDER BY [DateCreated] DESC
		END
		--end if checkbox result is deal
		--if checkbox result is participant
		ELSE IF(@checkboxResult = 'participant')
			BEGIN
					SELECT * FROM
					(
						SELECT   DISTINCT(de.[DealID])
										,de.[DateCreated]
										,pe.[GivenName]
										,pe.[FamilyName]
										,de.[Deleted]
										,org.[Name]
										,org.[LegalName]
										,u.[EntityID]
	
								FROM  [dbo].[Deal] de 

										LEFT JOIN [dbo].[User] u  
										ON de.[UserID] = u.[UserID]

										LEFT JOIN [dbo].[Participant] pt 
										ON de.[DealID] = pt.[DealID]

										LEFT JOIN [dbo].[Person] pe
										ON pt.[PersonID] = pe.[PersonID]

										LEFT JOIN [dbo].[Organization] org
										ON pt.[OrganizationID] = org.[OrganizationID]

								WHERE 
								(de.[UserID] = @UserId	OR  @UserId IS NULL) 	
								AND
								(@EntityID IS NULL OR @EntityID = u.[EntityID])
					) dealDetails
				  WHERE 
					(
						([GivenName]  LIKE '%'+@SearchResult+'%')
						OR
						([FamilyName] LIKE '%'+@SearchResult+'%')
						OR
						([LegalName] LIKE '%'+@SearchResult+'%')
						OR
						([Name] LIKE '%'+@SearchResult+'%')
					)
					AND
					[Deleted] =0	
				ORDER BY [DateCreated] DESC
			END
		--end if checkbox result is participant
		--if checkbox result is property
		ELSE IF(@checkboxResult = 'property')
			BEGIN
					SELECT * FROM
					(
						SELECT   DISTINCT(de.[DealID])
										,de.[DateCreated]
										,prty.[PropertyDescription]
										,u.[EntityID]
										,prty.[Deleted]

								FROM  [dbo].[Deal] de 

										LEFT JOIN [dbo].[User] u  
										ON de.[UserID] = u.[UserID]

										LEFT JOIN [dbo].[Property] prty
										ON de.[DealID] = prty.[DealID]

								WHERE 
								(de.[UserID] = @UserId	OR  @UserId IS NULL) 	
								AND
								(@EntityID IS NULL OR @EntityID = u.[EntityID])
					) dealDetails
				  WHERE 
					([PropertyDescription] LIKE '%'+@SearchResult+'%' )
					AND
					[Deleted] =0	
				ORDER BY [DateCreated] DESC
			END
		--end if checkbox result is property
		--if checkbox result is not selected or if all is selected [BY DEFAULT GET ALL INFO FOR DEAL]
		ELSE
			BEGIN
					SELECT * FROM
					(
						SELECT   DISTINCT(de.[DealID])
										,de.[UniqueReference]
										,de.[ClientReference]
										,de.[DealTypeID]
										,de.[WorkflowTaskID]
										,de.[DateCreated]
										,de.[DateModified]
										,pe.[GivenName]
										,pe.[FamilyName]
										,de.[Deleted]
										,org.[Name]
										,org.[LegalName]
										,prty.[PropertyDescription]
										,u.[EntityID]
	
								FROM  [dbo].[Deal] de 

										LEFT JOIN [dbo].[User] u  
										ON de.[UserID] = u.[UserID]

										LEFT JOIN [dbo].[Participant] pt 
										ON de.[DealID] = pt.[DealID]

										LEFT JOIN [dbo].[Person] pe
										ON pt.[PersonID] = pe.[PersonID]

										LEFT JOIN [dbo].[Organization] org
										ON pt.[OrganizationID] = org.[OrganizationID]


										LEFT JOIN [dbo].[Property] prty
										ON de.[DealID] = prty.[DealID]

								WHERE 
								(de.[UserID] = @UserId	OR  @UserId IS NULL) 	
								AND
								(@EntityID IS NULL OR @EntityID = u.[EntityID])
					) dealDetails
				  WHERE 
					(
						([PropertyDescription] LIKE '%'+@SearchResult +'%')
						OR
						([GivenName]  LIKE '%'+@SearchResult+'%')
						OR
						([FamilyName] LIKE '%'+@SearchResult+'%')
						OR
						([LegalName] LIKE '%'+@SearchResult+'%')
						OR
						([Name] LIKE '%'+@SearchResult+'%')
						OR
						([ClientReference] LIKE '%'+@SearchResult +'%')
						OR
						([UniqueReference] LIKE '%'+@SearchResult +'%')
					)
					AND
					[Deleted] =0	
			END
		--end if checkbox result is not selected or if all is selected [BY DEFAULT GET ALL INFO FOR DEAL]
GO
