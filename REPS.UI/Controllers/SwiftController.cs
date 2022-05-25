using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.prowidesoftware.swift.model.mt.mt1xx;
using com.prowidesoftware.swift.model;
using com.prowidesoftware.swift.model.field;
using java.util;
using System.IO;
using Global;

namespace REPS.UI.Controllers
{
    public class SwiftController : Controller
    {
        // GET: Swift
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get data for SWIFT text file and create the file in the directory specified in the web.config
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSwiftFile()
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

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
                        ViewBag.PageLayoutPath = null;
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                        ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
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

                #endregion Get DealID from URL Parameter (Unique Reference)

                #region Create swift text file
                string swiftFileContent = Models.SwiftModel.CreateSwiftFile(); // We get the content of the swift file
                // Filename format: SWIFT + DealID + Today's Date + Current Time.txt
                string dateTimeStamp = DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.TimeOfDay;
                string filePath = Global.File.GetFolderPath((int)Enums.FolderPath.Swift);
                using (var swiftTextfile = new StreamWriter(filePath + "SWIFT_" + dealID + "_" + dateTimeStamp.Replace('/', '.').Replace(':', '.') + ".txt", false))
                {
                    swiftTextfile.WriteLine(swiftFileContent);
                    swiftTextfile.Close();
                }
                #endregion Create swift text file

                return Content("ok");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return Content(ex.Message);
            }
        }
    }
}