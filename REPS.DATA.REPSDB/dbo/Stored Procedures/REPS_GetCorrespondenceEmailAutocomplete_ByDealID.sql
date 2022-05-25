-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get the email list from the Participants table for the current DealID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCorrespondenceEmailAutocomplete_ByDealID]
		@DealID INT,
		@Prefix VARCHAR(MAX)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	(
		SELECT
			 PE.GivenName + ' ' + PE.FamilyName + ' - ' + PE.Email
		FROM 
			[Participant] PA
		JOIN
			[Person] PE
		ON
			PA.PersonID = PE.PersonID
		WHERE
			[DealID] = @DealID
		AND
		(
			PE.Email LIKE @Prefix+'%'
			OR
			PE.FamilyName LIKE @Prefix+'%'
			OR
			PE.GivenName LIKE @Prefix+'%'
		)

	)

	UNION

	(
		SELECT
			O.Name + ' - ' + O.Email
		FROM 
			[Participant] PA
		JOIN
			[Organization] O
		ON
			PA.OrganizationID = O.OrganizationID
		WHERE
			[DealID] = @DealID
		AND
		(
			O.Email LIKE @Prefix+'%'
			OR
			O.Name LIKE @Prefix+'%'
		)
	)

END