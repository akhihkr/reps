-- ============================================================================
-- Author:Pravina Barosah
-- Create date: 18 April 2017
-- Description:	Add workflowtaskid for each document to documentworkflow table
-- ===========================================================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddWorkflowStepToDocumentTemplate]
		@documentTemplateVersionID int,@workflowStepID int,@createdByUserID int,@rowCount INT OUTPUT 
AS
	-- Insert Workflow steps to document
	INSERT INTO [dbo].[DocumentWorkflow]
			   (   
				   [DocumentVersionID]
				  ,[WorkflowStepID]
				  ,[DateCreated]
				  ,[CreatedByUserID]
				  ,[IsDeleted]
			   )
		 VALUES
			   (
				   @documentTemplateVersionID
				  ,@workflowStepID
				  ,GETDATE()
				  ,@createdByUserID
				  ,0
			   )

	SET @rowCount = SCOPE_IDENTITY();
GO
