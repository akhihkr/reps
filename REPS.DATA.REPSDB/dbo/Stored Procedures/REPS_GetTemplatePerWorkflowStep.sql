-- =============================================
-- Author:	Pravina Barosah
-- Create date: 20 July 2017
-- Description:	Get Template of each Workflow Step
-- or Specific Template when passing params
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetTemplatePerWorkflowStep]
	--pass params, for retriving the specific template of workflow step
	 @documentTemplateID int = null,@workflowStepID int = null
	--end of pass params, for retriving the specific template of workflow step
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT template.TemplateDisplayName, template.DocumentVersionID,template.Unassigned, documentWorkflow.Assigned,documentWorkflow.WorkflowStepID FROM
	 (
		-- Insert statements for procedure here	
		SELECT   DT.TemplateDisplayName
				,DT.DocumentVersionID
				,'0' AS 'WorkflowStepID'
				,'0' AS 'DocWorkflowDeleted'
				,DT.IsDeleted [DocTemplateDeleted]
				,'0' AS 'ID'
				,'0' AS 'Unassigned'

			FROM [dbo].DocumentTemplate DT

			INNER JOIN [dbo].DocumentVersion DV
			ON DV.ID =  DT.DocumentVersionID

			WHERE  DT.IsDeleted = 0	
	) template
		LEFT JOIN 
			(
				-- Insert statements for procedure here	
				SELECT   DT.TemplateDisplayName
						,Dw.DocumentVersionID
						,DW.WorkflowStepID
						,DW.IsDeleted[DocWorkflowDeleted]
						,DT.IsDeleted [DocTemplateDeleted]
						,DW.ID 
						,'1' AS 'Assigned'
					FROM [dbo].[DocumentWorkflow] DW

					INNER JOIN [dbo].[DocumentVersion] DV
					ON DV.ID = DW.DocumentVersionID
  
					INNER JOIN [dbo].[DocumentTemplate] DT
					ON DT.DocumentVersionID = DV.ID

					WHERE 		
					(DW.WorkflowStepID = @workflowStepID)
					AND DW.IsDeleted = 0
					AND DT.IsDeleted = 0

			)documentWorkflow

	on template.DocumentVersionID =documentWorkflow.DocumentVersionID 
	ORDER BY template.DocumentVersionID
END