-- =================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Properties 
-- =================================
CREATE PROCEDURE [dbo].[REPS_GetProperties]
		@DealID int,
		@PropertyID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT     P.[PropertyID]
				  ,[DealID]
				  ,[PropertyDescription]
				  ,[LegalDescription]
				  ,P.[AddressID]
				  ,P.[PropertyTypeID]
				  ,P.[Verified]
				  ,P.[DateCreated]
				  ,P.[DateModified]
				  ,P.[Deleted]
				  ,A.[AddressLine1][PropertyAddress]
				  ,A.[AddressLine2] 
				  ,A.[City] 
				  ,PT.[Description][PropertyDetails]
				  ,PD.[PropertyDetailID]
				  ,PD.[Geo]	
				  ,PD.[Size]
				  ,P.[PropertyGUID]
				  ,PD.[PropertyDetailGUID]
				  ,A.[AddressGUID]
 
		,ROW_NUMBER() OVER (ORDER BY P.[PropertyID] DESC) AS num
				 FROM [dbo].[Property] P
				 INNER JOIN
					[dbo].[PropertyType] PT
				 ON
					P.PropertyTypeID = PT.PropertyTypeID
				INNER JOIN
					[dbo].[Address] A
				 ON
					P.AddressID = A.AddressID	
				INNER JOIN
					[dbo].PropertyDetail PD
				 ON PD.PropertyID = P.PropertyID


	  ) 
		As allUsers
	  WHERE 
	  (	[DealID]= @DealID	)
		AND
	  (	@PropertyID IS NULL OR [PropertyID]= @PropertyID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO