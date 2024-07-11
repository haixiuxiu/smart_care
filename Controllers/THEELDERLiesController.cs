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
    public class THEELDERLiesController : Controller
    {
        private Entities db = new Entities();

        // GET: THEELDERLies
        public ActionResult Index()
        {
            return View(db.THEELDERLY.ToList());
        }

        // GET: THEELDERLies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THEELDERLY tHEELDERLY = db.THEELDERLY.Find(id);
            if (tHEELDERLY == null)
            {
                return HttpNotFound();
            }
            return View(tHEELDERLY);
        }

        // GET: THEELDERLies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: THEELDERLies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ELDERLY_ID,ELDERLY_ADDRESS,ELDERLY_PASSWORD,ELDERLY_PHONE,ELDERLY_NAME,ELDERLY_PROFILE,ELDERLY_BIRTHDAY,ELDERLY_STATUS,ELDERLY_GENDER,ELDERLY_CREATE_TIME")] THEELDERLY tHEELDERLY)
        {
            if (ModelState.IsValid)
            {
                db.THEELDERLY.Add(tHEELDERLY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHEELDERLY);
        }

        // GET: THEELDERLies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THEELDERLY tHEELDERLY = db.THEELDERLY.Find(id);
            if (tHEELDERLY == null)
            {
                return HttpNotFound();
            }
            return View(tHEELDERLY);
        }

        // POST: THEELDERLies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ELDERLY_ID,ELDERLY_ADDRESS,ELDERLY_PASSWORD,ELDERLY_PHONE,ELDERLY_NAME,ELDERLY_PROFILE,ELDERLY_BIRTHDAY,ELDERLY_STATUS,ELDERLY_GENDER,ELDERLY_CREATE_TIME")] THEELDERLY tHEELDERLY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHEELDERLY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHEELDERLY);
        }

        // GET: THEELDERLies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THEELDERLY tHEELDERLY = db.THEELDERLY.Find(id);
            if (tHEELDERLY == null)
            {
                return HttpNotFound();
            }
            return View(tHEELDERLY);
        }

        // POST: THEELDERLies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THEELDERLY tHEELDERLY = db.THEELDERLY.Find(id);
            db.THEELDERLY.Remove(tHEELDERLY);
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
