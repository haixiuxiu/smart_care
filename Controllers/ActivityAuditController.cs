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
            try
            {
                var activities = db.EVENT.AsQueryable().ToList();
                return View(activities);
            }
            catch (Exception ex)
            {
                // 记录错误日志
                ViewBag.ErrorMessage = "无法访问数据库中的 EVENT 表!" + ex.Message;
                return View(new List<EVENT>());
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
