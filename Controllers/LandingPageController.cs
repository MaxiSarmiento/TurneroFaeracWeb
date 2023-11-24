using TurneroFaeracWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FaeracT.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPage
        public ActionResult Home()
        {
            ListarEsp();
            return View();
        }
        public ActionResult _ListarDoctores()
        {
            return View();
        }
        public ActionResult _HorariosDisponibles()
        {
            return View();
        }
        private TurneroFaeracEntities db = new TurneroFaeracEntities();

        public ActionResult ListarEsp()
        {
            // Obtén la lista de especializaciones desde tu modelo (puedes consultar tu base de datos aquí)
            var especializaciones = new List<EspecializacionesCLS>
    {
          new EspecializacionesCLS { IDEspecializacion = 2, Descripcion = "Generalista" },
        new EspecializacionesCLS { IDEspecializacion = 3, Descripcion = "Gastroenterologia" },
        new EspecializacionesCLS { IDEspecializacion = 4, Descripcion = "Neurologia" },
        new EspecializacionesCLS { IDEspecializacion = 5, Descripcion = "Cirujano" },
        new EspecializacionesCLS { IDEspecializacion = 6, Descripcion = "Cuidados Paleativos" },
        new EspecializacionesCLS { IDEspecializacion = 7, Descripcion = "Pediatra" },
        new EspecializacionesCLS { IDEspecializacion = 8, Descripcion = "Ginecologo" },
        new EspecializacionesCLS { IDEspecializacion = 9, Descripcion = "Otorrinolaringologo" },
        new EspecializacionesCLS { IDEspecializacion = 10, Descripcion = "Oncologo" },
        new EspecializacionesCLS { IDEspecializacion = 11, Descripcion = "Obstetra" },
        new EspecializacionesCLS { IDEspecializacion = 12, Descripcion = "Proctologo" },
        new EspecializacionesCLS { IDEspecializacion = 1, Descripcion = "Nutricionista" },
     
        
        // Agrega más especializaciones según sea necesario
    };

            // Pasa la lista de especializaciones a la vista
            ViewBag.Especializaciones = new SelectList(especializaciones, "IDEspecializacion", "Descripcion");

            return View();
        }

        [HttpGet]
        public ActionResult BuscarDocs(int idEspecializacion, string nombreFiltro)
        {
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();

            using (var db = new TurneroFaeracEntities())
            {
                if (string.IsNullOrWhiteSpace(nombreFiltro))
                {
                    listaDocs = (from Doctores in db.Doctores
                                 where Doctores.IDEspecializacion == idEspecializacion
                                 select new DoctoresCLS
                                 {
                                     IDEspecializacion = Doctores.IDEspecializacion,
                                     ApeDoc = Doctores.ApeDoc,
                                     NombreDoc = Doctores.NombreDoc
                                 }).ToList();
                }
                else
                {
                    listaDocs = (from Doctores in db.Doctores
                                 where Doctores.IDEspecializacion == idEspecializacion && Doctores.ApeDoc.Contains(nombreFiltro)
                                 select new DoctoresCLS
                                 {
                                     IDEspecializacion = Doctores.IDEspecializacion,
                                     ApeDoc = Doctores.ApeDoc,
                                     NombreDoc = Doctores.NombreDoc 

                                 }).ToList();
                }
            }

            return PartialView("_ListaDoctores", listaDocs);
        }

        [HttpGet]
        public ActionResult ObtenerDoctores(int idEspecializacion)
        {
            var doctores = db.Doctores.Where(d => d.IDEspecializacion == idEspecializacion)
                                     .Select(d => new SelectListItem
                                     {
                                         Value = d.IdDoc.ToString(),
                                         Text = d.ApeDoc
                                     })
                                     .ToList();

            return Json(doctores, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObtenerHorariosDisponibles(string fecha, string hora)
        {
            // Aquí realiza la lógica para obtener los horarios disponibles según la fecha y hora seleccionadas
            // Puedes consultar tu base de datos u otro origen de datos y devolver los horarios disponibles

            // Por ejemplo, supongamos que tienes una lista de horarios disponibles
            var horariosDisponibles = new List<string>
    {
        "08:00 AM",
        "09:00 AM",
        "10:00 AM",
        "11:00 AM",
        "12:00 AM",
        "13:00 AM",
        "16:00 AM",
        "17:00 AM",
        "18:00 AM",
        "19:00 AM",
        "20:00 AM"
        // Agrega más horarios disponibles según tus necesidades
    };

            // Devuelve los horarios disponibles como vista parcial o JSON según tus necesidades
            return PartialView("_HorariosDisponibles", horariosDisponibles);
        }

    }
}