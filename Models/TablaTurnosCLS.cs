using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class TablaTurnosCLS
    {
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public Nullable<System.DateTime> Fin { get; set; }
        public Nullable<System.DateTime> inicio { get; set; }
        public string Estado { get; set; }
        public string NombrePaciente { get; set; }
    }
}