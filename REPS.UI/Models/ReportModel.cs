using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class ReportModel
    {
        /// <summary>
        /// call wcf to get all report names
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetReportNames(object location, object chartName, object chartType, object description, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all report names
                #region WCF to get all report names
                using (ReportServiceReference.ReportServiceClient reportServiceClient = new ReportServiceReference.ReportServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(reportServiceClient.InnerChannel))
                    {
                        resultValidator = reportServiceClient.GetReportNames(location, chartName, chartType, description, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF to get all report names
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get details for chart report
        /// </summary>
        /// <param name="chartProcedure"></param>
        /// <param name="chartParameter"></param>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object GetChartReport(string chartParameter, string chartProcedure, Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                string[] splitedParameter = chartParameter.Split(',');
                Dictionary<string, object> paramChart = new Dictionary<string, object>();
                string[] splitedParameterResult;
                var parameters = "";
                var splitedParameterStoreProcedure = "";
                var splitedParameterForm = "";
                object output;
                #endregion end variables

                #region parameter
                foreach (var chartParamsItem in splitedParameter)
                {
                    parameters = chartParamsItem;
                    splitedParameterResult = parameters.Split('=');
                    splitedParameterStoreProcedure = splitedParameterResult[0];
                    splitedParameterForm = splitedParameterResult[1];

                    foreach (KeyValuePair<string, object> paramChartValue in formObjects)
                    {
                        string key = paramChartValue.Key;
                        object value = paramChartValue.Value;
                        if (value.ToString() == "-1")
                        {
                            value = "null";
                        }
                        else
                        {
                            value = paramChartValue.Value;
                        }
                        if (key == splitedParameterForm)
                        {
                            paramChart.Add(splitedParameterStoreProcedure, value);
                        }
                    }
                }
                #endregion end parameter

                /// Call wcf to call deal types by userID dropdown menu
                #region wcf to call deal types by userID dropdown menu
                using (ReportServiceReference.ReportServiceClient reportServiceClient = new ReportServiceReference.ReportServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(reportServiceClient.InnerChannel))
                    {
                        resultValidator = reportServiceClient.GetReports(chartParameter, chartProcedure, paramChart);
                        if (resultValidator.output.Contains(Common.CString.GetEnumDescription(Enums.NullValues.DbNullValues)))
                        {
                            output = Common.CString.GetEnumDescription(Enums.NullValues.NullString);
                        }
                        else
                        {
                            output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        return output;
                    }
                }
                #endregion end wcf to call deal types by userID dropdown menu
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to get all filters by the report id
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public static object GetFilters(int? reportId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all filters by the report id
                #region WCF to get all filters by the report id
                using (ReportServiceReference.ReportServiceClient reportServiceClient = new ReportServiceReference.ReportServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(reportServiceClient.InnerChannel))
                    {
                        resultValidator = reportServiceClient.GetReportFilters(reportId == null ? null : reportId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var outputServercall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServercall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF to get all filters by the report id
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Call wcf to get dropdown menu for each call
        /// </summary>
        /// <param name="reportFilter"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetDropDown(object reportFilter, Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                Dictionary<string, object> filterDropDown = new Dictionary<string, object>();
                ///end of variables
                #endregion end variables

                ///multiple calls to get filter drop downs
                foreach (var dropdownDetails in reportFilter as IEnumerable<dynamic>)
                {
                    /// Call wcf to call deal types by userID dropdown menu
                    #region wcf to call deal types by userID dropdown menu
                    using (ReportServiceReference.ReportServiceClient reportServiceClient = new ReportServiceReference.ReportServiceClient())
                    {
                        //operation context to read headers
                        using (OperationContextScope scope = new OperationContextScope(reportServiceClient.InnerChannel))
                        {
                            resultValidator = reportServiceClient.GetDropDownFilters(dropdownDetails["Parameter"], dropdownDetails["DropdownProcedure"], formObjects);
                            var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            filterDropDown.Add(dropdownDetails["ReportsFiltersId"] + ";" + dropdownDetails["Description"] + ";" + dropdownDetails["Parameter"] + ";" + dropdownDetails["FilterID"], output);
                        }
                    }
                    #endregion end wcf to call deal types by userID dropdown menu
                }
                return (filterDropDown);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

    }
}