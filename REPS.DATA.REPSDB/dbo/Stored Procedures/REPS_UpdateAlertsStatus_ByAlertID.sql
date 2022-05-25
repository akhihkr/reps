-- ========================================================
-- Author:	Kenny
-- Create date: 07.02.2017
-- Description:	Update Alerts StatusID 
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_UpdateAlertsStatus_ByAlertID]
	@StatusID int,
	@AlertID int,
	@RowCount INT OUTPUT
AS
BEGIN
	UPDATE
		[dbo].[Alerts]
	SET
		[StatusID] = @StatusID
	WHERE
		[ID] = @AlertID

	SET @RowCount = @@ROWCOUNT;
END
