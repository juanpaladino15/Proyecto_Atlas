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
    public class TareasController : Controller
    {
        private AtlasBDEntities db = new AtlasBDEntities();

        // GET: Tareas
        public ActionResult Index()
        {
            var tarea = db.Tarea.Include(t => t.Equipo).Include(t => t.Prioridad).Include(t => t.Proyecto);
            return View(tarea.ToList());
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarea tarea = db.Tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre");
            ViewBag.id_prioridad = new SelectList(db.Prioridad, "id", "nombre");
            ViewBag.id_proyecto = new SelectList(db.Proyecto, "id", "nombre");
            return View();
        }

        // POST: Tareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_proyecto,nombre,fechaIni,fechaFin,id_prioridad,alcanzada,id_equipo")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.Tarea.Add(tarea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", tarea.id_equipo);
            ViewBag.id_prioridad = new SelectList(db.Prioridad, "id", "nombre", tarea.id_prioridad);
            ViewBag.id_proyecto = new SelectList(db.Proyecto, "id", "nombre", tarea.id_proyecto);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarea tarea = db.Tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", tarea.id_equipo);
            ViewBag.id_prioridad = new SelectList(db.Prioridad, "id", "nombre", tarea.id_prioridad);
            ViewBag.id_proyecto = new SelectList(db.Proyecto, "id", "nombre", tarea.id_proyecto);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_proyecto,nombre,fechaIni,fechaFin,id_prioridad,alcanzada,id_equipo")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_equipo = new SelectList(db.Equipo, "id", "nombre", tarea.id_equipo);
            ViewBag.id_prioridad = new SelectList(db.Prioridad, "id", "nombre", tarea.id_prioridad);
            ViewBag.id_proyecto = new SelectList(db.Proyecto, "id", "nombre", tarea.id_proyecto);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarea tarea = db.Tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarea tarea = db.Tarea.Find(id);
            db.Tarea.Remove(tarea);
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
