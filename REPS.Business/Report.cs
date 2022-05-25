using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Report
    {
        /// <summary>
        /// Stored procedures for dropdown should have reportsid and description column
        /// </summary>
        public int ReportsId { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// get reports names - filters for deal user names
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetReportNames(object location, object chartName, object chartType, object description, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object obj = null;
                #endregion end of variables

                #region logic : get reports names - filters for deal user names
                obj = REPSDB.REPS_GetReportName(location == null ? null : location.ToString(), chartName == null ? null : chartName.ToString(), chartType == null ? null : chartType.ToString(), description == null ? null : description.ToString(), startRow, endRow);
                return obj;
                #endregion end of logic : get reports names - filters for deal user names
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// get chart report details
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static DataTable GetReports(string storedProcedureName, string parameters, Dictionary<string, object> formObjects)
        {
            try
            {
                /// variables | set parameters 
                #region variables | set parameters
                object context = new DATA.Entity.REPSEntities();
                object allParameterString = null;

                foreach (KeyValuePair<string, object> paramChart in formObjects)
                {
                    string key = paramChart.Key;
                    object value = paramChart.Value;

                    if (formObjects.LastOrDefault().Equals(paramChart))
                    {
                        allParameterString += key + "=" + value;
                    }
                    else
                    {
                        allParameterString += key + "=" + value + ",";
                    }
                }

                string myQuery = string.Format("EXEC" + " " + storedProcedureName + " " + allParameterString.ToString());
                DataTable resultTable = new DataTable();
                #endregion end variables | set parameters

                ///use of expando object to get anonymous resultset from query
                foreach (IDictionary<string, object> keyValues in Common.CDynamicSqlRow.GetDynamicSql(myQuery, context).ToList())
                {
                    /// Declare DataColumn and DataRow variables.
                    DataColumn column;
                    ///add columns
                    foreach (string str in keyValues.Keys)
                    {
                        /// Create  columns if not exists
                        if (!resultTable.Columns.Contains(str))
                        {
                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = str;
                            resultTable.Columns.Add(column);
                        }
                    }
                    ///add rows                    
                    resultTable.Rows.Add(keyValues.Values.ToArray());
                }
                return resultTable;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// get report filters
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetReportFilters(int? reportId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object obj = null;
                #endregion end of variables 

                #region logic : get report filters
                {
                    obj = REPSDB.REPS_GetReportFilters(reportId, startRow, endRow);
                    return obj;
                }
                #endregion end of logic : get report filters
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// get dropdown menu
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static object GetDropDownFilters(string parameters, string storedProcedureName, Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                object allParameterString = null;
                DATA.Entity.REPSEntities dbFunctional = new DATA.Entity.REPSEntities();
                Dictionary<string, object> formObjectsParametersFilter = new Dictionary<string, object>();
                var parameterForm = "";
                var parameterStoreProcedure = "";
                string[] parameterString;
                var paramString = parameters.Split(',');
                #endregion end of variables

                #region logic : get dropdown menu
                {
                    ///check parameters
                    foreach (var paramCheck in paramString)
                    {
                        parameterString = paramCheck.Split('=');
                        parameterForm = parameterString[1];
                        parameterStoreProcedure = parameterString[0];
                        foreach (var parameterIDCheck in formObjects as Dictionary<string, object>)
                        {

                            if (parameterForm == parameterIDCheck.Key)
                            {
                                formObjectsParametersFilter.Add(parameterStoreProcedure, parameterIDCheck.Value);
                            }
                        }
                    }
                    ///end check parameters

                    foreach (KeyValuePair<string, object> paramChart in formObjectsParametersFilter)
                    {
                        string key = paramChart.Key;
                        object value = paramChart.Value;
                        if (formObjectsParametersFilter.LastOrDefault().Equals(paramChart))
                        {
                            allParameterString += key + "=" + value;
                        }
                        else
                        {
                            allParameterString += key + "=" + value + ",";
                        }
                    }
                    using (var context = dbFunctional)
                    {
                        List<Report> queryResult =  context.Database.SqlQuery<Report>("EXEC  " + storedProcedureName + " " + allParameterString).ToList();
                        return queryResult;
                    }
                }
                #endregion end of logic : get dropdown menu
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

    }
}
