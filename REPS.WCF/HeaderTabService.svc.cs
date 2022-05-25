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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HeaderTabService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HeaderTabService.svc or HeaderTabService.svc.cs at the Solution Explorer and start debugging.
    public class HeaderTabService : IHeaderTabService
    {
        public CValidator GetUserActiveDeals(string aspNetId)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.HeaderTab.GetUserActiveDeals(aspNetId)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator GetUserNotificationBarActiveDeals(string UserID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.HeaderTab.GetUserNotificationBarActiveDeals(UserID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator ToggleHeaderTabsActivation(string aspNetUserId, bool Status)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;

                if ((result = Business.HeaderTab.ToggleHeaderTabsActivation(aspNetUserId, Status)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "ToggleSuccess", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "ToggleFailed", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator InsertDeleteUserDeals(string aspNetUserId, int DealID, int Status)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;
                result = Business.HeaderTab.InsertDeleteUserDeals(aspNetUserId, DealID, Status);
                if (result == -2) // if deal cannot be added
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "ViewNameExists", true);
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

        public CValidator DeleteAllUserDeals(string aspNetUserId)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                int? result = 1;

                result = Business.HeaderTab.DeleteAllUserDeals(aspNetUserId);
                
                return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                //else
                //{
                //    return CValidator.initValidator("", serializer.Serialize(result), "DeleteFailed", false);
                //}
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator InsertLastViewName(string aspNetUserId, int DealID, string ViewName)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;
                result = Business.HeaderTab.InsertLastViewName(aspNetUserId, DealID, ViewName);
                if (result == -2) // if view name cannot be added
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "ViewNameExists", true);
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
    }
}
