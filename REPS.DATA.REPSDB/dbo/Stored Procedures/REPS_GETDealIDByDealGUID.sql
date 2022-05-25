-- ===========================================
-- Author:Pravina Barosah
-- Create date: 16 Jan 2016
-- Description:	Get EntityID by EntityGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GETDealIDByDealGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT D.[DealID]
		FROM [dbo].[Deal] D
		WHERE  D.[UniqueReference] = @uniqueReference 
GO