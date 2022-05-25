using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchWorksService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SearchWorksService.svc or SearchWorksService.svc.cs at the Solution Explorer and start debugging.
    public class SearchWorksService : ISearchWorksService
    {
        /// <summary>
        /// call service to get username ands password
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public CValidator SearchWorksToken(string sUserName, string sPassword)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                using (var client = new SearchWorksServiceReference.SearchWorksAPIServiceClient())
                {
                    return CValidator.initValidator("", serializer.Serialize(client.Login(sUserName, sPassword)), "FetchedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// search works for deeds
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public CValidator SearchWorksGetDeed(string token, string reference)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                using (var client = new SearchWorksServiceReference.SearchWorksAPIServiceClient())
                {
                    System.Web.Script.Serialization.JavaScriptSerializer jSerialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, int> deedoff = new Dictionary<string, int>();
                    List<deedOff> deedoffs = new List<deedOff>();
                    var i = 0;
                    foreach (SearchWorksServiceReference.DeedsOffices item in Enum.GetValues(typeof(SearchWorksServiceReference.DeedsOffices)))
                    {
                        deedoffs.Add(new deedOff() { Name = item.ToString(), ID = (int)((SearchWorksServiceReference.DeedsOffices)Enum.Parse(typeof(SearchWorksServiceReference.DeedsOffices), item.ToString())) });
                    }
                    return CValidator.initValidator("", serializer.Serialize(JsonConvert.SerializeObject(deedoffs)), "FetchedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get search works for town
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reference"></param>
        /// <param name="DeedsOfficeIDs"></param>
        /// <param name="FilterText"></param>
        /// <returns></returns>
        public CValidator GetSearchWorksTown(string token, string reference, string FilterText, string DeedsOfficeIDs)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                using (var client = new SearchWorksServiceReference.SearchWorksAPIServiceClient())
                {
                    var towns = client.LookupTownships(token, FilterText == null ? "" : FilterText, DeedsOfficeIDs);
                    return CValidator.initValidator("", JsonConvert.SerializeObject(towns, Formatting.Indented), "FetchedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// set deed off 
        /// </summary>
        public class deedOff
        {
            public string Name;
            public int ID;
        }
    }
}
