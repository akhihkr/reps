-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: modify and activate user
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateReactivateUser]
		@EntityID int,
		@TitleID int,
		@GivenName varchar(100),
		@FamilyName varchar(100),
		@IdentityNumber varchar(20),
		@PassportNumber varchar(100),
		@PassportCountryID int,
		@TaxID varchar(20),
		@BirthDate date,
		@BirthPlace varchar(100),
		@Telephone numeric(18,0),
		@Mobile numeric(18,0),
		@FaxNumber numeric(18,0),
		@JobTitleID int,
		@Verified bit,
		@DateModified datetime,
		@AspNetUserId varchar(128) = NULL,
		@WorkflowID int,
		@Email varchar(100),
		@DateCreated datetime
AS
	DECLARE @CounterCheck INT;
	DECLARE @EntityCheck INT;

	SET @CounterCheck = 
	(
		SELECT
			COUNT(TaxID)
		FROM
			[dbo].[User]
		WHERE
			[AspNetUsersId] = @AspNetUserId
	)

		SET @EntityCheck = 
	(
		SELECT
			COUNT(TaxID)
		FROM
			[dbo].[User]
		WHERE
			[AspNetUsersId] = @AspNetUserId
			AND EntityID = @EntityID
	)

	IF (@CounterCheck <> 0 AND @EntityCheck <> 0) --If more than 1 record, set all record to Deleted = 1 and if there is entity update where aspnetId and entity id matches 
	
		BEGIN
			UPDATE
				[dbo].[User]
			SET 
				[Deleted] = 1
			WHERE
				[AspNetUsersId] = @AspNetUserId


			UPDATE
				[dbo].[User]
			SET
				EntityID = @EntityID, TitleID = @TitleID, GivenName = @GivenName, FamilyName = @FamilyName, IdentityNumber = @IdentityNumber, PassportNumber = @PassportNumber,
				PassportCountryID = @PassportCountryID, TaxID = @TaxID, BirthDate = @BirthDate, BirthPlace = @BirthPlace, Telephone = @Telephone, FaxNumber = @FaxNumber,
				MobileNumber = @Mobile,JobTitleID = @JobTitleID, Verified = @Verified, DateModified = @DateModified, Deleted = 0, [WorkflowID] = @WorkflowID
			WHERE
				[AspNetUsersId] = @AspNetUserId
				AND EntityID = @EntityID
		END


	ELSE IF (@CounterCheck <> 0 AND @EntityCheck = 0) --If more than 1 record, set all record to Deleted = 1 and if there is no entity for specific User, update the most recent record
			BEGIN
			UPDATE
				[dbo].[User]
			SET 
				[Deleted] = 1
			WHERE
				[AspNetUsersId] = @AspNetUserId


			UPDATE
				[dbo].[User]
			SET
				EntityID = @EntityID, TitleID = @TitleID, GivenName = @GivenName, FamilyName = @FamilyName, IdentityNumber = @IdentityNumber, PassportNumber = @PassportNumber,
				PassportCountryID = @PassportCountryID, TaxID = @TaxID, BirthDate = @BirthDate, BirthPlace = @BirthPlace, Telephone = @Telephone, FaxNumber = @FaxNumber,
				MobileNumber = @Mobile,JobTitleID = @JobTitleID, Verified = @Verified, DateModified = @DateModified, Deleted = 0, [WorkflowID] = @WorkflowID
			WHERE
				[AspNetUsersId] = @AspNetUserId
				AND [UserID] = (Select top 1 [UserID] From [User]
				where [AspNetUsersId] = @AspNetUserId 
				Order By UserId Desc)
		END

	--IF (@CounterCheck = 0)
	ELSE
	BEGIN
		INSERT INTO [dbo].[User]
		(
			EntityID,
			TitleID,
			GivenName,
			FamilyName,
			IdentityNumber,
			PassportNumber,
			PassportCountryID,
			TaxID,
			BirthDate,
			BirthPlace,
			Telephone ,
			FaxNumber,
			Email ,
			MobileNumber,
			JobTitleID,
			Verified,
			DateCreated ,
			DateModified ,
			Deleted ,
			AspNetUsersId,
			[HeaderTabToggle],
			[WorkflowID]
		)
		 VALUES
		(
			@EntityID ,
			@TitleID ,
			@GivenName ,
			@FamilyName ,
			@IdentityNumber ,
			@PassportNumber ,
			@PassportCountryID ,
			@TaxID ,
			@BirthDate ,
			@BirthPlace ,
			@Telephone ,
			@FaxNumber ,
			@Email ,
			@Mobile,
			@JobTitleID ,
			@Verified ,
			@DateCreated ,
			@DateModified ,
			0,
			@AspNetUserId,
			1,
			@WorkflowID
		)
	END