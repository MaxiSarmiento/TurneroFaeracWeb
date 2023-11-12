using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class AnalisisPacientesCLS
    {
        public int IdUser { get; set; }
        public int IdPaciente { get; set; }
        public DateTime FechaAnalisis { get; set; }
        public string NombrePac { get; set; }
        public string ApePac { get; set; }
        public string LinkAnalisis { get; set; }
    }


}