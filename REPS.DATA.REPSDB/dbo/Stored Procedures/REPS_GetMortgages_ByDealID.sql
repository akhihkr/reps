-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Mortgages
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetMortgages_ByDealID]
		@DealID INT = null,
	    @MortgageID	int = null
AS
BEGIN
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		FI.[InstrumentID] AS [InstrumentID]
		,[ParticipantID]
		,FT.[FinancialTransactionID] AS [TransactionID]
		,[CurrencyTypeID]
		,cast([Value] AS decimal(20,2)) AS [Value]
		,cast([Deposit] AS decimal(20,2)) AS [Deposit]
		,[InterestRate]
		,[Term]
		,L.[ID] AS [LenderID]
		,L.[Name] AS [LenderName]
		,IT.[InterestTypeID] AS [InterestTypeID]
		,IT.[Description] AS [InterestTypeDesc]
		,IType.[InstrumentTypeID] AS [InstrumentTypeID]
		,IType.[Description] AS [InstrumentTypeDesc]
		,FI.[InstrumentGUID] AS [InstrumentGUID]
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
		FI.Deleted = 0
	AND
	 (@DealID IS NULL OR FT.DealID = @DealID)
	AND
	(@MortgageID IS NULL OR FT.[InstrumentID] = @MortgageID	)
		
	ORDER BY FI.[DateCreated] DESC

END