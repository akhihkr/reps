-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Deal Completion Date
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDealCompletionDate_ByDealID]
	@DealID INT = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @CounterCheck VARCHAR(MAX);

	SET @CounterCheck = (SELECT CONVERT(VARCHAR, CompletionDate, 106)
	FROM
		[dbo].Deal
	WHERE
		DealID = @DealID)

	IF (@CounterCheck != 'NULL')
	BEGIN
		SELECT
			CONVERT(VARCHAR, CompletionDate, 106) AS [ExportCompletionDate],
			[CompletionDate] AS [RegistrationDate]
		FROM
			[dbo].Deal
		WHERE
			DealID = @DealID
	END
	ELSE
	BEGIN
		SELECT '-' AS [ExportCompletionDate],
		'-' AS [RegistrationDate]
	END
END