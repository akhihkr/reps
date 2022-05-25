-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get all Province by country ID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetProvince]
		@CountryID int =null,
		@ProvinceID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [ProvinceID]
			   ,[CountryID]
			   ,[Description]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [CountryID] DESC) AS num
				 FROM [dbo].[Province]
	  ) 
		As allUsers
	  WHERE 
	  (@CountryID IS NULL OR [CountryID]=@CountryID)
	  AND
	  (@ProvinceID IS NULL OR [ProvinceID]= @ProvinceID)
		AND
	  (@Start IS NULL OR num>= @Start)
		AND
	  (@End IS NULL OR num<= @End)
	  AND
	  [Deleted] =0	  
GO
