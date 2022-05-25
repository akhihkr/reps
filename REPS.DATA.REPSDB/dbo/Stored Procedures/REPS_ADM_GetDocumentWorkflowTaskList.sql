-- =============================================
-- Author:	Pravina Barosah
-- Create date: 19 April 2017
-- Description: Get Document Workflow Task Step ID by TemplateID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetDocumentWorkflowTaskList]
	@documentTemplateID int 

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT  DW.WorkflowStepID,
	        WA.DisplayName,
			DocumentTemplate.ID as documentTemplateID,
			DW.ID AS workflowDocumentID

		FROM [dbo].DocumentTemplate 

		INNER JOIN [dbo].[DocumentWorkflow] AS DW
		ON DW.[DocumentVersionID] =  [dbo].DocumentTemplate.DocumentVersionID
  
		INNER JOIN [dbo].[WorkflowAction] WA
		ON WA.[ID] =  DW.[WorkflowStepID]

		WHERE [dbo].[DocumentTemplate].ID = @documentTemplateID
		AND [dbo].[DocumentTemplate].IsDeleted = 0
		AND DW.IsDeleted = 0
END