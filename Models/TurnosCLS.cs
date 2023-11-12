using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class TurnosCLS
    {
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public int IdDoc { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public bool? TurnoRealizado { get; set; }
        public string ComentarioDoctor { get; set; }

        public virtual Doctores Doctores { get; set; }
        public virtual Pacientes Pacientes { get; set; }
    }
}