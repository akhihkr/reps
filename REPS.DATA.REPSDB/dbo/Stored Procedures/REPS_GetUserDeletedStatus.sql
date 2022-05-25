-- ===========================================
-- Author: Kenny
-- Create date: 03 April 2017
-- Description:	Get user deleted status
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetUserDeletedStatus]
		@AspNetUsersId varchar(1000) = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT
			[Deleted]
		FROM 
			[dbo].[User]
		WHERE 
			([AspNetUsersId] IS NULL OR  [AspNetUsersId] = @AspNetUsersId)
			AND Deleted = 0
END