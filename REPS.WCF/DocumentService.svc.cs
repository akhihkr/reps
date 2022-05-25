using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocumentService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DocumentService.svc or DocumentService.svc.cs at the Solution Explorer and start debugging.
    public class DocumentService : IDocumentService
    {
        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GetDocumentTypeList(string workflowName)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentTypeList(workflowName)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GetDocumentType(int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentType(workflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GetDocumentTemplateList(string workflowName, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentTemplateList(workflowName, dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetDocumentTemplateDetails(int? workflowID, int? dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentTemplateDetails(workflowID, dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Generate Documents from Document template using DocFusion
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GenerateDocuments(Dictionary<string, object> formObjects, List<object> DocumentListID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GenerateDocuments(formObjects, DocumentListID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Upload Standard documents
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator UploadStandardDocuments(Dictionary<string, object> formObject)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.UploadStandardDocuments(formObject)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Upload signed documents
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator UploadSignedDocuments(Dictionary<string, object> formObject)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.UploadSignedDocuments(formObject)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document stored in DB
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public CValidator GetBinaryDocument(int documentID, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetBinaryDocument(documentID, dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Static Document template
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public CValidator GetStaticDocumentTemplate(int documentTemplateID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetStaticDocumentTemplate(documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Signed Document stored in DB
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public CValidator GetSignedBinaryDocument(int documentID, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue; // Allow max size for large files
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetSignedBinaryDocument(documentID, dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        #region Admin Document Generations
        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GetAdminDocumentCategoryList()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetAdminDocumentCategoryList()), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public CValidator GetAdminDocumentTemplateList()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetAdminDocumentTemplateList()), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add New Document Category
        /// </summary>
        /// <param name="documentCategory"></param>
        /// <returns></returns>
        public CValidator AddDocumentCategory(Dictionary<string, dynamic> FormObjects)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.AddDocumentCategory(Convert.ToString(FormObjects["CategoryName"]))), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add Document Template
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public CValidator AddDocumentTemplate(Dictionary<string, dynamic> FormObjects, string templateFile, int documentTypeID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.AddDocumentTemplate(FormObjects, templateFile, documentTypeID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Remove Document Template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public CValidator RemoveDocumentTemplate(int documentTemplateID)
        {
            try
            {
                string thisGuid = Guid.NewGuid().ToString();
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.RemoveDocumentTemplate(documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Edit Document Template Field values
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public CValidator GetEditDocumentTemplateFields(int documentTemplateID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetEditDocumentTemplateFields(documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Edit Document Workflow Step Field values
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public CValidator GetDocumentWorkflowTaskStep(int documentTemplateID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentWorkflowTaskStep(documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Save Document Template
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <param name="templateFile"></param>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public CValidator SaveDocumentTemplate(Dictionary<string, dynamic> FormObjects, string templateFile, int documentTemplateID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.SaveDocumentTemplate(FormObjects, templateFile, documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Document Template Admin
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public CValidator GetDocumentTemplateAdmin(int documentTemplateID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.GetDocumentTemplateAdmin(documentTemplateID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Remove Document workflow from document workflow table
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="documentWorkflowID"></param>
        /// <returns></returns>
        public CValidator RemoveAdminDocumentWorkflow(int? documentTemplateID, int? documentWorkflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Document.RemoveAdminDocumentWorkflow(documentTemplateID == null ? null : documentTemplateID, documentWorkflowID == null ? null : documentWorkflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        #endregion End of Admin Document Generations
    }
}
