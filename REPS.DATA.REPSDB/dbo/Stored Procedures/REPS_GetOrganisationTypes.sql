-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: get organisation Types
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetOrganisationTypes]
		@OrganizationType int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [OrganizationTypeID] 
			  ,[Description]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [OrganizationTypeID] DESC) AS num
				 FROM [dbo].[OrganizationType]
	  ) 
		As allUsers
	  WHERE 
	  (	@OrganizationType IS NULL OR [OrganizationTypeID]= @OrganizationType	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO