using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Global;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class HeaderTabController : Controller
    {
        // GET: HeaderTab
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Activate/Deactivate Tabs Feature in User Profile
        /// </summary>
        /// <param name="ActivationStatus"></param>
        /// <param name="clearAllTabs"></param>
        public void ToggleHeaderTabsActivation(string ActivationStatus, bool clearAllTabs)
        {
            try
            {
                bool ActivateStats;
                if (ActivationStatus == "off")
                    ActivateStats = false;
                else
                    ActivateStats = true;
                #region logic
                /// Call WCF to insert user deals
                object dealTypes = Models.HeaderTabModel.ToggleHeaderTabsActivation(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString(), ActivateStats);

                if (clearAllTabs)
                {
                    object deleteAllDeals = Models.HeaderTabModel.DeleteAllUserDeals(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                }
                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Clear all the saved userdeals (header tabs)
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteAllUserDeals()
        {
            try
            {
                #region logic
                /// Call WCF to delete all user deals
                object deleteAllDeals = Models.HeaderTabModel.DeleteAllUserDeals(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                #endregion end logic
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        ///  Delete selected dealid from the database (AJAX)
        /// </summary>
        /// <param name="CloseDealID"></param>
        /// <param name="CurrentDealID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserDeals(string CloseURef)
        {
            try
            {
                #region logic
                int dealID = 0;
                object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(CloseURef); // We get the DealID from the UR
                if (dealIDObject == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                dealID = Convert.ToInt32(dealIDObject);

                /// Call WCF to insert user deals
                object deleteDeal = Models.HeaderTabModel.InsertDeleteUserDeals(dealID, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString(), 1);

                /// Call WCF to get all active deals for header tab by User
                object allActiveDeals = Models.HeaderTabModel.GetUserActiveDeals(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveHeaderTabs"] = allActiveDeals;

                return PartialView("~/Views/HeaderTab/PartialHeaderTabs.cshtml");

                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Delete the currently active deal from the database (Non-AJAX)
        /// </summary>
        /// <param name="CloseDealID"></param>
        /// <returns></returns>
        public ActionResult DeleteCurrentActiveUserDeals(string CloseURef)
        {
            try
            {
                #region logic
                int dealID = 0;
                object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(CloseURef); // We get the DealID from the UR
                if (dealIDObject == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                dealID = Convert.ToInt32(dealIDObject);

                /// Call WCF to insert user deals
                object deleteDeal = Models.HeaderTabModel.InsertDeleteUserDeals(dealID, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString(), 1);

                return RedirectToAction("Index", "Dashboard");

                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get the new tabs header after the AJAX update
        /// </summary>
        /// <returns></returns>
        public object UpdateMoreTabsHeader()
        {
            try
            {
                #region logic

                /// Call WCF to get all active deals for header tab by User
                object allActiveDeals = Models.HeaderTabModel.GetUserActiveDeals(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveHeaderTabs"] = allActiveDeals;

                /// Call WCF to get all active deals for notification bar by User
                object allActiveDealsNotificationBar = Models.HeaderTabModel.GetUserActiveDealsNotificationBar(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveDealsNotifBarHeaderTabs"] = allActiveDealsNotificationBar;

                return PartialView("~/Views/HeaderTab/PartialMoreHeaderTabs.cshtml");

                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get the new tabs header after the AJAX update
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object UpdateTabsHeaderDealEdit()
        {
            try
            {
                #region logic

                /// Call WCF to get all active deals for header tab by User
                object allActiveDeals = Models.HeaderTabModel.GetUserActiveDeals(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveHeaderTabs"] = allActiveDeals;

                /// Call WCF to get all active deals for notification bar by User
                object allActiveDealsNotificationBar = Models.HeaderTabModel.GetUserActiveDealsNotificationBar(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveDealsNotifBarHeaderTabs"] = allActiveDealsNotificationBar;

                return PartialView("~/Views/HeaderTab/PartialHeaderTabs.cshtml");

                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get the new more tabs count after the AJAX update
        /// </summary>
        /// <returns></returns>
        public object UpdateMoreTabsCounter()
        {
            try
            {
                #region logic

                /// Call WCF to get all active deals for notification bar by User
                object allActiveDealsNotificationBar = Models.HeaderTabModel.GetUserActiveDealsNotificationBar(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveDealsNotifBarHeaderTabs"] = allActiveDealsNotificationBar;

                /// Call WCF to get all active deals for header tab by User
                object allActiveDeals = Models.HeaderTabModel.GetUserActiveDeals(Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                ViewData["ActiveHeaderTabs"] = allActiveDeals;

                //return PartialView("RightHeaderTabCounterPartial");
                return null;
                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Handles the header tabs redirection feature
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="RedirectLink"></param>
        /// <returns></returns>
        public ActionResult DealRedirection(string UniqueReference, string RedirectLink)
        {
            try
            {
                #region logic
                string[] SplitURL = RedirectLink.Split('/');

                #region Set current workflow
                /// Call WCF to get Workflow Task Name
                //object workflowName = Models.WorkflowModel.GetWorkFlowTaskNameByDealID(DealID);
                //ControllerContext.HttpContext.Session["CurrentDealWorkflow"] = workflowName.ToString();
                #endregion

                return RedirectToAction(SplitURL[1], SplitURL[0], new { URef = UniqueReference });

                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}