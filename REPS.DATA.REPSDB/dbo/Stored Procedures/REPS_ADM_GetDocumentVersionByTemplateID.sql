-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document Version ID by TemplateID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetDocumentVersionByTemplateID]
			@templateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT    [DocumentVersionID]
		FROM  [dbo].[DocumentTemplate]
		WHERE [ID] = @templateID
		AND	  [IsDeleted] = 0

END