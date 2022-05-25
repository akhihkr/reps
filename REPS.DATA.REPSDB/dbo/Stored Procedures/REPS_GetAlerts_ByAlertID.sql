-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Alerts details from the Alerts table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAlerts_ByAlertID]
		@AlertID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		A.[ID]
		,A.[AlertsGUID]
		,[StartDate]
		,[EndDate]
		,[EventName]
		,[Comment]
		,[Location]
	FROM
		[dbo].[Alerts] A
	WHERE
		A.ID = @AlertID
END