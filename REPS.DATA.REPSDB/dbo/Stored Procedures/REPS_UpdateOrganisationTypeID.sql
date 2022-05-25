-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update Organization Type ID details
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateOrganisationTypeID]
			   @OrganisationID int,
               @OrganizationTypeID int,
			   @rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    UPDATE [dbo].[Organization]
				SET [OrganizationTypeID] =IsNull(@OrganizationTypeID, [OrganizationTypeID]),
			    [DateModified] =  GETDATE()

	WHERE @OrganisationID  = [OrganizationID];
 
END
SET @rowCount=  @@ROWCOUNT
GO