-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Bank Details for participant
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateParticipantBankDetail]
			@ParticipantID int,
			@BankID int,
			@AccountTypeID int,
			@AccountName varchar(100),
			@AccountNumber numeric(18,0),
			@SortCode numeric(18,0),
			@Verified bit,
			@ID int,
			@rowCount INT OUTPUT 

AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
		UPDATE [dbo].[ParticipantBankDetail] 
			SET 
				
				[ParticipantID] = IsNull(@ParticipantID, [ParticipantID]),
				[BankID] = IsNull(@BankID, @BankID),
				[AccountTypeID] = IsNull(@AccountTypeID, [AccountTypeID]),
				[AccountName] = IsNull(@AccountName, [AccountName]),
				[AccountNumber] = IsNull(@AccountNumber, [AccountNumber]),
				[SortCode] = IsNull(@SortCode, [SortCode]),
				[Verified] = IsNull(@Verified, [Verified])
		WHERE [ID] = @ID
END
SET @rowCount=  @@ROWCOUNT