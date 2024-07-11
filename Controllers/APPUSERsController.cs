using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class APPUSERsController : Controller
    {
        private Entities db = new Entities();

        // GET: APPUSERs
        public ActionResult Index()
        {
            var aPPUSER = db.APPUSER.Include(a => a.CARETAKER);
            return View(aPPUSER.ToList());
        }

        // GET: APPUSERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPUSER aPPUSER = db.APPUSER.Find(id);
            if (aPPUSER == null)
            {
                return HttpNotFound();
            }
            return View(aPPUSER);
        }

        // GET: APPUSERs/Create
        public ActionResult Create()
        {
            ViewBag.USER_ID = new SelectList(db.CARETAKER, "CARETAKER_ID", "CARETAKER_PHONE");
            return View();
        }

        // POST: APPUSERs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,USER_EMAIL,USER_PHONE,USER_PASSWORD,USER_NAME,USER_PROFILE,USER_CREATETIME,USER_BIRTHDAY,USER_GENDER,USER_STATUS")] APPUSER aPPUSER)
        {
            if (ModelState.IsValid)
            {
                db.APPUSER.Add(aPPUSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.CARETAKER, "CARETAKER_ID", "CARETAKER_PHONE", aPPUSER.USER_ID);
            return View(aPPUSER);
        }

        // GET: APPUSERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPUSER aPPUSER = db.APPUSER.Find(id);
            if (aPPUSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.CARETAKER, "CARETAKER_ID", "CARETAKER_PHONE", aPPUSER.USER_ID);
            return View(aPPUSER);
        }

        // POST: APPUSERs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,USER_EMAIL,USER_PHONE,USER_PASSWORD,USER_NAME,USER_PROFILE,USER_CREATETIME,USER_BIRTHDAY,USER_GENDER,USER_STATUS")] APPUSER aPPUSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPPUSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.CARETAKER, "CARETAKER_ID", "CARETAKER_PHONE", aPPUSER.USER_ID);
            return View(aPPUSER);
        }

        // GET: APPUSERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPUSER aPPUSER = db.APPUSER.Find(id);
            if (aPPUSER == null)
            {
                return HttpNotFound();
            }
            return View(aPPUSER);
        }

        // POST: APPUSERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APPUSER aPPUSER = db.APPUSER.Find(id);
            db.APPUSER.Remove(aPPUSER);
            db.SaveChanges();
            return RedirectToAction("Index");
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
