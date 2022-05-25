-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Property Details for participant
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdatePropertyDetail]
				@PropertyID int,
				@RightTypeID int,
				@PropertyNumber varchar(100),
				@PortionNumber varchar(100),
				@Township varchar(max),
				@PropertyName varchar(80),
				@RegistrationDivision varchar(max),
				@SectionNumber varchar(20),
				@PlanNumber varchar(20),
				@UnitNumber int,
				@SizeTypeID int,
				@Size varchar(100),
				@Geo varchar(100),
				@PropertyDetailID int,
				@rowCount INT OUTPUT 

AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    -- Insert statements for procedure here
		UPDATE [dbo].[PropertyDetail]
			SET 
				[PropertyID] =IsNull(@PropertyID, [PropertyID]),
				[RightTypeID] = IsNull(@RightTypeID, [RightTypeID]),
				[PropertyNumber] = IsNull(@PropertyNumber, [PropertyNumber]),
				[PortionNumber] = IsNull(@PortionNumber, [PortionNumber]),
				[Township] = IsNull(@Township, [Township]),
				[PropertyName] = IsNull(@PropertyName, [PropertyName]),
				[SectionNumber] = IsNull(@SectionNumber, [SectionNumber]),
				[RegistrationDivision] = IsNull(@RegistrationDivision, [RegistrationDivision]),
				[PlanNumber] = IsNull(@PlanNumber, [PlanNumber]),
				[UnitNumber] = IsNull(@UnitNumber, [UnitNumber]),
				[SizeTypeID] = IsNull(@SizeTypeID, [SizeTypeID]),
				[Size] = IsNull(@Size, [Size]),
				[Geo] = IsNull(@Geo, [Geo]),
				[DateModified] =  GETDATE()

		WHERE [PropertyDetailID] = @PropertyDetailID
END
SET @rowCount=  @@ROWCOUNT
GO