using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Data.Entity.Core.Objects;
using System.IO;
using Global;
using Docfusion;
using System.Xml;

namespace REPS.Business
{
    public class Document
    {
        public const string FilenameSeparator = "_";

        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <returns></returns>
        public static object GetDocumentTypeList(string workflowName)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Document Type list
                results = REPSDB.REPS_GetDocumentTypeListByWorkflow(Convert.ToInt32(workflowName));
                return results;
                #endregion end of logic : Get Document Type list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <returns></returns>
        public static object GetDocumentType(int workflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Document Type list
                results = REPSDB.REPS_GetDocumentType(workflowID);
                return results;
                #endregion end of logic : Get Document Type list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <returns></returns>
        public static object GetDocumentTemplateList(string workflowName, int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables 

                #region logic : Get Document Template list
                results = REPSDB.REPS_GetDocumentTemplateListByWorkflow(Convert.ToInt32(workflowName), dealID);
                return results;
                #endregion end of logic : Get Document Template list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Document Template list
        /// </summary>
        /// <returns></returns>
        public static object GetDocumentTemplateDetails(int? workflowID, int? dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables 

                #region logic : Get Document Template list
                results = REPSDB.REPS_GetDocumentTemplateDetails(workflowID, dealID);
                return results;
                #endregion end of logic : Get Document Template list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Generate Documents using DocFusion
        /// </summary>
        /// <returns></returns>
        public static object GenerateDocuments(Dictionary<string, object> formObjects, List<object> DocumentListID)
        {
            try
            {
                #region variables             
                //object results = null;
                var results = new Dictionary<string, object>();
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                int DealID = 0;
                int UserID = 0;
                int EntityID = 0;
                int WorkflowID = 0;
                Enums.MimeType DocType = Enums.MimeType.pdf;     // PDF as Default value
                string[] parametersplited;
                int DocumentTemplateID = 0;
                int WorkflowStepID = 0;
                string WorkFlowStepName = "";
                #endregion end of variables 

                #region logic : Generate Documents using DocFusion
                /// Get DealId from submitted form
                if (formObjects.Keys.Contains("dealID"))
                {
                    DealID = Convert.ToInt32(formObjects["dealID"]);
                }

                /// Get UserID
                if (formObjects.Keys.Contains("userID"))
                {
                    UserID = Convert.ToInt32(formObjects["userID"]);
                }

                /// Get entityID
                if (formObjects.Keys.Contains("entityID"))
                {
                    EntityID = Convert.ToInt32(formObjects["entityID"]);
                }

                /// Get workflowID
                if (formObjects.Keys.Contains("WorkflowID"))
                {
                    WorkflowID = Convert.ToInt32(formObjects["WorkflowID"]);
                }

                /// Get Document Mime Type ( PDF or Docx)
                if (formObjects.Keys.Contains("docType"))
                {
                    if (Convert.ToString(formObjects["docType"]).Contains("PDF"))
                    {
                        DocType = Enums.MimeType.pdf;
                    }
                    else
                    {
                        DocType = Enums.MimeType.docx;
                    }
                }

                foreach (var DocumentList in DocumentListID as IEnumerable<dynamic>)
                {

                    if (DocumentList.ToString().Contains("_"))
                    {
                        parametersplited = DocumentList.ToString().Split('_');
                        DocumentTemplateID = Convert.ToInt32(parametersplited[0]);
                        WorkflowStepID = Convert.ToInt32(parametersplited[1]);

                        WorkFlowStepName = GetWorkFlowStepName(WorkflowStepID); //Get WorkFlowStepName

                        // Get template filename path
                        string templatefile = GetDocumentTemplateByID(DocumentTemplateID);
                        string filepath = Global.File.GetFolderPath((int)Enums.FolderPath.Templates) + templatefile;
                        byte[] TemplateContent = System.IO.File.ReadAllBytes(filepath);

                        // Get template JSON data
                        string TemplateJsonData = GetTemplateJSONData(DealID, DocumentTemplateID);

                        #region Get Document name
                        string docName = GetDocumentName(DocumentTemplateID).TrimEnd('.');
                        #endregion end of Get Document name

                        //Connect to DocFusion and generate Document
                        Docfusion.PDFDocumentOutputSettings PdfSetting = null;
                        CDocfusionLite.SetDocfusionParams(DocType);

                        #region Check if docs, set pdfsetting to null                   
                        if (DocType.ToString().ToLower() == "docx")
                        {
                            PdfSetting = null;
                        }
                        else if (DocType.ToString().ToLower() == "pdf")
                        {
                            PdfSetting = CDocfusionLite.pdfSettings;
                        }
                        #endregion Check if docs, set pdfsetting to null 

                        Task<GenerationResult> generationResult = CDocfusionLite.GenerateDocument(TemplateContent, TemplateJsonData, null, "1", null, PdfSetting);

                        generationResult.Wait();

                        if (Convert.ToInt32(generationResult.Result.GenerationErrorCode) == 0)      // Successfully generated   
                        {
                            //update document table set isdeleted true
                            REPSDB.REPS_UpdateDocumentTemplate(DocumentTemplateID, WorkflowStepID, true, rowCount);
                            //end of update document table set isdeleted true

                            // Save generated document in database
                            int? SaveResult = SaveGeneratedDocument(Common.CFile.FromBase64StringToByteArray(generationResult.Result.DocumentData), DocumentTemplateID, WorkflowStepID, UserID, DealID, DocType.ToString(), WorkflowID, EntityID, DocumentTemplateID);

                            if (SaveResult == null || Convert.ToInt32(SaveResult) == 0)
                            {
                                results.Add(docName + "-" + WorkFlowStepName, -2); // Error occurred while saving
                            }
                            else
                            {
                                results.Add(docName + "-" + WorkFlowStepName, 0);
                            }
                        }
                        else if (Convert.ToInt32(generationResult.Result.GenerationErrorCode) == 7)   //Information missing in JSON
                        {
                            results.Add(docName + "-" + WorkFlowStepName, "Param " + -1);
                        }
                        else            // Error Document not generated
                        {
                            results.Add(docName + "-" + WorkFlowStepName, TemplateJsonData);
                        }
                    }
                }
                return results;
                #endregion end of logic : Generate Documents using DocFusion
            }
            catch (DirectoryNotFoundException DirErr)               // Directory not found
            {
                var errResult = new Dictionary<string, object>();
                errResult.Add("", -3);
                return errResult;

            }
            catch (FileNotFoundException FileErr)                    // File not found
            {
                var errResult = new Dictionary<string, object>();
                errResult.Add("", -3);
                return errResult;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// Get Document Template by ID
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public static string GetDocumentTemplateByID(int templateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<string> results = null;
                #endregion end of variables 

                #region logic : Get Document Template by ID
                results = REPSDB.REPS_GetDocumentTemplateByID(templateID);
                return results.FirstOrDefault();
                #endregion end of logic : Get Document Template by ID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Template data for DocFusion in JSON format
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public static string GetTemplateJSONData(int dealID, int templateID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                string results = null;
                string error = null;
                #endregion end of variables

                #region logic : Get Template data for DocFusion in JSON format

                // Build Query parameters
                string QueryParameter = "@DealID=" + dealID;

                // Get Template XML SPROC
                string TemplateSPname = REPSDB.REPS_GetDocumentTemplateXMLByID(templateID).FirstOrDefault();
                if (TemplateSPname != null)
                {
                    // Execute Dynamic query
                    List<string> queryResult = REPSDB.Database.SqlQuery<string>("EXEC  " + TemplateSPname + " " + QueryParameter).ToList();
                    foreach (var item in queryResult)
                    {
                        results = results + item;   // Concatenate result if multiple lines
                    }

                    #region: Validate xml
                    error = ValidateXML(results);

                    if (!(error.Equals("NoError")))
                    {
                        return error;

                    }
                    #endregion: 

                    return results;
                }

                //$$$$$$$$$$$$$$$$$$$$$$$$$ To Delete code below after all templates have been finalised $$$$$$$$$$$$$$$$$$$$$$$$$

                // get json details for deal participant etc
                var deals = Template.GetClientCare(dealID) as IEnumerable<dynamic>;
                FileStream fileStream;

                // Temporary JSON file => to replace with proper sproc
                switch (templateID)
                {
                    case 1:
                        fileStream = new FileStream(@"C:\Log\ClientCareLetter.txt", FileMode.Open, FileAccess.Read);
                        break;
                    case 2:
                        fileStream = new FileStream(@"C:\Log\Mortgage_Letter_json.txt", FileMode.Open, FileAccess.Read);
                        break;
                    case 15:
                        fileStream = new FileStream(@"C:\Log\Mortgage_Letter_json.txt", FileMode.Open, FileAccess.Read);
                        break;
                    default:
                        return "{'CustomerDetails':{'FirstName':'Anthony','Surname':'Bloom'}}";
                }

                using (StreamReader r = new StreamReader(fileStream))
                {
                    string json = r.ReadToEnd();
                    dynamic result = new JavaScriptSerializer().Deserialize<dynamic>(json);
                    foreach (var deal in deals)
                    {
                        result["Deal"]["FileRef"] = deal.DealID;
                        result["Deal"]["TotalLegalCosts"] = deal.Value;
                        result["Deal"]["Buyers"]["Buyer"]["ClientName"] = deal.GivenName;
                        result["Deal"]["Buyers"]["Buyer"]["ClientSurname"] = deal.FamilyName;
                        result["Deal"]["CurrentDate"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                        break; // took first one only for demo
                    }
                    return new JavaScriptSerializer().Serialize(result);
                }
                #endregion end of logic : 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Save Generated Documents
        /// </summary>
        /// <returns></returns>
        public static int? SaveGeneratedDocument(byte[] documentContent, int documentTemplateID,int WorkflowStepID, int userID, int dealID, string mimeType, int workflowID, int entityID, int templateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Save Generated Documents
                string filename = GenerateFilename(workflowID, entityID, dealID, mimeType, templateID);
                int? CurrentDocumentID = (REPSDB.REPS_GetDocumentIDbyDocumentTemplateID(dealID, documentTemplateID)).FirstOrDefault();
                results = REPSDB.REPS_AddGeneratedDocument(dealID, userID, documentTemplateID, WorkflowStepID, mimeType, (int)Enums.TransactionType.New, 4, (int)Enums.WokflowTask.GenerateDocument, filename, rowCount);

                //Save generated document
                // Build New File Path
                string filepath = Global.File.GetFilePathDocuments(dealID, (int)rowCount.Value, filename, (int)Enums.FolderPath.Documents);

                // Create Directory and move old files (if any)
                Global.File.ArchiveDocumentFiles(dealID, (CurrentDocumentID == null ? 0 : Convert.ToInt32(CurrentDocumentID)), (int)rowCount.Value, (int)Enums.FolderPath.Documents);

                // Write New files to path
                Global.File.WriteFiletoPath(filepath, filename, documentContent);

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Save Generated Documents
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Generate a unique filename
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="entityID"></param>
        /// <param name="dealID"></param>
        /// <param name="mimeType"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public static string GenerateFilename(int workflowID, int entityID, int dealID, string mimeType, int templateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<REPS.DATA.Entity.REPS_GetEntities_Result> EntityResults = null;
                ObjectResult<string> DocumentVersionResults = null;
                string Firmname = null;
                string Versionname = null;
                string GeneratedFilename = null;
                #endregion end of variables

                #region logic :  Generate a unique filename
                // Get Firm name
                EntityResults = REPSDB.REPS_GetEntities(null, null, null, entityID, -1);
                Firmname = EntityResults.FirstOrDefault().Name.ToString();

                // Get Template Version
                DocumentVersionResults = REPSDB.REPS_GetDocumentVersionByTemplateID(templateID);
                Versionname = DocumentVersionResults.FirstOrDefault().ToString();

                // Build Filename::: Convention: [Firmname] -[Workflow ID] -[Matter Number] -[Date] -[Time] -[Version].PDF
                GeneratedFilename = Firmname + FilenameSeparator + workflowID.ToString() + FilenameSeparator + dealID.ToString()
                                  + FilenameSeparator + DateTime.Now.ToString("yyyyMMdd") + FilenameSeparator + DateTime.Now.ToString("HHmm")
                                  + FilenameSeparator + 'v' + Versionname + '.' + mimeType;
                return GeneratedFilename;
                #endregion end of logic :  Generate a unique filename
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// add standard document to database
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object UploadStandardDocuments(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                string mimeType;
                string Filename = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : update standard document to database
                //vairables
                int DocumentTemplateID = Convert.ToInt32(formObjects["DocumentTemplateID"]);
                var UploadOption = formObjects["UploadOption"];
                int dealID = Convert.ToInt32(formObjects["dealID"]);
                int userID = Convert.ToInt32(formObjects["userID"]);
                int workflowStepId = Convert.ToInt32(formObjects["workflowStepId"]);
                // end of variables 

                //byte[] FileContent = Common.CFile.FromBase64StringToByteArray(formObjects["UploadedFile"]);
                byte[] FileContent = formObjects["UploadedFile"];
                if (formObjects["FileExtension"].ToString().Contains(Enums.MimeType.pdf.ToString()))
                {
                    mimeType = Enums.MimeType.pdf.ToString();
                }
                else
                {
                    mimeType = Enums.MimeType.docx.ToString();
                }
                Filename = formObjects["Filename"];
                results = REPSDB.REPS_AddUploadedStandardDocument(Filename, mimeType, DocumentTemplateID, dealID, userID, (int)Enums.TransactionType.New, 4, workflowStepId, (int)Enums.WokflowTask.UploadDocument, rowCount);

                //Save uploaded document
                // Build New File Path
                string filepath = Global.File.GetFilePathDocuments(dealID, (int)rowCount.Value, Filename, (int)Enums.FolderPath.Documents);

                // Create Directory
                Global.File.CreateNewDirectoryDocuments(dealID, (int)rowCount.Value, (int)Enums.FolderPath.Documents);

                // Write New files to path
                Global.File.WriteFiletoPath(filepath, Filename, FileContent);

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic :update standard document to database
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// save uploadSigned Document to database
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object UploadSignedDocuments(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                string mimeType;
                string Filename = null;
                #endregion end of variables 

                #region logic : save uploadSigned Document to database

                //variables 
                int DocumentTemplateID = Convert.ToInt32(formObjects["DocumentTemplateID"]);
                var UploadOption = formObjects["UploadOption"];
                int dealID = Convert.ToInt32(formObjects["dealID"]);
                int userID = Convert.ToInt32(formObjects["userID"]);
                int workflowStepId = Convert.ToInt32(formObjects["workflowStepId"]);
                // end of variables

                byte[] FileContent = formObjects["UploadedFile"];
                //byte[] FileContent = Common.CFile.FromBase64StringToByteArray(formObjects["UploadedFile"]);

                if (formObjects["FileExtension"].ToString().Contains(Enums.MimeType.pdf.ToString()))
                {
                    mimeType = Enums.MimeType.pdf.ToString();
                }
                else
                {
                    mimeType = Enums.MimeType.docx.ToString();
                }
                Filename = formObjects["Filename"];
                results = REPSDB.REPS_AddUploadedSignedDocument(Filename, mimeType, DocumentTemplateID, dealID, userID, (int)Enums.TransactionType.New, 4, workflowStepId, (int)Enums.WokflowTask.UploadDocument, rowCount);

                //Save uploaded document
                // Build New File Path
                string filepath = Global.File.GetFilePathDocuments(dealID, (int)rowCount.Value, Filename, (int)Enums.FolderPath.Documents);

                // Create Directory
                Global.File.CreateNewDirectoryDocuments(dealID, (int)rowCount.Value, (int)Enums.FolderPath.Documents);

                // Write New files to path
                Global.File.WriteFiletoPath(filepath, Filename, FileContent);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : save uploadSigned Document to database
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Retrieve stored document
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public static object GetBinaryDocument(int documentID, int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<REPS.DATA.Entity.REPS_GetStoredDocumentByID_Result> results = null;
                Byte[] TemplateFile = null;
                #endregion end of variables

                #region logic : Retrieve stored document
                results = REPSDB.REPS_GetStoredDocumentByID(documentID);
                var res = results.FirstOrDefault();

                Dictionary<string, dynamic> convertedresult = new Dictionary<string, dynamic>();
                convertedresult.Add("TemplateDisplayName", res.GeneratedDocFileName);

                // Get Template Content
                TemplateFile = Global.File.GetFileDocuments(dealID, res.ID, res.GeneratedDocFileName, (int)Enums.FolderPath.Documents);
                convertedresult.Add("FileContent", Common.CFile.ConvertByteArrayToBase64String(TemplateFile));
                convertedresult.Add("MimeTypeID", res.MimeTypeID);
                return convertedresult;
                #endregion end of logic : Retrieve stored document
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Retrieve Static document template
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public static object GetStaticDocumentTemplate(int documentTemplateID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<REPS.DATA.Entity.REPS_DownloadStaticTemplate_Result> results = null;
                byte[] TemplateFile = null;
                #endregion end of variables

                #region logic : Retrieve Static document template
                results = REPSDB.REPS_DownloadStaticTemplate(documentTemplateID);
                var res = results.FirstOrDefault();

                Dictionary<string, dynamic> convertedresult = new Dictionary<string, dynamic>();
                convertedresult.Add("TemplateDisplayName", res.TemplateFileName);
                // Get Template Content
                TemplateFile = Global.File.GetFile(res.ID, res.TemplateFileName, (int)Enums.FolderPath.Templates);
                convertedresult.Add("FileContent", Common.CFile.ConvertByteArrayToBase64String(TemplateFile));
                convertedresult.Add("MimeTypeID", res.MimeTypeID);
                return convertedresult;
                #endregion end of logic : Retrieve Static document template
            }
            catch (IOException err)
            {
                return null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Retrieve uploaded signed document
        /// </summary>
        /// <param name="workflowName"></param>
        /// <returns></returns>
        public static object GetSignedBinaryDocument(int documentID, int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<REPS.DATA.Entity.REPS_GetStoredSignedDocumentByID_Result> results = null;
                Byte[] TemplateFile = null;
                #endregion

                #region logic : Retrieve uploaded signed document
                results = REPSDB.REPS_GetStoredSignedDocumentByID(documentID);
                var res = results.FirstOrDefault();

                Dictionary<string, dynamic> convertedresult = new Dictionary<string, dynamic>();
                convertedresult.Add("TemplateDisplayName", res.SignedDocFileName);
                // Get Template Content 
                TemplateFile = Global.File.GetFileDocuments(dealID, res.ID, res.SignedDocFileName, (int)Enums.FolderPath.Documents);
                //convertedresult.Add("FileContent", Common.CFile.ConvertByteArrayToBase64String(TemplateFile));
                convertedresult.Add("FileContent", TemplateFile);
                convertedresult.Add("MimeTypeID", res.SignedDocMimeTypeID);
                return convertedresult;
                #endregion end of logic : Retrieve uploaded signed document
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region Admin Document Generations 

        /// <summary>
        /// Get Document Type list
        /// </summary>
        /// <returns></returns>
        public static object GetAdminDocumentCategoryList()
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_ADM_GetDocumentCategoryList();
                return results;
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Document Template list
                results = REPSDB.REPS_ADM_GetDocumentTemplateList();
                return results;
                #endregion end of logic : Get Document Template list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add Document category
        /// </summary>
        /// <param name="documentCategory"></param>
        /// <returns></returns>
        public static object AddDocumentCategory(string documentCategory)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic : Add Document category
                results = REPSDB.REPS_ADM_AddNewDocumentCategory(documentCategory, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Add Document category
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add Document Template
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <param name="templateFile"></param>
        /// <param name="documentTypeID"></param>
        /// <returns></returns>
        public static object AddDocumentTemplate(Dictionary<string, dynamic> FormObjects, string templateFile, int documentTypeID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                string filename = null;
                //string[] workflowTaskStepSplit = null;
                #endregion end of variables

                #region logic : add document template
                // Get filename
                if (FormObjects.Keys.Contains("filename"))
                {
                    filename = Convert.ToString(FormObjects["filename"]);
                }

                results = REPSDB.REPS_ADM_AddDocumentTemplate
                    (
                        Convert.ToInt32(FormObjects["documentTypeID"]),
                        Convert.ToString(FormObjects["TemplateName"]),
                        Convert.ToString(FormObjects["TemplateVersion"]) == null ? null : Convert.ToString(FormObjects["TemplateVersion"]),
                        FormObjects.Keys.Contains("TemplateSPROC") ? Convert.ToString(FormObjects["TemplateSPROC"]) : null,
                        filename, FormObjects.Keys.Contains("MimeType") ? Convert.ToString(FormObjects["MimeType"]) : null,
                        Convert.ToInt32(FormObjects["UserID"]),
                        FormObjects.Keys.Contains("TemplateESign") ? true : false,
                        FormObjects.Keys.Contains("IsStaticDocument") ? true : false,
                        FormObjects.Keys.Contains("IsActive") ? true : false,
                        FormObjects.Keys.Contains("IsDocFusionTemplate") ? true : false,
                        Guid.NewGuid(),
                        identity
                    );

                // Save uploaded document template to folder
                if (FormObjects.Keys.Contains("filename"))
                {
                    // Get Byte Array
                    byte[] filecontent = Common.CFile.FromBase64StringToByteArray(templateFile);

                    // Build New File Path
                    string filepath = Global.File.GetFilePathTemplates(Convert.ToInt32(identity.Value), filename, (int)Enums.FolderPath.Templates);

                    // Create Directory
                    Global.File.CreateNewDirectoryTemplates(Convert.ToInt32(identity.Value), (int)Enums.FolderPath.Templates);

                    // Write New files to path
                    Global.File.WriteFiletoPath(filepath, filename, filecontent);
                }

                //// Get workflowStepID
                //foreach (var frmObjectWorkflowKey in FormObjects as Dictionary<string, dynamic>)
                //{
                //    if (frmObjectWorkflowKey.Key.Contains("workflowStepID"))
                //    {
                //        if (frmObjectWorkflowKey.Value.Contains(","))
                //        {
                //            workflowTaskStepSplit = frmObjectWorkflowKey.Value.ToString().Split(',');
                //            #region logic : Add workflowtaskid for each document to documentworkflow table
                //            foreach (var workflowStepID in workflowTaskStepSplit as IEnumerable<dynamic>)
                //            {
                //                REPSDB.REPS_ADM_AddWorkflowStepToDocumentTemplate(
                //                        Convert.ToInt32(identity.Value),
                //                        Convert.ToInt32(workflowStepID),
                //                        Convert.ToInt32(FormObjects["UserID"]),
                //                        rowCount
                //                    );
                //            }
                //            #endregion end of logic : Add workflowtaskid for each document to documentworkflow table
                //        }
                //        else
                //        {
                //            #region logic : Add workflowtaskid for each document to documentworkflow table
                //            REPSDB.REPS_ADM_AddWorkflowStepToDocumentTemplate(
                //                    Convert.ToInt32(identity.Value),
                //                    Convert.ToInt32(frmObjectWorkflowKey.Value),
                //                    Convert.ToInt32(FormObjects["UserID"]),
                //                    rowCount
                //                );
                //            #endregion end of logic : Add workflowtaskid for each document to documentworkflow table
                //        }
                //    }
                //}

                return (identity.Value == DBNull.Value) ? null : (int?)identity.Value;
                #endregion end of logic : add document template
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Remove Document Template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object RemoveDocumentTemplate(int documentTemplateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Remove Document Template
                results = REPSDB.REPS_ADM_RemoveDocumentTemplate(documentTemplateID, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Remove Document Template
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Edit Document Template Field values
                results = REPSDB.REPS_ADM_GetEditDocumentTemplateFields(documentTemplateID);
                return results;
                #endregion end of logic : Get Edit Document Template Field values
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Edit Document Template Field values
                results = REPSDB.REPS_ADM_GetDocumentWorkflowTaskList(documentTemplateID);
                return results;
                #endregion end of logic : Get Edit Document Template Field values
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Save Document Template
        /// </summary>
        /// <param name="documentCategory"></param>
        /// <returns></returns>
        public static object SaveDocumentTemplate(Dictionary<string, dynamic> FormObjects, string templateFile, int documentTemplateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                object results = null;
                string filename = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                string[] workflowTaskStepSplit = null;
                Guid documentTemplateGUID = Guid.Empty;
                #endregion end of variables

                #region logic : Save Document Template 

                if (FormObjects.Keys.Contains("filename")) // Get filename
                {
                    filename = Convert.ToString(FormObjects["filename"]);
                }

                if (FormObjects.Keys.Contains("TemplateFixGUID")) // Get template guid
                {
                    documentTemplateGUID = new Guid(FormObjects["TemplateFixGUID"].Trim());
                }


                #region get document template details to check if file name is already exist or not
                object templateFilename = REPSDB.REPS_ADM_GetTemplateFilename(documentTemplateID).FirstOrDefault();
                #endregion end of gett document template details to check if file name is already exist or not

                #region if filename has been uploaded then achive the old one
                if (filename != null) //chk if file has been uploaded
                {
                    #region chk if folder exist, move to new folder
                    if (templateFilename != null)
                    {
                        #region save the template version and template document and get the last inserted id
                        results = REPSDB.REPS_ADM_AddDocumentTemplate
                            (
                                Convert.ToInt32(FormObjects["DocumentTypeID"]),
                                Convert.ToString(FormObjects["TemplateName"]),
                                Convert.ToString(FormObjects["TemplateVersion"]) == null ? null : Convert.ToString(FormObjects["TemplateVersion"]),
                                FormObjects.Keys.Contains("TemplateSPROC") ? Convert.ToString(FormObjects["TemplateSPROC"]) : null,
                                filename, FormObjects.Keys.Contains("MimeType") ? Convert.ToString(FormObjects["MimeType"]) : null,
                                Convert.ToInt32(FormObjects["UserID"]),
                                FormObjects.Keys.Contains("TemplateESign") ? true : false,
                                FormObjects.Keys.Contains("IsStaticDocument") ? true : false,
                                FormObjects.Keys.Contains("IsActive") ? true : false,
                                FormObjects.Keys.Contains("IsDocFusionTemplate") ? true : false,
                                documentTemplateGUID,
                                identity
                            );
                        #endregion end of save the template version and template document and get the last inserted id

                        #region update document template table set delete to 1 (true), to avoid display when get document details
                        REPSDB.REPS_ADM_UpdateDocumentTemplate(Convert.ToInt32(FormObjects["DocumentVersionID"]), rowCount);
                        #endregion end of update document template table set delete to 1 (true), to avoid display when get document details

                        // move file to new directory created (Archive)
                        // Global.File.ArchiveTemplatesFiles(documentTemplateID, Convert.ToInt32(identity.Value), (int)Enums.FolderPath.Templates);
                        Global.File.ArchiveTemplatesFiles(Convert.ToInt32(FormObjects["DocumentVersionID"]), Convert.ToInt32(identity.Value), (int)Enums.FolderPath.Templates);

                        #region  Save uploaded document template to folder 
                        byte[] filecontent = Common.CFile.FromBase64StringToByteArray(templateFile);// Get Byte Array                                                                                               
                        string filepath = Global.File.GetFilePathTemplates(Convert.ToInt32(identity.Value), filename, (int)Enums.FolderPath.Templates);// Build New File Path                      
                        Global.File.CreateNewDirectoryTemplates(Convert.ToInt32(identity.Value), (int)Enums.FolderPath.Templates);// Create New folder                 
                        Global.File.WriteFiletoPath(filepath, filename, filecontent);// Write New files to path
                        #endregion end of Save uploaded document template to folder 
                    }
                    #endregion end of chk if folder exist, move to new folder

                    #region verify if folder does not exist, creates new one with new template version id
                    else
                    {
                        #region update file to folder 
                        results = REPSDB.REPS_ADM_UpdateDocumentVersionDocumentTemplate
                        (
                            documentTemplateID,
                            Convert.ToInt32(FormObjects["DocumentVersionID"]),
                            Convert.ToString(FormObjects["TemplateName"]),
                            Convert.ToString(FormObjects["TemplateVersion"]),
                            FormObjects.Keys.Contains("TemplateSPROC") ? Convert.ToString(FormObjects["TemplateSPROC"]) : null,
                            filename == null ? (templateFilename == null ? null : templateFilename.ToString()) : filename,
                            FormObjects.Keys.Contains("MimeType") ? Convert.ToString(FormObjects["MimeType"]) : null,
                            Convert.ToInt32(FormObjects["UserID"]),
                            FormObjects.Keys.Contains("TemplateESign") ? true : false,
                            FormObjects.Keys.Contains("IsStaticDocument") ? true : false,
                            FormObjects.Keys.Contains("IsActive") ? true : false,
                            FormObjects.Keys.Contains("IsDocFusionTemplate") ? true : false,
                            documentTemplateGUID,
                            rowCount
                        );
                        #endregion end of update file to folder 

                        #region  Save uploaded document template to folder 
                        byte[] filecontent = Common.CFile.FromBase64StringToByteArray(templateFile);// Get Byte Array                  
                        //string filepath = Global.File.GetFilePathTemplates(documentTemplateID, filename, (int)Enums.FolderPath.Templates);// Build New File Path                  
                        string filepath = Global.File.GetFilePathTemplates(Convert.ToInt32(FormObjects["DocumentVersionID"]), filename, (int)Enums.FolderPath.Templates);// Build New File Path                  
                        //Global.File.CreateNewDirectoryTemplates(documentTemplateID, (int)Enums.FolderPath.Templates);// Create New folder          
                        Global.File.CreateNewDirectoryTemplates(Convert.ToInt32(FormObjects["DocumentVersionID"]), (int)Enums.FolderPath.Templates);// Create New folder          
                        Global.File.WriteFiletoPath(filepath, filename, filecontent); // Write New files to path
                        #endregion end of Save uploaded document template to folder 
                    }
                    #endregion end of verify if folder does not exist, creates new one with new template version id
                }
                #endregion end of if filename has been uploaded then achive the old one

                #region if no filename uploaded, then updated the fields in document template and document version with document template ID
                else if (filename == null)
                {
                    results = REPSDB.REPS_ADM_UpdateDocumentVersionDocumentTemplate
                                (
                                    documentTemplateID,
                                    Convert.ToInt32(FormObjects["DocumentVersionID"]),
                                    Convert.ToString(FormObjects["TemplateName"]),
                                    Convert.ToString(FormObjects["TemplateVersion"]),
                                    FormObjects.Keys.Contains("TemplateSPROC") ? Convert.ToString(FormObjects["TemplateSPROC"]) : null,
                                    filename == null ? (templateFilename == null ? null : templateFilename.ToString()) : filename,
                                    FormObjects.Keys.Contains("MimeType") ? Convert.ToString(FormObjects["MimeType"]) : null,
                                    Convert.ToInt32(FormObjects["UserID"]),
                                    FormObjects.Keys.Contains("TemplateESign") ? true : false,
                                    FormObjects.Keys.Contains("IsStaticDocument") ? true : false,
                                    FormObjects.Keys.Contains("IsActive") ? true : false,
                                    FormObjects.Keys.Contains("IsDocFusionTemplate") ? true : false,
                                    documentTemplateGUID,
                                    rowCount
                                );
                }
                #endregion end of if no filename uploaded, then updated the fields in document template and document version with document template ID    

                //#region set delete true in document workflow table if workflow existed with the same template id
                //REPSDB.REPS_ADM_DeleteDocumentWorkflow(documentTemplateID, null, true, rowCount);
                //#endregion end of set delete true in document workflow table if workflow existed with the same template id

                //#region update document workflow store procedure with the lastest id if document workflow existed
                //REPSDB.REPS_ADM_UpdateDocumentWorkflow(Convert.ToInt32(identity.Value) == 0 ? Convert.ToInt32(FormObjects["DocumentVersionID"]) : Convert.ToInt32(identity.Value), Convert.ToInt32(FormObjects["DocumentVersionID"]), rowCount);
                //#endregion end of  update document workflow store procedure with the lastest id if document workflow existed

                //#region add workflow document to table if workflow was added when editing
                //foreach (var frmObjectWorkflowKey in FormObjects as Dictionary<string, dynamic>) // Get workflowStepID
                //{
                //    if (frmObjectWorkflowKey.Key.Contains("workflowStepID"))
                //    {
                //        if (frmObjectWorkflowKey.Value.Contains(","))
                //        {
                //            workflowTaskStepSplit = frmObjectWorkflowKey.Value.ToString().Split(',');
                //            #region logic : Add workflowtaskid for each document to documentworkflow table
                //            foreach (var workflowStepID in workflowTaskStepSplit as IEnumerable<dynamic>)
                //            {
                //                REPSDB.REPS_ADM_AddWorkflowStepToDocumentTemplate(
                //                        Convert.ToInt32(identity.Value) == 0 ? Convert.ToInt32(FormObjects["DocumentVersionID"]) : Convert.ToInt32(identity.Value),
                //                        Convert.ToInt32(workflowStepID),
                //                        Convert.ToInt32(FormObjects["UserID"]),
                //                        rowCount
                //                    );
                //            }
                //            #endregion end of logic : Add workflowtaskid for each document to documentworkflow table
                //        }
                //        else
                //        {
                //            #region logic : Add workflowtaskid for each document to documentworkflow table
                //            REPSDB.REPS_ADM_AddWorkflowStepToDocumentTemplate(
                //                    Convert.ToInt32(identity.Value) == 0 ? Convert.ToInt32(FormObjects["DocumentVersionID"]) : Convert.ToInt32(identity.Value),
                //                    Convert.ToInt32(frmObjectWorkflowKey.Value),
                //                    Convert.ToInt32(FormObjects["UserID"]),
                //                    rowCount
                //                );
                //            #endregion end of logic : Add workflowtaskid for each document to documentworkflow table
                //        }
                //    }
                //}
                //#endregion end of add workflow document to table if workflow was added when editing
                #endregion end of logic : Save Document Template 

                return results;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Document Template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public static object GetDocumentTemplateAdmin(int documentTemplateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Document Template
                results = REPSDB.REPS_ADM_DownloadTemplate(documentTemplateID);
                return results;
                #endregion end of logic : Get Document Template
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Remove Document Template
                results = REPSDB.REPS_ADM_DeleteDocumentWorkflow(documentTemplateID == null ? null : documentTemplateID, documentWorkflowID == null ? null : documentWorkflowID, true, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Remove Document Template
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion End of Admin Document Generations

        #region Validate JsonXML before sending to service
        /// <summary>
        /// save uploadSigned Document to database
        /// </summary>
        /// <param string="xmlDoc"></param>
        /// <returns>Error message</returns>
        /// <Author>Hemraj B</Author>
        public static string ValidateXML(string xmlDoc)
        {
            try
            {
                #region variables
                string error = null;
                var xml = new XmlDocument();
                #endregion

                #region Logic
                xml.LoadXml(xmlDoc);

                List<string> ElementsMissing = new List<string>();

                foreach (XmlNode itemNode in xml.DocumentElement.ChildNodes)
                {
                    System.Xml.XmlElement itemElement = (XmlElement)itemNode;

                    for (int i = 0; i < itemElement.ChildNodes.Count; i++)
                    {
                        if (itemElement.ChildNodes[i].InnerXml.Equals("0"))
                        {
                            string Elements = itemElement.ChildNodes[i].Name;
                            Elements = Elements.Replace("Conveyancers", "Buyer Sollicitor");
                            ElementsMissing.Add(Elements);
                        }
                    }
                }
                if (ElementsMissing.Count > 0)
                {
                    for (int j = 0; j < ElementsMissing.Count; j++)
                    {
                        error = error + ElementsMissing[j] + ",";

                    }
                    error = "Param " + error.TrimEnd(',');

                }
                else
                {
                    error = "NoError";
                }
                return error;
            }
            #endregion Logic

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion Validate JsonXML before sending to service

        #region Get Template ID
        /// <summary>
        /// Get Document Template by ID
        /// </summary>
        /// <param int templateID></param>
        /// <returns></returns>
        /// <Author>Hemraj B</Author>
        public static string GetDocumentName(int templateID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<string> results = null;
                #endregion end of variables 

                #region logic : Get Document Template by ID
                results = REPSDB.REPS_GetFileNameFromTemplateDescription(templateID, "");
                return results.FirstOrDefault();
                #endregion end of logic : Get Document Template by ID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion Get Template ID


        #region Get WorkFlowStep Name
        /// <summary>
        /// Get Document Template by ID
        /// </summary>
        /// <param int templateID></param>
        /// <returns></returns>
        /// <Author>Hemraj B</Author>
        public static string GetWorkFlowStepName(int WorkFlowStepId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<string> results = null;
                #endregion end of variables 

                #region logic : Get Document Template by ID
                results = REPSDB.REPS_GetWorkFlowAction(WorkFlowStepId);
                return results.FirstOrDefault();
                #endregion end of logic : Get Document Template by ID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion Get WorkFlowStep Name
    }
}
