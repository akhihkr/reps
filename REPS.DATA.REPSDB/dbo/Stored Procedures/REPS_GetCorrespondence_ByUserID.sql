-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get correspondence details from the Correspondence table
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCorrespondence_ByUserID]
		@UserID int
		,@DealID int
AS
BEGIN

	SELECT
		C.[ID]
		,C.[DateCreated]
		,[Subject]
		,[Body]
		,[SmtpStatusID]
		,[Headers]
		,[Text]
		,[AttachmentID]
		,C.[Html]
		,C.[CorrespondenceGUID]
	FROM
		[dbo].[Correspondence] C
	INNER JOIN
		[dbo].[Transaction] T
	ON
		T.TransactionID = C.TransactionID
	WHERE
		C.UserID = @UserID
	AND
		T.DealID = @DealID
	ORDER BY
		C.[DateCreated] DESC
END