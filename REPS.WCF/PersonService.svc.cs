using Common;
using REPS.DATA.Entity;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PersonService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PersonService.svc or PersonService.svc.cs at the Solution Explorer and start debugging.
    public class PersonService : IPersonService
    {
        /// <summary>
        /// save person details to db
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddPerson(Participant objParticipant, Person obj)
        {
            try
            {
                //variables 
                int? result;
                var serializer = new JavaScriptSerializer();
                //end of variables

                result = Business.Person.AddPerson(objParticipant,obj);
                obj.PersonID = result.Value;

                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdatePerson(Participant participantObj, Person obj)
        {
            try
            {
                //variables
                int? result;
                var serializer = new JavaScriptSerializer();
                //end of variables

                result = Business.Person.UpdatePerson(participantObj,obj);

                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
    }
}
