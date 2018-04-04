using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduPointStudApp.Models;

namespace EduPointStudApp.Controllers
{
    public class LuokkahuoneetController : Controller
    {
        private OpiskelijarekisteriEntities db = new OpiskelijarekisteriEntities();

        // GET: Luokkahuoneet
        public ActionResult Index()
        {
            return View(db.Luokkahuone.ToList());
        }

        // GET: Luokkahuoneet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkahuone luokkahuone = db.Luokkahuone.Find(id);
            if (luokkahuone == null)
            {
                return HttpNotFound();
            }
            return View(luokkahuone);
        }

        // GET: Luokkahuoneet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Luokkahuoneet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LuokkaID,LuokanNimi,LuokanKoodi")] Luokkahuone luokkahuone)
        {
            if (ModelState.IsValid)
            {
                db.Luokkahuone.Add(luokkahuone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(luokkahuone);
        }

        // GET: Luokkahuoneet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkahuone luokkahuone = db.Luokkahuone.Find(id);
            if (luokkahuone == null)
            {
                return HttpNotFound();
            }
            return View(luokkahuone);
        }

        // POST: Luokkahuoneet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LuokkaID,LuokanNimi,LuokanKoodi")] Luokkahuone luokkahuone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(luokkahuone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(luokkahuone);
        }

        // GET: Luokkahuoneet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkahuone luokkahuone = db.Luokkahuone.Find(id);
            if (luokkahuone == null)
            {
                return HttpNotFound();
            }
            return View(luokkahuone);
        }

        // POST: Luokkahuoneet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Luokkahuone luokkahuone = db.Luokkahuone.Find(id);
            db.Luokkahuone.Remove(luokkahuone);
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
