using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TurneroFaeracWeb.Models
{
    public class TurnosDoctores
    {
        
            public Doctores Doctores { get; set; }
            public Pacientes Pacientes { get; set; }
        
           public Turnos  Turnos { get; set; }  
    }
}