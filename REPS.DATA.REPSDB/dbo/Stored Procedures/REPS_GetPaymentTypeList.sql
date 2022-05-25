-- =============================================
-- Author:	Kenny
-- Create date: 22 March 2017
-- Description:	Get Payment Type List
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetPaymentTypeList]
	@StartDate Datetime,
	@EndDate Datetime
AS

BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT [FeeTypeID] 
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
