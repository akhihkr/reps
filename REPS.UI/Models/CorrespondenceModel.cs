using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using System.ComponentModel.DataAnnotations;

namespace REPS.UI.Models
{
    public class MailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
    }

    public class SMSModel
    {
        public string Number { get; set; }
        public string Message { get; set; }
    }

    public class CorrespondenceModel
    {
        /// <summary>
        /// get correspondence info
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetCorrespondenceInfo(int userID, int dealID)
        {
            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to get all the Correspondence emails
            #region Call WCF to get all the Correspondence emails
            using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
            {
                resultValidator = correspondenceServiceClient.GetCorrespondenceInfo(userID, dealID);
                var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                {
                    return output;
                }
                else
                {
                    throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                }
            }
            #endregion end Call WCF to get all the Correspondence emails
        }

        /// <summary>
        /// get correspondence info
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetCorrespondenceDetails_ByID(int CorrespondenceID)
        {
            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to get all the Correspondence emails
            #region Call WCF to get all the Correspondence emails
            using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
            {
                resultValidator = correspondenceServiceClient.GetCorrespondenceDetails_ByID(CorrespondenceID);
                var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                {
                    return output;
                }
                else
                {
                    throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                }
            }
            #endregion end Call WCF to get all the Correspondence emails
        }


        /// <summary>
        /// Get email list autocomplete
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public static object GetEmailAutocomplete(int DealID, string Prefix)
        {
            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to get all the Correspondence emails
            #region Call WCF to get all the Correspondence emails
            using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
            {
                resultValidator = correspondenceServiceClient.GetEmailAutocomplete(DealID, Prefix);
                var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                {
                    return output;
                }
                else
                {
                    throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                }
            }
            #endregion end Call WCF to get all the Correspondence emails
        }



        /// <summary>
        /// wcf call get correspondence id by correspondenceGUID
        /// </summary>
        /// <param name="dealUniqueRef"></param>
        /// <returns></returns>
        public static object GetCorrespondenceIDByCorrespondenceGUID(string correspondenceGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (CorrespondenceServiceReference.CorrespondenceServiceClient client = new CorrespondenceServiceReference.CorrespondenceServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        resultValidator = client.GetCorrespondenceIDByCorrespondenceGUID(correspondenceGUID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Get telephone number list autocomplete
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public static object GetTelephoneAutocomplete(int DealID, string Prefix)
        {
            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to get all the Correspondence emails
            #region Call WCF to get all the Correspondence emails
            using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
            {
                resultValidator = correspondenceServiceClient.GetTelephoneAutocomplete(DealID, Prefix);
                var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                {
                    return output;
                }
                else
                {
                    throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                }
            }
            #endregion end Call WCF to get all the Correspondence emails
        }

        /// <summary>
        /// Insert correspondence
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="Subject"></param>
        /// <param name="UserID"></param>
        /// <param name="Headers"></param>
        /// <param name="Html"></param>
        /// <param name="Text"></param>
        /// <param name="Body"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static void InsertCorrespondence(int DealID, string Subject, int UserID, string Headers, string Html, string Text, string Body, int Status, int DocumentTemplateID, string mimeType, Dictionary<string, dynamic> formObjects, bool saveAttachment, byte[] fileToBeUploaded)
        {
            Common.CLog.WriteLogInfo("INSIDE MODEL. Deal: " + DealID + " Subject: " + Subject + " Sender: " + Headers + " Body: " + Body + " Formobjects: " + formObjects + " Save attachment: " + saveAttachment, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to insert the Correspondence Details
            #region Call WCF to insert the Correspondence Details
            using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
            {
                //HtmlDocument htmlEmailBody = new HtmlDocument();
                //htmlEmailBody.LoadHtml(Body);
                resultValidator = correspondenceServiceClient.InsertCorrespondence(DealID, Subject, UserID, Headers, Body, Subject, Body, 0, DocumentTemplateID, mimeType, formObjects, saveAttachment, fileToBeUploaded);

                var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
            }
            #endregion end Call WCF to insert the Correspondence Details
        }
    }
}