-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Deal type description for dropdown
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDealTypes]
		@DealTypeID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [DealTypeID] 
			  ,[Description]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [DealTypeID] DESC) AS num
				 FROM [dbo].[DealType]
	  ) 
		As allUsers
	  WHERE 
	  (	@DealTypeID IS NULL OR  [DealTypeID] = @DealTypeID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO
