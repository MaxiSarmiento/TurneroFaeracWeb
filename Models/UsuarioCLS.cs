using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using TurneroFaeracWeb.Datos;

namespace TurneroFaeracWeb.Models
{
    public class UsuarioCLS
    {
        public int IdUser { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int IdTipo { get; set; }
        public string Correo { get; set; }
        public string NumeroContacto { get; set; }
        public string Descripcion { get; set; }
        public string Token { get; set; }
        public Nullable<int> Genero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctores> Doctores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pacientes> Pacientes { get; set; }
     
       

        public string nombreFiltro { get; set; }

    }
}