-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document Template List by WorkflowID and DealID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentTemplateListByWorkflow]
			@workflowID int,
			@dealID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT 
			[dbo].DocumentType.ID,	
			[dbo].Document.DealID,
			[dbo].DocumentTemplate.ID AS "DocumentTemplateID",
			[dbo].DocumentTemplate.TemplateDisplayName,
			[dbo].DocumentTemplate.IsDocfusionTemplate,
			[dbo].[User].GivenName + ' ' + [dbo].[User].FamilyName AS "CreatedByUser",
			[dbo].DocumentVersion.IsStaticDoc,
			[dbo].DocumentVersion.VersionName,
			[dbo].DocumentVersion.eSignable,
			[dbo].Document.ID AS "DocumentID",
			[dbo].Document.GeneratedDocFileName,
			[dbo].Document.SignedDocFileName,
			[dbo].Document.DateCreated,
			[dbo].Document.MimeTypeID,
			[dbo].Document.CreatedByUserID,
			[dbo].DocumentType.Description,
			[dbo].DocumentType.OrderBy

		FROM [dbo].DocumentTemplate 

		INNER JOIN [dbo].[DocumentWorkflow] 
		ON [dbo].DocumentTemplate.DocumentVersionID = [dbo].[DocumentWorkflow].DocumentVersionID
		AND DocumentTemplate.IsDeleted = 0

		INNER JOIN [dbo].DocumentVersion
		ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
  
		INNER JOIN [dbo].DocumentType
		ON [dbo].DocumentType.ID = [dbo].DocumentVersion.DocumentTypeID
		
		LEFT JOIN [dbo].[User]
		ON [dbo].DocumentVersion.CreatedByUserID = [dbo].[User].UserID

	    LEFT JOIN [dbo].Document
		ON [dbo].Document.DocumentTemplateID = [dbo].DocumentTemplate.ID
		AND [dbo].Document.DealID = @dealID
		AND Document.IsDeleted =0 

		WHERE [dbo].[DocumentWorkflow].WorkflowStepID = @workflowID
		--AND ( [dbo].Document.DealID = @dealID  OR [dbo].Document.DealID is null)
		--AND DocumentTemplate.IsDeleted = 0
		--AND (Document.IsDeleted =0 OR Document.IsDeleted is null)
		ORDER BY [dbo].DocumentTemplate.DisplayOrder
END