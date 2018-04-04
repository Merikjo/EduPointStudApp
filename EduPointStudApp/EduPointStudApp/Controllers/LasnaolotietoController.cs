using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduPointStudApp.Models;
using EduPointStudApp.Utilities;
using Newtonsoft.Json;

namespace EduPointStudApp.Controllers
{
    public class LasnaolotietoController : Controller
    {
        private OpiskelijarekisteriEntities db = new OpiskelijarekisteriEntities();

        // GET: Lasnaolotieto
        public ActionResult Index()
        {
            List<KurssiRekisteriViewModel> model = new List<KurssiRekisteriViewModel>();

            OpiskelijarekisteriEntities entities = new OpiskelijarekisteriEntities();
            try
            {
                List<Lasnaolotiedot> lasnaolot = entities.Lasnaolotiedot.ToList();

                CultureInfo fiFi = new CultureInfo("fi-FI");

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (Lasnaolotiedot lasnaolo in lasnaolot)
                {
                    KurssiRekisteriViewModel view = new KurssiRekisteriViewModel();
                    view.RekisteriID = lasnaolo.RekisteriID;
                    view.Luokkakoodi = lasnaolo.Luokkakoodi;
                    //view.AssetName = asset.Assets.Type + ": " + asset.Assets.Model;
                    view.KirjattuSisaan = lasnaolo.KirjattuSisaan.GetValueOrDefault();
                    //view.KirjattuUlos = lasnaolo.KirjattuUlos.GetValueOrDefault();

                    view.KurssiID = lasnaolo.Kurssi?.KurssiID;
                    view.Kurssinimi = lasnaolo.Kurssi.Kurssinimi;
                    view.Kurssikoodi = lasnaolo.Kurssi.Kurssikoodi;

                    //view.Etunimi = lasnaolo.Opiskelija?.Etunimi;
                    //view.Sukunimi = lasnaolo.Opiskelija?.Sukunimi;
                    view.OpiskelijaID = lasnaolo.Opiskelija?.OpiskelijaID;
                    view.Opiskelijanro = lasnaolo.Opiskelija.Opiskelijanro;
                    view.OpiskelijaNimi = lasnaolo.Opiskelija?.Etunimi + " " + lasnaolo.Opiskelija?.Sukunimi;

                    view.OpettajaID = lasnaolo.Opettaja?.OpettajaID;
                    //view.EtunimiOpe = lasnaolo.Opettaja?.Etunimi;
                    //view.SukunimiOpe = lasnaolo.Opettaja?.Sukunimi;
                    view.OpettajaNimi = lasnaolo.Opettaja?.Etunimi + " " + lasnaolo.Opettaja?.Sukunimi;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //Lasnaolotieto.cs - KURSSITIEDON TALLENTAMINEN (SQL) TIETOKANTAAN
        public JsonResult TallennaKurssi()
        {
            string json = Request.InputStream.ReadToEnd();
            KurssiDataModel inputData =
                JsonConvert.DeserializeObject<KurssiDataModel>(json);

            bool success = false;
            string error = "";

            OpiskelijarekisteriEntities entities = new OpiskelijarekisteriEntities();

            try
            {
                //haetaan ensin luokan id-numero koodin perusteella:
                int opiskelijaId = (from o in entities.Opiskelija
                                where o.Opiskelijanro == inputData.Opiskelijanro
                                select o.OpiskelijaID).FirstOrDefault();

                //haetaan kurssin id-numero koodin perusteella:
                int kurssiId = (from k in entities.Kurssi
                                where k.Kurssikoodi == inputData.Kurssikoodi
                                select k.KurssiID).FirstOrDefault();

                if ((opiskelijaId > 0) && (kurssiId > 0))
                {
                    //tallennetaan uusi rivi aikaleiman kanssa kantaan:
                    Lasnaolotiedot newEntry = new Lasnaolotiedot();
                    newEntry.KirjattuID = opiskelijaId;
                    newEntry.LuokkaID = kurssiId;
                    newEntry.KirjattuSisaan = DateTime.Now;

                    entities.Lasnaolotiedot.Add(newEntry);

                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }

            //palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }

        // GET: Lasnaolotieto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lasnaolotiedot lasnaolotiedot = db.Lasnaolotiedot.Find(id);
            if (lasnaolotiedot == null)
            {
                return HttpNotFound();
            }
            return View(lasnaolotiedot);
        }

        // GET: Lasnaolotieto/Create
        public ActionResult Create()
        {
            ViewBag.KurssiID = new SelectList(db.Kurssi, "KurssiID", "Kurssinimi");
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi");
            ViewBag.OpiskelijaID = new SelectList(db.Opiskelija, "OpiskelijaID", "Etunimi");
            return View();
        }

        // POST: Lasnaolotieto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kirjautuminen_sisaan,Kirjautuminen_ulos,Luokkanumero,OpettajaID,RekisteriID,OpiskelijaID,KurssiID")] Lasnaolotiedot lasnaolotiedot)
        {
            if (ModelState.IsValid)
            {
                db.Lasnaolotiedot.Add(lasnaolotiedot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KurssiID = new SelectList(db.Kurssi, "KurssiID", "Kurssinimi", lasnaolotiedot.KurssiID);
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", lasnaolotiedot.OpettajaID);
            ViewBag.OpiskelijaID = new SelectList(db.Opiskelija, "OpiskelijaID", "Etunimi", lasnaolotiedot.OpiskelijaID);
            return View(lasnaolotiedot);
        }

        // GET: Lasnaolotieto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lasnaolotiedot lasnaolotiedot = db.Lasnaolotiedot.Find(id);
            if (lasnaolotiedot == null)
            {
                return HttpNotFound();
            }
            ViewBag.KurssiID = new SelectList(db.Kurssi, "KurssiID", "Kurssinimi", lasnaolotiedot.KurssiID);
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", lasnaolotiedot.OpettajaID);
            ViewBag.OpiskelijaID = new SelectList(db.Opiskelija, "OpiskelijaID", "Etunimi", lasnaolotiedot.OpiskelijaID);
            return View(lasnaolotiedot);
        }

        // POST: Lasnaolotieto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kirjautuminen_sisaan,Kirjautuminen_ulos,Luokkanumero,OpettajaID,RekisteriID,OpiskelijaID,KurssiID")] Lasnaolotiedot lasnaolotiedot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lasnaolotiedot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KurssiID = new SelectList(db.Kurssi, "KurssiID", "Kurssinimi", lasnaolotiedot.KurssiID);
            ViewBag.OpettajaID = new SelectList(db.Opettaja, "OpettajaID", "Etunimi", lasnaolotiedot.OpettajaID);
            ViewBag.OpiskelijaID = new SelectList(db.Opiskelija, "OpiskelijaID", "Etunimi", lasnaolotiedot.OpiskelijaID);
            return View(lasnaolotiedot);
        }

        // GET: Lasnaolotieto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lasnaolotiedot lasnaolotiedot = db.Lasnaolotiedot.Find(id);
            if (lasnaolotiedot == null)
            {
                return HttpNotFound();
            }
            return View(lasnaolotiedot);
        }

        // POST: Lasnaolotieto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lasnaolotiedot lasnaolotiedot = db.Lasnaolotiedot.Find(id);
            db.Lasnaolotiedot.Remove(lasnaolotiedot);
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
