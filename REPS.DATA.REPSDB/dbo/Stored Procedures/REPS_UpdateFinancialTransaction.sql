-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Financial Instrument
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateFinancialTransaction]
			@DealID int,
			@ObligationID int,
			@InstrumentID int,
			@FinancialTransactionID int,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    -- Insert statements for procedure here
		UPDATE [dbo].[FinancialTransaction]
			SET 
					[DealID] = IsNull(@DealID, [DealID]),
					[ObligationID] = IsNull(@ObligationID, [ObligationID]),
					[InstrumentID] = IsNull(@InstrumentID, [InstrumentID]),
					[DateModified] =  GETDATE()

		WHERE [FinancialTransactionID] = @FinancialTransactionID
 
END
SET @rowCount=  @@ROWCOUNT
GO