-- =============================================
-- Author:Pravina Barosah
-- Create date: 09 March 2017
-- Description: Update Document Table and set Isdeleted true
-- ========================================================

CREATE PROCEDURE [dbo].[REPS_UpdateDocumentTemplate]
			   @DocumentTemplateID int,
			   @WorkflowStepID int,
               @Deleted bit,
			   @rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return
    UPDATE [dbo].[Document]
				SET  [IsDeleted] =IsNull(@Deleted, [IsDeleted])

	WHERE [DocumentTemplateID]  = @DocumentTemplateID 
	AND [WorkflowStepID]=@WorkflowStepID;
END
SET @rowCount=  @@ROWCOUNT