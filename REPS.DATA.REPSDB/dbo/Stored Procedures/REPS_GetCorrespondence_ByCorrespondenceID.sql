-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get correspondence details from the Correspondence table by Correspondence ID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCorrespondence_ByCorrespondenceID]
		@CorrespondenceID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		C.[DateCreated]
		,[Subject]
		,[Body]
		,[Headers]
		,[Html]
		,[Text]
	FROM
		[dbo].[Correspondence] C
	INNER JOIN
		[dbo].[Transaction] T
	ON
		T.TransactionID = C.TransactionID
	WHERE
		C.[ID] = @CorrespondenceID
END