-- ========================================================
-- Author:	Kenny Elaheebacus
-- Create date: 12.01.2017
-- Description:	Insert User Deal details for the Header Tabs
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_AddUserDeal]
		@DealID INT,
		@UserID INT,
		@Type INT, -- 0 = INSERT, 1 = DELETE
		@TopValue INT
AS
BEGIN

	DECLARE @CounterCheck INT;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Check if DealID & UserID are present
	SET @CounterCheck = 
	(
		SELECT
			COUNT([DateCreated])
		FROM
			[dbo].[UserDeal]
		WHERE
			[UserID] = @UserID
		AND
			[DealID] = @DealID
	)

	IF (@Type <> 0) -- If Type != 0, we delete the UserDeal
	BEGIN
		DELETE FROM
			[dbo].[UserDeal]
		WHERE
			[UserID] = @UserID
		AND
			[DealID] = @DealID
	END
	ELSE
	BEGIN

		UPDATE -- We reset the active deal
			[dbo].[UserDeal]
		SET
			[IsActive] = 0
		WHERE
			[UserID] = @UserID

		IF (@CounterCheck < 1) -- If the UserID & DealID are not present in the table
		BEGIN
			DECLARE @CountOrder INT;
			DECLARE @MaxOrder INT;
			DECLARE @NewOrder INT;

			SET @CountOrder  = (SELECT COUNT([Order]) FROM [dbo].[UserDeal] WHERE [UserID] = @UserID); -- We check if there are any Ordering for the User

			IF (@CountOrder <> 0) -- If we already have an ordering of deals for the user
			BEGIN
				SET @MaxOrder = (SELECT MAX([Order]) FROM [dbo].[UserDeal] WHERE [UserID] = @UserID); -- We select the Max order number for the user
				SET @NewOrder = (@MaxOrder + 1); -- The new order = old max order + 1
			END
			ELSE
			BEGIN
				SET @NewOrder = 1; -- If the user doesn't have any ordering, we set the new Order = 1
			END
			
			INSERT INTO
				[dbo].[UserDeal]
				([UserID]
				,[DealID]
				,[IsActive]
				,[DateCreated]
				,[Deleted]
				,[Order])
			VALUES
				(@UserID
				,@DealID
				,1
				,GETDATE()
				,0
				,@NewOrder);
		END
		ELSE -- If the deal already exists, we update the Ordering so that it gets the Max Order number + 1
		BEGIN

			IF (@DealID IN (SELECT TOP(@TopValue) [DealID] FROM [dbo].[UserDeal] WHERE [UserID] = @UserID ORDER BY [Order] DESC)) -- Check if the deal is already in the header tab
			BEGIN

				UPDATE
					[dbo].[UserDeal]
				SET
					[IsActive] = 1
				WHERE
					[UserID] = @UserID
				AND
					[DealID] = @DealID
			END

			ELSE -- If the deal is in the notification bar, we set a new order so that it goes to the header tab

			BEGIN

				SET @MaxOrder = (SELECT MAX([Order]) FROM [dbo].[UserDeal] WHERE [UserID] = @UserID); -- We select the Max order number for the user
				SET @NewOrder = (@MaxOrder + 1); -- The new order = old max order + 1

				UPDATE
					[dbo].[UserDeal]
				SET
					[Order] = @NewOrder
					,[IsActive] = 1
				WHERE
					[UserID] = @UserID
				AND
					[DealID] = @DealID
			END
		END
	END
	SELECT @DealID
END