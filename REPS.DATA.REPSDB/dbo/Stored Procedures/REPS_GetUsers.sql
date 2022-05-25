-- =============================================
-- Author:		Abhinav
-- Create date: 16 January 2017
-- Description:	Get all users
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetUsers]
		@GivenName VARCHAR(100),
		@FamilyName VARCHAR(100),
		@IdentityNumber  VARCHAR(20),
		@UserID int = null,
		@EntityID int = null,
		@emptyEntityId int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT u.[UserID]
		  ,[EntityID]
		  ,[TitleID]
		  ,[GivenName]
		  ,[FamilyName]
		  ,[IdentityNumber]
		  ,[PassportNumber]
		  ,[PassportCountryID]
		  ,[TaxID]
		  ,[BirthDate]
		  ,[BirthPlace]
		  ,[Telephone]
		  ,[MobileNumber]
		  ,[FaxNumber]
		  ,[Email]
		  ,[JobTitleID]
		  ,ur.[RoleID]
		  ,[Verified]
		  ,u.[DateCreated]
		  ,u.[DateModified]
		  ,u.[Deleted]
		  ,[AspNetUsersId]   
 
		,ROW_NUMBER() OVER (ORDER BY [EntityID] DESC) AS num
				 FROM [dbo].[User] u
				 LEFT JOIN [dbo].[UserRole] ur
				 ON u.[UserID] = ur.[UserID]
	  ) 
		As allUsers
	  WHERE 
		(	@GivenName IS NULL OR [GivenName] LIKE @GivenName	)
		AND
		(	@FamilyName IS NULL OR [FamilyName] LIKE @FamilyName	)
		AND
		(	@IdentityNumber IS NULL OR [IdentityNumber] LIKE @IdentityNumber	)
		AND
	  (	@UserID IS NULL OR [UserID]= @UserID	)
		AND
	  (	@EntityID IS NULL OR [EntityID]= @EntityID	)
		AND
	  (	@emptyEntityId IS NULL OR [EntityID] > @emptyEntityId	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0

	  ORDER BY [UserID] DESC
