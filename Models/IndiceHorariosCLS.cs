using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class IndiceHorariosCLS
    {
        public int IdHorarioDoc { get; set; }
        public int IdDoc { get; set; }
        public string Apellido { get; set; }
        public Nullable<System.TimeSpan> DesdeTiempo { get; set; }
        public Nullable<System.TimeSpan> HastaTiempo { get; set; }
        public bool Activo { get; set; }

        public virtual Doctores Doctores { get; set; }
    }
}