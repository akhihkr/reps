-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2017
-- Description:	Get Stored Document by ID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetStoredDocumentByID]
			@documentID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT    [dbo].[Document].ID,
			  [dbo].[Document].[GeneratedDocFileName],
			  [dbo].[DocumentTemplate].TemplateDisplayName,
			  [dbo].[Document].[MimeTypeID]
		FROM  [dbo].[Document]
		INNER JOIN [dbo].[DocumentTemplate]
		ON [dbo].Document.DocumentTemplateID = [dbo].[DocumentTemplate].ID
		WHERE [dbo].[Document].ID = @documentID
		AND	  [dbo].[Document].[IsDeleted] = 0
END