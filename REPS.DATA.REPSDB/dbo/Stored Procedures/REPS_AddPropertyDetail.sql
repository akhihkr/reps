-- =============================================
-- Author: Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Property details Info to PropertyDetail table 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddPropertyDetail]
				@PropertyID int,
				@RightTypeID int,
				@ErfNumber varchar(100),
				@PortionNumber varchar(100),
				@Township varchar(max),
				@Name varchar(80),
				@SectionNumber varchar(20),
				@RegistrationDivision varchar(max),
				@PlanNumber varchar(20),
				@UnitNumber int,
				@SizeTypeID int,
				@Size varchar(100),
				@Geo varchar(100),
				@DateCreated datetime,
				@Deleted bit,
				@Identity int OUTPUT

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[PropertyDetail]
           (		
				 [PropertyID]
				,[RightTypeID]
				,[PropertyNumber]
				,[PortionNumber]
				,[Township]
				,[PropertyName]
				,[SectionNumber]
				,[RegistrationDivision]
				,[PlanNumber]
				,[UnitNumber]
				,[SizeTypeID]
				,[Size]
				,[Geo]
				,[DateCreated]
				,[Deleted]
				,[PropertyDetailGUID]
		   )
     VALUES
           (
				@PropertyID,
				@RightTypeID,
				@ErfNumber,
				@PortionNumber,
				@Township,
				@Name,
				@SectionNumber,
				@RegistrationDivision,
				@PlanNumber,
				@UnitNumber,
				@SizeTypeID,
				@Size,
				@Geo,
				@DateCreated,
				@Deleted,
				NEWID()
		   )
		SET @Identity = SCOPE_IDENTITY()
END
