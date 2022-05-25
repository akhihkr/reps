-- =============================================
-- Author:		Kenny
-- Create date: 17.04.2017
-- Description:	Update User Info
-- =============================================
CREATE PROCEDURE [dbo].[REPS_UpdateUser_ToggleHeaderTab_ByUserID]
			@ToggleStatus BIT = null,
			@UserID INT,
			@RowCount INT OUTPUT
AS
		UPDATE
			[dbo].[User]
		SET 
			[HeaderTabToggle] = @ToggleStatus
		WHERE
			[UserID] = @UserID

		SET @RowCount = @@ROWCOUNT
GO