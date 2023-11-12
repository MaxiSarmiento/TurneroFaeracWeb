using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    public class DoctoresCLS
    {

        public int IdDoc { get; set; }
        public int IdUser { get; set; }
        public Nullable<int> idConsultorio { get; set; }
        public string NombreDoc { get; set; }
        public string ApeDoc { get; set; }
        public Nullable<int> IDEspecializacion { get; set; }

        public virtual Usuarios Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndiceHorarios> IndiceHorarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnos> Turnos { get; set; }
        public string nombreFiltro { get; set; }
    }
}