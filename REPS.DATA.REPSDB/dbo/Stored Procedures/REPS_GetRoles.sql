-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all roles
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetRoles]
		@RoleID int = null,
		@Start  int = null,
		@End  int = null

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT [RoleID]
		  ,[Description]
		  ,[DateCreated]
		  ,[DateModified]
		  ,[Deleted]
		  ,ROW_NUMBER() OVER (ORDER BY [RoleID] DESC) AS num
			FROM [dbo].[Role]
	  ) 
		As allEntities
	  WHERE 
	  (	@RoleID IS NULL OR [RoleID]= @RoleID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)	  	
	   AND
	  [Deleted] =0

	  ORDER BY [RoleID] ASC
END