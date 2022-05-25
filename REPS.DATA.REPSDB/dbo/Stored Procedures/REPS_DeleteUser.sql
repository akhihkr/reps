-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Remove User
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteUser]
			@UserID int,
			@Deleted bit,
			@rowCount INT OUTPUT
		
AS
BEGIN
		UPDATE [dbo].[User]
			SET 				
				[Deleted]= @Deleted
		WHERE [UserID] = @UserID

		SET @rowCount=  @@ROWCOUNT	
END
