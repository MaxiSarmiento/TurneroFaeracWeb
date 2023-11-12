using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurneroFaeracWeb.Models
{
    public class UsuarioCLS
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID:")]
        public int IdUser { get; set; }

        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Display(Name = "Contraseña:")]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Display(Name = "Tipo de Usuario:")] // Add a display name for TipoUsuario
        public int TipoUsuario { get; set; } // Property for TipoUsuario


        [Display(Name = "Correo Electrónico:")]
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [StringLength(50)]
        public string Correo { get; set; }


        public string Token { get; set; }

        public string NombreFiltro { get; set; }
        // List<UsuarioCLS> Usuarios { get; set; } = new List<UsuarioCLS>();

        public int IdTipo { get; set; }

        public string NumeroContacto { get; set; }
        public string Descripcion { get; set; }

        public int IdGenero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctores> Doctores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pacientes> Pacientes { get; set; }
        public virtual TipoUsuarios TipoUsuarios { get; set; }

    }
}