-- =============================================
-- Author:	pravina
-- Create date: 21 April 2017
-- Description:	Update document version table and document template if no file has been added yet
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateDocumentVersionDocumentTemplate]
			@templateID int,
			@templateVersionID int,
			@templateName VARCHAR(MAX),
			@versionName VARCHAR(MAX),
			@xmlSPROC NVARCHAR(MAX),
			@templateFile NVARCHAR(MAX),
			@mimeTypeID VARCHAR(50),
			@userID int,
			@eSignable bit,
			@isStaticDoc bit,
			@isActive bit,
			@isDocfusion bit,
			@templateFixGUID uniqueidentifier,
			@rowCount INT OUTPUT 
AS
	UPDATE [dbo].[DocumentTemplate] 

	SET  [DocumentTemplate].TemplateDisplayName =@templateName
		,[DocumentTemplate].IsActive = @isActive
		,[DocumentTemplate].IsDocfusionTemplate =@isDocfusion
		,[DocumentTemplate].[TemplateFixGUID] =@templateFixGUID

	WHERE [DocumentTemplate].DocumentVersionID = @templateVersionID


	UPDATE [dbo].[DocumentVersion] 

	SET  [DocumentVersion].[VersionName] = @versionName
		,[DocumentVersion].[MimeTypeID] = @mimeTypeID
		,[DocumentVersion].[TemplateFileName] =@templateFile
		,[DocumentVersion].[XMLStoredProc] =@xmlSPROC
		,[DocumentVersion].[CreatedByUserID] = @userID
		,[DocumentVersion].[eSignable] =@eSignable
		,[DocumentVersion].[IsStaticDoc] = @isStaticDoc
 
	WHERE [DocumentVersion].ID = @templateVersionID

	SET @rowCount=  @@ROWCOUNT
GO
