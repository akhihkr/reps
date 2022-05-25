-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Assign/Unassign RoleAction To Role
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AssignRoleActionToRole]
				@RoleID int,
				@UserActionID int,
				@IsActive bit,
				@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @RoleCount int
	DECLARE @tmptable table (ID int)

	/* Check if Role/Action exists in the database */
	SELECT @RoleCount = COUNT([UserRoleActionID])
	FROM   [dbo].UserRoleAction ua
	WHERE  ua.RoleID = @RoleID AND ua.UserActionID  = @UserActionID
	
	IF  ( @RoleCount > 0 )		/* IF Role/Action exists */
		BEGIN
			IF (@IsActive = 0)		/* IF Remove Action */
					BEGIN
					DElETE FROM [dbo].UserRoleAction OUTPUT deleted.UserRoleActionID into @tmptable  WHERE [RoleID] = @RoleID AND [UserActionID] = @UserActionID
					SELECT @rowCount = ID FROM @tmptable
					END
		END
	ELSE
		IF ( @IsActive = 1)			/* IF Add Action */
		BEGIN
			INSERT INTO [dbo].UserRoleAction VALUES ( @RoleID, @UserActionID )
			SET @rowCount = SCOPE_IDENTITY();
		END
END
