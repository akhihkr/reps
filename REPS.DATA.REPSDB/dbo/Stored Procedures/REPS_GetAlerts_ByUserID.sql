-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Alerts details from the Alerts table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAlerts_ByUserID]
		@UserID int
		,@Filter varchar(max)
		,@DealID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@Filter = 'archive')
		BEGIN
			SELECT
				A.[ID]
				,A.[AlertsGUID]
				,[StartDate]
				,[EventName]
				,[Comment]
				,[Location]
				,DATEDIFF(MINUTE, [StartDate], [EndDate]) AS [Duration]
				,AST.[Description]
			FROM
			[dbo].[Alerts] A
			INNER JOIN
			[dbo].[AlertStatus] AST
		    ON
			AST.ID = A.StatusID
			WHERE
			A.CreatedByUserID = @UserID
			AND
			A.DealID = @DealID
			AND
				A.StatusID = 4
			ORDER BY
				[StartDate] ASC
		END
	ELSE IF (@Filter = 'today')
		BEGIN
			SELECT
				A.[ID]
				,A.[AlertsGUID]
				,[StartDate]
				,[EventName]
				,[Comment]
				,[Location]
				,DATEDIFF(MINUTE, [StartDate], [EndDate]) AS [Duration]
				,AST.[Description]
			FROM
				[dbo].[Alerts] A
			INNER JOIN
				[dbo].[AlertStatus] AST
			ON
				AST.ID = A.StatusID
			WHERE
				A.CreatedByUserID = @UserID
			AND
				CONVERT(date, [StartDate]) = CONVERT(date, getdate())
			AND
				A.DealID = @DealID
			AND
				A.StatusID NOT IN (4,5)
			ORDER BY
				[StartDate] ASC
		END
	ELSE IF (@Filter = 'coming')
		BEGIN
			SELECT
				A.[ID]
				,A.[AlertsGUID]
				,[StartDate]
				,[EventName]
				,[Comment]
				,[Location]
				,DATEDIFF(MINUTE, [StartDate], [EndDate]) AS [Duration]
				,AST.[Description]
			FROM
			[dbo].[Alerts] A
			INNER JOIN
			[dbo].[AlertStatus] AST
			ON
			AST.ID = A.StatusID
			WHERE
			A.CreatedByUserID = @UserID
			AND
			CONVERT(date, [StartDate]) > CONVERT(date, getdate())
			AND
			A.DealID = @DealID
			AND
			A.StatusID NOT IN (4,5)
			ORDER BY
				[StartDate] ASC
		END
	ELSE
		BEGIN
			SELECT
				A.[ID]
				,A.[AlertsGUID]
				,[StartDate]
				,[EventName]
				,[Comment]
				,[Location]
				,DATEDIFF(MINUTE, [StartDate], [EndDate]) AS [Duration]
				,AST.[Description]
			  FROM
				[dbo].[Alerts] A
				  INNER JOIN
					[dbo].[AlertStatus] AST
				  ON
					AST.ID = A.StatusID
			  WHERE
				A.CreatedByUserID = @UserID
			  AND
				A.DealID = @DealID
			  AND
			    A.StatusID NOT IN (4,5)
			ORDER BY
				[StartDate] ASC
		END
END