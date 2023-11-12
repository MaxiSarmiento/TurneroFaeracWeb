using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class IndiceAnalisisCLS
    {
        public int IdAnalisis { get; set; }
        public int IdPaciente { get; set; }

        public string LinkAnalisis { get; set; }
        public DateTime FechaAnalisis { get; set; }

    }
}