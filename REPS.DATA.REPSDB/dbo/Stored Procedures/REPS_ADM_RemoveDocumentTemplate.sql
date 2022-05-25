-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Remove Document Template
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_RemoveDocumentTemplate]
			@documentTemplateID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @DocumentVersionID int,
			@rowcnt int;

	SELECT @DocumentVersionID = [DocumentVersionID]
	  FROM [dbo].[DocumentTemplate]
	  WHERE [ID] = @documentTemplateID

	UPDATE [dbo].[DocumentWorkflow]
	   SET [IsDeleted] = 1
	 WHERE [DocumentVersionID] = @DocumentVersionID

	 SET @rowcnt = @rowcnt + @@ROWCOUNT

	UPDATE [dbo].[DocumentVersion]
	   SET [IsDeleted] = 1
	 WHERE [ID] = @DocumentVersionID

	 SET @rowcnt = @rowcnt + @@ROWCOUNT

	UPDATE [dbo].[DocumentTemplate]
	   SET [IsDeleted] = 1
	 WHERE [ID] = @documentTemplateID

	 SET @rowcnt = @rowcnt + @@ROWCOUNT

	 IF ( @rowcnt > 2 )
		SET @rowCount = @rowcnt;

END