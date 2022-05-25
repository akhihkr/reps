-- =============================================
-- Author:		Kenny
-- Create date: 12/01/2017
-- Description:	Delete all tabs for user by UserID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteAllTabsForUser_ByUserGUID]
		@UserID INT,
		@RowCount INT OUTPUT
AS
	DELETE FROM
		[dbo].[UserDeal]
	WHERE
		[UserID] = @UserID

	SET @RowCount = @@ROWCOUNT

SELECT @UserID