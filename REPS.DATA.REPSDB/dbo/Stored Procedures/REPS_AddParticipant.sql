-- =============================================
-- Author:	Pravina Barosah
-- Create date: 26 Jane 2016
-- Description: Insert Participant details to participant table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddParticipant]
            @DealID int
           ,@ParticipantTypeID int
           ,@ParticipantRoleID int
           ,@PersonID int
           ,@OrganizationID int
           ,@BankID int
           ,@DateCreated datetime
           ,@Deleted bit
		   ,@EntityID int
           ,@Identity int OUTPUT
AS 
 DECLARE	@AlreadyIn int = ''

    -- First we need to check whether the row exists in the database table or not.
    SELECT @AlreadyIn = (SELECT COUNT(*)
							FROM[dbo].[Participant] Part 

							WHERE (DealID = @DealID )
							AND ([ParticipantRoleID] =@ParticipantRoleID)
							AND (@PersonID IS NULL OR PersonID =@PersonID)
							AND (@OrganizationID IS NULL OR OrganizationID = @OrganizationID)
							AND (@EntityID IS NULL OR EntityID = @EntityID)
							)
	-- END OF  First we need to check whether the row exists in the database table or not.


	-- In this way, we can avoid inserting duplicate records. 
    IF(@AlreadyIn = 0)
		BEGIN
        INSERT INTO [dbo].[Participant]
            (            
				 [DealID]
				,[ParticipantTypeID]
				,[ParticipantRoleID]
				,[PersonID]
				,[OrganizationID]
				,[BankID]
				,[DateCreated]
				,[Deleted]
				,[ParticipantGUID]
				,[EntityID]
            )
         VALUES
            (               
                 @DealID
                ,@ParticipantTypeID
                ,@ParticipantRoleID
                ,@PersonID
                ,@OrganizationID
                ,@BankID
                ,@DateCreated
                ,@Deleted
				,NEWID()
				,@EntityID
            )

    SET @Identity = SCOPE_IDENTITY()
	END 
	ELSE IF(@AlreadyIn != 0)
		BEGIN 
			-- Return the existance status.
			SET @Identity = 0
		END 
GO