using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class TablaTurnosCLS
    {
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fin { get; set; }
        public DateTime inicio { get; set; }
        public string Estado { get; set; }
        [Display(Name ="Paciente")]
        public string NombrePaciente { get; set; }
        public int IdDoc { get; set; }
        
        public string NombreDoc { get; set; }
        public string ApeDoc { get; set; }
        public int IdDia { get; set; }
        [Display(Name = "Dia")]
        public string NombreDia { get; set; }
        public string Fecha { get; set; }
        [Range(minimum: 0, maximum: 85)]
        public int EdadPac { get; set; }

        public virtual string NombreEspecializacion { get; set; }
        public virtual Doctores Doctores { get; set; }
    }
}