-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Person
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdatePerson]
			@OrganizationID int,
			@TitleID int,
			@GivenName varchar(100),
			@FamilyName varchar(100),
			@IdentityTypeID int,
			@IdentityNumber varchar(20),
			@PassportNumber varchar(100),
			@PassportCountryID int,
			@TaxID varchar(20),
			@BirthDate date,
			@BirthPlace varchar(100),
			@Telephone numeric(18,0),
			@FaxNumber numeric(18,0),
			@MobileNumber numeric(18,0),
			@Email varchar(100),
			@JobTitleID int,
			@Verified bit,
			@PersonID int,
			@ParticipantRoleID int,
			@DealID int,
			@rowCount INT OUTPUT 
AS

SET NOCOUNT OFF -- enable rowcount return 

 DECLARE	@AlreadyIn int = ''
    -- First we need to assign person id to alreadyin
       (SELECT @AlreadyIn=Per.[PersonID]
							FROM[dbo].[Participant] Part
							INNER JOIN [dbo].[Person] Per 
							ON Part.PersonID = Per.PersonID

							WHERE [GivenName] = @GivenName  
							AND [FamilyName] = @FamilyName
							AND [ParticipantRoleID] =@ParticipantRoleID
							AND [DealID] =@DealID
							AND [Email] =@Email 
							)

    -- If the row exists, the select query will return 1 otherwise it will return 0.
    -- In this way, we can avoid inserting duplicate records.   
 IF(@AlreadyIn = 0 )
  BEGIN
		UPDATE
			[dbo].[Person]
		SET 
			[OrganizationID] = IsNull(@OrganizationID, [OrganizationID]),
			[TitleID] = IsNull(@TitleID, [TitleID]),
			[GivenName] =  IsNull(@GivenName, [GivenName]),
			[FamilyName] =  IsNull(@FamilyName, [FamilyName]),
			[IdentityTypeID] =  IsNull(@IdentityTypeID, [IdentityTypeID]),
			[IdentityNumber] =  IsNull(@IdentityNumber, [IdentityNumber]),
			[PassportNumber] =  IsNull(@PassportNumber, [PassportNumber]),
			[PassportCountryID] =  IsNull(@PassportCountryID, [PassportCountryID]),
			[TaxID] =  IsNull(@TaxID, [TaxID]),
			[BirthDate] =  IsNull(@BirthDate, [BirthDate]),
			[BirthPlace] =  IsNull(@BirthPlace, [BirthPlace]),
			[Telephone] =  IsNull(@Telephone, [Telephone]),
			[FaxNumber] =  IsNull(@FaxNumber, [FaxNumber]),
			[MobileNumber] =  IsNull(@MobileNumber, [MobileNumber]),
			[Email] =  IsNull(@Email, [Email]),
			[JobTitleID] =  IsNull(@JobTitleID, [JobTitleID]),
			[Verified] =  IsNull(@Verified, [Verified]),
			[DateModified] =  GETDATE()
		WHERE
			[PersonID] = @PersonID
		SET @rowCount=  @@ROWCOUNT
  END


ELSE IF(@AlreadyIn >0 AND @AlreadyIn = @PersonID)

  BEGIN
		UPDATE
			[dbo].[Person]
		SET 
			[OrganizationID] = IsNull(@OrganizationID, [OrganizationID]),
			[TitleID] = IsNull(@TitleID, [TitleID]),
			[GivenName] =  IsNull(@GivenName, [GivenName]),
			[FamilyName] =  IsNull(@FamilyName, [FamilyName]),
			[IdentityTypeID] =  IsNull(@IdentityTypeID, [IdentityTypeID]),
			[IdentityNumber] =  IsNull(@IdentityNumber, [IdentityNumber]),
			[PassportNumber] =  IsNull(@PassportNumber, [PassportNumber]),
			[PassportCountryID] =  IsNull(@PassportCountryID, [PassportCountryID]),
			[TaxID] =  IsNull(@TaxID, [TaxID]),
			[BirthDate] =  IsNull(@BirthDate, [BirthDate]),
			[BirthPlace] =  IsNull(@BirthPlace, [BirthPlace]),
			[Telephone] =  IsNull(@Telephone, [Telephone]),
			[FaxNumber] =  IsNull(@FaxNumber, [FaxNumber]),
			[MobileNumber] =  IsNull(@MobileNumber, [MobileNumber]),
			[Email] =  IsNull(@Email, [Email]),
			[JobTitleID] =  IsNull(@JobTitleID, [JobTitleID]),
			[Verified] =  IsNull(@Verified, [Verified]),
			[DateModified] =  GETDATE()
		WHERE
			[PersonID] = @PersonID
		SET @rowCount=  @@ROWCOUNT
  END



	ELSE IF(@@ROWCOUNT = 0)
		BEGIN 
			-- Return the existance status.
			SET @rowCount = 0
		END 
 