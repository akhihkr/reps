-- ========================================================
-- Author:	Kenny
-- Create date: 12.01.2017
-- Description:	Update Alerts details 
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_UpdateLastActiveView_ByDealGUIDUserGUID]
			@UserID int,
			@DealID int,
			@ViewName varchar(max),
			@RowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE
		[dbo].[UserDeal]
	SET
		[LastViewName] = @ViewName,
		[DateModified] = GETDATE()
	WHERE
		[DealID] = @DealID
	AND
		[UserID] = @UserID

	SET @RowCount = @@ROWCOUNT

END