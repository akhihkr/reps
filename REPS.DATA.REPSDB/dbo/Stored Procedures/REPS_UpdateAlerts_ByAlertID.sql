-- ========================================================
-- Author:	Kenny
-- Create date: 07.02.2017
-- Description:	Update Alerts details
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_UpdateAlerts_ByAlertID]
	@EventName varchar(200),
	@Location varchar(2000),
	@StartDate datetime,
	@EndDate datetime,
	@Description varchar(max),
	@AlertID int,
	@rowCount INT OUTPUT
AS
BEGIN

	UPDATE
		[dbo].[Alerts]
	SET
		[EventName] = @EventName,
		[Location] = @Location,
		[StartDate] = @StartDate,
		[EndDate] = @EndDate,
		[Comment] = @Description
	WHERE
		([ID] = @AlertID)
	AND
		([EventName] != @EventName OR [Location] != @Location OR [StartDate] != @StartDate OR [EndDate] != @EndDate OR [Comment] != @Description)

	SET @rowCount =  @@ROWCOUNT
END