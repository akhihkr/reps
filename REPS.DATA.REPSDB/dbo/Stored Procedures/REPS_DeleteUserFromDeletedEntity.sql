-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Remove User when entity is deleted
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteUserFromDeletedEntity]
			@entityID int,
			@entityNull int,
			@rowCount INT OUTPUT
		
AS
BEGIN
		UPDATE [dbo].[User]
			SET [EntityID]= @entityNull, [TokenId] = NULL
			WHERE [EntityID] = @entityID

		SET @rowCount=  @@ROWCOUNT	
END