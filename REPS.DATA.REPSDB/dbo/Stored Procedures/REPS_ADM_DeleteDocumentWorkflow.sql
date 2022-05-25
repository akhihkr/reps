-- =============================================
-- Author:	Pravina Barosah
-- Create date: 24 April 20167
-- Description:	Update document workflow; set deleted 1
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_DeleteDocumentWorkflow]
			@DocumentVersionID int,
			@DocumentWorkflowID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
		
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
 		UPDATE [dbo].[DocumentWorkflow]
			SET 				
				[IsDeleted]= @Deleted
		WHERE
		 (@DocumentVersionID IS NULL OR [DocumentVersionID] = @DocumentVersionID)
		 AND 
		 (@DocumentWorkflowID IS NULL OR [WorkflowStepID] = @DocumentWorkflowID)
END
SET @rowCount=  @@ROWCOUNT