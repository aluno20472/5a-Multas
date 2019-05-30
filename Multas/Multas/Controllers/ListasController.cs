using Multas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multas.Controllers
{
    public class ListasController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listas
        // [Authorize]
        //   [Authorize(Roles = "Admin,Cliente")]
        //[Authorize(Roles = "Cliente")]
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {


            Agentes agente = db.Agentes.Find(1);

            Condutores condutor = db.Condutores.Find(1);

            Multas.Models.Multas multa = db.Multas.Find(1);


            var lista = new ListaViewModel();
            lista.Agente = agente;
            lista.Condutor = condutor;
            lista.Multa = multa;


            return View(lista);
        }


    }
}
