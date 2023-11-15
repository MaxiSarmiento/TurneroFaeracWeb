using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurneroFaeracWeb.Models
{
    public class PacientesCLS
    {


        public int IdPaciente { get; set; }
        public string NombrePac { get; set; }
        public string ApePac { get; set; }
        public string DNIPac { get; set; }
        public Nullable<int> EdadPac { get; set; }
       
        public int Genero { get; set; }
        public string TipoDNI { get; set; }
        public int IdUser { get; set; }
        
        public string Correo { get; set; }
     
        public string NumeroContacto { get; set; }
      

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorariosDisponibles> HorariosDisponibles { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnos> Turnos { get; set; }

    }
}