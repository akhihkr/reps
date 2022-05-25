using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrganizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrganizationService.svc or OrganizationService.svc.cs at the Solution Explorer and start debugging.
    public class OrganizationService : IOrganizationService
    {
        /// <summary>
        /// get organisation types 
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetOrganisationTypes(int? organisationTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Organization.GetOrganisationTypes(organisationTypeId, startRow, endRow)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get organisation details
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetOrganisation(int? organisationTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Organization.GetOrganisation(organisationTypeId, startRow, endRow)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// add organisation details 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddOrganisation(DATA.Entity.Organization obj, DATA.Entity.Participant objParticipant)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                //end of variables 

                result = Business.Organization.AddOrganisation(obj, objParticipant); //add organisation to database organisation table
         
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// update organisation details 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateOrganisation(DATA.Entity.Participant participantObj, DATA.Entity.Organization obj)
        {
            int? result;

            var serializer = new JavaScriptSerializer();

            try
            {
                result = Business.Organization.UpdateOrganisation(participantObj, obj);
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
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
