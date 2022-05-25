-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document Type List by WorkflowID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentTypeListByWorkflow]
			@workflowID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT [dbo].DocumentType.ID,
			[dbo].DocumentType.Description,
			[dbo].DocumentType.OrderBy

		FROM [dbo].[DocumentWorkflow] 

		INNER JOIN [dbo].DocumentTemplate
		ON [dbo].DocumentTemplate.DocumentVersionID = [dbo].[DocumentWorkflow].DocumentVersionID

		INNER JOIN [dbo].DocumentVersion
		ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
  
		INNER JOIN [dbo].DocumentType
		ON [dbo].DocumentType.ID = [dbo].DocumentVersion.DocumentTypeID

		WHERE [dbo].[DocumentWorkflow].WorkflowStepID = @workflowID
		ORDER BY [dbo].DocumentType.OrderBy

END