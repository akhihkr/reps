-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update entity 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateEntity]
			   @EntityID int,
               @OrganizationTypeID int ,
               @Name varchar(100),
               @RegistrationNumber varchar(20),
               @LegalName varchar(100) = null,
               @AlternateName varchar(100)=null,
               @VatID varchar(100)=null,
               @Telephone numeric(18,0),
               @FaxNumber numeric(18,0) = null,
               @Email varchar(200)=null,
			   @AddressTypeID  int,
			   @AddressLine1  varchar(100),
			   @AddressLine2 varchar(100)=null,
			   @City varchar(30),
			   @ProvinceID int,
			   @CountryID int,
			   @PostalCode varchar(10)=null,
			   @ParentEntityID int,
               @Verified bit,
               @Deleted bit,
			   @rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return
    UPDATE [dbo].[Entity]
				SET [OrganizationTypeID] =@OrganizationTypeID,
				[Name] =@Name,
				[RegistrationNumber] =@RegistrationNumber,
				[LegalName] =@LegalName,
				[AlternateName] =@AlternateName,
				[VatID] =@VatID,
				[Telephone] =@Telephone,
				[FaxNumber] =@FaxNumber,
				[Email] =@Email,
				[Verified] =IsNull(@Verified, [Verified]),
				[AddressTypeID] =@AddressTypeID,
				[AddressLine1] =@AddressLine1,
				[AddressLine2] =@AddressLine2,
				[City] =@City,
				[ProvinceID] =@ProvinceID,
				[CountryID] =@CountryID,
				[PostalCode] =@PostalCode,
				[ParentEntityID] =@ParentEntityID,
				[Deleted] =IsNull(@Deleted, [Deleted])

	WHERE @EntityID  = [EntityID];
END
SET @rowCount=  @@ROWCOUNT