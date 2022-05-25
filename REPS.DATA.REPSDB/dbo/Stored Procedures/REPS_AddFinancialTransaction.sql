-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Financial Instrument transaction
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_AddFinancialTransaction]
			@InstrumentID int,
			@DealID int,
			@Identity int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO
		[dbo].[FinancialTransaction]
		(	
			[DealID]
			,[InstrumentID]
			,[DateCreated]
			,[Deleted]
		)
		VALUES
		(
			@DealID
			,@InstrumentID
			,GETDATE()
			,0
		);

		SET @Identity = SCOPE_IDENTITY()

END