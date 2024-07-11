using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ActivityAuditController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetActivities()
        {
            try
            {
                var activities = db.EVENT
                                   .Where(e => e.EVENT_STATE == "未审核")
                                   .AsQueryable()
                                   .ToList();

                var result = activities.Select(a => new
                {
                    a.EVENT_ID,
                    a.EVENT_NAME,
                    EVENT_DATE = a.EVENT_DATE.ToString("yyyy-MM-dd HH:mm"),
                    a.INITIATOR_ID
                }).ToList();

                return Json(new { activities = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = "无法访问数据库中的 EVENT 表!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
