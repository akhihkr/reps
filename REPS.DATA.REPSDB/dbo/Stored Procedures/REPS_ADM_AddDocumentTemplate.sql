-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06-Jan-2016
-- Description:	Insert document template of admin 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddDocumentTemplate]
			@docTypeID int,
			@templateName VARCHAR(MAX),
			@versionName VARCHAR(MAX),
			@xmlSPROC NVARCHAR(MAX),
			@templateFile NVARCHAR(MAX),
			@mimeTypeID VARCHAR(MAX),
			@userID int,
			@eSignable bit,
			@isStaticDoc bit,
			@isActive bit,
			@isDocfusion bit,
			@templateFixGUID uniqueidentifier,
			@Identity int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @InsertedVersionID int,
			@InsertedDocTemplateID int,
			@LastDisplayOrder int;
		
	INSERT INTO [dbo].[DocumentVersion]
			   ([DocumentTypeID]
			   ,[VersionName]
			   ,[TemplateFileName]
			   ,[XMLStoredProc]
			   ,[MimeTypeID]
			   ,[DateCreated]
			   ,[CreatedByUserID]
			   ,[eSignable]
			   ,[IsStaticDoc]
			   ,[IsDeleted])
		 VALUES
			   (@docTypeID
			   ,@versionName
			   ,@templateFile
			   ,@xmlSPROC
			   ,@mimeTypeID
			   ,GETDATE()
			   ,@userID
			   ,@eSignable
			   ,@isStaticDoc
			   ,0)
	 SET @InsertedVersionID = SCOPE_IDENTITY();

	SELECT TOP 1 @LastDisplayOrder = [DisplayOrder]
	  FROM [dbo].[DocumentTemplate]
	  ORDER BY [dbo].DocumentTemplate.DisplayOrder DESC

	INSERT INTO [dbo].[DocumentTemplate]
			   ([TemplateDisplayName]
			   ,[DocumentVersionID]
			   ,[IsActive]
			   ,[IsDeleted]
			   ,[IsDocfusionTemplate]
			   ,[DisplayOrder]
			   ,[TemplateFixGUID])
		 VALUES
			   (@templateName
			   ,@InsertedVersionID
			   ,@isActive
			   ,0
			   ,@isDocfusion
			   ,@LastDisplayOrder + 1
			   ,@templateFixGUID)

	SELECT @InsertedDocTemplateID = SCOPE_IDENTITY();


	SET @Identity = @InsertedVersionID;
END