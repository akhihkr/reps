-- =============================================
-- Author:		Hemraj B
-- Create date: 07 Jul 2017
-- Description:	Get Existing User
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetExistingUser]
		@aspNetUserId varchar(1000),
		@EntityID int= null
AS
	BEGIN
		SELECT us.[UserID]
		FROM [dbo].[User] us
		WHERE  us.AspNetUsersId = @aspNetUserId 
		AND US.EntityID = @EntityID
END