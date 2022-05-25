-- ===========================================
-- Author:Pravina Barosah
-- Create date: 19 Jan 2016
-- Description:	Get Particiapnt ID by ParticiapntGUID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetParticipantIDByParticipantGUID]
	@particiapntGUID uniqueidentifier
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT P.ParticipantID
		FROM [dbo].[Participant] P
		WHERE  P.ParticipantGUID = @particiapntGUID 
GO