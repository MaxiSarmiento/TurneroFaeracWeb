
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TurneroFaeracWeb.Models
{
    public class TurnosModCLS
    {
        public int IdTurno { get; set; }
        public int IdDoc { get; set; }
        public int IdPaciente { get; set; }
        public int IdHorario { get; set; }
        public int Atendido { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string ColorTema { get; set; } // Campo para almacenar información sobre el color del tema
        public int IdDia { get; set; }

        public virtual DiasSemana DiasSemana { get; set; }
        public virtual Doctores Doctores { get; set; }
        public virtual Pacientes Pacientes { get; set; }
        public virtual TablaHorarios TablaHorarios { get; set; }
      
    }
}