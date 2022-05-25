-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update User Info
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateUser_WorkflowID_ByUserID]
		@WorkflowID INT = null,
		@UserID INT = null

AS
		UPDATE
			[dbo].[User]
		SET 
			[WorkflowID] = @WorkflowID
		WHERE
			[UserID] = @UserID

		SELECT @UserID
GO