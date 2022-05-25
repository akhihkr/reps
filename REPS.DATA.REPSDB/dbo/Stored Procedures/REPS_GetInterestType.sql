-- ========================================================
-- Author:	Kenny
-- Create date: 25.01.2017
-- Description:	Get Interest Type
-- ========================================================

CREATE PROCEDURE [dbo].[REPS_GetInterestType]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[InterestTypeID]
		,[Description]
	FROM
		[dbo].[InterestType]
END