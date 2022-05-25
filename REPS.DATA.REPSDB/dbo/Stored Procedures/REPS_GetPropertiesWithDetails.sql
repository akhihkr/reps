-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Properties with details
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetPropertiesWithDetails]
		@DealID int ,
		@PropertyID int= null,
		@Start  int = null,
		@End  int = null

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT   
					
					pr.[PropertyID],
					pr.[DealID],
					pr.[PropertyDescription],
					pr.[LegalDescription],
					pr.[AddressID],
					ad.[AddressLine1],
					ad.[AddressLine2],
					ad.[AddressTypeID],
					ad.[CountryID],
					co.[Description] CountryDesc,
					ad.[ProvinceID],
					ad.[City],
					ad.[PostalCode],
					pr.[PropertyTypeID],
					pt.[Description] PropertyType,
					pr.[Verified],
					pr.[DateCreated],
					pr.[DateModified],
					pr.[Deleted],
					pd.[PropertyNumber],
					pd.[PortionNumber],
					pd.[Township],
					pd.[PropertyDetailID],
					pd.[PropertyName],
					pd.[SectionNumber],
					pd.[PlanNumber],
					pd.[UnitNumber],
					pd.[SizeTypeID],
					pd.[RegistrationDivision],
					st.[Description] SizeType,
					pd.[Size],
					pd.[Geo]
 
		,ROW_NUMBER() OVER (ORDER BY pr.[PropertyID] DESC) AS num

					FROM [dbo].[Property] pr
					LEFT JOIN [dbo].[Address] ad
					ON pr.[AddressID] = ad.[AddressID]
					LEFT JOIN [dbo].[PropertyType] pt
					ON pr.[PropertyTypeID] = pt.[PropertyTypeID]
					LEFT JOIN [dbo].[PropertyDetail] pd
					ON pr.[PropertyID] = pd.[PropertyID]
					/*LEFT JOIN [dbo].[Township] tow
					ON pd.[TownshipID] =tow.[TownshipID]*/
					LEFT JOIN [dbo].[SizeType] st
					ON pd.[SizeTypeID] = st.[SizeTypeID]
					LEFT JOIN [dbo].Country co	
					ON ad.CountryID = co.CountryID
	  ) 
		As allUsers
	  WHERE 
	(	[DealID]= @DealID	)
	AND
	(	@PropertyID  IS NULL OR [PropertyID]= @PropertyID)
	AND
	(	@Start IS NULL OR num>= @Start	)
	AND
	(	@End IS NULL OR num<= @End	)
	AND
	[Deleted] =0
END