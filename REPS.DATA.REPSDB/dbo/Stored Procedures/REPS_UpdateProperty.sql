-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Property
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateProperty]
				@DealID int,
				@PropertyDescription varchar(1000),
				@LegalDescription varchar(1000),
				@AddressID int,
				@PropertyTypeID int,
				@Verified bit,
				@PropertyID int,
				@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
		UPDATE [dbo].[Property]
			SET 
				  [DealID] = IsNull(@DealID, [DealID]),
				  [PropertyDescription] = IsNull(@PropertyDescription, [PropertyDescription]),
				  [LegalDescription] = IsNull(@LegalDescription, [LegalDescription]),
				  [AddressID] = IsNull(@AddressID, [AddressID]),
				  [PropertyTypeID] = IsNull(@PropertyTypeID, [PropertyTypeID]),
				  [Verified] = IsNull(@Verified, [Verified]),
				  [DateModified] =  GETDATE()
		WHERE [PropertyID] = @PropertyID
END
SET @rowCount=  @@ROWCOUNT
GO