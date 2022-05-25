-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert User to User Table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddUser]
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
		@MobileNumber numeric(18,0),
		@Email varchar(100),
		@FaxNumber numeric(18,0) = null,
		@JobTitleID int,
		@Verified bit,
		@DateCreated datetime,
		@DateModified datetime,
		@Deleted bit,
		@AspNetUserId varchar(128)= NULL,
		@WorkflowID int,
		@Identity int OUTPUT,
		@TokenId varchar(500)= null
AS
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
				MobileNumber,
				Email ,
				FaxNumber,
				JobTitleID,
				Verified,
				DateCreated ,
				DateModified ,
				Deleted ,
				AspNetUsersId,
				TokenId,
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
				@MobileNumber ,
				@Email ,
				@FaxNumber,
				@JobTitleID ,
				@Verified ,
				@DateCreated ,
				@DateModified ,
				@Deleted ,
				@AspNetUserId,
				@TokenId,
				1,
				@WorkflowID
		   )

		   SET @Identity =  SCOPE_IDENTITY()
