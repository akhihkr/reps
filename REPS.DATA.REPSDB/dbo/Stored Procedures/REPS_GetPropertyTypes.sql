-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Properties Types
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetPropertyTypes]
		@PropertyTypeID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [PropertyTypeID]
			  ,[Description]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [PropertyTypeID] DESC) AS num
				 FROM [dbo].[PropertyType]
	  ) 
		As allUsers
	  WHERE 
	  (	@PropertyTypeID IS NULL OR [PropertyTypeID]= @PropertyTypeID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO	
