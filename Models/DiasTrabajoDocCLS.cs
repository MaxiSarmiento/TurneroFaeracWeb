using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class DiasTrabajoDocCLS
    {
        public int Id { get; set; }
        public int IdDia { get; set; }
        public int IdDoc { get; set; }

        public virtual DiasSemana DiasSemana { get; set; }
        public virtual Doctores Doctores { get; set; }
    }
}