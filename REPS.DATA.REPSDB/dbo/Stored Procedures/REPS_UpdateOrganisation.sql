-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update organisation details
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateOrganisation]
			   @OrganisationID int,
               @OrganizationTypeID int,
               @Name varchar(100),
               @RegistrationNumber varchar(20),
               @LegalName varchar(100),
               @VatID varchar(100),
               @Telephone numeric(18,0),
               @FaxNumber numeric(18,0) = null,
               @Email varchar(200),
               @Verified bit,
               @Deleted bit,
               @EntityID int,
			   @DealID int= null,
			   @ParticipantRoleID int = null,
			   @rowCount INT OUTPUT 
AS
SET NOCOUNT OFF -- enable rowcount return 

 DECLARE	@AlreadyIn int = ''
    -- First we need to assign person id to alreadyin
       (SELECT @AlreadyIn=Org.OrganizationID
							FROM[dbo].[Participant] Part
							INNER JOIN [dbo].[Organization] Org 
							ON Part.OrganizationID = Org.OrganizationID

							WHERE [RegistrationNumber] = @RegistrationNumber  
							AND [ParticipantRoleID] =@ParticipantRoleID
							AND [DealID] =@DealID
							)
    -- If the row exists, the select query will return 1 otherwise it will return 0.
    -- In this way, we can avoid inserting duplicate records.   
 IF(@AlreadyIn = 0 )
	BEGIN
		UPDATE [dbo].[Organization]
					SET [OrganizationTypeID] =IsNull(@OrganizationTypeID, [OrganizationTypeID]),
					[Name] =IsNull(@Name, [Name]),
					[RegistrationNumber] =IsNull(@RegistrationNumber, [RegistrationNumber]),
					[LegalName] =IsNull(@LegalName, [LegalName]),
					[VatID] =IsNull(@VatID, [VatID]),
					[Telephone] =IsNull(@Telephone, [Telephone]),
					[FaxNumber] =@FaxNumber,
					[Email] =IsNull(@Email, [Email]),
					[Verified] =IsNull(@Verified, [Verified]),
					[Deleted] =IsNull(@Deleted, [Deleted]),
					[EntityID] = IsNull(@EntityID, [EntityID]),
					[DateModified] =  GETDATE()

		WHERE @OrganisationID  = [OrganizationID];
		SET @rowCount=  @@ROWCOUNT
	END
	

ELSE IF(@AlreadyIn >0 AND @AlreadyIn = @OrganisationID)
  BEGIN
		UPDATE [dbo].[Organization]
					SET [OrganizationTypeID] =IsNull(@OrganizationTypeID, [OrganizationTypeID]),
					[Name] =IsNull(@Name, [Name]),
					[RegistrationNumber] =IsNull(@RegistrationNumber, [RegistrationNumber]),
					[LegalName] =IsNull(@LegalName, [LegalName]),
					[VatID] =IsNull(@VatID, [VatID]),
					[Telephone] =IsNull(@Telephone, [Telephone]),
					[FaxNumber] =@FaxNumber,
					[Email] =IsNull(@Email, [Email]),
					[Verified] =IsNull(@Verified, [Verified]),
					[Deleted] =IsNull(@Deleted, [Deleted]),
					[EntityID] = IsNull(@EntityID, [EntityID]),
					[DateModified] =  GETDATE()

		WHERE @OrganisationID  = [OrganizationID];
		SET @rowCount=  @@ROWCOUNT
   END

	ELSE IF(@@ROWCOUNT = 0)
		BEGIN 
			-- Return the existance status.
			SET @rowCount = 0
		END 