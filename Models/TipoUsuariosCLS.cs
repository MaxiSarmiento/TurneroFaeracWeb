using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurneroFaeracWeb.Models
{
    public class TipoUsuariosCLS
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID:")]
        public int Id { get; set; }
        public int TipoUsuario { get; set; }
        public string Descripcion { get; set; }
    }
}