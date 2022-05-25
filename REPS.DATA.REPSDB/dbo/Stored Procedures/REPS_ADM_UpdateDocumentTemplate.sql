-- =============================================
-- Author: Pravina Barosah
-- Create date:21 April 2017
-- Description: set document template to 1 when file is archive 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateDocumentTemplate]
	@templateVersionID int ,@rowCount INT OUTPUT 

AS
	UPDATE [dbo].[DocumentTemplate] 

	SET  [DocumentTemplate].IsDeleted = 1

	WHERE [DocumentTemplate].DocumentVersionID = @templateVersionID

	SET @rowCount=  @@ROWCOUNT
GO