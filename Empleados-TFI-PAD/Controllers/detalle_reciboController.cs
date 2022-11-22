using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Empleados_TFI_PAD;
using Newtonsoft.Json.Linq;

namespace Empleados_TFI_PAD.Controllers
{
    public class detalle_reciboController : Controller
    {
        private EmpleadosTFIEntities db = new EmpleadosTFIEntities();

        // GET: detalle_recibo
        public ActionResult Index(int? numeroRecibo)
        {
            //var detalle_recibo = db.detalle_recibo.Include(d => d.recibo_sueldo);
            //return View(detalle_recibo.ToList());

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("BEARER", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2OTk0MDM3NzUsInR5cGUiOiJleHRlcm5hbCIsInVzZXIiOiJtb3JlYWxpbmNhc3RybzEyM0BnbWFpbC5jb20ifQ.h6ti6lzWbfWSk1vnmJms09prwqLAAo1cXntRU1ueabdOgNGrh3as5Y4FYbxbmjZ9JaIMBFOzc-JlPvJl41Up9g");
            var respuesta = cliente.GetStringAsync("https://api.estadisticasbcra.com/usd_of");
            String textoRespuesta = respuesta.Result;
            JArray jsonArray = JArray.Parse(textoRespuesta);
            ViewBag.dolar = jsonArray.ToArray().Last()["v"];

            IQueryable<detalle_recibo> detalleRecibo;

            if (numeroRecibo != null)
            {
                //detalleRecibo = db.detalle_recibo.Include(d => d.recibo_sueldo);
                detalleRecibo = db.detalle_recibo.Where(item => item.num_recibo == numeroRecibo);
            }
            else
            {
                //detalleRecibo = db.detalle_recibo.Where(item => item.num_recibo == numeroRecibo);
                detalleRecibo = db.detalle_recibo.Include(r => r.recibo_sueldo);
            }
            return View(detalleRecibo.ToList());
        }

        // GET: detalle_recibo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_recibo detalle_recibo = db.detalle_recibo.Find(id);
            if (detalle_recibo == null)
            {
                return HttpNotFound();
            }
            return View(detalle_recibo);
        }

        // GET: detalle_recibo/Create
        public ActionResult Create()
        {
            ViewBag.num_recibo = new SelectList(db.recibo_sueldo, "num_recibo", "num_recibo");
            return View();
        }

        // POST: detalle_recibo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_detalle,num_recibo,des_concepto,cantidad,deb_o_cred,sueldo_base")] detalle_recibo detalle_recibo)
        {
            if (ModelState.IsValid)
            {
                db.detalle_recibo.Add(detalle_recibo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.num_recibo = new SelectList(db.recibo_sueldo, "num_recibo", "num_recibo", detalle_recibo.num_recibo);
            return View(detalle_recibo);
        }

        // GET: detalle_recibo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_recibo detalle_recibo = db.detalle_recibo.Find(id);
            if (detalle_recibo == null)
            {
                return HttpNotFound();
            }
            ViewBag.num_recibo = new SelectList(db.recibo_sueldo, "num_recibo", "num_recibo", detalle_recibo.num_recibo);
            return View(detalle_recibo);
        }

        // POST: detalle_recibo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_detalle,num_recibo,des_concepto,cantidad,deb_o_cred,sueldo_base")] detalle_recibo detalle_recibo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_recibo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.num_recibo = new SelectList(db.recibo_sueldo, "num_recibo", "num_recibo", detalle_recibo.num_recibo);
            return View(detalle_recibo);
        }

        // GET: detalle_recibo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_recibo detalle_recibo = db.detalle_recibo.Find(id);
            if (detalle_recibo == null)
            {
                return HttpNotFound();
            }
            return View(detalle_recibo);
        }

        // POST: detalle_recibo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalle_recibo detalle_recibo = db.detalle_recibo.Find(id);
            db.detalle_recibo.Remove(detalle_recibo);
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
