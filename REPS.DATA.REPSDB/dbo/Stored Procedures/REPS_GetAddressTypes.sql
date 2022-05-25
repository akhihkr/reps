-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: get address types details
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAddressTypes]
		@AddressTypeID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT   [AddressTypeID]
				,[Description]
				,[DateCreated]
				,[DateModified]
				,[Deleted]
			  
		,ROW_NUMBER() OVER (ORDER BY [AddressTypeID] DESC) AS num
				 FROM [dbo].[AddressType]
	  ) 
		As allUsers
	  WHERE 
	  (	@AddressTypeID IS NULL OR [AddressTypeID]= @AddressTypeID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO