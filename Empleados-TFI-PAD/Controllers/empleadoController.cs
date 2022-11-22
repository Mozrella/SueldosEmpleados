using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Empleados_TFI_PAD;

namespace Empleados_TFI_PAD.Controllers
{
    public class empleadoController : Controller
    {
        private EmpleadosTFIEntities db = new EmpleadosTFIEntities();

        // GET: empleado
        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.departamento).Include(e => e.rol).Include(e => e.empleado2);
            return View(empleado.ToList());
        }

        // GET: empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: empleado/Create
        public ActionResult Create()
        {

            ViewBag.id_depto = new SelectList(db.departamento, "id_depto", "nombre_depto");
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "nombre_rol");

            var supervisores = db.empleado.ToList();

            ViewBag.id_supervisor = new SelectList(
               supervisores.Select(
                   item => new { id_empleado = item.id_empleado, nombreCompleto = $"{item.nombre} {item.apellido}" }),
                   "id_empleado",
                   "nombreCompleto"
           );
            return View();
        }

        // POST: empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleado,dni,id_rol,id_depto,id_supervisor,nombre,apellido,direccion,mail,telefono,fecha_nac")] empleado empleado)
        {

            if (true)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_depto = new SelectList(db.departamento, "id_depto", "nombre_depto", empleado.id_depto);
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "nombre_rol", empleado.id_rol);
            var supervisores = db.empleado.ToList();
            ViewBag.id_supervisor = new SelectList(
               supervisores.Select(
                   item => new { id_empleado = item.id_empleado, nombreCompleto = $"{item.nombre} {item.apellido}" }),
                   "id_empleado",
                   "nombreCompleto"
           );
            return View(empleado);
        }

        // GET: empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_depto = new SelectList(db.departamento, "id_depto", "nombre_depto", empleado.id_depto);
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "nombre_rol", empleado.id_rol);
            //ViewBag.id_supervisor = new SelectList(db.empleado, "id_empleado", "nombre", empleado.id_supervisor);
            var supervisores = db.empleado.ToList();

            ViewBag.id_supervisor = new SelectList(
               supervisores.Select(
                   item => new { id_empleado = item.id_empleado, nombreCompleto = $"{item.nombre} {item.apellido}" }),
                   "id_empleado",
                   "nombreCompleto"
           );
            return View(empleado);
        }

        // POST: empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,dni,id_rol,id_depto,id_supervisor,nombre,apellido,direccion,mail,telefono,fecha_nac")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_depto = new SelectList(db.departamento, "id_depto", "nombre_depto", empleado.id_depto);
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "nombre_rol", empleado.id_rol);
            //ViewBag.id_supervisor = new SelectList(db.empleado, "id_empleado", "nombre", empleado.id_supervisor);
            var supervisores = db.empleado.ToList();
            ViewBag.id_supervisor = new SelectList(
               supervisores.Select(
                   item => new { id_empleado = item.id_empleado, nombreCompleto = $"{item.nombre} {item.apellido}" }),
                   "id_empleado",
                   "nombreCompleto"
           );
            return View(empleado);
        }

        // GET: empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleado empleado = db.empleado.Find(id);
            db.empleado.Remove(empleado);
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
