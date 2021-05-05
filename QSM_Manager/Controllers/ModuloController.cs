using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QSM_Manager.Models;

namespace QSM_Manager.Controllers
{
    public class ModuloController : Controller
    {
        private QuienSabeMasDBEntities db = new QuienSabeMasDBEntities();

        // GET: Modulo
        public ActionResult Index()
        {
            var modulo = db.modulo.Include(m => m.empresa);
            return View(modulo.ToList());
        }

        // GET: Modulo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = db.modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Modulo/Create
        public ActionResult Create()
        {
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa");
            return View();
        }

        // POST: Modulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modulo_id,empresa_id,nombre_modulo,descripcion_modulo,tiempo_certificacion,cantidad_preguntas,habilitado,porcentaje_certificacion,score_model_id")] modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.modulo.Add(modulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", modulo.empresa_id);
            return View(modulo);
        }

        // GET: Modulo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = db.modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", modulo.empresa_id);
            return View(modulo);
        }

        // POST: Modulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "modulo_id,empresa_id,nombre_modulo,descripcion_modulo,tiempo_certificacion,cantidad_preguntas,habilitado,porcentaje_certificacion,score_model_id")] modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", modulo.empresa_id);
            return View(modulo);
        }

        // GET: Modulo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = db.modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modulo modulo = db.modulo.Find(id);
            db.modulo.Remove(modulo);
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