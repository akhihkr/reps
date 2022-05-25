using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace REPS.UI.Models
{
    public class ErrorHandlingModel
    {
        public static int ErrorHandling(string exception, bool ajax)
        {
            try
            {
                bool tokenValid = true;
                int counter = 1;
                while (counter > 0)
                {
                    string appSettingsPath = "WCFUnauthorised" + counter;
                    if (ConfigurationManager.AppSettings[appSettingsPath] != null)
                    {
                        if (exception.Contains(ConfigurationManager.AppSettings[appSettingsPath].ToString())) // We check if the error is listed in the web.config
                        {
                            Common.CLog.WriteLogInfo("The WCF has failed to validate the token", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                            tokenValid = false; // The WCF has failed to validate the token
                        }
                        counter++;
                    }
                    else
                    {
                        break; // If we don't have anymore errors listed in the web.config, we exit the loop
                    }
                }
                if (tokenValid)
                {
                    if (ajax)
                    {
                        Common.CLog.WriteLogInfo("Token is valid, but we encountered another error.", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        return (int)Global.Enums.WCFTokenError.ErrorToastrAjax; // We return the error toastr along with the error
                    }
                    else
                    {
                        Common.CLog.WriteLogInfo("Token is valid, but we encountered another error.", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        return (int)Global.Enums.WCFTokenError.ErrorToastrNonAjax; // We return the error toastr along with the error
                    }
                }
                else if (ajax)
                {
                    Common.CLog.WriteLogInfo("Wrong token and not using AJAX.", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    return (int)Global.Enums.WCFTokenError.WrongTokenAjax; // We return wrongtoken so that javascript handles the error and redirects to the login
                }
                else
                {
                    Common.CLog.WriteLogInfo("Wrong token and using AJAX", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    return (int)Global.Enums.WCFTokenError.WrongTokenNonAjax;
                    //return RedirectToAction("Index", "Login", new { tokenerror = "true" });
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return (int)Global.Enums.WCFTokenError.ErrorInHandling;
                //return JavaScript("ErrorPage('ErrorHandling', 'ErrorForm')");
            }
        }

        public static string ErrorHandlingModelReturn(string exception, bool ajaxRequest)
        {
            string error = null;
            switch (Models.ErrorHandlingModel.ErrorHandling(exception, ajaxRequest))
            {
                case (int)Global.Enums.WCFTokenError.ErrorToastrAjax:
                    error = "false";
                    break;

                case (int)Global.Enums.WCFTokenError.ErrorToastrNonAjax:
                    error = "true";
                    break;

                case (int)Global.Enums.WCFTokenError.WrongTokenAjax:
                case (int)Global.Enums.WCFTokenError.WrongTokenNonAjax:
                    error = "tokenerror";
                    break;

                default:
                    error = "false";
                    break;
            }
            return error;
        }
    }
}