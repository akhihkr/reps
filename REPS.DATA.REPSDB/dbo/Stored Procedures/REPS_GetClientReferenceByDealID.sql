-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get Client Reference by DealID
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetClientReferenceByDealID]
			@DealID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [ClientReference]
	  FROM [dbo].[Deal]
	  WHERE [DealID] = @DealID
END