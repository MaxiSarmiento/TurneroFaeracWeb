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
    
    public partial class HorariosDisponibles
    {
        public int ID { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }
        public bool Disponible { get; set; }
        public Nullable<int> IdPaciente { get; set; }
    
        public virtual Pacientes Pacientes { get; set; }
    }
}
