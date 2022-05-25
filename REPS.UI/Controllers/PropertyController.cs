using Global;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class PropertyController : Controller
    {    
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Add Property Panel
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddProperty(string dealID)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                ViewData["PropertyTypes"] = "";
                ViewData["Properties"] = "";
                ViewData["dealId"] = Convert.ToInt32(dealID);
                #endregion end vaiables

                #region logic call wcf
                /// Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                /// Call WCF to get all property types
                object getPropertyType = Models.PropertyModel.GetPropertyTypes(null, null, null);
                ViewData["PropertyTypes"] = getPropertyType;

                /// call WCF to get all size types
                object getSizeType = Models.PropertyModel.GetSizeTypes(null, null, null);
                ViewData["SizeTypes"] = getSizeType;
                #endregion end logic call wcf

                #region get token search works
                object getToken = Models.PropertyModel.GetTokenSearchWorks();
                ViewData["getToken"] = getToken.ToString();
                #endregion end of get token search works

                #region deeds office call
                if (getToken != null)
                {
                    object deedsOffice = Models.PropertyModel.GetDeedsOfficeProperty(getToken.ToString(), "REPS");
                    ViewData["deedsOffice"] = deedsOffice.ToString();
                }
                #endregion end of deeds office call

                return View("PopupAddProperty");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add new property
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProperty()
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

                #region form Values for address
                /// address form inputs
                Address address = new Address();
                address.AddressTypeID = 10;///hardcoded for demo purpose
                address.AddressLine1 = Request["AddressLine1"];
                address.AddressLine2 = Request["AddressLine2"];
                address.City = Request["City"];
                address.PostalCode = Request["PostalCode"];
                address.CountryID = Convert.ToInt32(Request["CountryID"]);
                address.Verified = false;
                address.Deleted = false;
                int addressID = 0;
                int propertyID = 0;
                ///variables 
                var addressIDResult = "";
                var propertyResult = "";
                #endregion End form values for address

                #region ServerCAll Address Add
                /// save address to db
                string getAddressResult = Models.AddressModel.SaveAddress(address, dealID);
                addressIDResult = getAddressResult;
                int.TryParse(addressIDResult, out addressID);
                #endregion End ServerCALL Address Add

                ///save property if addressID > 0
                #region form Values for property
                /// property form inputs
                Property property = new Property();
                property.DealID = Convert.ToInt32(dealID);
                property.PropertyTypeID = Convert.ToInt16(Request["propertyType"]); ;
                property.AddressID = addressID;
                property.PropertyDescription = Request["PropertyDescription"];
                property.LegalDescription = Request["LegalDescription"];

                /// Call WCF to save property and country 
                object saveProperty = Models.PropertyModel.SaveProperty(property, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                propertyResult = saveProperty.ToString();

                int.TryParse(propertyResult, out propertyID);
                PropertyDetail propertyDetail = new PropertyDetail();
                propertyDetail.PropertyID = propertyID;
                propertyDetail.SizeTypeID = Convert.ToInt32(Request["SizeTypeID"]);
                propertyDetail.Size = Request["Size"];
                propertyDetail.PortionNumber = Request["PortionNumber"];
                propertyDetail.PlanNumber = Request["PlanNumber"];
                propertyDetail.Township = Request["township"];
                propertyDetail.Geo = Request["Coordinates"];
                /// Call WCF to save property details
                object savePropertyDetails = Models.PropertyModel.SavePropertyDetail(dealID, propertyDetail);
                #endregion form Values for property


                object propertyPerDeal = Models.PropertyModel.GetProperty(dealID, null, null, null);
                ViewData["Properties"] = propertyPerDeal;

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }

                Guid guidOutput;
                bool isGuid = Guid.TryParse(savePropertyDetails.ToString(), out guidOutput);

                if (isGuid)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }

                return View("PartialProperty");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// serach work for town 
        /// </summary>
        /// <param name="townname"></param>
        /// <param name="deedsOffice"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchTownDetailForm()
        {
            try
            {
                #region Variables
                ///property form inputs value
                string townname = Request["townname"];
                string deedsOffice = Request["deedsOfficeID"];
                List<object> formTownshipObjects = new List<object>();
                #endregion end of variables

                #region get token search works
                object getToken = Models.PropertyModel.GetTokenSearchWorks();
                ViewData["getToken"] = getToken.ToString();
                #endregion end of get token search works

                #region deeds office call
                if (getToken != null)
                {
                    object deedsOfficeCall = Models.PropertyModel.GetDeedsOfficeProperty(getToken.ToString(), "REPS");
                    ViewData["deedsOffice"] = deedsOfficeCall.ToString();
                }
                #endregion end of deeds office call

                #region get town details
                object townDetails = Models.PropertyModel.SearchTownDetail(getToken.ToString(), "REPS", townname, deedsOffice);
                if(townDetails != null)
                {
                    foreach (var townshipOutput in townDetails as Dictionary<string, object>)
                    {
                        foreach (var pairTownship in townshipOutput.Value as IEnumerable<dynamic>)
                        {
                            formTownshipObjects.Add(pairTownship);
                        }
                    }
                    ViewData["townDetails"] = formTownshipObjects.ToList();
                }
                else
                {
                    ViewData["townDetailsNull"] = Common.CString.GetEnumDescription(Enums.NullValues.Null);
                }
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

                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
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

                //ViewData["DealIDSession"] = Convert.ToInt32(ControllerContext.HttpContext.Session["DealID"]);
                ViewData["DealIDSession"] = dealID;
                #endregion end of get town details

                return View("PartialPropertyTownship");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get edit details of property 
        /// </summary>
        /// <param name="propertyID"></param>
        /// <param name="propertyDetailID"></param>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public ActionResult PopupEditProperty(string propertyGUID, string propertyDetailGUID, string addressGUID)
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

                /// Call WCF to get all property types
                object getPropertyType = Models.PropertyModel.GetPropertyTypes(null, null, null);
                ViewData["PropertyTypes"] = getPropertyType;

                /// Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                /// call WCF to get all size types
                object getSizeType = Models.PropertyModel.GetSizeTypes(null, null, null);
                ViewData["SizeTypes"] = getSizeType;

                ViewData["PropertyGUID"] = propertyGUID;
                ViewData["PropertyDetailGUID"] = propertyDetailGUID;
                ViewData["AddressGUID"] = addressGUID;

                int propertyID = (int)Models.PropertyModel.GetPropertyIDByPropertyGUID(propertyGUID);

                ViewData["PropertyInfo"] = Models.PropertyModel.GetPropertyDetails(dealID, propertyID, null, null);

                #region get token search works
                object getToken = Models.PropertyModel.GetTokenSearchWorks();
                ViewData["getToken"] = getToken.ToString();
                #endregion end of get token search works

                #region deeds office call
                if (getToken != null)
                {
                    object deedsOffice = Models.PropertyModel.GetDeedsOfficeProperty(getToken.ToString(), "REPS");
                    ViewData["deedsOffice"] = deedsOffice.ToString();
                }
                #endregion end of deeds office call

                return View("PopupEditProperty");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Update Property Ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProperty()
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

                #region variables
                int propertyID = 0;
                int propertyDetailID = 0;
                int addressID = 0;
                propertyID = (int)Models.PropertyModel.GetPropertyIDByPropertyGUID(Convert.ToString(Request["propertyGUID"]));
                propertyDetailID = (int)Models.PropertyModel.GetPropertyDetailIDByPropertyDetailGUID(Convert.ToString(Request["propertyDetailGUID"]));
                addressID = (int)Models.AddressModel.GetAddressIDByAddressGUID(Convert.ToString(Request["addressGUID"]));
                #endregion end of variables

                #region form Values for address
                /// address form inputs
                Address address = new Address();
                address.AddressID = addressID;
                address.AddressTypeID = 10;///hardcoded for demo purpose
                address.AddressLine1 = Request["AddressLine1"];
                address.AddressLine2 = Request["AddressLine2"];
                address.City = Request["City"];
                address.PostalCode = Request["PostalCode"];
                address.CountryID = Convert.ToInt32(Request["CountryID"]);
                address.Verified = false;
                address.Deleted = false;
                ///variables 
                var addressIDResult = "";
                var propertyResult = "";
                var propertyDetailResult = "";
                #endregion End form values for address

                #region ServerCAll Address Add
                /// save address to db
                string getAddressResult = Models.AddressModel.UpdateAddress(address, dealID);
                addressIDResult = getAddressResult;
                #endregion End ServerCALL Address Add

                ///save property 
                #region form Values for property
                /// property form inputs
                Property property = new Property();
                property.PropertyID = propertyID;
                property.DealID = dealID;
                property.PropertyTypeID = Convert.ToInt16(Request["propertyType"]);
                property.AddressID = addressID;
                property.PropertyDescription = Request["PropertyDescription"];
                property.LegalDescription = Request["LegalDescription"];

                /// Call WCF to save property and country 
                object saveProperty = Models.PropertyModel.UpdateProperty(property);
                propertyResult = saveProperty.ToString();

                PropertyDetail propertyDetail = new PropertyDetail();
                propertyDetail.PropertyDetailID = propertyDetailID;
                propertyDetail.PropertyID = propertyID;
                propertyDetail.SizeTypeID = Convert.ToInt32(Request["SizeTypeID"]);
                propertyDetail.Size = Request["Size"];
                propertyDetail.PortionNumber = Request["PortionNumber"];
                propertyDetail.Township = Request["township"];
                propertyDetail.PlanNumber = Request["PlanNumber"];
                propertyDetail.Geo = Request["Coordinates"];

                /// Call WCF to save property details
                object updatePropertyDetails = Models.PropertyModel.UpdatePropertyDetail(propertyDetail);
                //propertyDetailResult = updatePropertyDetails.ToString();
                #endregion form Values for property

                object propertyPerDeal = Models.PropertyModel.GetProperty(dealID, null, null, null);
                ViewData["Properties"] = propertyPerDeal;

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                // Call WCF for financialtransaction add
 
                Guid guidOutput;
                bool isGuid = Guid.TryParse(updatePropertyDetails.ToString(), out guidOutput);

                if (isGuid)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }
                return View("PartialProperty");
            }
            catch (Exception ex)
            {
                Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.systemCrash + "></div>");
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                else if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "true")
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;

                TempData["ErrorMessage"] = ex;

                return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString());
            }
        }

        /// <summary>
        /// remove property detail set details to true
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveProperty(string propertyGUID)
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

                int PropertyID = (int)Models.PropertyModel.GetPropertyIDByPropertyGUID(propertyGUID);

                #region Variables
                ///propery form inputs value
                ViewData["Deleted_Property"] = "";
                Property property = new Property();
                property.PropertyID = PropertyID;
                property.Deleted = true;
                ///variables result
                var propertyResult = "";
                #endregion end vaiables
                /// call wcf to remove property 
                #region removing a property
                string delProperty = Models.PropertyModel.RemoveProperty(property, dealID);
                propertyResult = delProperty.ToString();
                #endregion end removing a property

                object propertyPerDeal = Models.PropertyModel.GetProperty(dealID, null, null, null);
                ViewData["Properties"] = propertyPerDeal;

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                Guid guidOutput;
                bool isGuid = Guid.TryParse(delProperty.ToString(), out guidOutput);

                if (isGuid)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }

                return View("PartialProperty");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}