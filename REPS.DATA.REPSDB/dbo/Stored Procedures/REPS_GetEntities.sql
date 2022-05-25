-- =============================================
-- Author:		Abhinav
-- Create date: 17 January 2017
-- Description:	Get Entities
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetEntities]
		@Name AS VARCHAR(100),
		@LegalName AS VARCHAR(100),
		@RegistrationNumber  AS VARCHAR(20),
		@EntityId int = null,
		@emptyEntityId int = null
AS
	Select * FROM
	(
		SELECT [EntityID]
		  ,[OrganizationTypeID]
		  ,[Name]
		  ,[RegistrationNumber]
		  ,[LegalName]
		  ,[AlternateName]
		  ,[VatID]
		  ,[Telephone]
		  ,[FaxNumber]
		  ,[Email]
		  ,[Verified]
		  ,[AddressTypeID]
		  ,[AddressLine1]
		  ,[AddressLine2]
		  ,[City]
		  ,[ProvinceID]
		  ,[CountryID]
		  ,[PostalCode]
		  ,[ParentEntityID]
		  ,[Deleted]
		  ,[DataVerification]
		  ,[EntityGUID]
		  ,ROW_NUMBER() OVER (ORDER BY [EntityID] DESC) AS num
			FROM [dbo].[Entity]
	  ) 
		As allEntities
	  WHERE 
		(	@Name IS NULL OR [Name] LIKE @Name	)
		AND
		(	@LegalName IS NULL OR [LegalName] LIKE @LegalName	)
		AND
		(	@RegistrationNumber IS NULL OR [RegistrationNumber] LIKE @RegistrationNumber	)
		AND
		(	@EntityId IS NULL OR [EntityID]= @EntityId	)	
		AND
	    (@emptyEntityId IS NULL OR [EntityID] > @emptyEntityId)
		AND
		[Deleted] =0

	  ORDER BY [EntityID] DESC
