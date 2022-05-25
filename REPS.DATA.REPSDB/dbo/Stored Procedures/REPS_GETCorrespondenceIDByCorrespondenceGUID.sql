-- ===========================================
-- Author: Kenny
-- Create date: 12 July 2016
-- Description:	Get CorrespondenceID by CorrespondenceGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GETCorrespondenceIDByCorrespondenceGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT
			[ID]
		FROM
			[dbo].[Correspondence]
		WHERE
			[CorrespondenceGUID] = @uniqueReference
GO