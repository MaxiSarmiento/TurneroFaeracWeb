using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class HorariosDisponiblesCLS
    {
        public int ID { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }
        public bool Disponible { get; set; }
        public Nullable<int> IdPaciente { get; set; }

        public virtual Pacientes Pacientes { get; set; }
    }
}