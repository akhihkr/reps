-- =============================================
-- Author:	pravina
-- Create date: 06-Jan-2016
-- Description:	Insert Address Details of to address table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddAddress]
			 @ParticipantID int
			,@AddressTypeID int
			,@AddressLine1 varchar(100)
			,@AddressLine2 varchar(100)
			,@City varchar(30)
			,@ProvinceID int
			,@CountryID int
			,@PostalCode varchar(10)
			,@DateCreated datetime
			,@Deleted bit
			,@Verified bit
			,@Identity int OUTPUT

AS
	INSERT INTO [dbo].[Address]
           (			
			    [ParticipantID]
			   ,[AddressTypeID]
			   ,[AddressLine1]
			   ,[AddressLine2]
			   ,[City]
			   ,[ProvinceID]
			   ,[CountryID]
			   ,[PostalCode]
			   ,[DateCreated]
			   ,[Deleted]
			   ,[Verified]
			   ,[AddressGUID]
		   )
     VALUES
           (
				 @ParticipantID
				,@AddressTypeID
				,@AddressLine1
				,@AddressLine2
				,@City
				,@ProvinceID
				,@CountryID
				,@PostalCode
				,@DateCreated
				,@Deleted
				,@Verified
				,NEWID()
		   )

		SET @Identity = SCOPE_IDENTITY()
GO
