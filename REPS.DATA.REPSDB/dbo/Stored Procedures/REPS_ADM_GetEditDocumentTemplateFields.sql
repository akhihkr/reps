-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Edit Document Template Values
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetEditDocumentTemplateFields]
			@documentTemplateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT 
			[dbo].DocumentType.ID AS "DocumentTypeID",	
			[dbo].DocumentTemplate.ID AS "DocumentTemplateID",
			[dbo].DocumentTemplate.DocumentVersionID AS "DocumentVersionID",
			[dbo].DocumentTemplate.TemplateDisplayName,
			[dbo].DocumentTemplate.IsDocfusionTemplate,
			[dbo].DocumentTemplate.IsActive,
			[dbo].DocumentTemplate.TemplateFixGUID,

			[dbo].DocumentVersion.IsStaticDoc,
			[dbo].DocumentVersion.VersionName,
			[dbo].DocumentVersion.TemplateFileName,
			[dbo].DocumentVersion.XMLStoredProc,
			[dbo].DocumentVersion.eSignable,

			[dbo].DocumentType.Description,
			[dbo].DocumentType.OrderBy

		FROM [dbo].DocumentTemplate 

		INNER JOIN [dbo].DocumentVersion
		ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
  
		INNER JOIN [dbo].DocumentType
		ON [dbo].DocumentType.ID = [dbo].DocumentVersion.DocumentTypeID

		WHERE [dbo].[DocumentTemplate].ID = @documentTemplateID
		AND [dbo].[DocumentTemplate].IsDeleted = 0
		ORDER BY [dbo].DocumentType.OrderBy
END