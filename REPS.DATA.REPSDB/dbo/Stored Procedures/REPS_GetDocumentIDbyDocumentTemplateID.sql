-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get DocumentID by DocumentTemplateID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentIDbyDocumentTemplateID]
			@dealID int,
			@templateID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		DECLARE @DocumentID int = null;

		SELECT  @DocumentID = [ID]
		FROM	[dbo].[Document]
		 WHERE [DealID] = @dealID
		 AND   [DocumentTemplateID] = @templateID
		 AND   [IsDeleted] = 0;

		 SELECT @DocumentID;

END