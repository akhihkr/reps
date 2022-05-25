-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get current deal reference
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCurrentDealReference_ByDealID]
		@DealID INT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		D.ClientReference
	FROM
		[dbo].[Deal] D
	WHERE
		D.[DealID] = @DealID
END