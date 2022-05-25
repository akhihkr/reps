-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: get participant bank details only 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetBankDetailsPerParticipant]
	--variables
	 @ParticipantID int = null, @ParticipantBankDetailID int =null
	--end of variables

--execute store procedure
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT pa.[ParticipantID]
		,pa.[DealID]

		,pb.ID
		,pb.[BankID]
		,ba.[BankName]
		,pb.[AccountTypeID]
		,pb.[ID] [ParticipantBankDetailID]
		,Acctype.[Description] AccountType
		,pb.[AccountName]
		,pb.[AccountNumber]
		,pb.[SortCode]
		,pb.[Verified] VerifiedBank
		,pa.[Deleted]

		FROM [dbo].[Participant] pa

		LEFT JOIN [dbo].[ParticipantBankDetail] pb
		ON pa.[ParticipantID] = pb.[ParticipantID]

		AND pb.[Deleted] = 0

		INNER JOIN [dbo].[BankorCreditUnion] ba
		ON pb.[BankID] = ba.[BankID]

		LEFT JOIN [dbo].AccountType Acctype
		ON pb.[AccountTypeID] = Acctype.[AccountTypeID]

	  ) As participantBankDetails

	  WHERE 
	  (	 @ParticipantID IS NULL OR [ParticipantID]= @ParticipantID	)
	  AND
	  (	 @ParticipantBankDetailID IS NULL OR ParticipantBankDetailID = @ParticipantBankDetailID	)
	  AND
	  [Deleted] =0
GO