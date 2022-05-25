-- =============================================
-- Author:	Pravina Barosah
-- Create date: 10 April 2017
-- Description: Remove Img
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteTransactionImg]
			@transactionDetailID int,
			@Deleted bit,
			@rowCount INT OUTPUT		
AS
BEGIN
		UPDATE [dbo].[TransactionDetail]
			SET [Deleted]= @Deleted
		WHERE [TransactionDetailID] = @transactionDetailID

		SET @rowCount=  @@ROWCOUNT	
END
