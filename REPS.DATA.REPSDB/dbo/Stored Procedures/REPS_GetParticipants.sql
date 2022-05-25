-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Participant Details
-- ==============================================
CREATE PROCEDURE [dbo].[REPS_GetParticipants]
		@DealID int,
		@ParticipantID int = null,
		@GivenName varchar(100) = '',
		@FamilyName varchar(100) = '',
		@IdentityNumber varchar(20) = '',
		@ParticipantTypeID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT pa.[ParticipantID]
		,pa.[DealID]
		,pa.[ParticipantTypeID]
		,pa.[ParticipantRoleID]
		,pr.[Description] [Role]
		,pa.[PersonID]
		,pa.[OrganizationID]
		,pa.[BankID]	  
		,pa.[DateCreated]
		,pa.[DateModified]
		,pa.[Deleted]
		,pe.[TitleID]
		,ti.[Description] Title
		,pe.[GivenName]
		,org.[Name]
		,pe.[FamilyName]
		,org.[LegalName]
		,pe.BirthDate
		,pe.TaxID
		,pe.FaxNumber
		,pe.[IdentityNumber]
		,ba.[BankName]
		,pe.[Email]
		,org.[Email]	EmailOrg	
		,pe.[MobileNumber]
		,pe.[Telephone]
		,org.[Telephone]	TelephoneOrg
		,pa.[ParticipantGUID]
		,ROW_NUMBER() OVER (ORDER BY [ParticipantID] DESC) AS num

		FROM [dbo].[Participant] pa
			LEFT JOIN [Person] pe ON pa.[PersonID] = pe.[PersonID]
			LEFT JOIN [Title] ti ON pe.[TitleID] = ti.[TitleID]
			LEFT JOIN [BankorCreditUnion] ba ON pa.[BankID] = ba.[BankID]
			LEFT JOIN  [dbo].[ParticipantRole] pr ON pa.[ParticipantRoleID] = pr.[ParticipantRoleID]

			LEFT JOIN [Organization] org ON pa.OrganizationID = org.OrganizationID
	  ) 
	As allUsers
	WHERE
		(	[DealID] = @DealID	)
			AND 
		(	@ParticipantID IS NULL OR [ParticipantID]= @ParticipantID	)
			AND
		(	@GivenName = ''  OR [GivenName] LIKE @GivenName	)
			AND
		(	@FamilyName = ''  OR [FamilyName] LIKE @FamilyName	)
			AND
		(	@IdentityNumber = ''  OR [IdentityNumber] LIKE @IdentityNumber	)
			AND
		(	@ParticipantTypeID IS NULL  OR [ParticipantTypeID] = @ParticipantTypeID	)
			AND		
		(	@Start IS NULL OR num>= @Start	)
			AND
		(	@End IS NULL OR num<= @End	)
		AND
		[Deleted] =0  
	  
GO
