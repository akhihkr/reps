-- =============================================
-- Author: Pravina Barosah
-- Create date:24 April 2017
-- Description: Get template filename 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetTemplateFilename]
 @documentTemplateID int

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT 	[dbo].DocumentVersion.TemplateFileName

		FROM [dbo].DocumentTemplate 

		INNER JOIN [dbo].DocumentVersion
		ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
  

		WHERE [dbo].[DocumentTemplate].ID = @documentTemplateID
		AND [dbo].[DocumentTemplate].IsDeleted = 0 
GO