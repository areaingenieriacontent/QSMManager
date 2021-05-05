using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using QSM_Manager.Models;
using QSM_Manager.ViewModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace QSM_Manager.Controllers
{
    public class MatriculaController : Controller
    {
        private QuienSabeMasDBEntities db = new QuienSabeMasDBEntities();

        // GET: Matricula
        public ActionResult Index()
        {
            var usuario = db.usuario.Include(u => u.empresa).Include(u => u.Grupo).Include(u => u.tipo_usuario);
            return View(usuario.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatriculaViewModel userdata = new MatriculaViewModel
            {
                user = db.usuario.Find(id),
                enrollments =db.matricula.Where(x=>x.usuario_id==id).ToList()
            };

            if (userdata == null)
            {
                return HttpNotFound();
            }
            return View(userdata);
        }

        public ActionResult Desmatricular(int? userid, int? modId)
        {
            matricula matriculaARemover = db.matricula.Find(userid, modId);
            db.matricula.Remove(matriculaARemover);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult IndexModulo()
        {
            var modulos = db.modulo.ToList();
            return View(modulos);
        }

        public ActionResult Matricular(int? id)
        {
            ViewBag.moduloId = id;
            var usuarios = db.usuario.ToList();
            return View(usuarios);
        }

        public ActionResult IndexGrupo()
        {
            var grupos = db.Grupo.ToList();
            return View(grupos);
        }

        public ActionResult ModuloGrupo(int? grupo_id)
        {
            ViewBag.grupo_id = grupo_id;
            var modulos = db.modulo.ToList();
            return View(modulos);
        }

        public ActionResult ConfirmarMatricula(int? userid, int? modId)
        {
            matricula matriculaAAgregar = db.matricula.Find(userid, modId);
            if (matriculaAAgregar == null)
            {
                matriculaAAgregar = new matricula { modulo_id = (int)modId, usuario_id = (int)userid, habilitado = 1, intentos = 2 };
                db.matricula.Add(matriculaAAgregar);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult MatricularGrupo(int? grupoId, int? modId)
        {
            List<usuario> listaDeUsuarios = db.usuario.Where(x => x.grupo_id == grupoId).ToList();
            for (int cont = 0;cont<listaDeUsuarios.Count; cont++)
            {
                usuario usu = listaDeUsuarios[cont];
                matricula matriculaAAgregar = db.matricula.Find(usu.usuario_id, modId);
                if (matriculaAAgregar == null)
                {
                    matriculaAAgregar = new matricula { modulo_id = (int)modId, usuario_id = usu.usuario_id, habilitado = 1, intentos = 2 };
                    db.matricula.Add(matriculaAAgregar);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}