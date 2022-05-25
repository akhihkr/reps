using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class DocumentModel
    {
        /// <summary>
        /// get workflow documents description list 
        /// </summary>
        /// <param name="workflowStepID"></param>
        /// <returns></returns>
        public static object GetDocumentTypeList(string workflowStepID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Type list depending on workflow
                        resultValidator = documentClient.GetDocumentTypeList(workflowStepID);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get workflow documents description list 
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetDocumentType(int workflowID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Type list depending on workflow
                        resultValidator = documentClient.GetDocumentType(workflowID);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get workflow document template list
        /// </summary>
        /// <param name="workflowStepID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetDocumentTemplateList(string workflowStepID, int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document Template list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Template list depending on workflow
                        resultValidator = documentClient.GetDocumentTemplateList(workflowStepID, dealID);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document Template list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  get workflow document template list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetDocumentTemplateDetails(int? workflowID, int? dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document Template list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Template list depending on workflow
                        resultValidator = documentClient.GetDocumentTemplateDetails(workflowID, dealID);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document Template list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// upload document for signed and download
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public static object UploadDocumentToFolder(Dictionary<string, dynamic> FormObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document Template list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        if ((FormObjects["UploadOption"]).ToString().Contains(Enums.UploadType.standard.ToString()))
                        {
                            resultValidator = documentClient.UploadStandardDocuments(FormObjects);
                        }
                        else
                        {
                            resultValidator = documentClient.UploadSignedDocuments(FormObjects);
                        }
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document Template list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get signed binary document
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetSignedBinaryDocumentFilename(int documentID, string filename, int DealID)
        {
            return Global.File.GetFileDocuments(DealID, documentID, filename, (int)Enums.FolderPath.Documents);
        }

        /// <summary>
        /// get signed binary document
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetSignedBinaryDocument(int documentID, int DealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        resultValidator = documentClient.GetSignedBinaryDocument(documentID, DealID);
                        var serializer = new JavaScriptSerializer();
                        serializer.MaxJsonLength = Int32.MaxValue;  // Allow big file size 
                        var FileResultObj = serializer.Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return FileResultObj;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object DownloadFile(int documentID, int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        resultValidator = documentClient.GetBinaryDocument(documentID, dealID);
                        var FileResultObj = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return FileResultObj;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Call WCF to get Document list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Call WCF to get Document static document template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object GetStaticDocumentTemplate(int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document static document template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Type list depending on workflow
                        resultValidator = documentClient.GetStaticDocumentTemplate(documentTemplateID);
                        var serializer = new JavaScriptSerializer();
                        serializer.MaxJsonLength = Int32.MaxValue; // Allow big file size 
                        var FileResultObj = serializer.Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return FileResultObj;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Call WCF to get Document static document template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// generate document file if template existed
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public static object GenerateDocuments(Dictionary<string, object> FormObjects, List<object> DocumentListID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Call WCF to get Document list depending on workflow
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        resultValidator = documentClient.GenerateDocuments(FormObjects, DocumentListID.ToArray());
                        var FileResultObj = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return FileResultObj;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Call WCF to get Document list depending on workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        #region Admin
        /// <summary>
        /// Get Document Category list
        /// </summary>
        /// <returns></returns>
        public static object GetAdminDocumentCategoryList()
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion End of variables

                #region Logic : Get Document Category list         
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient()) /// Call WCF to get Document list depending on workflow
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Category list 
                        resultValidator = documentClient.GetAdminDocumentCategoryList();
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End of Logic : Get Document Category list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <returns></returns>
        public static object GetAdminDocumentTemplateList()
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion End of variables

                #region Logic : Get Document Template list          
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())/// Call WCF to get Document list depending on workflow
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //get Document Category list 
                        resultValidator = documentClient.GetAdminDocumentTemplateList();
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Get Document Template list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add New Document Category
        /// </summary>
        /// <returns></returns>
        public static object AddDocumentCategory(Dictionary<string, dynamic> FormObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to add Document Category
                /// Call WCF to add Document Category
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Add Document Category 
                        resultValidator = documentClient.AddDocumentCategory(FormObjects);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to add Document Category
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Document Template
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public static object AddDocumentTemplate(Dictionary<string, dynamic> FormObjects, byte[] templateFile, int documentTypeID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to add Document Template
                /// Call WCF to add Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //Add Document Template
                        resultValidator = documentClient.AddDocumentTemplate(FormObjects, templateFile == null ? null : Common.CFile.ConvertByteArrayToBase64String(templateFile), documentTypeID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to add Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Save Document Template
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <returns></returns>
        public static object SaveDocumentTemplate(Dictionary<string, dynamic> FormObjects, byte[] templateFile, int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to add Document Template
                /// Call WCF to add Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        //Add Document Template
                        resultValidator = documentClient.SaveDocumentTemplate(FormObjects, templateFile == null ? null : Common.CFile.ConvertByteArrayToBase64String(templateFile), documentTemplateID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to add Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove Document template from category
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object RemoveDocumentTemplate(int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to remove Document Template
                /// Call WCF to remove Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Remove Document Template
                        resultValidator = documentClient.RemoveDocumentTemplate(documentTemplateID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return resultValidator.reason;
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion end of  Logic : Call WCF to remove Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Edit Document Template Field values
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object GetEditDocumentTemplateFields(int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to remove Document Template
                /// Call WCF to remove Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Remove Document Template
                        resultValidator = documentClient.GetEditDocumentTemplateFields(documentTemplateID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to remove Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Edit Document Workflow Step Field values
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object GetDocumentWorkflowTaskStep(int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to remove Document Template
                /// Call WCF to remove Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Remove Document Template
                        resultValidator = documentClient.GetDocumentWorkflowTaskStep(documentTemplateID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to remove Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Download Document Template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object[] DownloadDocumentTemplate(int documentTemplateID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to get Document Template
                /// Call WCF to get Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Get Document Template
                        resultValidator = documentClient.GetDocumentTemplateAdmin(documentTemplateID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to get Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add workflowtaskid for each document to documentworkflow table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object AddWorkflowTask(DATA.Entity.DocumentWorkflow obj)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to add Document Template
                /// Call WCF to add Document Template
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        //Add Document workflow
                        resultValidator = workflowClient.AddWorkflowStep(obj);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of Logic : Call WCF to add Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove Document workflow from document workflow table
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="documentWorkflowID"></param>
        /// <returns></returns>
        public static object RemoveAdminDocumentWorkflow(int? documentTemplateID, int? documentWorkflowID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Call WCF to remove Document Template
                /// Call WCF to remove Document Template
                using (DocumentServiceReference.DocumentServiceClient documentClient = new DocumentServiceReference.DocumentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(documentClient.InnerChannel))
                    {
                        // Remove Document Template
                        resultValidator = documentClient.RemoveAdminDocumentWorkflow(documentTemplateID == null ? null : documentTemplateID, documentWorkflowID == null ? null : documentWorkflowID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of  Logic : Call WCF to remove Document Template
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

       #endregion Admin
    }
}