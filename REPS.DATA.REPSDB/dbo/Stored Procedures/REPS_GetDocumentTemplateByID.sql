-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document Tempalte content by TemplateID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentTemplateByID]
			@templateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT    CAST([dbo].[DocumentVersion].[ID] AS nvarchar(max)) + '\' + [dbo].[DocumentVersion].[TemplateFileName] AS "Filename"
		FROM  [dbo].[DocumentVersion]

		INNER JOIN [dbo].[DocumentTemplate]
		ON [dbo].[DocumentTemplate].DocumentVersionID = [dbo].[DocumentVersion].ID
		AND [dbo].[DocumentTemplate].[IsDeleted] = 0

		WHERE [dbo].[DocumentTemplate].[ID] = @templateID
		AND	  [dbo].[DocumentVersion].[IsDeleted] = 0

END