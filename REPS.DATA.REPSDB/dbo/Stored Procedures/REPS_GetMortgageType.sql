-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Mortgage Type
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetMortgageType]
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[InstrumentTypeID]
		,[Description]
	FROM
		[dbo].[InstrumentType]

END