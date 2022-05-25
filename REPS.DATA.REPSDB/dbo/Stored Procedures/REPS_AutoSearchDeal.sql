-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	GET DEAL BY SEARCH REF SURNAME NAME DGUID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AutoSearchDeal]
		@SearchResult varchar(1000)=null,
		@UserId int = null,  
		@EntityID int =null
		
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 
	--end of fetch EnetityID from user database by user session 

		SELECT TOP 100  * FROM
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
							,u.[EntityID]
							,u.[FamilyName][UserFamilyName]
							,u.[GivenName][UserGivenName]
							,u.[UserID][UserID]
		  		
					FROM  [dbo].[User] u  

							LEFT JOIN [dbo].[Deal] de 
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
		([GivenName]  LIKE '%'+@SearchResult+'%')
		OR
		([FamilyName] LIKE '%'+@SearchResult+'%')
		OR
		([LegalName] LIKE '%'+@SearchResult+'%')
		OR
		([Name] LIKE '%'+@SearchResult+'%')
		OR
		([ClientReference] LIKE '%'+@SearchResult + '%')
		OR
		([UniqueReference] LIKE '%'+@SearchResult + '%')
		OR
		([UserFamilyName] LIKE '%'+@SearchResult + '%')
		OR
		([UserGivenName] LIKE '%'+@SearchResult +'%')
		AND
		[Deleted] =0	

	ORDER BY [DateCreated] DESC
	
GO
