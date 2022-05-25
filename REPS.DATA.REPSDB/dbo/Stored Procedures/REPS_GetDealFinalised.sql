-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Deal Finalised
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDealFinalised]
		@DealID int
AS
		SELECT COUNT([ParticipantID]) FROM [dbo].[Participant]
		WHERE [DealID]  = @DealID
		AND ApprovedDate IS NULL
		AND [ParticipantRoleID] BETWEEN  29 AND 71   	  
GO

