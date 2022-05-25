-- ========================================================
-- Author: Pravina Barosah
-- Create date: 08 March 2017
-- Description:	Template_Client_Care_Letter
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_Template_Client_Care_Letter]
	@DealId int
AS
		Select d.[DealID],ClientReference, pe.GivenName , pe.FamilyName , 
		pr.PropertyDescription,ad.AddressLine1,ad.AddressLine2, fi.[Value]
		FROM [Deal]  d
			INNER JOIN [Participant] pa ON d.[DealID] = pa.DealID
			INNER JOIN [Person] pe ON pa.PersonID = pe.PersonID
			INNER JOIN [Property] pr ON d.DealID = pr.DealID
			INNER JOIN [Address] ad ON pr.AddressID = ad.AddressID
			INNER JOIN  FinancialTransaction ft on d.DealID = ft.DealID
			INNER JOIN  [FinancialInstrument] fi on ft.InstrumentID = ft.InstrumentID
		WHERE d.DealID =  @DealId