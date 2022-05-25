-- =============================================
-- Author: Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Person Info to person table 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddPerson]
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
			@DateCreated datetime,
			@Deleted bit,
			@DealID int= null,
			@ParticipantRoleID int = null,
			@Identity int OUTPUT
AS
    DECLARE	@AlreadyIn int = ''
    -- First we need to check whether the row exists in the database table or not.
    SELECT @AlreadyIn = (SELECT COUNT(*)
							FROM[dbo].[Participant] Part
							INNER JOIN [dbo].[Person] Per 
							ON Part.PersonID = Per.PersonID

							WHERE [GivenName] = @GivenName  
							AND [FamilyName] = @FamilyName
							AND [ParticipantRoleID] =@ParticipantRoleID
							AND [DealID] =@DealID
							AND [Email] =@Email )
    -- If the row exists, the select query will return 1 otherwise it will return 0.
    -- In this way, we can avoid inserting duplicate records.   
    IF(@AlreadyIn = 0)
		BEGIN
			INSERT INTO [dbo].[Person]
					(			
						 [OrganizationID]
						,[TitleID]
						,[GivenName]
						,[FamilyName]
						,[IdentityTypeID]
						,[IdentityNumber]
						,[PassportNumber]
						,[PassportCountryID]
						,[TaxID]
						,[BirthDate]
						,[BirthPlace]
						,[Telephone]
						,[FaxNumber]
						,[MobileNumber]
						,[Email]
						,[JobTitleID]
						,[Verified]
						,[DateCreated]
						,[Deleted]
					)
				VALUES
					(
						@OrganizationID,
						@TitleID,
						@GivenName,
						@FamilyName,
						@IdentityTypeID,
						@IdentityNumber,
						@PassportNumber,
						@PassportCountryID,
						@TaxID,
						@BirthDate,
						@BirthPlace,
						@Telephone,
						@FaxNumber,
						@MobileNumber,
						@Email,
						@JobTitleID,
						@Verified,
						@DateCreated,
						@Deleted
					)
				SET @Identity = SCOPE_IDENTITY()
				-- Return the existance status.
				--SELECT @AlreadyIn 'ExistanceStatus'
		END

	ELSE IF(@AlreadyIn != 0)
		BEGIN 
			-- Return the existance status.
			SET @Identity = 0
		END 

