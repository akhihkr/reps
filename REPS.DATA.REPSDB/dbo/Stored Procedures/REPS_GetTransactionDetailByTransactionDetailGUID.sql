-- ===========================================
-- Author: Pravina
-- Create date: 10 April 2017
-- Description:	Get TransactionDetailID by TransactionDetailGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetTransactionDetailByTransactionDetailGUID]
	@uniqueReference uniqueidentifier
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		TD.TransactionDetailID
	FROM
		[dbo].[TransactionDetail] TD
	WHERE
		[TransactionDetailGUID] = @uniqueReference