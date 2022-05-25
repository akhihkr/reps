-- ===========================================
-- Author: Kenny
-- Create date: 14 April 2017
-- Description:	Get DealID by AlertGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetDealIDByAlertsGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[DealID]
	FROM
		[dbo].[Alerts]
	WHERE
		[AlertsGUID] = @uniqueReference