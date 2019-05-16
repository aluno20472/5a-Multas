using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes
    {
        public Agentes()
        {
            ListaDasMultas = new HashSet<Multas>();
        }
        public int ID { get; set; }

        [RegularExpression("([A-Za-zç]+( |-|')?)+", ErrorMessage = "Só pode escrever letras no nome. Deve começar por uma maiúscula.")]
        public string Nome { get; set; }

        [RegularExpression("(Tomar|Ourém|Torres Novas|Lisboa|Leiria)")]
        public string Esquadra { get; set; }

        public string Fotografia { get; set; }

        public virtual ICollection<Multas> ListaDasMultas { get; set; }
    }
}