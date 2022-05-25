-- =============================================
-- Author:	Pravina Barosah
-- Create date: 27 Jan 2017
-- Description:p payment monthly report 
-- using master..spt_values (system procedures) to produce various report : spt : table for system procedure
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ReportMonthlyPaymentTable]
	@UserId int= null, @Year char(4) , @EntityID int = null,@DealProcessTaskID int =null, @StartDate Datetime = null, @EndDate Datetime = null
AS
	--fetch EnetityID from user database by user session 
	SELECT @EntityID = [EntityID] FROM [dbo].[User] WHERE [UserID] = @UserId 

	/*select sum of intrument value per months*/
	SELECT 
		U.[GivenName] + ' ' + U.[FamilyName][Created By],U.[Email] [Email],
		LEFT(DATENAME(m,DATEADD(mm,MONTH(fi.[DateCreated]),-1)),100) [Month],   
		YEAR(fi.[DateCreated])[Year],
		ROUND(ROUND(SUM(fi.[Value]),0),0) [Total Sum]
 
		FROM [dbo].[FinancialTransaction] ft
		INNER JOIN [dbo].[Deal] D ON ft.[DealID] = D.[DealID]
		INNER JOIN [dbo].[FinancialInstrument] fi ON ft.InstrumentID = fi.InstrumentID
		INNER JOIN [User] U on D.[UserID] = U.[UserID]


		WHERE 
		(YEAR(D.[DateCreated]) = @Year OR  @Year IS NULL)
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
		AND
		fi.Deleted = 0
	
		GROUP BY MONTH(fi.[DateCreated]),U.[GivenName] + ' ' + U.[FamilyName],U.[Email],YEAR(fi.[DateCreated])