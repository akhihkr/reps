-- =============================================
-- Author:		Kenny
-- Create date: 02/03/2017
-- Description:	Get Users Info by ASPID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetUsersByAspId]
		@AspNetUsersId varchar(128)
AS
	
	Select * FROM
	(
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
 
		,ROW_NUMBER() OVER (ORDER BY us.[EntityID] DESC) AS num
		FROM [dbo].[User] us
		Left JOIN [dbo].[UserRole] ur
		ON us.[UserID]=ur.[UserID]
		Left JOIN [dbo].[Entity] en
		ON us.[EntityID]=en.[EntityID]
		left join [dbo].[Person] per
		on us.[IdentityNumber] = per.[IdentityNumber]
	  ) 
		As allUsers
	  WHERE 

	  (	 [AspNetUsersId]= @AspNetUsersId	AND Deleted = 0)

	  
GO



