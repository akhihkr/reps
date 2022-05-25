-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Financial Instrument
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateFinancialInstrument]
			@InstrumentID int,
			@Participant int,
			@Value decimal,
			@Deposit decimal,
			@TermLoan varchar(max),
			@InterestRate decimal,
			@LenderID int,
			@InstrumentTypeID int,
			@InterestTypeID int,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    -- Insert statements for procedure here
		UPDATE [dbo].[FinancialInstrument]
		   SET 
		       [ParticipantID] = @Participant
			  ,[Value] = IsNull(@Value, [Value])
			  ,[Deposit] = IsNull(@Deposit, [Deposit])
			  ,[InterestRate] = IsNull(@InterestRate, [InterestRate])
			  ,[Term] = IsNull(@TermLoan, [Term])
			  ,[LenderID] = IsNull(@LenderID, [LenderID])
			  ,[InstrumentTypeID] = IsNull(@InstrumentTypeID, [InstrumentTypeID])
			  ,[InterestTypeID] = IsNull(@InterestTypeID, [InterestTypeID])
			  ,[DateModified] = GETDATE()
			  ,[Deleted] = 0
		 WHERE [InstrumentID] = @InstrumentID
END
SET @rowCount=  @@ROWCOUNT
GO