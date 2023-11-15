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
        public GeneroUsuario Generos { get; set; }

        public virtual ICollection<IndiceGenero> ListarGenero { get; set; } = new List<IndiceGenero>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctores> Doctores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pacientes> Pacientes { get; set; }
        // Enumeración para representar los géneros de usuario
        public enum GeneroUsuario
        {
            Masculino = 1,
            Femenino = 2,
            Otro = 3
        }



        public string nombreFiltro { get; set; }


        //Propiedades de Pacientes
        public int IdPaciente { get; set; }
        public string NombrePac { get; set; }
        public string ApePac { get; set; }
        public string DNIPac { get; set; }
        public Nullable<int> EdadPac { get; set; }

      
        public string TipoDNI { get; set; }



        //Propiedades de Doctores

        public int IdDoc { get; set; }
        public Nullable<int> idConsultorio { get; set; }
        public string NombreDoc { get; set; }
        public string ApeDoc { get; set; }
        public Nullable<int> IDEspecializacion { get; set; }

        public virtual Usuarios Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndiceHorarios> IndiceHorarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnos> Turnos { get; set; }
   


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorariosDisponibles> HorariosDisponibles { get; set; }
      

    }
}