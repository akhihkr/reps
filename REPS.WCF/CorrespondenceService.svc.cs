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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CorrespondenceService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CorrespondenceService.svc or CorrespondenceService.svc.cs at the Solution Explorer and start debugging.
    public class CorrespondenceService : ICorrespondenceService
    {
        public CValidator InsertCorrespondence(int DealID, string Subject, int UserID, string Headers, string Html, string Text, string Body, int Status, int DocumentTemplateID, string mimeType, Dictionary<string, dynamic> formObjects, bool saveAttachment, byte[] fileUpload)
        {

            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;

                result = Business.Correspondence.InsertCorrespondence(DealID, Subject, UserID, Headers, Html, Text, Body, Status, DocumentTemplateID, mimeType, formObjects, saveAttachment, fileUpload);
                if (result == -2) //if participant existed
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "CorrespondenceExists", false);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }
        public CValidator GetCorrespondenceInfo(int UserID, int DealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetCorrespondenceInfo(UserID, DealID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetBinaryDocumentAttachment(int documentID, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetBinaryDocumentAttachment(documentID, dealID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        public CValidator GetCorrespondenceDetails_ByID(int CorrespondenceID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetCorrespondenceDetails_ByID(CorrespondenceID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetEmailAutocomplete(int DealID, string Prefix)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetEmailAutocomplete(DealID, Prefix)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator GetTelephoneAutocomplete(int DealID, string Prefix)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetTelephoneAutocomplete(DealID, Prefix)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get correspondence id by correspondenceGUID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetCorrespondenceIDByCorrespondenceGUID(string correspondenceGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Correspondence.GetCorrespondenceIDByCorrespondenceGUID(correspondenceGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
    }
}