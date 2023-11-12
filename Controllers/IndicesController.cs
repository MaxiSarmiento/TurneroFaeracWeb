using TurneroFaeracWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace FaeracT.Controllers
{
    public class IndicesController : Controller
    {
        public IndicesController()
        {
            // Constructor (si tienes lógica específica aquí).
        }

        public ActionResult IndexRecepcion()
        {
            return View();
        }

        public ActionResult IndexTurnos()
        {
            // Obtener el IdPaciente de la sesión (asegúrate de que esto esté configurado en tu aplicación).

            listarComboRol();
            llenarEspecializacion();

            List<TurnosCLS> listaTurnos = new List<TurnosCLS>();
            using (var bd = new TurneroFaeracEntities())
            {
                listaTurnos = (from turnos in bd.Turnos
                               join doctores in bd.Doctores
                               on turnos.IdDoc equals
                               doctores.IdDoc
                               join Pacientes in bd.Pacientes
                               on turnos.IdPaciente equals
                               Pacientes.IdPaciente
                               where turnos.TurnoRealizado == false
                               select new TurnosCLS
                               {
                                   IdPaciente = turnos.IdPaciente,
                                   IdDoc = turnos.IdDoc,
                                   Fecha = turnos.Fecha,
                                   Hora = turnos.Hora,

                               }).ToList();
            }
            return View(listaTurnos);

        }

        public ActionResult IndexDoctores()
        {
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult PanelUser()
        {
            return View();
        }
        List<SelectListItem> listaEspecializacion;
        private void llenarEspecializacion()
        {
            using (var db = new TurneroFaeracEntities())
            {
                listaEspecializacion = (from Especializaciones in db.Especializaciones

                                        select new SelectListItem
                                        {
                                            Text = Especializaciones.Descripcion,
                                            Value = Especializaciones.IDEspecializacion.ToString()
                                        }).ToList();

                listaEspecializacion.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });

            }
        }
        List<SelectListItem> listaDoctores;
        private void DocsDisponibles()
        {
        }

        public ActionResult Filtrar(DoctoresCLS oDoctoresCLS)
        {
            string nombreDoc = oDoctoresCLS.nombreFiltro;
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();
            using (var db = new TurneroFaeracEntities())

                if (nombreDoc == null)
                {
                    listaDocs = (from Doctores in db.Doctores
                                 select new DoctoresCLS

                                 {
                                     IdDoc = Doctores.IdDoc,
                                     NombreDoc = Doctores.NombreDoc,
                                     ApeDoc = Doctores.ApeDoc

                                 }).ToList();

                }
                else
                {
                    listaDocs = (from Doctores in db.Doctores
                                 where Doctores.NombreDoc.Contains(nombreDoc)
                                 select new DoctoresCLS
                                 {
                                     IdDoc = Doctores.IdDoc,
                                     NombreDoc = Doctores.NombreDoc,
                                     ApeDoc = Doctores.ApeDoc
                                 }).ToList();
                }
            return PartialView("_TablaDocs", listaDocs);
        }
        /* public int Guardar(TurnosCLS oTurnosCLS, int titulo)
         {
             int rpta = 0;
             using (var bd = new TurneroFaeracEntities())
             {
                 if (titulo == 1)
                 {
                     // Crear una instancia de Turnos que se corresponda con la entidad de Entity Framework
                     FaeracT.Models.Turnos oTurnos = new FaeracT.Models.Turnos
                     {
                         IdPaciente = oTurnosCLS.IdPaciente,
                         Fecha = oTurnosCLS.Fecha,
                         Hora = oTurnosCLS.Hora,
                         TurnoRealizado = false,
                     };

                     bd.Turnos.Add(oTurnos);
                     rpta = bd.SaveChanges();

                     if (rpta == 0) rpta = 0;
                 }
             }
             return rpta;
         }

         */

        public string Guardar(TurnosCLS oTurnosCLS, int titulo)
        {
            //Error
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new TurneroFaeracEntities())
                    {
                        int cantidad = 0;
                        //agregar
                        if (titulo == -1)
                        {
                            cantidad = bd.Turnos.Where(p => p.IdTurno == oTurnosCLS.IdTurno
                              && oTurnosCLS.IdTurno == oTurnosCLS.IdTurno).Count();

                            if (cantidad >= 1)
                            {
                                rpta = "-1";
                            }
                            else
                            {
                                Turnos oTurnos = new Turnos();
                                oTurnosCLS.IdTurno = oTurnosCLS.IdTurno;
                                oTurnosCLS.IdPaciente = oTurnosCLS.IdPaciente;
                                oTurnosCLS.Fecha = oTurnosCLS.Fecha;
                                oTurnosCLS.Hora = oTurnosCLS.Hora;
                                oTurnosCLS.TurnoRealizado = false;
                                bd.Turnos.Add(oTurnos);
                                rpta = bd.SaveChanges().ToString();
                                if (rpta == "0") rpta = "";
                            }
                        }
                        else
                        {
                            cantidad = bd.Turnos.Where(p => p.IdTurno == oTurnosCLS.IdTurno
                            && p.IdPaciente == oTurnosCLS.IdPaciente
                            && oTurnosCLS.IdTurno != titulo).Count();
                            if (cantidad >= 1)
                            {
                                rpta = "-1";
                            }
                            else
                            {
                                Turnos oTurnos = bd.Turnos.Where(p => p.IdTurno == titulo).First();
                                oTurnosCLS.IdTurno = oTurnosCLS.IdTurno;
                                oTurnosCLS.IdPaciente = oTurnosCLS.IdPaciente;
                                rpta = bd.SaveChanges().ToString();
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                rpta = "";
            }
            return rpta;
        }
        public void listarComboRol()
        {
            //agregar
            List<SelectListItem> lista;
            using (var bd = new TurneroFaeracEntities())
            {
                lista = (from item in bd.Doctores

                         select new SelectListItem
                         {
                             Text = item.ApeDoc,

                             Value = item.IDEspecializacion.ToString(),
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaEspecializacion = lista;
            }
        }
        public ActionResult AgregarTurno(TurnosCLS oTurnosCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarEspecializacion();
                ViewBag.lista = listaEspecializacion;
                return View(oTurnosCLS);
            }
            else
            {
                using (var db = new TurneroFaeracEntities())
                {
                    // Verificar si el DNI ya existe
                    bool TurnoExistente = db.Turnos.Any(e => e.IdTurno == oTurnosCLS.IdTurno);

                    if (TurnoExistente)
                    {
                        // DNI existente, mostrar mensaje de error
                        ModelState.AddModelError("Turno", "Turno ya registrado");
                        llenarEspecializacion();
                        ViewBag.lista = listaEspecializacion;
                        return View(oTurnosCLS);
                    }
                    else
                    {
                        // El DNI no existe, proceder con el agregado
                        Turnos oTurnos = new Turnos
                        {
                            IdPaciente = oTurnosCLS.IdPaciente,
                            IdDoc = oTurnosCLS.IdDoc,
                            Fecha = oTurnosCLS.Fecha,
                            Hora = oTurnosCLS.Hora
                        };

                        db.Turnos.Add(oTurnos);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public string Eliminar(TurnosCLS oTurnosCLS)
        {
            string rpta = "";
            try
            {
                int id = oTurnosCLS.IdTurno;
                using (var db = new TurneroFaeracEntities())
                {
                    Turnos oTurno = db.Turnos.Where(p => p.IdTurno == id).First();
                    db.Turnos.Remove(oTurno);
                    rpta = db.SaveChanges().ToString();
                }

            }
            catch (Exception)
            {
                rpta = "";
            }
            return rpta;
        }


        public JsonResult RellenarCampos(int titulo)
        {
            EspecializacionesCLS oEspecializacionesCLS = new EspecializacionesCLS();
            using (var db = new TurneroFaeracEntities())
            {
                Especializaciones oEspecializaciones = db.Especializaciones.Where(p => p.IDEspecializacion == titulo).First();
                oEspecializaciones.Descripcion = oEspecializacionesCLS.Descripcion;

            }
            return Json(oEspecializacionesCLS, JsonRequestBehavior.AllowGet);
        }



    }
}
/*   private List<HorarioPredefinido> HorariosTurnos()
   {
       using (var context = new TurneroFaeracEntities())
       {
           var horarios = context.HorariosPredefinidos.ToList();
           return horarios;
       }
   }*/



