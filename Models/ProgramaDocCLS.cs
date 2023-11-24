using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class ProgramaDocCLS
    {
        public int IdPrograma { get; set; }
        public int IdDoc { get; set; }
        public int DiaSemana { get; set; }
        public Nullable<System.TimeSpan> Inicio { get; set; }
        public Nullable<System.TimeSpan> Fin { get; set; }
        public string NombrePac { get; set; }
        public Nullable<int> EdadPac { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }

        public virtual Doctores Doctores { get; set; }

        public virtual DiasSemanaCLS DiaNavegacion { get; set; }
       
    }
}