-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document categories List
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetDocumentCategoryList]

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  [dbo].DocumentType.ID,
			[dbo].DocumentType.Description,
			[dbo].DocumentType.OrderBy,
			[dbo].DocumentType.DateCreated

		FROM [dbo].DocumentType

		ORDER BY [dbo].DocumentType.OrderBy

END