-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06-Jan-2016
-- Description:	Insert Entity info of admin 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddEntity]
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
			   @Identity int OUTPUT
AS
BEGIN 

 	INSERT INTO [dbo].[Entity]
					([OrganizationTypeID]
					,[Name]
					,[RegistrationNumber]
					,[LegalName]
					,[AlternateName]
					,[VatID]
					,[Telephone]
					,[FaxNumber]
					,[Email]
					,[AddressTypeID]
					,[AddressLine1]
					,[AddressLine2]
					,[City]
					,[ProvinceID]
					,[CountryID]
					,[PostalCode]
					,[ParentEntityID]
					,[Verified]
					,[Deleted]
					,[DataVerification]
					,[EntityGUID]
					)
		 VALUES
			   (
				@OrganizationTypeID
			   ,@Name
			   ,@RegistrationNumber
			   ,@LegalName
			   ,@AlternateName
			   ,@VatID
			   ,@Telephone
			   ,@FaxNumber
			   ,@Email
			   ,@AddressTypeID
			   ,@AddressLine1
			   ,@AddressLine2
			   ,@City
			   ,@ProvinceID
			   ,@CountryID
			   ,@PostalCode
			   ,@ParentEntityID
			   ,1
			   ,0
			   ,0
			   ,NEWID()
			   )
		SET @Identity = SCOPE_IDENTITY()
END
