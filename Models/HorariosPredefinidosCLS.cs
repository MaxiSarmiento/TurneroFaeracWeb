using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Models
{
    [Table("HorariosPredefinidos")]
    public class HorarioPredefinido
    {
        [Key]
        public int ID { get; set; }
        public TimeSpan Hora { get; set; }
    }
}