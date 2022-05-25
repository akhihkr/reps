-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get the email list from the Participants table for the current DealID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCorrespondenceNumberAutocomplete_ByDealID]
		@DealID INT,
		@Prefix VARCHAR(MAX)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	(
		SELECT
			 PE.FamilyName + ' ' + PE.GivenName + ' - ' + CONVERT(varchar(MAX), PE.Telephone)
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
			PE.Telephone LIKE @Prefix+'%'
			OR
			PE.FamilyName LIKE @Prefix+'%'
			OR
			PE.GivenName LIKE @Prefix+'%'
		)
	)

	UNION

	(
		SELECT
			O.Name + ' - ' + CONVERT(varchar(MAX), O.Telephone)
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
			O.Telephone LIKE @Prefix+'%'
			OR
			O.Name LIKE @Prefix+'%'
		)
	)

END