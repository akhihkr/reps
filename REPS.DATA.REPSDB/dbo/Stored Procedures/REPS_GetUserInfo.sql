-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get User profile information
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetUserInfo]
		@userID int = null ,
		@AspNetUsersId varchar(255) = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT us.[UserID]
		  ,us.[EntityID]
		  ,us.[TitleID]
		  ,us.[GivenName]
		  ,us.[FamilyName]
		  ,us.[IdentityNumber]
		  ,us.[PassportNumber]
		  ,us.[PassportCountryID]
		  ,us.[TaxID]
		  ,us.[BirthDate]
		  ,us.[BirthPlace]
		  ,us.[Telephone]
		  ,us.[MobileNumber]
		  ,us.[FaxNumber]
		  ,us.[Email]
		  ,us.[JobTitleID]
		  ,us.[Verified]
		  ,us.[DateCreated]
		  ,us.[DateModified]
		  ,us.[Deleted]
		  ,us.[AspNetUsersId]
		  ,us.[TokenId]
		  ,ur.[RoleID]
		  ,en.[DataVerification]
		  ,per.[PersonID]
		  ,us.[HeaderTabToggle]
		  ,us.[WorkflowID]
		  ,T.[TaskWorkflowID]
		  ,T.[TaskGUID]
		  ,en.[EntityGUID]
 
		,ROW_NUMBER() OVER (ORDER BY us.[EntityID] DESC) AS num
		FROM [dbo].[User] us
		Left JOIN [dbo].[UserRole] ur
		ON us.[UserID]=ur.[UserID]
		Left JOIN [dbo].[Entity] en
		ON us.[EntityID]=en.[EntityID]
		Left join [dbo].[Person] per
		ON us.[IdentityNumber] = per.[IdentityNumber]
		Left JOIN [dbo].[Task] T
		ON T.[TaskID] = us.[WorkflowID]		

		WHERE 
		(
			( us.[UserID] is null or  us.[UserID]= @userID )
			OR
			( us.AspNetUsersId is null or  us.AspNetUsersId= @AspNetUsersId )
			AND us.Deleted = 0
		)

END