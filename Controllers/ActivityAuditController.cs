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
                    a.INITIATOR_ID,
                    a.EVENT_STATE
                }).ToList();

                return Json(new { activities = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = "无法访问数据库中的 EVENT 表!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ApproveActivities(int eventId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Approving activity with ID: " + eventId); // 添加日志
                var activity = db.EVENT.FirstOrDefault(a => a.EVENT_ID == eventId);
                if (activity == null)
                {
                    return Json(new { success = false, message = "活动未找到" });
                }

                activity.EVENT_STATE = "审核通过";
                db.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Successfully approved activity with ID: " + eventId); // 添加日志
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error approving activity: " + ex.Message); // 添加日志
                return Json(new { success = false, message = "审核失败: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RejectActivity(int eventId)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("Approving activity with ID: " + eventId); // 添加日志
                var activity = db.EVENT.FirstOrDefault(a => a.EVENT_ID == eventId);
                if (activity == null)
                {
                    return Json(new { success = false, message = "活动未找到" });
                }

                activity.EVENT_STATE = "驳回";
                db.SaveChanges();

                //System.Diagnostics.Debug.WriteLine("Successfully approved activity with ID: " + eventId); // 添加日志
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error rejecting activity: " + ex.Message); // 添加日志
                return Json(new { success = false, message = "驳回失败: " + ex.Message });
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
