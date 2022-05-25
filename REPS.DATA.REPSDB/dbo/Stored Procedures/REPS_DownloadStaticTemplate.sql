-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Download static template
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DownloadStaticTemplate]
		@documentTemplateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT    [dbo].[DocumentVersion].[TemplateFileName],
			  [dbo].[DocumentTemplate].TemplateDisplayName,
			  [dbo].[DocumentVersion].[MimeTypeID],
			  [dbo].[DocumentVersion].ID
		FROM  [dbo].[DocumentTemplate]
		INNER JOIN [dbo].[DocumentVersion]
		ON [dbo].[DocumentTemplate].DocumentVersionID = [dbo].[DocumentVersion].ID
		WHERE [dbo].[DocumentTemplate].ID = @documentTemplateID
		AND	  [dbo].[DocumentTemplate].[IsDeleted] = 0
		AND	  [dbo].[DocumentVersion].[IsDeleted] = 0
		AND	  [dbo].[DocumentTemplate].[IsDocfusionTemplate] = 0
		AND	  [dbo].[DocumentVersion].[IsStaticDoc] = 1
END