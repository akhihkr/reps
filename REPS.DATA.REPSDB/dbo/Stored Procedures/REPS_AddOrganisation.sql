-- =============================================
-- Author: Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Company Details to organisation table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddOrganisation]
               @OrganizationTypeID int,
               @Name varchar(100),
               @RegistrationNumber varchar(20),
               @LegalName varchar(100),
               @VatID varchar(100),
               @Telephone numeric(18,0),
               @FaxNumber numeric(18,0),
               @Email varchar(200),
               @Verified bit,
               @DateCreated datetime,
               @Deleted bit,
               @EntityID int,
			   @DealID int= null,
			   @ParticipantRoleID int = null,
               @Identity int OUTPUT
AS
	DECLARE	@AlreadyIn int = ''
    -- First we need to check whether the row exists in the database table or not.
    SELECT @AlreadyIn = (SELECT COUNT(*)
							FROM[dbo].[Participant] Part
							INNER JOIN [dbo].[Organization] Org 
							ON Part.OrganizationID = Org.OrganizationID

							WHERE [RegistrationNumber] = @RegistrationNumber  
							AND [ParticipantRoleID] =@ParticipantRoleID
							AND [DealID] =@DealID )
    -- If the row exists, the select query will return 1 otherwise it will return 0.
    -- In this way, we can avoid inserting duplicate records.   
    IF(@AlreadyIn = 0)
	BEGIN
		INSERT INTO [dbo].[Organization]
			   (
					[OrganizationTypeID]
				   ,[Name]
				   ,[RegistrationNumber]
				   ,[LegalName] 
				   ,[VatID]
				   ,[Telephone]
				   ,[FaxNumber]
				   ,[Email]
				   ,[Verified]
				   ,[DateCreated]
				   ,[Deleted]
				   ,[EntityID]
			   )
		 VALUES
			   (
				   @OrganizationTypeID, 
				   @Name,
				   @RegistrationNumber,
				   @LegalName,
				   @VatID, 
				   @Telephone, 
				   @FaxNumber, 
				   @Email,
				   @Verified, 
				   @DateCreated,
				   @Deleted,
				   @EntityID
			   )
			   SET @Identity = SCOPE_IDENTITY()
	END 
	ELSE IF(@AlreadyIn != 0)
		BEGIN 
			-- Return the existance status.
			SET @Identity = 0
		END 
