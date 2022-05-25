-- =============================================
-- Author:	Kenny
-- Create date: 13 April 2017
-- Description:	Get all user role actions
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetUserRoleActions]
		@RoleID int= null,
		@userID int = null,
		@Start  int = null,
		@End  int = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF( @RoleID is null AND @userID is not null)
	BEGIN
		SELECT @RoleID = [RoleID] FROM [dbo].[UserRole] WHERE  [UserID] = @userID
	END

	Select * FROM
	(
		
		SELECT 
		ua.[Code],ROW_NUMBER() OVER (ORDER BY ro.[RoleID] DESC) AS num
		
		FROM [dbo].[Role] ro			
		INNER JOIN [dbo].[UserRoleAction] ur
		ON ro.[RoleID] = ur.[RoleID]
		INNER JOIN [dbo].[UserAction] ua
		ON ur.[UserActionID] = ua.[UserActionID]
		WHERE 

		 (	@RoleID IS NULL OR ro.[RoleID]= @RoleID	)
		AND
		ro.[Deleted] =0	
	  ) 
		As allUsers
	  WHERE 
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
END