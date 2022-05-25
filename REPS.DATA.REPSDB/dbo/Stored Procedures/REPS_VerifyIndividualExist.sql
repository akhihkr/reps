-- =============================================
-- Author: Pravina Barosah
-- Create date: 23 March 2017
-- Description:	Verify if person exist with same role
-- =============================================
CREATE PROCEDURE [dbo].[REPS_VerifyIndividualExist]
			@DealID int= null,
			@ParticipantRoleID int = null,
			@GivenName varchar(100) = null,
			@FamilyName varchar(100)= null,
			@Email varchar(100)= null
AS
SELECT Part.[DealID]
      ,Part.[ParticipantRoleID]
      ,Per.[GivenName]
      ,Per.[FamilyName]
      ,Per.[Email]

  FROM[dbo].[Participant] Part
  INNER JOIN [dbo].[Person] Per 
  ON Part.PersonID = Per.PersonID

GO
