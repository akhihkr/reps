using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Audit
    {
        /// <summary>
        /// get audits details
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        public static object GetAudits(int dealId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables 

                #region logic : get audits details                  
                results = REPSDB.REPS_GetWorkflowProgress(dealId);
                return results;
                #endregion end of logic : get audits details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// get audit details to timeline js
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        public static object GetAuditsDetailsTimeline(int dealId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get audit details to timeline js
                {
                    results = REPSDB.REPS_GetAuditDetails_ByDealID(dealId);
                    return results;
                }
                #endregion end of logic : get audit details to timeline js
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
}
