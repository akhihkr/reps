-- =============================================
-- Author:		Pravina
-- Create date: 27 Jan 2017
-- Description:	Get Fee Type List dropdown
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportFilterFeeTypesListDropDown]
	@StartDate Datetime =null,
	@EndDate Datetime =null
AS

BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT [FeeTypeID] AS [ReportsId]
		  ,[Description]
		  ,[Deleted]
		  ,[DateCreated]
	  FROM [dbo].[FeeType]
	  WHERE [Deleted] = 0
			AND
	  		/* compare dates */
			(@StartDate IS NULL OR [DateCreated] >= @StartDate )
			AND 
			(@EndDate IS NULL OR [DateCreated] <= @EndDate)
END
