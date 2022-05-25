-- ===========================================
-- Author: Kenny
-- Create date: 27 March 2017
-- Description:	Get TransactionDetailsID from TransactionDetailsGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetTransactionDetailsIDByTransactionDetailsGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[TransactionID]
	FROM
		[dbo].[TransactionDetail]
	WHERE
		[TransactionDetailGUID] = @uniqueReference