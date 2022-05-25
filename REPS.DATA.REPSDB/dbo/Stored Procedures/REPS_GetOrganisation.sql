-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: get organisation info
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetOrganisation]
		@OrganizationType int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [OrganizationID]
			  ,[OrganizationTypeID]
			  ,[Name]
			  ,[RegistrationNumber]
			  ,[LegalName]
			  ,[VatID]
			  ,[Telephone]
			  ,[FaxNumber]
			  ,[Email]
			  ,[Verified]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]
			  ,[EntityID]
 
		,ROW_NUMBER() OVER (ORDER BY [OrganizationID] DESC) AS num
				 FROM [dbo].Organization
	  ) 
		As allOrganisation
	  WHERE 
	  (	@OrganizationType IS NULL OR [OrganizationTypeID]= @OrganizationType)
	  AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	  
	  
GO