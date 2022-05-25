-- ===========================================
-- Author: Abhinav
-- Create date: 20 Jan 2016
-- Description:	Get PropertyDetailID by PropertyDetailGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetPropertyDetailIDByPropertyDetailGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT [PropertyDetailID]
  FROM [dbo].[PropertyDetail]
  WHERE [PropertyDetailGUID] = @uniqueReference