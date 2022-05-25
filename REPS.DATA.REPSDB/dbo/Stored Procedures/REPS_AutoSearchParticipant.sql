-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get existing participant names
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AutoSearchParticipant]
		@UserId int,  
		@SearchResult varchar(1000)=null,
		@EntityID int = null,
		@Start  int = null,
		@End  int = null
AS
	--fetch EnetityID from user database by user session 
	IF NULLIF(@EntityID, NULL) IS NULL
	BEGIN
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId AND Deleted = 0
	END

	--get all existing participant details (name/legal name/family & given name)
	SELECT [OrganizationID],[PersonID],[LegalName],[Name],[GivenName],[FamilyName],[ParticipantTypeID],[PersonTelephone],[ORGTelephone],[Description]
	
	FROM
	( 
		SELECT
				ORG.[OrganizationID] [OrganizationID],
				PER.[PersonID] [PersonID],
				ORG.[LegalName] [LegalName],
				ORG.[Name] [Name],
				PER.[GivenName],
				PER.[FamilyName],
				PER.[Telephone][PersonTelephone],
				ORG.[Telephone][ORGTelephone],
				PT.[Description][Description],
				PART.[ParticipantID],
				PART.[ParticipantTypeID][ParticipantTypeID],
				DE.[DealID],
				DE.[UserID]
				,PART.[EntityID][EntityID]

		FROM [dbo].[Participant] PART
			LEFT JOIN  [dbo].[Organization] ORG
			ON PART.OrganizationID = ORG.OrganizationID
			LEFT JOIN [dbo].[Person] PER
			ON PART.[PersonID] = PER.[PersonID]
			INNER JOIN [dbo].[Deal] DE
			ON PART.[DealID] = DE.[DealID]
			INNER JOIN [dbo].[ParticipantType] PT
			ON PT.[ParticipantTypeID] = PART.[ParticipantTypeID]

		  WHERE
		    ([LegalName] LIKE @SearchResult+'%' OR [GivenName]  LIKE @SearchResult+'%' OR [FamilyName] LIKE @SearchResult+'%' OR [Name] LIKE @SearchResult+'%')
			AND
			(@EntityID IS NULL OR PART.[EntityID] = @EntityID)	
			AND
			PART.[Deleted] =0
		
		GROUP BY 
			ORG.[LegalName] ,ORG.[Name],PER.[GivenName],PER.[FamilyName],  
			ORG.[OrganizationID],PER.[PersonID],PART.[ParticipantID],DE.[DealID],
			DE.[UserID],PART.[ParticipantTypeID],PER.[Telephone],ORG.[Telephone],PT.[Description],PART.[EntityID] 
		)allSearchResult

		  WHERE
		  ([LegalName] LIKE @SearchResult+'%' OR [GivenName]  LIKE @SearchResult+'%' OR [FamilyName] LIKE @SearchResult+'%' OR [Name] LIKE @SearchResult+'%')
		  AND
		  (@EntityID IS NULL OR [EntityID] = @EntityID)
		GROUP BY [OrganizationID],[PersonID],[LegalName],[Name],[GivenName],[FamilyName]
		,[ParticipantTypeID],[PersonTelephone],[ORGTelephone],[Description],[EntityID]

GO