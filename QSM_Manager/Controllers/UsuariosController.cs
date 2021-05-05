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
    public class UsuariosController : Controller
    {
        private QuienSabeMasDBEntities db = new QuienSabeMasDBEntities();

        // GET: usuarios
        public ActionResult Index()
        {
            var usuario = db.usuario.Include(u => u.empresa).Include(u => u.Grupo).Include(u => u.tipo_usuario);
            return View(usuario.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa");
            ViewBag.grupo_id = new SelectList(db.Grupo, "id_grupo", "nombre_grupo");
            ViewBag.tipo_usuario_id = new SelectList(db.tipo_usuario, "tipo_usuario_id", "nombre_tipo_usuario");
            return View();
        }

        // POST: usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuario_id,empresa_id,tipo_usuario_id,nombres,apellidos,documento_identificacion,password,sexo,fecha_registro,ultimo_ingreso,correo,grupo_id,ciudad_id")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", usuario.empresa_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "id_grupo", "nombre_grupo", usuario.grupo_id);
            ViewBag.tipo_usuario_id = new SelectList(db.tipo_usuario, "tipo_usuario_id", "nombre_tipo_usuario", usuario.tipo_usuario_id);
            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", usuario.empresa_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "id_grupo", "nombre_grupo", usuario.grupo_id);
            ViewBag.tipo_usuario_id = new SelectList(db.tipo_usuario, "tipo_usuario_id", "nombre_tipo_usuario", usuario.tipo_usuario_id);
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuario_id,empresa_id,tipo_usuario_id,nombres,apellidos,documento_identificacion,password,sexo,fecha_registro,ultimo_ingreso,correo,grupo_id,ciudad_id")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empresa_id = new SelectList(db.empresa, "empresa_id", "nombre_empresa", usuario.empresa_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "id_grupo", "nombre_grupo", usuario.grupo_id);
            ViewBag.tipo_usuario_id = new SelectList(db.tipo_usuario, "tipo_usuario_id", "nombre_tipo_usuario", usuario.tipo_usuario_id);
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
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