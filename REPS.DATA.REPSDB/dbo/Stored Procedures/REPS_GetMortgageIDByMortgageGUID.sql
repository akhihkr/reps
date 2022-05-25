-- ===========================================
-- Author: Kenny
-- Create date: 26 Jan 2017
-- Description:	Get MortgageID by MortgageGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetMortgageIDByMortgageGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[InstrumentID]
	FROM
		[dbo].[FinancialInstrument]
	WHERE
		[InstrumentGUID] = @uniqueReference