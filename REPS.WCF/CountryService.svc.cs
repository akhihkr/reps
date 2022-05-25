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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CountryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CountryService.svc or CountryService.svc.cs at the Solution Explorer and start debugging.
    public class CountryService : ICountryService
    {
        public CValidator GetCountries(int? countryId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Country.GetCountries(countryId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// call wct to get province
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="provinceId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetProvince(int? countryId = null, int? provinceId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Country.GetProvince(countryId, provinceId, startRow, endRow)), "FetchedSuccessfully", true);
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
