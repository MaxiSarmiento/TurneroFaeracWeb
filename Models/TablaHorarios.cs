//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TurneroFaeracWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TablaHorarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TablaHorarios()
        {
            this.TurnosMod = new HashSet<TurnosMod>();
        }
    
        public int IdHorario { get; set; }
        public Nullable<System.TimeSpan> IniHorario { get; set; }
        public Nullable<System.TimeSpan> FinHorario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TurnosMod> TurnosMod { get; set; }
    }
}
