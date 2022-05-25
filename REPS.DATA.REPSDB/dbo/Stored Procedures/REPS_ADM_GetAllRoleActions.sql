-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all Role Actions
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetAllRoleActions]
			@roleID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserActionID], [Code], [Description], MAX(Assigned) Assigned
	FROM
	-- Get all User Role Actions which are not assigned
		 (		SELECT 
					 ua.[UserActionID], 
					 ua.[Code],
					 ua.[Description],
					 '1' AS 'Assigned'
					FROM [dbo].[Role] ro			
					INNER JOIN [dbo].[UserRoleAction] ur
					ON ro.[RoleID] = ur.[RoleID]
					INNER JOIN [dbo].[UserAction] ua
					ON ur.[UserActionID] = ua.[UserActionID]
					WHERE  ro.[RoleID]= @RoleID
					AND
					ro.[Deleted] =0	
		UNION

		-- Get assigned User Role Actions
	    SELECT 
			ua.[UserActionID],
			ua.[Code],
			ua.[Description],
			'0' AS 'Assigned'
		
			FROM [dbo].[UserAction] ua
			WHERE ua.[UserActionID] not in ( 	
												SELECT 
													 ua.[UserActionID]
		
													FROM [dbo].[Role] ro			
													INNER JOIN [dbo].[UserRoleAction] ur
													ON ro.[RoleID] = ur.[RoleID]
													INNER JOIN [dbo].[UserAction] ua
													ON ur.[UserActionID] = ua.[UserActionID]
													WHERE  ro.[RoleID]= @RoleID
													AND
													ro.[Deleted] = 0 
										    ) 

			)
		AS R GROUP BY  UserActionID, [Code], [Description]
		ORDER BY UserActionID
	 
END
