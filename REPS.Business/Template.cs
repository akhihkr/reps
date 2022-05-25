using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Template
    {
        /// <summary>
        /// get client care template
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        public static object GetClientCare(int? dealId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get client care template
                results = REPSDB.REPS_Template_Client_Care_Letter(dealId);
                return results;
                #endregion end of logic : get client care template             
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
