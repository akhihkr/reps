-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update address
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateAddress]
				@ParticipantID int,
				@AddressTypeID int,
				@AddressLine1 varchar(100),
				@AddressLine2 varchar(100),
				@City varchar(30),
				@ProvinceID int,
				@CountryID int,
				@PostalCode varchar(10),
				@AddressID int,
				@Verified bit,
				@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
	UPDATE [dbo].[Address]
		SET 
				[ParticipantID] =  @ParticipantID,
				[AddressTypeID] =  @AddressTypeID,
				[AddressLine1] =  @AddressLine1,
				[AddressLine2] =  @AddressLine2,
				[City] =@City,
				[ProvinceID] =  @ProvinceID,
				[CountryID] = @CountryID,
				[PostalCode] = @PostalCode,
				[Verified] = IsNull(@Verified, [Verified]),
				[DateModified] =  GETDATE()
	WHERE [AddressID] = @AddressID
	SET @rowCount=  @@ROWCOUNT
END
GO