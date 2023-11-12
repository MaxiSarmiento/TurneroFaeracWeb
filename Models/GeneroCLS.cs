using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurneroFaeracWeb.Models
{
    public class GeneroCLS
    {
        public int IdGenero { get; set; }

        [Display(Name = "Genero")]
        public string Descripcion { get; set; }
        public int Habilitado { get; set; }
    }
}