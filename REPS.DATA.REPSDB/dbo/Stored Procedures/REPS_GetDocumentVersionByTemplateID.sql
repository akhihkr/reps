-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get DocumentVersion By TemplateID 
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentVersionByTemplateID]
			@templateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT [dbo].DocumentVersion.VersionName
			FROM [dbo].DocumentTemplate 

			INNER JOIN [dbo].DocumentVersion
			ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
			WHERE [dbo].[DocumentTemplate].ID = @templateID
			AND [dbo].[DocumentVersion].[IsDeleted] = 0
END