-- =============================================
-- Author: Pravina Barosah
-- Create date:26 April 2017
-- Description: Get document type list 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentType]
	@workflowID int
AS
    -- Insert statements for procedure here
	SELECT DISTINCT [dbo].DocumentType.ID,
			[dbo].DocumentType.[Description],
			[dbo].DocumentType.OrderBy,
			[dbo].[WorkflowTask].WorkflowID

		FROM  [dbo].[WorkflowTask]
		
		INNER JOIN [dbo].[WorkflowActionMap] WAM
		ON [dbo].[WorkflowTask].WorkflowTaskID = WAM.WorkflowTaskID 

		INNER JOIN [dbo].[WorkflowAction] WA
		ON WAM.WorkflowActionID = WA.ID

		INNER JOIN [dbo].[DocumentWorkflow]
		ON [dbo].[DocumentWorkflow].WorkflowStepID = WA.ID

		INNER JOIN [dbo].DocumentTemplate
		ON [dbo].DocumentTemplate.DocumentVersionID = [dbo].[DocumentWorkflow].DocumentVersionID

		INNER JOIN [dbo].DocumentVersion
		ON [dbo].DocumentVersion.ID =  [dbo].DocumentTemplate.DocumentVersionID
  
		INNER JOIN [dbo].DocumentType
		ON [dbo].DocumentType.ID = [dbo].DocumentVersion.DocumentTypeID

		WHERE 
		(@workflowID IS NULL OR [dbo].[WorkflowTask].WorkflowID =@workflowID)

		ORDER BY [dbo].DocumentType.OrderBy
GO
