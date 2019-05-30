using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class ListaViewModel
    {

        public Condutores Condutor { get; set; }
        public Multas Multa { get; set; }
        public Agentes Agente { get; set; }
    }
}