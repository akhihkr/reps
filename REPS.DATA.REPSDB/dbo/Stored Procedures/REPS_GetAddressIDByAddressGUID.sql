-- ===========================================
-- Author: Abhinav
-- Create date: 20 Jan 2016
-- Description:	Get AddressID by AddressGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetAddressIDByAddressGUID]
	@uniqueReference varchar(1000)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT [AddressID]
  FROM [dbo].[Address]
  WHERE [AddressGUID] = @uniqueReference