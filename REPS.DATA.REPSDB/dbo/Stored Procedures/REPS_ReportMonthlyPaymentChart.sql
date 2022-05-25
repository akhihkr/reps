-- =============================================
-- Author:	Pravina Barosah
-- Create date: 27 Jan 2017
-- Description:p payment monthly report 
-- using master..spt_values (system procedures) to produce various report : spt : table for system procedure
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportMonthlyPaymentChart]
	@UserId int= null, @Year char(4) , @EntityID int = null,@DealProcessTaskID int =null, @StartDate Datetime = null, @EndDate Datetime = null
AS
DECLARE @TotalCount bigint = null
 
SET NOCOUNT ON

	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 

  
	/*select sum of intrument value per months*/
	SELECT @TotalCount  = SUM(fi.[Value])  
		FROM [dbo].[FinancialTransaction] ft
		INNER JOIN [dbo].[Deal] D ON ft.[DealID] = D.[DealID]
		INNER JOIN [dbo].[FinancialInstrument] fi ON ft.InstrumentID = fi.InstrumentID
		INNER JOIN [User] U on D.[UserID] = U.[UserID]
		WHERE 
			(YEAR(fi.[DateCreated]) = @Year OR  @Year IS NULL)
			AND (@UserId IS NULL OR  D.UserID =  @UserId)	
			AND (@DealProcessTaskID IS NULL OR D.DealProcessTaskID = @DealProcessTaskID )
			AND ([EntityID] = @EntityID)/* for current company only */
			AND (@StartDate IS NULL OR CONVERT(VARCHAR(10), fi.[DateCreated], 120) >= @StartDate )/* compare dates */
			AND  (@EndDate IS NULL OR CONVERT(VARCHAR(10), fi.[DateCreated], 120) <= @EndDate)
			AND D.[Deleted] =0	
			AND fi.Deleted = 0
 

 IF( @TotalCount > 0)
 BEGIN
 	SELECT SUM(Value) [value],label ,DATEPART(MM,Label + '01 01') number FROM
		(	
			(
			/*using master..spt_values to get all month name in one select query*/
			SELECT  0 as [value],LEFT(DATENAME(m, @Year+'-' + CAST(number as varchar(2)) + '-1'),3) [label], number
			FROM master..spt_values
			WHERE Type = 'P' 
				and (number between 1 and 12)
				GROUP by number
			)
			UNION
			(	
				/*select sum of intrument value per months*/
				SELECT 
					ROUND(ROUND(SUM(fi.[Value]),0),0) [value],
					LEFT(DATENAME(m,DATEADD(mm,MONTH(fi.[DateCreated]),-1)),3) [label],MONTH(fi.[DateCreated]) as number
					FROM [dbo].[FinancialTransaction] ft
					INNER JOIN [dbo].[Deal] D ON ft.[DealID] = D.[DealID]
					INNER JOIN [dbo].[FinancialInstrument] fi ON ft.InstrumentID = fi.InstrumentID
					INNER JOIN [User] U on D.[UserID] = U.[UserID]
					WHERE 
					(YEAR(fi.[DateCreated]) = @Year OR  @Year IS NULL)
					AND
					(@UserId IS NULL OR  D.UserID =  @UserId)
					AND (@DealProcessTaskID IS NULL OR D.DealProcessTaskID = @DealProcessTaskID )
					/* for current company only */
					AND
					([EntityID] = @EntityID)
					AND
					/* compare dates */
					(@StartDate IS NULL OR CONVERT(VARCHAR(10), fi.[DateCreated], 120) >= @StartDate )
					AND 
					(@EndDate IS NULL OR CONVERT(VARCHAR(10), fi.[DateCreated], 120) <= @EndDate)
					AND
					D.[Deleted] =0	
					AND fi.Deleted = 0
					GROUP BY MONTH(fi.[DateCreated]),fi.[DateCreated]
				)
		)allreps
		GROUP BY  label
		ORDER by number  	 
END