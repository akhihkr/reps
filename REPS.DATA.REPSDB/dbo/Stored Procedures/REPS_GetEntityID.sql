-- ===========================================
-- Author:Pravina Barosah
-- Create date: 16 Jan 2016
-- Description:	Get EntityID by EntityGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetEntityID]
	@entityGUID varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT EN.[EntityID]
		FROM [dbo].[Entity] EN
		WHERE  EN.EntityGUID = @entityGUID 
GO