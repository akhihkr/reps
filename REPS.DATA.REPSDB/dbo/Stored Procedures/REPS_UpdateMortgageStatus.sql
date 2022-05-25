-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Mortgage type 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateMortgageStatus]
	@InstrumentID INT = null,
	 @rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
	UPDATE
		[dbo].[FinancialInstrument]
	SET
		[Deleted] = 1
	WHERE
		[InstrumentID] = @InstrumentID

END
SET @rowCount=  @@ROWCOUNT
GO