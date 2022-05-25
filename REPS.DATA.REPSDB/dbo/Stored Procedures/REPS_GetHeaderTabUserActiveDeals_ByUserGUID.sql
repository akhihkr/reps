-- ========================================================
-- Author:	Kenny Elaheebacus
-- Create date: 07.09.2016
-- Description:	Get all active deals by User ID for breadcrumbs
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetHeaderTabUserActiveDeals_ByUserGUID]
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
			TOP(@TopValue) D.[ClientReference]
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
		ORDER BY
			[Order] DESC
	)
	SELECT
		*
	FROM
		[HeaderTabBreadCrumbs]
	ORDER BY
		[Order] ASC;
END