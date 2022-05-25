-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update User Info
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateUser]
			@EntityID int= null,
			@TitleID int= null,
			@GivenName varchar(100)= null,
			@FamilyName varchar(100)= null,
			@IdentityNumber varchar(20)= null,
			@PassportNumber varchar(100)= null,
			@PassportCountryID int= null,
			@TaxID varchar(20)= null,
			@BirthDate date= null,
			@BirthPlace varchar(100)= null,
			@Telephone numeric(18,0)= null,
			@MobileNumber numeric(18,0)= null,
			@Email varchar(100)= null,
			@FaxNumber numeric(18,0) = null,
			@JobTitleID int= null,
			@Verified bit= null,
			@UserId int,
			@RoleID int = null,
			@WorkflowID int = null,
			@TokenId varchar(500)= null,
			@rowCount INT OUTPUT

AS
		
		DECLARE @UserRowCount int = 0;

		--Update table user 
		UPDATE
			[dbo].[User]
		SET 
			[EntityID] = IsNull(@EntityID, [EntityID]),
			[TitleID] =  IsNull(@TitleID, [TitleID]),
			[GivenName] = IsNull(@GivenName,[GivenName]),
			[FamilyName] = IsNull(@FamilyName, [FamilyName]), 
			[IdentityNumber] = IsNull(@IdentityNumber, [IdentityNumber]),
			[PassportNumber] = IsNull(@PassportNumber, [PassportNumber]),
			[PassportCountryID] = IsNull(@PassportCountryID, [PassportCountryID]),
			[TaxID] = IsNull(@TaxID , [TaxID]),
			[BirthDate] =  IsNull(@BirthDate , [BirthDate]),
			[BirthPlace] =  IsNull(@BirthPlace, [BirthPlace]),
			[Telephone] =  @Telephone,
			[MobileNumber] =@MobileNumber, 
			[Email] =  IsNull(@Email, [Email]),
			[FaxNumber] =@FaxNumber,
			[JobTitleID] = IsNull(@JobTitleID, [JobTitleID]),
			[Verified] = IsNull(@Verified,[Verified]),
			[DateModified] =  GETDATE(),
			[WorkflowID] = IsNull(@WorkflowID, [WorkflowID]), 
			[TokenId] = ISNULL(@TokenId,[TokenId]) 
		WHERE
			[UserID] = @UserId
			--Added by Hemraj
			AND Deleted = 0
		-- End of Update table user 

		SET @UserRowCount = @UserRowCount + @@ROWCOUNT;

	 --Check if userid existed to userRole table before update
	 IF NOT EXISTS(SELECT [UserID] FROM [dbo].[UserRole] WHERE [UserID]=@UserId)
		 BEGIN
			-- Insert statements for procedure here if userid not existed
			INSERT INTO [dbo].[UserRole]
				   ( [UserID]
				   ,[RoleID]
				   ,[IsActive]
				   ,[DateCreated]
				   ,[Deleted])
			 VALUES
				   (@UserId
				   ,@RoleID
				   ,1
				   ,GETDATE()
				   ,0)
			-- End of Insert statements for procedure here if userid not existed

			SET @UserRowCount = @UserRowCount + @@ROWCOUNT;

		 END
	ELSE
		 BEGIN
		 --update role ID if userid is present in userRole table
 				UPDATE
					[dbo].[UserRole]
				SET
					[RoleID] = @RoleID
				WHERE
					[UserID] = @UserId
				AND
					([RoleID] != @RoleID)
		--End of update role ID if userid is present in userRole table

			SET @UserRowCount = @UserRowCount + @@ROWCOUNT;

		 END
		--End of Check if userid existed to userRole table before update

		IF ( @UserRowCount >= 1 )
			SET @rowCount = @UserRowCount;
GO