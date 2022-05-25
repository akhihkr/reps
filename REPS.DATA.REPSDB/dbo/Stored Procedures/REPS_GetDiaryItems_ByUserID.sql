-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Alerts details from the Alerts table
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDiaryItems_ByUserID]
	@UserID INT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		CONVERT(VARCHAR, [StartDate], 106) AS [DiaryDate]
		,CAST([StartDate] AS TIME) AS [DiaryTime]
		,D.[DealID]
		,A.[ID]
		,[StartDate]
		,[EventName]
		,[Comment]
		,[Location]
		,DATEDIFF(MINUTE, [StartDate], [EndDate]) AS [Duration]
		,A.[AlertsGUID]
	FROM
		[dbo].[Alerts] A
	INNER JOIN
		[dbo].[Deal] D
	ON
		A.[DealID] = D.[DealID]
	WHERE
		A.CreatedByUserID = @UserID
	AND
		CONVERT(date, [StartDate]) >= CONVERT(date, getdate())
	AND
		CONVERT(date, [StartDate]) <= DATEADD(WW, 1, getdate())
	AND
		D.Deleted = 0
	AND
		A.StatusID = 1
	ORDER BY
		[StartDate] ASC
END
