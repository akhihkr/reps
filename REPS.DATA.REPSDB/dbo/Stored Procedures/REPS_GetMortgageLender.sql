-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Mortgage Lender
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetMortgageLender]
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[ID]
		,[Name]
	FROM
		[dbo].[Lenders]

END