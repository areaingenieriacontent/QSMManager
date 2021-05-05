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
    public class IntentosController : Controller
    {
        private QuienSabeMasDBEntities db = new QuienSabeMasDBEntities();

        // GET: Intentos
        public ActionResult Index()
        {
            var usuarios = db.usuario.Include(m => m.matricula);
            return View(usuarios.ToList());
        }

        // GET: Intentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matricula matricula = db.matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // GET: Intentos/Create
        public ActionResult Create()
        {
            ViewBag.modulo_id = new SelectList(db.modulo, "modulo_id", "nombre_modulo");
            ViewBag.usuario_id = new SelectList(db.usuario, "usuario_id", "nombres");
            return View();
        }

        // POST: Intentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuario_id,modulo_id,habilitado,intentos")] matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.matricula.Add(matricula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.modulo_id = new SelectList(db.modulo, "modulo_id", "nombre_modulo", matricula.modulo_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "usuario_id", "nombres", matricula.usuario_id);
            return View(matricula);
        }

        // GET: Intentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<matricula> mat = db.matricula.Where(x => x.usuario_id == id).ToList();
            if (mat == null)
            {
                return HttpNotFound();
            }
            ViewBag.modulo_id = new SelectList(db.modulo, "modulo_id", "nombre_modulo");
            ViewBag.usuario_id = new SelectList(db.usuario, "usuario_id", "nombres");
            return View(mat);
        }

        // POST: Intentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(IList<matricula> matricula)
        {
            for (int cont = 0; cont < matricula.Count; cont++)
            {
                if (matricula[cont].usuario_id != 0 && matricula[cont].modulo_id != 0)
                {
                    db.Entry(matricula[cont]).State = EntityState.Modified;
                    db.SaveChanges();
                }
                if(cont==matricula.Count-1)
                    return RedirectToAction("Edit");
            }
            //ViewBag.modulo_id = new SelectList(db.modulo, "modulo_id", "nombre_modulo", matricula.modulo_id);
            //ViewBag.usuario_id = new SelectList(db.usuario, "usuario_id", "nombres", matricula.usuario_id);
            return View(matricula);
        }

        // GET: Intentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            matricula matricula = db.matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Intentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            matricula matricula = db.matricula.Find(id);
            db.matricula.Remove(matricula);
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