-- ========================================================
-- Author:	Kenny Elaheebacus
-- Create date: 12.01.2017
-- Description:	Get all active deals by User ID for header tabs
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetHeaderTabNotificationBarActiveDeals_ByUserGUID]
		@UserID INT,
		@TopValue INT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH HeaderTabBreadCrumbs AS 
	(
		SELECT
			D.[ClientReference]
			,D.[UniqueReference]
			,UD.[DealID]
			,UD.[IsActive]
			,UD.[Order]
			,UD.[LastViewName]
		FROM
			[dbo].[UserDeal] UD
		INNER JOIN
			[dbo].[Deal] D
		ON
			UD.[DealID] = D.[DealID]
		WHERE
			UD.[UserID] = @UserID
		AND
			UD.[DealID] NOT IN (SELECT TOP(@TopValue) UD.[DealID] FROM [dbo].[UserDeal] UD WHERE UD.UserID = @UserID ORDER BY [Order] DESC)
	)
	SELECT
		*
	FROM
		[HeaderTabBreadCrumbs]
	ORDER BY
		[Order] DESC;
END