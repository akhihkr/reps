CREATE PROCEDURE [dbo].[REPS_UpdateUserToken]
			@TokenId varchar(max)= null,
			@AspNetUsersId varchar(128),
			@rowCount INT OUTPUT 

AS
		UPDATE [dbo].[User]
			SET 
				[TokenId] = IsNull(@TokenId, [TokenId])

		WHERE [AspNetUsersId] = @AspNetUsersId AND Deleted = 0

		SET @rowCount=  @@ROWCOUNT
GO