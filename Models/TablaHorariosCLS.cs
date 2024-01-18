
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TurneroFaeracWeb.Models
{
    public class TablaHorariosCLS
    {
        public int IdHorario { get; set; }
        public Nullable<System.TimeSpan> IniHorario { get; set; }
        public Nullable<System.TimeSpan> FinHorario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TurnosMod> TurnosMod { get; set; }
    }
}