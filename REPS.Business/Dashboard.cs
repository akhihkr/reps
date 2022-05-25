using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
   public class Dashboard
    {

        /// <summary>
        /// limits up to 5 deal details for dashboard 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetDashboardDeals(int? userId, int? dealProcessTaskID, int? entityID, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                {
                  
                    results = REPSDB.REPS_GetDealsDashboard(userId == null ? null : userId, null, null, null, dealProcessTaskID, null, null, null, entityID, startRow, endRow);
                    return results;
                }
                #endregion end of logic               
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Client reference
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetClientReference(int DealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetClientReferenceByDealID(DealID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// Get Details for the Ellipsis (see more) popup
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static object GetDashboardDetailsByID(int objID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic :Get Details for the Ellipsis (see more) popup
                results = REPSDB.REPS_GetCorrespondence_ByCorrespondenceID(objID);
                return results;
                #endregion  end of logic : Get Details for the Ellipsis (see more) popup

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}

