using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Empleados_TFI_PAD;

namespace Empleados_TFI_PAD.Controllers
{
    public class recibo_sueldoController : Controller
    {
        private EmpleadosTFIEntities db = new EmpleadosTFIEntities();

        // GET: recibo_sueldo
        public ActionResult Index(int? idEmpleado)
        {
            //var recibo_sueldo = db.recibo_sueldo.Include(r => r.empleado);
            //return View(recibo_sueldo.ToList());
            IQueryable<recibo_sueldo> reciboSueldo;

            if (idEmpleado != null)
            {
                reciboSueldo = db.recibo_sueldo.Where(item => item.id_empleado == idEmpleado);
            }
            else
            {
                reciboSueldo = db.recibo_sueldo.Include(r => r.empleado);
            }


            return View(reciboSueldo.ToList());
        }

        // GET: recibo_sueldo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibo_sueldo recibo_sueldo = db.recibo_sueldo.Find(id);
            if (recibo_sueldo == null)
            {
                return HttpNotFound();
            }
            return View(recibo_sueldo);
        }

        // GET: recibo_sueldo/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre");
            return View();
        }

        // POST: recibo_sueldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "num_recibo,id_empleado,fecha_liq")] recibo_sueldo recibo_sueldo)
        {
            if (ModelState.IsValid)
            {
                db.recibo_sueldo.Add(recibo_sueldo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre", recibo_sueldo.id_empleado);
            return View(recibo_sueldo);
        }

        // GET: recibo_sueldo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibo_sueldo recibo_sueldo = db.recibo_sueldo.Find(id);
            if (recibo_sueldo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre", recibo_sueldo.id_empleado);
            return View(recibo_sueldo);
        }

        // POST: recibo_sueldo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num_recibo,id_empleado,fecha_liq")] recibo_sueldo recibo_sueldo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recibo_sueldo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre", recibo_sueldo.id_empleado);
            return View(recibo_sueldo);
        }

        // GET: recibo_sueldo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recibo_sueldo recibo_sueldo = db.recibo_sueldo.Find(id);
            if (recibo_sueldo == null)
            {
                return HttpNotFound();
            }
            return View(recibo_sueldo);
        }

        // POST: recibo_sueldo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recibo_sueldo recibo_sueldo = db.recibo_sueldo.Find(id);
            db.recibo_sueldo.Remove(recibo_sueldo);
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
