-- ========================================================
-- Author:	Pravina
-- Create date: 26.01.2017
-- Description:	Get total sum of Mortgages Purchase value if exists
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetMortgagePurchasePrice]
	@DealID INT = null
AS
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	DECLARE @CounterCheck INT;

	SET @CounterCheck = (SELECT COUNT(L.[Name])

		FROM

			[dbo].[FinancialInstrument] FI
		INNER JOIN
			[dbo].[Lenders] L
		ON
			FI.LenderID = L.ID
		INNER JOIN
			[dbo].[InterestType] IT
		ON
			FI.InterestTypeID = IT.InterestTypeID
		INNER JOIN
			[dbo].[InstrumentType] IType
		ON
			FI.InstrumentTypeID = IType.InstrumentTypeID
		INNER JOIN
			[dbo].[FinancialTransaction] FT
		ON
			FI.InstrumentID = FT.InstrumentID

		WHERE
			FT.[DealID] = @DealID
			AND 
			FI.Deleted = 0)

	IF (@CounterCheck <> 0)
	BEGIN
		SELECT
			SUM (CAST(Value AS decimal(20,2))) AS [Price], 'YES' AS [LenderName]
		FROM

			[dbo].[FinancialInstrument] FI
		INNER JOIN
			[dbo].[Lenders] L
		ON
			FI.LenderID = L.ID
		INNER JOIN
			[dbo].[InterestType] IT
		ON
			FI.InterestTypeID = IT.InterestTypeID
		INNER JOIN
			[dbo].[InstrumentType] IType
		ON
			FI.InstrumentTypeID = IType.InstrumentTypeID
		INNER JOIN
			[dbo].[FinancialTransaction] FT
		ON
			FI.InstrumentID = FT.InstrumentID

		WHERE
			FT.[DealID] = @DealID
			AND 
			FI.Deleted = 0
		GROUP BY  FT.DealID
	END
	ELSE
	BEGIN
		SELECT CAST(0 AS decimal(20,2)) AS [Price], 'No' AS [LenderName]
	END