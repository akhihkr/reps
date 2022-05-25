-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Countries details
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCountries]
		@CountryID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [CountryID]
			  ,[ISOCode]
			  ,[Description]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]  
 
		,ROW_NUMBER() OVER (ORDER BY [CountryID] ASC) AS num
				 FROM [dbo].[Country]
	  ) 
		As allres
	  WHERE 
	  (	@CountryID IS NULL OR [CountryID]= @CountryID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO


