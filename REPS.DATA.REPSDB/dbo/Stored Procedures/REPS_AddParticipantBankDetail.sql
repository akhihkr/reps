-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Bank Details for participant to table participant bank details
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddParticipantBankDetail]
		@ParticipantID int, 
        @BankID int, 
        @AccountTypeID int, 
        @AccountName varchar(100),
        @AccountNumber numeric(18,0),
        @SortCode numeric(18,0),
        @Verified bit,
		@Identity int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		INSERT INTO [dbo].[ParticipantBankDetail]
           (
				
			   [ParticipantID]
			   ,[BankID]
			   ,[AccountTypeID]
			   ,[AccountName]
			   ,[AccountNumber]
			   ,[SortCode]
			   ,[Verified]
		   )
     VALUES
           (
				@ParticipantID,
				@BankID,
				@AccountTypeID,
				@AccountName,
				@AccountNumber,
				@SortCode,
				@Verified
		   )
		SET @Identity = SCOPE_IDENTITY()
END
