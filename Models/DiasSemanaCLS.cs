using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class DiasSemanaCLS
    {
        public DiasSemanaCLS()
        {
            DiasTrabajoDocsCLS = new HashSet<DiasTrabajoDocCLS>();
            ProgramaDocCLS = new HashSet<ProgramaDocCLS>();
        }
        public int Id { get; set; }
        public string Dia {get; set; }

        public virtual ICollection<DiasTrabajoDocCLS> DiasTrabajoDocsCLS { get; set; }
        public virtual ICollection<ProgramaDocCLS> ProgramaDocCLS { get; set; }
    }
}