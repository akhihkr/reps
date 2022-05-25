-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Document Template List by WorkflowID and DealID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetDocumentTemplateDetails]
	@workflowID int, @dealID  int
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	SELECT 
			[dbo].DocumentType.ID,	
			D.DealID,
			DT.ID AS "DocumentTemplateID",
			WA.DisplayName,
			WA.[ID] AS workflowStepId,
			DT.TemplateDisplayName,
			DT.IsDocfusionTemplate,
			U.GivenName + ' ' + U.FamilyName AS "CreatedByUser",
			DV.IsStaticDoc,
			DV.VersionName,
			DV.eSignable,
			D.ID AS "DocumentID",
			D.GeneratedDocFileName,
			D.SignedDocFileName,
			D.DateCreated,
			D.SignedDocDate,
			D.MimeTypeID,
			D.CreatedByUserID,
			[dbo].DocumentType.Description,
			[dbo].DocumentType.OrderBy

			FROM [dbo].DocumentTemplate DT 

			INNER JOIN [dbo].[DocumentVersion] DV ON DT.[DocumentVersionID]=DV.[ID]
			INNER JOIN [dbo].DocumentType ON [dbo].DocumentType.ID = DV.DocumentTypeID
			LEFT JOIN [dbo].[DocumentWorkflow] DW  ON DV.ID = DW.DocumentVersionID
			LEFT JOIN [dbo].[WorkflowAction] WA  ON WA.ID = DW.WorkflowStepID
			LEFT JOIN [dbo].[WorkflowActionMap] WAM  ON WAM.WorkflowActionID = WA.ID
			LEFT JOIN [dbo].[WorkflowTask] WT  ON WT.WorkflowTaskID = WAM.WorkflowTaskID
			LEFT OUTER JOIN [dbo].Document D ON DT.ID = D.DocumentTemplateID
			AND (D.DealID = @dealID OR D.DealID IS NULL)
			AND D.IsDeleted !=1 
			AND DW.WorkflowStepID = D.WorkflowStepID		
			LEFT JOIN [dbo].[User] U ON DV.CreatedByUserID = U.UserID

			WHERE 
			DT.IsDeleted !=1 
			AND  WT.WorkflowID = @workflowID

			ORDER BY DT.DisplayOrder
GO
