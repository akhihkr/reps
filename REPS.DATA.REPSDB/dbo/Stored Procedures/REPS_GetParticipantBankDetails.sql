-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Participant Bank Details
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetParticipantBankDetails]
			@ParticipantID int = null,
			@Start  int = null,
			@End  int = null			
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT pa.[ParticipantID]
		,pa.[ParticipantGUID]
		,pa.[DealID]

		,pb.ID
		,pb.[BankID]
		,ba.[BankName]
		
		,pb.[AccountTypeID]
		,pb.[ID] AS 'ParticipantBankDetailID'
		,at.[Description] AccountType
		,pb.[AccountName]
		,pb.[AccountNumber]
		,pb.[SortCode]
		,pb.[Verified] VerifiedBank
			
		,pe.[TitleID]
		,pe.[GivenName]
		,pe.[FamilyName]
		,pa.[Deleted]
					
 
		,ROW_NUMBER() OVER (ORDER BY pa.[ParticipantID] DESC) AS num
				 	FROM [dbo].[Participant] pa

					LEFT JOIN [dbo].[Person] pe
					ON pa.[PersonID] = pe.[PersonID]
					LEFT JOIN [dbo].[Title] ti
					ON pe.[TitleID] = ti.[TitleID]
					LEFT JOIN [dbo].[Organization] org
					ON pa.[OrganizationID] = org.[OrganizationID]
					LEFT JOIN [dbo].[Address] ad
					ON pa.[ParticipantID] = ad.[ParticipantID]
					LEFT JOIN [dbo].[ParticipantBankDetail] pb
					ON pa.[ParticipantID] = pb.[ParticipantID]
					AND pb.[Deleted] = 0
					INNER JOIN [dbo].[BankorCreditUnion] ba
					ON pb.[BankID] = ba.[BankID]
					LEFT JOIN [dbo].AccountType at
					ON pb.[AccountTypeID] = at.[AccountTypeID]
	  ) 
		As alls
	  WHERE 
	  (	[ParticipantID]= @ParticipantID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0

END