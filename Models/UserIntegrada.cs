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
    
    public partial class UserIntegrada
    {
        public int IdUsuario { get; set; }
        public string Nusuario { get; set; }
        public string Password { get; set; }
        public Nullable<int> Especializacion { get; set; }
        public string DNIUsuario { get; set; }
        public string email { get; set; }
        public string Telefono { get; set; }
        public Nullable<int> Genero { get; set; }
        public Nullable<int> Edad { get; set; }
        public int IdTipo { get; set; }
        public string Token { get; set; }
    }
}
