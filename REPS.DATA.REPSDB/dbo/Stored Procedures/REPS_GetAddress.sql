-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: get address details
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAddress]
		@participantID int = null,
		@addressID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT   [AddressID]
				  ,[ParticipantID]
				  ,A.[AddressTypeID]
				  ,AT.[Description][State]
				  ,[AddressLine1]
				  ,[AddressLine2]
				  ,[City]
				  ,A.[ProvinceID]
				  ,Pro.[Description][Province]
				  ,A.[CountryID]
				  ,C.[Description][Country]
				  ,[PostalCode]
				  ,A.[DateCreated]
				  ,A.[DateModified]
				  ,A.[Deleted]
				  ,A.[AddressGUID]
			
 
		,ROW_NUMBER() OVER (ORDER BY [AddressID] DESC) AS num
		FROM [dbo].[Address] A
			  INNER JOIN [dbo].[AddressType] AT
			  ON A.[AddressTypeID] = AT.[AddressTypeID]
			  LEFT JOIN [dbo].[Province] Pro
			  ON A.ProvinceID = Pro.ProvinceID
			  LEFT JOIN [dbo].[Country] C
			  ON A.CountryID = C.CountryID
	  ) 
		As allUsers
	  WHERE 
	  (	@participantID IS NULL OR [ParticipantID]= @participantID	)
		AND
	  (	@addressID IS NULL OR [AddressID]= @addressID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO
