-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Client Care Letter
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DocXML_ClientCareLetter]
		@DealID int

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @XmlResult TABLE(
		Deals XML NULL
	)


	INSERT INTO @XmlResult
	SELECT
	(SELECT '<Deal>') +

	-- Start of General Information
	(SELECT '<FileRef>' + CAST(123 AS varchar) + '</FileRef>') +
	(SELECT '<TotalLegalCosts>' + CAST(450 AS varchar) +'</TotalLegalCosts>') +
	(SELECT '<CurrentDate>'+ CAST(GETDATE() AS varchar)+'</CurrentDate>') +
	-- End of General Information

	-- Start of 'Conveyancers'
	(SELECT '<Conveyancers>') +
	( SELECT ISNULL((
			Select ISNULL([GivenName],[Name] ) AS 'SolicitorName',
				   ISNULL([FamilyName],[LegalName]) AS 'SolicitorSurname',
				   ISNULL([Email],[OrgEmail] ) AS 'SolicitorEmail',
				   ISNULL([Telephone],[OrgTel])  AS 'SolicitorTelNo',
				   ISNULL([FaxNumber],[FaxOrg]) AS 'SolicitorFax',
				   ISNULL([MobileNumber],0) AS 'SolicitorMobile',

				   'Michelle' AS 'SecretaryName',
				   'Calitz' AS 'SecretarySurname',
				   'Ruan' AS 'AssistantName',
				   'Van Jaarsveld' AS 'AssistantSurname'
			 FROM
			(
				SELECT pa.[ParticipantID]
				,pa.[DealID]
				,pa.[Deleted]
				,pe.[GivenName] 
				,pe.[FamilyName]
				,pe.BirthDate
				,pe.TaxID
				,pe.FaxNumber
				,pe.[IdentityNumber]
				,pe.[Email]
				,pe.[MobileNumber]
				,pe.[Telephone]
				,pr.[ParticipantRoleID]
				,org.[Name]
				,org.[LegalName]
				,org.[Telephone][OrgTel]
				,org.[FaxNumber][FaxOrg]
				,org.[Email][OrgEmail]
				,org.[RegistrationNumber]
				,ROW_NUMBER() OVER (ORDER BY [ParticipantID] DESC) AS num

				FROM [dbo].[Participant] pa
					LEFT JOIN [Person] pe ON pa.[PersonID] = pe.[PersonID]
					LEFT JOIN [Organization] org ON org.OrganizationID = pa.OrganizationID
					LEFT JOIN  [dbo].[ParticipantRole] pr ON pa.[ParticipantRoleID] = pr.[ParticipantRoleID]
			  ) 
			As allUsers
			WHERE
				(	[DealID] = @DealID	)
				AND ([ParticipantRoleID] = 40 )		-- Buyer Sollicitor
				AND
				[Deleted] =0  FOR XML PATH('Conveyancer'), ELEMENTS XSINIL
		),0) )
		+ ( SELECT '</Conveyancers>') 
		-- End of 'Conveyancers'
		
		-- Start of 'Buyers'
		+ (SELECT '<Buyers>') +
			( SELECT ISNULL((
			Select ISNULL([GivenName],[Name] ) AS 'ClientName',
				   ISNULL([FamilyName],[LegalName]) AS 'ClientSurname',
				   ISNULL([Email],[OrgEmail] ) AS 'ClientEmail',
				   ISNULL([Telephone],[OrgTel])  AS 'ClientTelNo',
				   ISNULL([FaxNumber],[FaxOrg]) AS 'ClientFax',
				   ISNULL([MobileNumber],0) AS 'ClientMobile'
			 FROM
			(
				SELECT pa.[ParticipantID]
				,pa.[DealID]
				,pa.[Deleted]
				,pe.[GivenName] 
				,pe.[FamilyName]
				,pe.BirthDate
				,pe.TaxID
				,pe.FaxNumber
				,pe.[IdentityNumber]
				,pe.[Email]
				,pe.[MobileNumber]
				,pe.[Telephone]
				,pr.[ParticipantRoleID]
				,org.[Name]
				,org.[LegalName]
				,org.[Telephone][OrgTel]
				,org.[FaxNumber][FaxOrg]
				,org.[Email][OrgEmail]
				,org.[RegistrationNumber]
				,ROW_NUMBER() OVER (ORDER BY [ParticipantID] DESC) AS num

				FROM [dbo].[Participant] pa
					LEFT JOIN [Person] pe ON pa.[PersonID] = pe.[PersonID]
					LEFT JOIN [Organization] org ON org.OrganizationID = pa.OrganizationID
					LEFT JOIN  [dbo].[ParticipantRole] pr ON pa.[ParticipantRoleID] = pr.[ParticipantRoleID]
			  ) 
			As allUsers
			WHERE
				(	[DealID] = @DealID	)
				AND ([ParticipantRoleID] = 90 )		-- Buyer
				AND
				[Deleted] =0  FOR XML PATH('Buyer'), ELEMENTS XSINIL
		),0) )
		+ (SELECT '</Buyers>') +
		-- End of 'Buyers'

		-- Start of 'Properties'
		+ (SELECT '<Properties>') +
			( SELECT ISNULL((
				Select TOP  1 [PropertyAddress] AS 'PropertyDetails'
				 FROM
				(
					SELECT   P.[PropertyID]
							  ,[DealID]
							  ,P.[Deleted]
							  ,P.[PropertyDescription] + ',' + A.[AddressLine1] + ',' + A.[AddressLine2] + ',' + A.[City] + ','  + A.[PostalCode] AS [PropertyAddress]
					,ROW_NUMBER() OVER (ORDER BY P.[PropertyID] DESC) AS num
							 FROM [dbo].[Property] P
							INNER JOIN
								[dbo].[Address] A
							 ON
								P.AddressID = A.AddressID	
				  ) 
					As allUsers
				  WHERE 
				  (	[DealID]= @DealID	)
				  AND
				  [Deleted] =0 FOR XML PATH('Property'), ELEMENTS XSINIL
				),0) 
			 )
		+ (SELECT '</Properties>') +
		-- End of 'Properties'

		-- Start of 'Firm Details'
		+ ( SELECT '<FirmDetails>')

		+ 
			( SELECT ISNULL((
				Select TOP  1 FirmName AS 'FirmName',
							  FirmAddress AS 'FirmAddress',
							  FirmTelNo AS 'FirmTelNo',
							  FirmFaxNo AS 'FirmFaxNo',
							  FirmwebURL AS 'FirmwebURL',
							  DirectorName AS 'DirectorName',
							  DirectorSurname AS 'DirectorSurname',
							  CCManagerName AS 'CCManagerName',
							  CCManagerSurname AS 'CCManagerSurname',
							  CCTitle AS 'CCTitle',
							  CCContactNumber AS 'CCContactNumber',
							  CCEmail AS 'CCEmail',
							  CCAddress AS 'CCAddress',
							  CCWeb AS 'CCWeb'
							  FROM FirmDetails
							  FOR XML PATH('FirmDetails'), ELEMENTS XSINIL
				),0) 
			 )

		+ ( SELECT '</FirmDetails>')
		-- End of 'Firm Details'

		+ ( SELECT '</Deal>')
		-- End of 'Deal'


	SELECT * FROM @XmlResult
	for xml path ('')
END