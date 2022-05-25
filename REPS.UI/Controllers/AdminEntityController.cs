using REPS.DATA.Entity;
using REPS.UI.EntityServiceReference;
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
    public class AdminEntityController : Controller
    {
        // GET: AdminEntity
        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index(bool partial = false, bool responseResult = false)
        {
            try
            {
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }
                //get entity 
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                /// Call WC to get all entities
                object EntitiesList = Models.EntityModel.GetEntities(null, null, null, null, -1);
                ViewData["EntitiesList"] = EntitiesList;
                ViewData["entityID"] = entityID.ToString();
                ViewData["SelectedLayout"] = "Admin";

                if (partial)
                {
                    if (responseResult)
                    {
                        Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                    }
                    return PartialView("PartialAdminEntityList");
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get entity details
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddEntity()
        {
            try
            {
                //Call WCF to get all Address Types
                object getAddressTypes = Models.AddressModel.GetAddressType(null, null, null);
                ViewData["AddressType"] = getAddressTypes;

                /// Call WCF to get all organisation types 
                object getOrgTypes = Models.ParticipantModel.GetOrganisationType(null, null, null);
                ViewData["OrganizationTypes"] = getOrgTypes;

                /// Call WCF to get all countries
                object[] CountryList = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = CountryList;

                object getProvince = null;

                /////get province details if country is selected
                foreach (var selectedLoadedCountryID in CountryList.FirstOrDefault() as Dictionary<string, dynamic>)
                {
                    if (selectedLoadedCountryID.ToString().Contains("CountryID"))
                    {
                        getProvince = Models.AddressModel.GetProvince(selectedLoadedCountryID.Value, null, null, null);
                    }
                }

                ViewData["Province"] = getProvince;

                /// Call WC to get all entities
                object AllEntitiesList = Models.EntityModel.GetEntities(null, null, null, null, -1);
                ViewData["AllEntitiesList"] = AllEntitiesList;

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// display details of edit entity
        /// </summary>
        /// <param name="entityGUID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PopupEditEntity(string entityGUID)
        {
            try
            {

                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));

                //Call WCF to get all Address Types
                object getAddressTypes = Models.AddressModel.GetAddressType(null, null, null);
                ViewData["AddressType"] = getAddressTypes;

                /// Call WCF to get all organisation types 
                object getOrgTypes = Models.ParticipantModel.GetOrganisationType(null, null, null);
                ViewData["OrganizationTypes"] = getOrgTypes;

                /// Call WCF to get all countries
                object[] CountryList = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = CountryList;

                /////get province details if country is selected
                object getProvince = Models.AddressModel.GetProvince(null, null, null, null);
                ViewData["Province"] = getProvince;

                // Call WC to get all entities
                object EntitiesList = Models.EntityModel.GetEntities(null, null, null, entityID, -1);
                ViewData["EntitiesList"] = EntitiesList;

                /// Call WC to get all entities
                object AllEntitiesList = Models.EntityModel.GetEntities(null, null, null, null, -1);
                ViewData["AllEntitiesList"] = AllEntitiesList;

                return View("PopupEditEntity");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add entity to db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEntity()
        {
            try
            {
                #region get form values
                Entity entityDetails = new Entity();
                entityDetails.OrganizationTypeID = Convert.ToInt32(Request["organisationType"].Trim());
                entityDetails.Name = Request["entityName"].Trim();
                entityDetails.RegistrationNumber = Request["registrationName"].Trim();
                entityDetails.LegalName = Request["legalName"].Trim() == "" ? null : Request["legalName"].Trim();
                // entityDetails.AlternateName = Request["alternateName"].Trim() == "" ? null : Request["alternateName"].Trim();
                entityDetails.VatID = Request["vatID"].Trim() == "" ? null : Request["vatID"].Trim();
                entityDetails.Telephone = Convert.ToInt32(Request["telephone"].Trim());
                if (Request["fax"].Trim() == "")
                {
                    entityDetails.FaxNumber = null;
                }
                else
                {
                    entityDetails.FaxNumber = Convert.ToInt32(Request["fax"].Trim());
                }
                entityDetails.Email = Request["email"].Trim() == "" ? null : Request["email"].Trim();
                entityDetails.AddressTypeID = Convert.ToInt32(Request["addressType"].Trim());
                entityDetails.AddressLine1 = Request["AddressLine1"].Trim();
                entityDetails.AddressLine2 = Request["AddressLine2"].Trim() == "" ? null : Request["AddressLine2"].Trim();
                entityDetails.City = Request["city"].Trim();
                if (Request.Form["province"].Trim() == "-1")
                {
                    entityDetails.ProvinceID = null;
                }
                else
                {
                    entityDetails.ProvinceID = Convert.ToInt32(Request.Form["province"].Trim());
                }
                entityDetails.CountryID = Convert.ToInt32(Request["countryID"].Trim());
                entityDetails.PostalCode = Request["postal"].Trim() == "" ? null : Request["postal"].Trim();
                entityDetails.ParentEntityID = Convert.ToInt32(Request["parentEntityID"].Trim());
                entityDetails.Verified = false;
                entityDetails.Deleted = false;
                #endregion end of get form values

                #region ServerCAll Entity Add
                object addEntity = Models.EntityModel.AddEntity(entityDetails);
                #endregion End ServerCALL Entity Add

                Guid guidOutput;
                bool isGuid = Guid.TryParse(addEntity.ToString(), out guidOutput);
                bool responseResult = false;
                if (isGuid)
                {
                    responseResult = true;
                }
                return RedirectToAction("Index", new { partial = true, responseResult = responseResult });
                //return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        [HttpPost]
        public ActionResult EditEntity()
        {

            try
            {
                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                #region get form values
                int entityID = Convert.ToInt32(Request["entityID"]);
                Entity entityDetails = new Entity();
                entityDetails.EntityID = entityID;
                entityDetails.OrganizationTypeID = Convert.ToInt32(Request["organisationType"].Trim());
                entityDetails.Name = Request["entityName"].Trim();
                entityDetails.RegistrationNumber = Request["registrationName"].Trim();
                entityDetails.LegalName = Request["legalName"].Trim() == "" ? null : Request["legalName"].Trim();
                // entityDetails.AlternateName = Request["alternateName"].Trim() == "" ? null : Request["alternateName"].Trim();
                entityDetails.VatID = Request["vatID"].Trim() == "" ? null : Request["vatID"].Trim();
                entityDetails.Telephone = Convert.ToInt32(Request["telephone"].Trim());
                if (Request["fax"].Trim() == "")
                {
                    entityDetails.FaxNumber = null;
                }
                else
                {
                    entityDetails.FaxNumber = Convert.ToInt32(Request["fax"].Trim());
                }
                entityDetails.Email = Request["email"].Trim() == "" ? null : Request["email"].Trim();
                entityDetails.AddressTypeID = Convert.ToInt32(Request["addressType"].Trim());
                entityDetails.AddressLine1 = Request["AddressLine1"].Trim();
                entityDetails.AddressLine2 = Request["AddressLine2"].Trim() == "" ? null : Request["AddressLine2"].Trim();
                entityDetails.City = Request["city"].Trim();
                if (Request.Form["province"].Trim() == "-1")
                {
                    entityDetails.ProvinceID = null;
                }
                else
                {
                    entityDetails.ProvinceID = Convert.ToInt32(Request.Form["province"].Trim());
                }
                entityDetails.CountryID = Convert.ToInt32(Request["countryID"].Trim());
                entityDetails.PostalCode = Request["postal"].Trim() == "" ? null : Request["postal"].Trim();
                entityDetails.ParentEntityID = Convert.ToInt32(Request["parentEntityID"].Trim());
                entityDetails.Verified = false;
                entityDetails.Deleted = false;
                #endregion end of get form values

                #region ServerCAll Entity Add
                object updateEntity = Models.EntityModel.UpdateEntity((int)userID, entityDetails);
                #endregion End ServerCALL Entity Add

                return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// remove entity per ID
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveEntity(string entityGUID)
        {
            try
            {
                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                #region Variables
                ///entity values
                Entity Entity = new Entity();
                Entity.EntityID = entityID;
                Entity.Deleted = true;
                ///result varianle if saved
                var entityResult = "";
                object removeUserFromDeletedEntity = null;
                #endregion end vaiables

                #region remove entity set delete to 1
                object entityDel = Models.EntityModel.RemoveEntity(Entity);
                entityResult = entityDel.ToString();
                #endregion remove entity set delete to 1


                #region remove user from that entity
                //get user details
                object Userslist = Models.UserModel.GetUsers(null, null, null, null, null, null, null, null);

                if (entityResult != null)
                {
                    //chck if any user exist in that entity
                    foreach (var existEntityvalue in Userslist as IEnumerable<dynamic>)
                    {
                        foreach (var existEntity in existEntityvalue as Dictionary<string, dynamic>)
                        {
                            if (Convert.ToString(existEntity.Value) == entityID.ToString())
                            {
                                removeUserFromDeletedEntity = Models.EntityModel.RemoveUserFromDeletedEntity(Convert.ToInt32(entityID));
                            }
                        }
                    }
                    //end of chck if any user exist in that entity
                }
                #endregion end of remove user from that entity


                Guid guidOutput;
                bool isGuidEntityDel = Guid.TryParse(entityDel.ToString(), out guidOutput);
                bool isGuidRemoveUserFromDeletedEntity = Guid.TryParse(Convert.ToString(removeUserFromDeletedEntity), out guidOutput);

                bool responseResult = false;
                if (isGuidEntityDel || isGuidRemoveUserFromDeletedEntity)
                {
                    responseResult = true;
                }
                return RedirectToAction("Index", new { partial = true, responseResult = responseResult });

                //return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

    }
}