-- ===========================================
-- Author:Pravina Barosah
-- Create date: 19 Jan 2016
-- Description:	Get User ID by asp net user id information
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetUserIDByAspNetUserID]
		@AspNetUsersId varchar(1000) = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT us.[UserID]

		FROM [dbo].[User] us	

		WHERE 
			( us.AspNetUsersId IS NULL OR  us.AspNetUsersId= @AspNetUsersId )
END