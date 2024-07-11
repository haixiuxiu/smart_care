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
    public class EVENTsController : Controller
    {
        private Entities db = new Entities();

        // GET: EVENTs
        public ActionResult Index()
        {
            var eVENT = db.EVENT.Include(e => e.EVENTCHECK);
            return View(eVENT.ToList());
        }

        // GET: EVENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // GET: EVENTs/Create
        public ActionResult Create()
        {
            ViewBag.EVENT_ID = new SelectList(db.EVENTCHECK, "EVENT_ID", "REVIEW_RESULT");
            return View();
        }

        // POST: EVENTs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EVENT_ID,EVENT_NAME,EVENT_TYPE,EVENT_CONTENT,EVENT_SITE,EVENT_DATE,EA_ID,EVENT_START_TIME,EVENT_END_TIME,STAR_NUMBER,PARTICIPANT_NUMBER,INITIATOR_ID")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.EVENT.Add(eVENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EVENT_ID = new SelectList(db.EVENTCHECK, "EVENT_ID", "REVIEW_RESULT", eVENT.EVENT_ID);
            return View(eVENT);
        }

        // GET: EVENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.EVENT_ID = new SelectList(db.EVENTCHECK, "EVENT_ID", "REVIEW_RESULT", eVENT.EVENT_ID);
            return View(eVENT);
        }

        // POST: EVENTs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EVENT_ID,EVENT_NAME,EVENT_TYPE,EVENT_CONTENT,EVENT_SITE,EVENT_DATE,EA_ID,EVENT_START_TIME,EVENT_END_TIME,STAR_NUMBER,PARTICIPANT_NUMBER,INITIATOR_ID")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EVENT_ID = new SelectList(db.EVENTCHECK, "EVENT_ID", "REVIEW_RESULT", eVENT.EVENT_ID);
            return View(eVENT);
        }

        // GET: EVENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: EVENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENT eVENT = db.EVENT.Find(id);
            db.EVENT.Remove(eVENT);
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
