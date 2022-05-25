-- =============================================
-- Author:	Abhinav
-- Create date: 05 January 2017
-- Description:	Get Banks
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetBanks]
		@BankName VARCHAR(50),
		@SwiftCode  VARCHAR(30),
		@EntityID int = null,
		@BankID int = null,
		@Start  int = null,
		@End  int = null
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT   [BankID]
				,[EntityID]
				,[BankName]
				,[UniversalSortCode]
				,[SwiftCode]
				,[DateCreated]
				,[DateModified]
				,[Deleted]
			
			
 
		,ROW_NUMBER() OVER (ORDER BY [BankID] DESC) AS num
				 FROM [dbo].[BankorCreditUnion]
	  ) 
		As allUsers
	  WHERE 
	  (	@BankName IS NULL OR [BankName] LIKE @BankName	)
		AND
	  (	@SwiftCode IS NULL OR [SwiftCode] LIKE @SwiftCode	)
		AND
	  (	@EntityID IS NULL OR [EntityID]= @EntityID	)
		AND
	  (	@BankID IS NULL OR [BankID]= @BankID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0
END