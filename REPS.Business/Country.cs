using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Country
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetCountries(int? countryId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic                     
                //using (_dbFunc)
                {
                    results = REPSDB.REPS_GetCountries(countryId, startRow, endRow);
                    return results;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// get province details
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="provinceId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetProvince(int? countryId, int? provinceId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic : get province details
                results = REPSDB.REPS_GetProvince(countryId, provinceId, startRow, endRow);
                return results;
                #endregion end of logic : get province details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
