-- =============================================================================================================
-- Author: Pravina 
-- Create date: 21 April 2017
-- Description:	Update document workflow if a NEW file has created by the user by documentTemplateID.
-- NEW row added to docuement template and document version when file is inserted.
-- Keep an old version template document, update documentTemplateID with the new versionID : "documentTemplateID".
-- =============================================================================================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateDocumentWorkflow]
	@NewDocumentVerisonID int, @OldDocumentVerisonID int,@rowCount INT OUTPUT 

AS
	UPDATE [dbo].[DocumentWorkflow]
	SET  [DocumentWorkflow].[DocumentVersionID] = @NewDocumentVerisonID
	WHERE [DocumentWorkflow].[DocumentVersionID] = @oldDocumentVerisonID
	SET @rowCount=  @@ROWCOUNT
GO
