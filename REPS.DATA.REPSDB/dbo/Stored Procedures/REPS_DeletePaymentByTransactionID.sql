-- =============================================
-- Author:	Kenny
-- Create date: 28 March 2017
-- Description:	Delete payment; set deleted 1
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeletePaymentByTransactionID]
		@transactionID INT,
		@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
	UPDATE
		[dbo].[TransactionDetail]
	SET
		[Deleted] = 1
	WHERE
		[TransactionID] = @transactionID

END
SET @rowCount =  @@ROWCOUNT
GO