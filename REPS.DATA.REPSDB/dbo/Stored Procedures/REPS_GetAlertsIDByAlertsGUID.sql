-- ===========================================
-- Author: Kenny
-- Create date: 13 Feb 2017
-- Description:	Get AlertID by AlertGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetAlertsIDByAlertsGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		[ID]
	FROM
		[dbo].[Alerts]
	WHERE
		[AlertsGUID] = @uniqueReference