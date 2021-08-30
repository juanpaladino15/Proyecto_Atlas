using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Atlas.Models;

namespace Atlas.Controllers
{
    public class RolesController : Controller
    {
        private AtlasBDEntities db = new AtlasBDEntities();

        // GET: Roles
        public ActionResult Index()
        {
            var rol = db.Rol.Include(r => r.Equipo).Include(r => r.Usuario);
            return View(rol.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre");
            ViewBag.id_usuario = new SelectList(db.Usuario, "id", "nombre");
            return View();
        }

        // POST: Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,id_equipo,id_usuario")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", rol.id_equipo);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id", "nombre", rol.id_usuario);
            return View(rol);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", rol.id_equipo);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id", "nombre", rol.id_usuario);
            return View(rol);
        }

        // POST: Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,id_equipo,id_usuario")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", rol.id_equipo);
            ViewBag.id_usuario = new SelectList(db.Usuario, "id", "nombre", rol.id_usuario);
            return View(rol);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rol rol = db.Rol.Find(id);
            db.Rol.Remove(rol);
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
