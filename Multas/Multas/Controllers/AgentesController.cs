using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Agentes
        public ActionResult Index()
        {
            var listaAgentes = db.Agentes.OrderBy(a => a.Nome).ToList();

            return View(listaAgentes);
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// Mostra os dados de um agentes.
        /// </summary>
        /// <param name="id">Identifica o agente</param>
        /// <returns>Devolev a view com os dados</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes, HttpPostedFileBase fotografia)
        {
            string caminho = "";

            if (fotografia == null)
            {
                agentes.Fotografia = "BeatrizPinto.jpg";
            }
            else
            {
                if(fotografia.ContentType == "image/jpeg" || fotografia.ContentType == "image/png")
                {
                    string extensao = Path.GetExtension(fotografia.FileName).ToLower();
                    string nome;
                    Guid g = new Guid();

                    nome = g.ToString() + extensao;
                    caminho = Path.Combine(Server.MapPath("~/imagens"), nome);
                    agentes.Fotografia = nome;
                }
            }


            if (ModelState.IsValid)
            {

                try
                {
                    db.Agentes.Add(agentes);
                    db.SaveChanges();
                    fotografia.SaveAs(caminho);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "");
                }
                
                return RedirectToAction("Index");
            }

            return View(agentes);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Agentes agentes = db.Agentes.Find(id);

            if (agentes == null)
            {
                return RedirectToAction("Index");
            }

            Session["Agente"] = agentes.ID;

            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            if(id != (int) Session["Agente"])
            {
                return RedirectToAction("Index");
            }

            Agentes agentes = db.Agentes.Find(id);

            if (agentes == null)
                return RedirectToAction("Index");

            try
            {
                db.Agentes.Remove(agentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não é possível remover o agente. Provavelmente, ele tem multas associadas a ele...");
                return View(agentes);
            }
            
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
