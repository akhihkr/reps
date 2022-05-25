-- =============================================
-- Author:		Pravina
-- Create date: 27 Jan 2017
-- Description:	get deal by user id for dropdown
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportGetUsers]

		@UserID int = null, 
		@EntityID int = null

AS
	Select * FROM
	(
		SELECT u.[UserID] AS ReportsId
		  ,[EntityID]
		  ,[GivenName] +  ' '  +[FamilyName] AS 'Description'
		  ,u.[Deleted]
		  ,[AspNetUsersId]   
 
		,ROW_NUMBER() OVER (ORDER BY [EntityID] DESC) AS num
				 FROM [dbo].[User] u
				 LEFT JOIN [dbo].[UserRole] ur
				 ON u.[UserID] = ur.[UserID]
	  ) 
		As allUsers
	  WHERE 


	  (	@UserID IS NULL OR ReportsId= @UserID	)
		AND
	  (	@EntityID IS NULL OR [EntityID]= @EntityID	)
		AND

	  [Deleted] =0

	  ORDER BY ReportsId DESC
	  
GO



