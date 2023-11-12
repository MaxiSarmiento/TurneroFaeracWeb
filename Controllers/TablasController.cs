using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;
using System.Security.Cryptography;
using FaeracT.Controllers;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using System.Data.Entity;
using TurneroFaeracWeb.Datos;
using TurneroFaeracWeb.Models;

namespace FaeracT.Controllers
{
    public class TablasController : Controller
    {


        public ActionResult TablaEmpleados()
        {
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                listaUsuario = (from Usuarios in db.Usuarios
                                select new UsuarioCLS
                                {
                                    IdUser = Usuarios.IdUser,
                                    Usuario = Usuarios.Usuario,
                                    Contraseña = Usuarios.Contraseña,
                                    IdTipo = Usuarios.IdTipo
                                }).ToList();
            }
            return View(listaUsuario);
        }
        public ActionResult TablaTurnosUserDoc()
        {
            return View();
        }
        public ActionResult TablaEstudiosUser()
        {
            return View();
        }
        public ActionResult TablaTurnosAdmin()
        {
            return View();
        }
        public ActionResult ListarAnalisisUsr()
        {
            List<AnalisisPacientesCLS> listaAnalisis = ObtenerListaAnalisis();
            List<PacientesCLS> listaPacientes = ObtenerListaPacientes();

            // Combinar datos de ambas listas en una lista de TablaViewModel
            List<TablaAnalisisCLS> listaCombinada = listaAnalisis
                .Join(listaPacientes,
                    a => a.IdPaciente,
                    p => p.IdPaciente,
                    (a, p) => new TablaAnalisisCLS
                    {
                        NombrePaciente = p.NombrePac,
                        LinkAnalisis = a.LinkAnalisis,
                        FechaAnalisis = a.FechaAnalisis
                    })
                .ToList();

            return View(listaCombinada);
        }

        private List<PacientesCLS> ObtenerListaPacientes()
        {
            List<PacientesCLS> listaPacientes = new List<PacientesCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                listaPacientes = (from Pacientes in db.Pacientes
                                  select new PacientesCLS
                                  {
                                      IdPaciente = Pacientes.IdPaciente,
                                      NombrePac = Pacientes.NombrePac


                                  }).ToList();
                return listaPacientes;
            }



        }

        private List<AnalisisPacientesCLS> ObtenerListaAnalisis()
        {
            List<AnalisisPacientesCLS> listaAnalisis = new List<AnalisisPacientesCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                listaAnalisis = (from ia in db.IndiceAnalisis
                                 join p in db.Pacientes on ia.IdPaciente equals p.IdPaciente
                                 join u in db.Usuarios on p.IdUser equals u.IdUser
                                 where u.IdUser == 3 // Cambia 3 al valor del IdUser que desees consultar
                                 select new AnalisisPacientesCLS
                                 {
                                     FechaAnalisis = ia.FechaAnalisis,
                                     NombrePac = p.NombrePac,
                                     ApePac = p.ApePac,
                                     LinkAnalisis = ia.LinkAnalisis
                                 }).ToList();

                return listaAnalisis;
            }
        }


        public ActionResult TablaTurnosUserDoc1()
        {
            List<PacientesCLS> pacientes = ObtenerListaPacientes(); // Obtén tus datos de pacientes
            List<AnalisisPacientesCLS> analisis = ObtenerListaAnalisis(); // Obtén tus datos de análisis

            // Puedes combinar los datos como sea necesario y pasarlos a la vista parcial
            return PartialView("_TablaTurnosUserDoc", pacientes);
        }


        public ActionResult Filtrar(UsuarioCLS oUsuarioCLS)
        {
            string nombreUsuario = oUsuarioCLS.NombreFiltro;
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                if (nombreUsuario == null)
                {
                    listaUsuario = (from Usuario in db.Usuarios
                                    select new UsuarioCLS
                                    {
                                        IdUser = Usuario.IdUser,
                                        Usuario = Usuario.Usuario,
                                        Contraseña = Usuario.Contraseña,
                                        IdTipo = Usuario.IdTipo
                                    }).ToList();
                }
                else
                {
                    listaUsuario = (from Usuario in db.Usuarios
                                    where Usuario.Usuario.Contains(nombreUsuario)
                                    select new UsuarioCLS
                                    {
                                        IdUser = Usuario.IdUser,
                                        Usuario = Usuario.Usuario,
                                        Contraseña = Usuario.Contraseña,
                                        IdTipo = Usuario.IdTipo
                                    }).ToList();
                }
            }
            return PartialView("_TablaEmpleados", listaUsuario);
        }

        public int Guardar(UsuarioCLS oUsuarioCLS, int titulo)
        {
            int rpta = 0;
            using (var db = new TurneroFaeracEntities())
            {
                if (titulo == 1)
                {
                    Usuarios oUsuario = new Usuarios
                    {
                        IdUser = oUsuarioCLS.IdUser,
                        Usuario = oUsuarioCLS.Usuario,
                        Contraseña = oUsuarioCLS.Contraseña,
                        IdTipo = oUsuarioCLS.TipoUsuario
                    };
                    db.Usuarios.Add(oUsuario);
                    rpta = db.SaveChanges();
                }
            }
            return rpta;
        }

        public string Eliminar(UsuarioCLS oUsuarioCLS)
        {
            string rpta = "";
            try
            {
                int id = oUsuarioCLS.IdUser;
                using (var db = new TurneroFaeracEntities())
                {
                    Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser == id).First();
                    db.Usuarios.Remove(oUsuario);
                    rpta = db.SaveChanges().ToString();
                }
            }
            catch (Exception)
            {
                rpta = "";
            }
            return rpta;
        }

        public string Guardar2(UsuarioCLS oUsuarioCLS, int titulo)
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class ='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var db = new TurneroFaeracEntities())
                    {
                        if (titulo == -1)
                        {
                            Usuarios oUsuario = new Usuarios
                            {
                                IdUser = oUsuarioCLS.IdUser,
                                Usuario = oUsuarioCLS.Usuario,
                                Contraseña = oUsuarioCLS.Contraseña,
                                IdTipo = oUsuarioCLS.IdTipo
                            };
                            db.Usuarios.Add(oUsuario);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser == titulo).First();
                            oUsuario.Usuario = oUsuarioCLS.Usuario;
                            oUsuario.Contraseña = oUsuarioCLS.Contraseña;
                            oUsuario.IdTipo = oUsuarioCLS.IdTipo;
                            rpta = db.SaveChanges().ToString();
                        }
                    }
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
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            using (var db = new TurneroFaeracEntities())
            {
                Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser == titulo).First();
                oUsuarioCLS.Usuario = oUsuario.Usuario;
                oUsuarioCLS.Contraseña = oUsuario.Contraseña;
                oUsuarioCLS.IdTipo = oUsuario.IdTipo;
            }
            return Json(oUsuarioCLS, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public static List<SelectListItem> GetTipoUsuarioList()
        {
            List<SelectListItem> tipoUsuarioList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Administrador" },
        new SelectListItem { Value = "2", Text = "Jefe de Personal" },
        new SelectListItem { Value = "3", Text = "EmpleadoBOX" },
        new SelectListItem { Value = "4", Text = "Doctor" }
    };

            return tipoUsuarioList;
        }
    }
}
