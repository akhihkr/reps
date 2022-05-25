-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Alerts details 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateDealCompletionDate_ByDealID]
			@CompletionDate datetime,
			@DealID int,
			@RowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE
		[dbo].[Deal]
	SET
		[CompletionDate] = @CompletionDate
	WHERE
		[DealID] = @DealID

	SET @RowCount = @@ROWCOUNT

END