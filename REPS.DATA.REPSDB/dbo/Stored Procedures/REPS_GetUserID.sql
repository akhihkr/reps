-- ===========================================
-- Author:Pravina Barosah
-- Create date: 09 Jan 2016
-- Description:	Get UserID by ASpnetID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetUserID]
		@aspNetUserId varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT us.[UserID]
		FROM [dbo].[User] us
		WHERE  us.AspNetUsersId = @aspNetUserId 
		AND Deleted = 0
GO