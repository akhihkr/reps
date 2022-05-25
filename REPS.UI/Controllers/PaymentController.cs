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
    public class PaymentController : Controller
    {
        // GET: Payments
        public ActionResult Index(bool partial = false)
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)


                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }

                ViewData["FeesListResult"] = null;

                ViewBag.PaymentList = Models.PaymentModel.GetPaymentList(dealID);

                ViewBag.TotalPayments = Models.PaymentModel.GetSumPayments(dealID);

                #region pre populate total sum of mortgage value 
                /// call wcf to purchase price morgage            
                object[] getMortgageSumValue = Models.DealModel.GetMortgageSumValuePerDeal(dealID);
                if (getMortgageSumValue != null)
                {
                    Dictionary<string, object> res = (Dictionary<string, object>)getMortgageSumValue.FirstOrDefault();
                    ViewBag.PriceMortgageSumValue = res["Price"];
                }
                else
                {
                    ViewBag.PriceMortgageSumValue = "0";
                }
                #endregion pre populate total sum of mortgage value 
                if (partial)
                {
                    return PartialView("PaymentList");
                }
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Display popup to add payment
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddPayment()
        {
            try
            {
                TempData["FeesAddResult"] = null;

                ///// Call WCF to Get all Fees fields for Add View
                TempData["FeesAddResult"] = Models.PaymentModel.GetAddPaymentFields((int)Enums.Task.Fees, (int)Enums.WorkflowAction.AddFee);

                ///// Call WCF to Get all Fee Type
                TempData["FeeTypeList"] = Models.PaymentModel.GetPaymentTypeList();

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Display popup to edit payment
        /// </summary>
        /// <param name="transactionGUID"></param>
        /// <returns></returns>
        public ActionResult PopupEditPayment(string transactionGUID)
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;

                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }

                #endregion Get DealID from URL Parameter (Unique Reference)

                ViewBag.PageLayoutPath = null;

                TempData["EditFeeResult"] = null;

                int transactionID = (int)Models.PaymentModel.GetTransactionDetailsIDByTransactionDetailsGUID(transactionGUID);

                /// Call WCF to Get all Fees fields for Add View
                TempData["EditFeeResult"] = Models.PaymentModel.GetPaymentEditFields(dealID, transactionID, (int)Enums.WorkflowAction.AddFee);

                /// Call WCF to Get all Fee Type
                TempData["FeeTypeList"] = Models.PaymentModel.GetPaymentTypeList();
                
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Save changes to DB (Add Fee)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewPayment()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)
                    
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }

                // Log Start info
                Common.CLog.WriteLogInfo("Dashboard", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //get date time to variables*************

                var FormObjects = new Dictionary<string, dynamic>();
                //Copy posted values
                Request.Form.CopyTo(FormObjects);

                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());

                // Call WCF to save all fields for Add View
                var FeesAddResult = Models.PaymentModel.InsertPayment(FormObjects, dealID, Convert.ToInt32(userID));

                return RedirectToAction("Index", "Payment", new { partial = true });
            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }

        }

        /// <summary>
        /// Save changes to DB (Edit Fee)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditFee()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)
                    
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }

                /// Log Start info
                Common.CLog.WriteLogInfo("Dashboard", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                var FormObjects = new Dictionary<string, dynamic>();

                //Copy posted values
                Request.Form.CopyTo(FormObjects);

                /// Initiate Validator
                int DealID = Convert.ToInt32(dealID);
                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                
                var FeeResult = Models.PaymentModel.UpdatePayment(FormObjects, DealID, Convert.ToInt32(userID));

                return RedirectToAction("Index", "Payment", new { partial = true });
            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
        
        /// <summary>
        /// remove mortgage and set del to 1
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemovePayment(string transactionGUID)
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());

                int transactionID = (int)Models.PaymentModel.GetTransactionDetailsIDByTransactionDetailsGUID(transactionGUID);
                
                #region logic Call WCF to remove selected payment
                object removeMortgage = Models.PaymentModel.RemovePayment(transactionID, Convert.ToInt32(userID), Convert.ToInt32(dealID));
                #endregion end logic Call WCF to remove selected payment

                return RedirectToAction("Index", "Payment", new { partial = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}