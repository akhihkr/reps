using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using Global;

namespace REPS.Business
{
    public class Correspondence
    {
        /// <summary>
        /// Insert a new correspondence into the DB
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Subject"></param>
        /// <param name="UserID"></param>
        /// <param name="Headers"></param>
        /// <param name="Html"></param>
        /// <param name="Text"></param>
        /// <param name="Body"></param>
        /// <param name="Status"></param>
        /// <param name="DocumentTemplateID"></param>
        /// <param name="mimeType"></param>
        /// <param name="formObjects"></param>
        /// <param name="saveAttachment"></param>
        /// <returns></returns>
        public static int? InsertCorrespondence(int DealID, string Subject, int UserID, string Headers, string Html, string Text, string Body, int Status, int DocumentTemplateID, string mimeType, Dictionary<string, dynamic> formObjects, bool saveAttachment, byte[] fileUpload)
        {
            try
            {
                #region variables
                
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                ObjectParameter documentidreturn = new ObjectParameter("documentidreturn", typeof(int));

                #endregion

                #region logic
                int saveFileOnServer;
                if (saveAttachment)
                    saveFileOnServer = 1;
                else
                    saveFileOnServer = 0;

                int WorkTypeID;

                if (mimeType == "email")
                    WorkTypeID = (int)Enums.WokflowTask.Email;
                else
                    WorkTypeID = (int)Enums.WokflowTask.SMS;

                REPSDB.REPS_AddCorrespondence(DealID, Subject, UserID, Headers, Text, Html, Body, Status, formObjects["Filename"], Enums.TransactionType.Email.ToString(), DocumentTemplateID, 0, 0, WorkTypeID, saveFileOnServer, identity, documentidreturn);
                #endregion

                #region logic

                if (saveAttachment)
                {
                    int DocumentID = (int)documentidreturn.Value;
                    byte[] FileContent = fileUpload;

                    //Save uploaded document

                    // Build New File Path
                    string filepath = Global.File.GetFilePathDocuments(DealID, DocumentID, formObjects["Filename"], (int)Enums.FolderPath.Correspondence);

                    // Create Directory
                    Global.File.CreateNewDirectoryDocuments(DealID, DocumentID, (int)Enums.FolderPath.Correspondence);

                    // Write New files to path
                    Global.File.WriteFiletoPath(filepath, formObjects["Filename"], FileContent);
                }

                return (identity.Value == null ? null : (int?)identity.Value);

                #endregion

            }
            catch (Exception Ex)
            {
                Common.CLog.WriteLogInfo("Crashed in InsertCorrespondence: " + Ex.Message, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw Ex;
            }

        }

        /// <summary>
        /// Retrieve saved attachment
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetBinaryDocumentAttachment(int documentID, int dealID)
        {
            try
            {

                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectResult<REPS.DATA.Entity.REPS_GetCorrespondenceAttachmentByID_Result> results = null;
                Byte[] TemplateFile = null;
                #endregion

                #region logic

                results = REPSDB.REPS_GetCorrespondenceAttachmentByID(documentID);
                //return results;

                var res = results.FirstOrDefault();
                Dictionary<string, dynamic> convertedresult = new Dictionary<string, dynamic>();

                convertedresult.Add("TemplateDisplayName", res.GeneratedDocFileName);

                // Get Template Content
                TemplateFile = Global.File.GetFileDocuments(dealID, documentID, res.GeneratedDocFileName, (int)Enums.FolderPath.Correspondence);

                convertedresult.Add("FileContent", Common.CFile.ConvertByteArrayToBase64String(TemplateFile));
                return convertedresult;
                //return Common.CFile.ConvertByteArrayToBase64String(results.FileContent);
                //return null;
                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        /// <summary>
        /// Get Correspondence info for the Correspondence List view
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetCorrespondenceInfo(int UserID, int DealID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                //UserID hardcoded
                results = REPSDB.REPS_GetCorrespondence_ByUserID(UserID, DealID);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Get Correspondence Details for the Ellipsis (see more) popup
        /// </summary>
        /// <param name="CorrespondenceID"></param>
        /// <returns></returns>
        public static object GetCorrespondenceDetails_ByID(int CorrespondenceID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                //UserID hardcoded
                results = REPSDB.REPS_GetCorrespondence_ByCorrespondenceID(CorrespondenceID);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Get email addresses for the email field
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public static object GetEmailAutocomplete(int DealID, string Prefix)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetCorrespondenceEmailAutocomplete_ByDealID(DealID, Prefix);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Get telephone numbers for the telephone field
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public static object GetTelephoneAutocomplete(int DealID, string Prefix)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetCorrespondenceNumberAutocomplete_ByDealID(DealID, Prefix);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object UploadCorrespondenceAttachment(Dictionary<string, dynamic> formObjects)
        {
            try
            {

                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                string Filename = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion


                #region logic
                int DocumentTemplateID = Convert.ToInt32(formObjects["DocumentTemplateID"]);
                var UploadOption = formObjects["UploadOption"];
                int dealID = Convert.ToInt32(formObjects["dealID"]);
                int userID = Convert.ToInt32(formObjects["userID"]);

                byte[] FileContent = Common.CFile.FromBase64StringToByteArray(formObjects["UploadedFile"]);

                Filename = formObjects["Filename"];

                results = REPSDB.REPS_AddUploadedStandardDocument(Filename, "email", DocumentTemplateID, dealID, userID, (int)Enums.TransactionType.Email, 1,null, (int)Enums.WokflowTask.UploadDocument, rowCount);

                //Save uploaded document

                // Build New File Path
                string filepath = Global.File.GetFilePathDocuments(dealID, (int)rowCount.Value, Filename, (int)Enums.FolderPath.Documents);

                // Create Directory
                Global.File.CreateNewDirectoryDocuments(dealID, (int)rowCount.Value, (int)Enums.FolderPath.Documents);

                // Write New files to path
                Global.File.WriteFiletoPath(filepath, Filename, FileContent);


                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }



        /// <summary>
        /// store procedure call to get correspondence id
        /// </summary>
        /// <param name="correspondenceGUID"></param>
        /// <returns></returns>
        public static object GetCorrespondenceIDByCorrespondenceGUID(string correspondenceGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GETCorrespondenceIDByCorrespondenceGUID(correspondenceGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
