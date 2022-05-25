-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: GET Count of bank and address per percipant
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetBankAddressCountByParticpantID]
--variables
	@DealID int = null,@ParticipantID int = null
--end of variables

--sql get count 
AS
--sql get count 
	SELECT [participantID],[ParticipantGUID],BankCount, AddressCount,[dealID], [Name]+' '+[LegalName][Company] ,[FamilyName] + ' ' + [GivenName][Individual],[Role],[Deleted]  FROM 
	(
		Select * FROM
		(
			SELECT pa.[ParticipantID] [participantID], pa.[ParticipantGUID] [ParticipantGUID],
			(Select COUNT(ID) from [ParticipantBankDetail] WHERE [ParticipantBankDetail].[ParticipantID] = pa.[ParticipantID] AND [Deleted] =0 ) BankCount,
			(Select COUNT(AddressID) from [Address] WHERE [Address].[ParticipantID] = pa.[ParticipantID] AND [Deleted] =0 ) AddressCount
			,pa.[DealID] [dealID]
			,pr.[Description] [Role]
			,pe.[GivenName] [GivenName]
			,pe.[FamilyName] [FamilyName]
			,org.[Name] [Name]
			,org.[LegalName][LegalName]
			,pa.[Deleted][Deleted]

			FROM [dbo].[Participant] pa
				LEFT JOIN [ParticipantBankDetail] PBD ON pa.[ParticipantID] = PBD.[ParticipantID]
				LEFT JOIN [Person] pe ON pa.[PersonID] = pe.[PersonID]
				LEFT JOIN [dbo].[Address] a ON a.[ParticipantID] = pa.[ParticipantID]
				LEFT JOIN  [dbo].[ParticipantRole] pr ON pa.[ParticipantRoleID] = pr.[ParticipantRoleID]
				LEFT JOIN [Organization] org ON pa.OrganizationID = org.OrganizationID
		  ) 
		As allUsers
		WHERE
			(@DealID IS NULL OR [DealID] = @DealID)
			AND 
			(@ParticipantID IS NULL OR [ParticipantID]= @ParticipantID)
			AND
			[Deleted] =0  
	)asParticipant

	GROUP BY [participantID],[ParticipantGUID],BankCount,AddressCount,[dealID],[Role],[Name]+' '+[LegalName],[FamilyName] + ' ' + [GivenName],[Deleted]

	ORDER BY [participantID] DESC
--end of sql get count
GO
--end of sql get count