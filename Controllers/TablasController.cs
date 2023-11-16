using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurneroFaeracWeb.Controllers;
using System.Text;
using System.Security.Cryptography;
using FaeracT.Controllers;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using System.Data.Entity;
using TurneroFaeracWeb.Datos;
using TurneroFaeracWeb.Models;
using System.Drawing;
using System.Web.UI.WebControls;

namespace FaeracT.Controllers
{
    public class TablasController : Controller
    {


        public ActionResult TablaEmpleados(UsuarioCLS oUsuarioCLS)
        {
            string nombreUSR = oUsuarioCLS.Usuario;
            llenarGenero();
            GetTipoUsuarioList();
            GetGeneroUsuarioList();
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var db = new TurneroFaeracEntities())

            {
                if (oUsuarioCLS.Usuario == null) {
                    listaUsuario = (from Usuario in db.Usuarios
                                    select new UsuarioCLS
                                    {
                                        IdUser = Usuario.IdUser,
                                        Usuario = Usuario.Usuario,
                                        Contraseña = Usuario.Contraseña,
                                        IdTipo = Usuario.IdTipo,
                                        Genero = Usuario.Genero,
                                        NumeroContacto = Usuario.NumeroContacto


                                    }).ToList();
                    Session["lista"] = listaUsuario;
                }
                else
                {
                    listaUsuario = (from Usuario in db.Usuarios
                                    where Usuario.IdTipo != 4
                                    select new UsuarioCLS
                                    {
                                        IdUser = Usuario.IdUser,
                                        Usuario = Usuario.Usuario,
                                        Contraseña = Usuario.Contraseña,
                                        IdTipo = Usuario.IdTipo,
                                        Genero = Usuario.Genero,
                                        NumeroContacto = Usuario.NumeroContacto
                                    }).ToList();

                    Session["lista"] = listaUsuario;
                }

            }
            return View(listaUsuario);
        }
        public ActionResult TablaDoctores(DoctoresCLS oDoctoresCLS)
        {
            llenarEspecializacion();
            ViewBag.Lista = listaEspecializacion;
            string apeDoc = oDoctoresCLS.ApeDoc;
          
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();
            using (var db = new TurneroFaeracEntities())

            {
                if (oDoctoresCLS.ApeDoc == null)
                {
                    listaDocs = (from doctores in db.Doctores
                                    select new DoctoresCLS
                                    {
                                        IdDoc = doctores.IdDoc,
                                        IdUser = doctores.IdUser,
                                        ApeDoc = doctores.ApeDoc,
                                        NombreDoc = doctores.NombreDoc,
                                        IDEspecializacion = doctores.IDEspecializacion,



                                    }).ToList();
                    Session["lista"] = listaDocs;
                }
                else
                {
                    listaDocs = (from doctores in db.Doctores
                                    
                                    select new DoctoresCLS
                                    {
                                        IdDoc = doctores.IdDoc,
                                        IdUser = doctores.IdUser,
                                        ApeDoc = doctores.ApeDoc,
                                        NombreDoc = doctores.NombreDoc,
                                        IDEspecializacion = doctores.IDEspecializacion,
                                    }).ToList();

                    Session["lista"] = listaDocs;
                }

            }
            return View(listaDocs);
        }
        public ActionResult TablaPacientes(PacientesCLS oPacientesCLS)
        {
            llenarEspecializacion();
            ViewBag.Lista = listaEspecializacion;
            string dnipac = oPacientesCLS.NombrePac;

            List<PacientesCLS> listaPacs = new List<PacientesCLS>();
            using (var db = new TurneroFaeracEntities())

            {
                if (oPacientesCLS.NombrePac == null)
                {
                    listaPacs = (from pacientes in db.Pacientes
                                 select new PacientesCLS
                                 {
                                     IdPaciente= pacientes.IdPaciente,
                                     NombrePac = pacientes.NombrePac,
                                     ApePac = pacientes.ApePac,
                                     DNIPac = pacientes.DNIPac,
                                     TipoDNI = pacientes.TipoDNI,
                                     EdadPac = pacientes.EdadPac,
                                     Genero = pacientes.Genero,
                                     IdUser = pacientes.IdUser,
                                     Correo = pacientes.Correo,
                                     NumeroContacto = pacientes.NumeroContacto



                                 }).ToList();
                    Session["lista"] = listaPacs;
                }
                else
                {
                    listaPacs = (from pacientes in db.Pacientes

                                 select new PacientesCLS
                                 {
                                     IdPaciente = pacientes.IdPaciente,
                                     NombrePac = pacientes.NombrePac,
                                     ApePac = pacientes.ApePac,
                                     DNIPac = pacientes.DNIPac,
                                     TipoDNI = pacientes.TipoDNI,
                                     EdadPac = (int)pacientes.EdadPac,
                                     Genero = (int)pacientes.Genero,
                                     IdUser = (int)pacientes.IdUser,
                                     Correo = pacientes.Correo,
                                     NumeroContacto = pacientes.NumeroContacto

                                 }).ToList();

                    Session["lista"] = listaPacs;
                }

            }
            return View(listaPacs);
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

        [HttpGet]
        public ActionResult Agregar()
        {
            if (Session["UserID"] == null)
            {

                return RedirectToAction("Login", "Login");
            }
            else
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View();
            }

        }
        public ActionResult Agregar(UsuarioCLS oUsuarioCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oUsuarioCLS);
            }
            else
            {
                using (var db = new TurneroFaeracEntities())
                {
                    // Verificar si el Usuario ya existe
                    bool dniExistente = db.Usuarios.Any(e => e.Usuario == oUsuarioCLS.Usuario);

                    if (dniExistente)
                    {
                        // Usuario Existente, mostrar mensaje de error
                        ModelState.AddModelError("Usuario", "Usuario ya existente");
                        llenarGenero();
                        ViewBag.lista = listaGenero;
                        llenarEspecializacion();
                        ViewBag.Lista = listaEspecializacion;
                        return View(oUsuarioCLS);
                    }
                    else
                    {
                        // El Usuario no existe, proceder con el agregado
                        Usuarios oUsuario = new Usuarios();
                        oUsuario.Usuario = oUsuario.Usuario;
                        oUsuario.Contraseña = oUsuario.Contraseña;
                        oUsuario.NumeroContacto = oUsuario.NumeroContacto;
                        oUsuario.Genero = (int)oUsuarioCLS.Genero;
                        oUsuario.IdTipo = (int)oUsuarioCLS.IdTipo;
                        oUsuario.Descripcion = oUsuarioCLS.Descripcion;
                        db.Usuarios.Add(oUsuario);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AgregarD()
        {
            if (Session["UserID"] == null)
            {

                return RedirectToAction("Login", "Login");
            }
            else
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View();
            }

        }
        public ActionResult AgregarD(DoctoresCLS oDoctoresCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oDoctoresCLS);
            }
            else
            {
                using (var db = new TurneroFaeracEntities())
                {
                    // Verificar si el Usuario ya existe
                    bool docExistente = db.Doctores.Any(e => e.IdUser == oDoctoresCLS.IdUser);

                    if (docExistente)
                    {
                        // Usuario Existente, mostrar mensaje de error
                        ModelState.AddModelError("Usuario", "Usuario ya existente");
                        llenarGenero();
                        ViewBag.lista = listaGenero;
                        llenarEspecializacion();
                        ViewBag.Lista = listaEspecializacion;
                        return View(oDoctoresCLS);
                    }
                    else
                    {
                        // El Usuario no existe, proceder con el agregado
                       Doctores oDoctores = new Doctores();
                       
                        oDoctores.IdUser = oDoctoresCLS.IdUser;
                        oDoctores.ApeDoc = oDoctoresCLS.ApeDoc;
                        oDoctores.NombreDoc = oDoctoresCLS.NombreDoc;
                        oDoctores.IDEspecializacion = oDoctoresCLS.IDEspecializacion;
                        db.Doctores.Add(oDoctores);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult AgregarP()
        {
            if (Session["UserID"] == null)
            {

                return RedirectToAction("Login", "Login");
            }
            else
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View();
            }

        }
        public ActionResult AgregarP(PacientesCLS oPacientesCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oPacientesCLS);
            }
            else
            {
                using (var db = new TurneroFaeracEntities())
                {
                    // Verificar si el Usuario ya existe
                    bool pacExistente = db.Pacientes.Any(e => e.IdPaciente == oPacientesCLS.IdPaciente);

                    if (pacExistente)
                    {
                        // Usuario Existente, mostrar mensaje de error
                        ModelState.AddModelError("Paciente", "Paciente ya existente");
                        llenarGenero();
                        ViewBag.lista = listaGenero;
                        llenarEspecializacion();
                        ViewBag.Lista = listaEspecializacion;
                        return View(oPacientesCLS);
                    }
                    else
                    {
                        // El Usuario no existe, proceder con el agregado
                        Pacientes oPacientes = new Pacientes();

                        oPacientes.IdPaciente = oPacientesCLS.IdPaciente;
                        oPacientes.NombrePac = oPacientesCLS.NombrePac;
                        oPacientes.ApePac = oPacientesCLS.ApePac;
                        oPacientes.DNIPac = oPacientesCLS.DNIPac;
                        oPacientes.TipoDNI = oPacientesCLS.TipoDNI;
                        oPacientes.EdadPac = oPacientesCLS.EdadPac;
                        oPacientes.Genero = (int)oPacientesCLS.Genero;
                        oPacientes.IdUser = oPacientesCLS.IdUser;
                        oPacientes.Correo = oPacientesCLS.Correo;
                        oPacientes.NumeroContacto = oPacientesCLS.NumeroContacto;
                        db.Pacientes.Add(oPacientes);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
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
        public ActionResult editar(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {

                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    llenarGenero();
                    ViewBag.lista = listaGenero;
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;

                    UsuarioCLS oUsuarioCLS = new UsuarioCLS();
                    using (var db = new TurneroFaeracEntities())
                    {
                        Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser.Equals(id)).First();
                        oUsuarioCLS.IdUser = oUsuario.IdUser;
                        oUsuarioCLS.Usuario = oUsuario.Usuario;
                        oUsuarioCLS.Genero = (int)oUsuario.Genero;
                        oUsuarioCLS.Descripcion = oUsuario.Descripcion;
                        oUsuarioCLS.Correo = oUsuario.Correo;
                        oUsuarioCLS.NumeroContacto = oUsuario.NumeroContacto;

                    }
                    return View(oUsuarioCLS);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public ActionResult editar(UsuarioCLS oUsuarioCLS)
        {
            int id = oUsuarioCLS.IdUser;

            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oUsuarioCLS);

            }

            using (var db = new TurneroFaeracEntities())
            {
                Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser.Equals(id)).First();
                oUsuarioCLS.IdUser = oUsuario.IdUser;
                oUsuarioCLS.Usuario = oUsuario.Usuario;
                oUsuarioCLS.Genero = (int)oUsuario.Genero;
                oUsuarioCLS.Descripcion = oUsuario.Descripcion;
                oUsuarioCLS.Correo = oUsuario.Correo;
                oUsuarioCLS.NumeroContacto = oUsuario.NumeroContacto;
                db.SaveChanges();

            
        }

    return RedirectToAction("Home");
    }
        public ActionResult editard(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {

                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    llenarGenero();
                    ViewBag.lista = listaGenero;
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;

                    DoctoresCLS oDoctoresCLS = new DoctoresCLS();
                    using (var db = new TurneroFaeracEntities())
                    {
                        Doctores oDoctores = db.Doctores.Where(p => p.IdDoc.Equals(id)).First();
                        oDoctores.IdDoc = oDoctoresCLS.IdDoc;
                        oDoctores.IdUser = oDoctoresCLS.IdUser;
                        oDoctores.ApeDoc = oDoctoresCLS.ApeDoc;
                        oDoctores.NombreDoc = oDoctoresCLS.NombreDoc;
                        oDoctores.IDEspecializacion = oDoctoresCLS.IDEspecializacion;
                        

                    }
                    return View(oDoctoresCLS);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]


        public ActionResult editard(DoctoresCLS oDoctoresCLS)
        {
            int id = oDoctoresCLS.IdDoc;

            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oDoctoresCLS);

            }

            using (var db = new TurneroFaeracEntities())
            {
                Doctores oDoctores = db.Doctores.Where(p => p.IdDoc.Equals(id)).First();
                oDoctores.IdDoc = oDoctoresCLS.IdDoc;
                oDoctores.IdUser = oDoctoresCLS.IdUser;
                oDoctores.ApeDoc = oDoctoresCLS.ApeDoc;
                oDoctores.NombreDoc = oDoctoresCLS.NombreDoc;
                oDoctores.IDEspecializacion = oDoctoresCLS.IDEspecializacion;
                db.SaveChanges();


            }

            return RedirectToAction("Home");
        }
        [HttpGet]
        public ActionResult editarp(int id)
        {
            try
            {
                if (Session["UserID"] == null)
                {

                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    llenarGenero();
                    ViewBag.lista = listaGenero;
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;

                    PacientesCLS oPacientesCLS = new PacientesCLS();
                    using (var db = new TurneroFaeracEntities())
                    {
                        Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();

                        oPacientes.IdPaciente = oPacientesCLS.IdPaciente;
                        oPacientes.NombrePac = oPacientesCLS.NombrePac;
                        oPacientes.ApePac = oPacientesCLS.ApePac;
                        oPacientes.DNIPac = oPacientesCLS.DNIPac;
                        oPacientes.TipoDNI = oPacientesCLS.TipoDNI;
                        oPacientes.EdadPac = oPacientesCLS.EdadPac;
                        oPacientes.Genero = oPacientesCLS.Genero;
                        oPacientes.IdUser = oPacientesCLS.IdUser;
                        oPacientes.Correo = oPacientesCLS.Correo;
                        oPacientes.NumeroContacto = oPacientesCLS.NumeroContacto;


                    }
                    return View(oPacientesCLS);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public ActionResult editarp(PacientesCLS oPacientesCLS)
        {
            int id = oPacientesCLS.IdPaciente;

            if (!ModelState.IsValid)
            {
                llenarGenero();
                ViewBag.lista = listaGenero;
                llenarEspecializacion();
                ViewBag.Lista = listaEspecializacion;
                return View(oPacientesCLS);

            }

            using (var db = new TurneroFaeracEntities())
            {
                Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();

                oPacientes.IdPaciente = oPacientesCLS.IdPaciente;
                oPacientes.NombrePac = oPacientesCLS.NombrePac;
                oPacientes.ApePac = oPacientesCLS.ApePac;
                oPacientes.DNIPac = oPacientesCLS.DNIPac;
                oPacientes.TipoDNI = oPacientesCLS.TipoDNI;
                oPacientes.EdadPac = oPacientesCLS.EdadPac;
                oPacientes.Genero = oPacientesCLS.Genero;
                oPacientes.IdUser = oPacientesCLS.IdUser;
                oPacientes.Correo = oPacientesCLS.Correo;
                oPacientes.NumeroContacto = oPacientesCLS.NumeroContacto;
                db.SaveChanges();


            }

            return RedirectToAction("Home");
        }




        public ActionResult FiltrarEmpleados(UsuarioCLS oUsuariosCLS)
    {
        string nombreUsr = oUsuariosCLS.nombreFiltro;
        List<UsuarioCLS> listaUsr = new List<UsuarioCLS>();
        using (var db = new TurneroFaeracEntities())

                if (nombreUsr == null)
                {
                    listaUsr = (from Usuarios in db.Usuarios
                                 select new UsuarioCLS

                                 {
                                     IdUser = Usuarios.IdUser,
                                     Usuario = Usuarios.Usuario,
                                     Contraseña = Usuarios.Contraseña,
                                     IdTipo = Usuarios.IdTipo,
                                     NumeroContacto = Usuarios.NumeroContacto,
                                     Genero = (int)Usuarios.Genero

                                 }).ToList();

                }
                else
                {
                    listaUsr = (from Usuarios in db.Usuarios
                                 where Usuarios.Usuario.Contains(nombreUsr)
                                 select new UsuarioCLS
                                 {
                                     IdUser = Usuarios.IdUser,
                                     Usuario = Usuarios.Usuario,
                                     Contraseña = Usuarios.Contraseña,
                                     IdTipo = Usuarios.IdTipo,
                                     NumeroContacto = Usuarios.NumeroContacto,
                                     Genero = (int)Usuarios.Genero
                                 }).ToList();
                }
            return PartialView("_TablaEmpleados", listaUsr);
        }


        private List<UsuarioCLS> ObtenerListaUsuarios()
        {
            llenarEspecializacion();
            List<UsuarioCLS> listaUsuarios = new List<UsuarioCLS>();
            using (var db = new TurneroFaeracEntities())
               
            {
                listaUsuarios = (from Usuarios in db.Usuarios
                                  select new UsuarioCLS
                                  {
                                      IdUser = Usuarios.IdUser,
                                      Usuario = Usuarios.Usuario,
                                      Contraseña = Usuarios.Contraseña,
                                      IdTipo = Usuarios.IdTipo,
                                      NumeroContacto = Usuarios.NumeroContacto,
                                      Genero = (int)Usuarios.Genero

                                  }).ToList();
                return listaUsuarios;
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
            string nombreUsuario = oUsuarioCLS.nombreFiltro;
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
        public ActionResult FiltrarD(DoctoresCLS oDoctoresCLS)
        {
            string nombreDoc = oDoctoresCLS.nombreFiltro;
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                if (nombreDoc == null)
                {
                    listaDocs = (from doctores in db.Doctores
                                    select new DoctoresCLS
                                    {
                                        IdDoc = doctores.IdDoc,
                                        ApeDoc = doctores.ApeDoc,
                                        IdUser = doctores.IdUser,
                                        NombreDoc = doctores.NombreDoc,
                                        IDEspecializacion = doctores.IDEspecializacion,
                                    }).ToList();
                }
                else
                {
                    listaDocs = (from doctores in db.Doctores
                                    where doctores.NombreDoc.Contains(nombreDoc)
                                    select new DoctoresCLS
                                    {
                                        IdDoc = doctores.IdDoc,
                                        IdUser = doctores.IdUser,
                                        ApeDoc = doctores.ApeDoc,
                                        NombreDoc = doctores.NombreDoc,
                                        IDEspecializacion = doctores.IDEspecializacion,
                                    }).ToList();
                }
            }
            return PartialView("_TablaDoctores", listaDocs);
        }
        public ActionResult FiltrarP(PacientesCLS oPacientesCLS)
        {
            string nombrePac = oPacientesCLS.nombreFiltro;
            List<PacientesCLS> listaPacs = new List<PacientesCLS>();
            using (var db = new TurneroFaeracEntities())
            {
                if (nombrePac == null)
                {
                    listaPacs = (from pacientes in db.Pacientes
                                 select new PacientesCLS
                                 {
                                     IdPaciente = pacientes.IdPaciente,
                                     NombrePac = pacientes.NombrePac,
                                     ApePac = pacientes.ApePac,
                                     DNIPac = pacientes.DNIPac,
                                     TipoDNI = pacientes.TipoDNI,
                                     EdadPac = (int)pacientes.EdadPac,
                                     Genero = (int)pacientes.Genero,
                                     IdUser = (int)pacientes.IdUser,
                                     Correo = pacientes.Correo,
                                     NumeroContacto = pacientes.NumeroContacto
                                 }).ToList();
                }
                else
                {
                    listaPacs = (from pacientes in db.Pacientes
                                 where pacientes.NombrePac.Contains(nombrePac)
                                 select new PacientesCLS
                                 {
                                     IdPaciente = pacientes.IdPaciente,
                                     NombrePac = pacientes.NombrePac,
                                     ApePac = pacientes.ApePac,
                                     DNIPac = pacientes.DNIPac,
                                     TipoDNI = pacientes.TipoDNI,
                                     EdadPac = (int)pacientes.EdadPac,
                                     Genero = (int)pacientes.Genero,
                                     IdUser = (int)pacientes.IdUser,
                                     Correo = pacientes.Correo,
                                     NumeroContacto = pacientes.NumeroContacto
                                 }).ToList();
                }
            }
            return PartialView("_TablaPacientes", listaPacs);
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
                        IdTipo = oUsuarioCLS.IdTipo
                    };
                    db.Usuarios.Add(oUsuario);
                    rpta = db.SaveChanges();
                }
            }
            return rpta;
        }
        public int GuardarD(DoctoresCLS oDoctoresCLS, int titulo)
        {
            int rpta = 0;
            using (var db = new TurneroFaeracEntities())
            {
                if (titulo == 1)
                {
                    Doctores oDoctores = new Doctores
                    {
                        IdDoc = oDoctoresCLS.IdDoc,
                        IdUser = oDoctoresCLS.IdUser,
                        ApeDoc = oDoctoresCLS.ApeDoc,
                        NombreDoc = oDoctoresCLS.NombreDoc,
                        IDEspecializacion = oDoctoresCLS.IDEspecializacion,
                    };
                    db.Doctores.Add(oDoctores);
                    rpta = db.SaveChanges();
                }
            }
            return rpta;
        }
        public int GuardarP(PacientesCLS oPacientesCLS, int titulo)
        {
            int rpta = 0;
            using (var db = new TurneroFaeracEntities())
            {
                if (titulo == 1)
                {
                    Pacientes oPacientes = new Pacientes
                    {

                     IdPaciente = oPacientesCLS.IdPaciente,
                     NombrePac = oPacientesCLS.NombrePac,
                     ApePac = oPacientesCLS.ApePac,
                     DNIPac = oPacientesCLS.DNIPac,
                     TipoDNI = oPacientesCLS.TipoDNI,
                     EdadPac = oPacientesCLS.EdadPac,
                     Genero = (int)oPacientesCLS.Genero,
                     IdUser = oPacientesCLS.IdUser,
                     Correo = oPacientesCLS.Correo,
                     NumeroContacto = oPacientesCLS.NumeroContacto,
                };
                    db.Pacientes.Add(oPacientes);
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
        public string Eliminard(DoctoresCLS oDoctoresCLS)
        {
            string rpta = "";
            try
            {
                int id = oDoctoresCLS.IdDoc;
                using (var db = new TurneroFaeracEntities())
                {
                    Doctores oDoctores = db.Doctores.Where(p => p.IdDoc == id).First();
                    db.Doctores.Remove(oDoctores);
                    rpta = db.SaveChanges().ToString();
                }
            }
            catch (Exception)
            {
                rpta = "";
            }
            return rpta;
        }
        public string EliminarP(PacientesCLS oPacientesCLS)
        {
            string rpta = "";
            try
            {
                int id = oPacientesCLS.IdPaciente;
                using (var db = new TurneroFaeracEntities())
                {
                    Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente == id).First();
                    db.Pacientes.Remove(oPacientes);
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
                    // Manejo de errores de validación del modelo
                    // Puedes agregar el código necesario para manejar esto según tus requisitos
                }
                else
                {
                    using (var db = new TurneroFaeracEntities())
                    {
                        if (titulo == -1)
                        {
                            // Agregar nuevo usuario
                            Usuarios oUsuario = new Usuarios
                            {
                                Usuario = oUsuarioCLS.Usuario,
                                NumeroContacto = oUsuarioCLS.NumeroContacto,
                                IdTipo = oUsuarioCLS.IdTipo,
                                Genero = oUsuarioCLS.Genero // Convertir el índice a la enumeración
                            };

                            db.Usuarios.Add(oUsuario);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            // Actualizar usuario existente
                            Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser == titulo).FirstOrDefault();

                            if (oUsuario != null)
                            {
                                oUsuario.Usuario = oUsuarioCLS.Usuario;
                                oUsuario.NumeroContacto = oUsuarioCLS.NumeroContacto;
                                oUsuario.IdTipo = oUsuarioCLS.IdTipo;
                                oUsuario.Genero = oUsuarioCLS.Genero; // Convertir el índice a la enumeración
                                rpta = db.SaveChanges().ToString();
                            }
                            else
                            {
                                // Manejar el caso en el que el usuario no se encuentra
                                rpta = "Usuario no encontrado";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "Error en otra cosa: " + ex.Message;
            }

            return rpta;
        }

        public string GuardarDocs(DoctoresCLS oDoctoresCLS, int titulo)
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    // Manejo de errores de validación del modelo
                    // Puedes agregar el código necesario para manejar esto según tus requisitos
                }
                else
                {
                    using (var db = new TurneroFaeracEntities())
                    {
                        if (titulo == -1)
                        {
                            // Agregar nuevo usuario
                            Doctores oDoctores = new Doctores
                            {
                            
                            IdUser = oDoctoresCLS.IdUser,
                            ApeDoc = oDoctoresCLS.ApeDoc,
                            NombreDoc = oDoctoresCLS.NombreDoc,
                            IDEspecializacion = oDoctoresCLS.IDEspecializacion,
                            };

                            db.Doctores.Add(oDoctores);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            // Actualizar usuario existente
                            Doctores oDoctores = db.Doctores.Where(p => p.IdDoc == titulo).FirstOrDefault();

                            if (oDoctores != null)
                            {
                               
                                oDoctores.IdUser = oDoctoresCLS.IdUser;
                                oDoctores.ApeDoc = oDoctoresCLS.ApeDoc;
                                oDoctores.NombreDoc = oDoctoresCLS.NombreDoc;
                                oDoctores.IDEspecializacion = oDoctoresCLS.IDEspecializacion;
                                rpta = db.SaveChanges().ToString();
                            }
                            else
                            {
                                // Manejar el caso en el que el usuario no se encuentra
                                rpta = "Doctor no encontrado";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "Error en otra cosa: " + ex.Message;
            }

            return rpta;
        }

        public string GuardarPacs(PacientesCLS oPacientesCLS, int titulo)
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    // Manejo de errores de validación del modelo
                    // Puedes agregar el código necesario para manejar esto según tus requisitos
                }
                else
                {
                    using (var db = new TurneroFaeracEntities())
                    {
                        if (titulo == -1)
                        {
                            // Agregar nuevo usuario
                            Pacientes oPacientes = new Pacientes
                            {

                                
                                NombrePac = oPacientesCLS.NombrePac,
                                ApePac = oPacientesCLS.ApePac,
                                DNIPac = oPacientesCLS.DNIPac,
                                TipoDNI = oPacientesCLS.TipoDNI,
                                EdadPac = oPacientesCLS.EdadPac,
                                Genero = oPacientesCLS.Genero,
                                IdUser = oPacientesCLS.IdUser,
                                Correo = oPacientesCLS.Correo,
                                NumeroContacto = oPacientesCLS.NumeroContacto,
                            };

                            db.Pacientes.Add(oPacientes);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            // Actualizar usuario existente
                            Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente == titulo).FirstOrDefault();

                            if (oPacientes != null)
                            {

                              
                                oPacientes.NombrePac = oPacientesCLS.NombrePac;
                                oPacientes.ApePac = oPacientesCLS.ApePac;
                                oPacientes.DNIPac = oPacientesCLS.DNIPac;
                                oPacientes.TipoDNI = oPacientesCLS.TipoDNI;
                                oPacientes.EdadPac = oPacientesCLS.EdadPac;
                                oPacientes.Genero = oPacientesCLS.Genero;
                                oPacientes.IdUser = oPacientesCLS.IdUser;
                                oPacientes.Correo = oPacientesCLS.Correo;
                                oPacientes.NumeroContacto = oPacientesCLS.NumeroContacto;

                                rpta = db.SaveChanges().ToString();
                            }
                            else
                            {
                                // Manejar el caso en el que el usuario no se encuentra
                                rpta = "Doctor no encontrado";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "Error en otra cosa: " + ex.Message;
            }

            return rpta;
        }
        public JsonResult RellenarCampos(int titulo)

        {
            
            GetTipoUsuarioList();
          GetGeneroUsuarioList();
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            using (var db = new TurneroFaeracEntities())
            {
                Usuarios oUsuario = db.Usuarios.Where(p => p.IdUser == titulo).First();
                oUsuarioCLS.Usuario = oUsuario.Usuario;
                oUsuarioCLS.Contraseña = oUsuario.Contraseña;
                oUsuarioCLS.IdTipo = oUsuario.IdTipo;
                oUsuarioCLS.Genero = oUsuario.Genero;
                oUsuarioCLS.NumeroContacto = oUsuario.NumeroContacto;

            }
            return Json(oUsuarioCLS, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RellenarDocs(int titulo)

        {

            llenarEspecializacion();
            DoctoresCLS oDoctoresCLS = new DoctoresCLS();
            using (var db = new TurneroFaeracEntities())
            {
                Doctores oDoctores = db.Doctores.Where(p => p.IdDoc == titulo).First();
                oDoctores.IdDoc = oDoctoresCLS.IdDoc;
                oDoctores.IdUser = oDoctoresCLS.IdUser;
                oDoctores.ApeDoc = oDoctoresCLS.ApeDoc;
                oDoctores.NombreDoc = oDoctoresCLS.NombreDoc;
                oDoctores.IDEspecializacion = oDoctoresCLS.IDEspecializacion;

            }
            return Json(oDoctoresCLS, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RellenarPacs(int titulo)

        {

            llenarEspecializacion();
            PacientesCLS oPacientesCLS = new PacientesCLS();
            using (var db = new TurneroFaeracEntities())
            {
                Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente == titulo).First();

                oPacientes.IdPaciente = oPacientesCLS.IdPaciente;
                oPacientes.NombrePac = oPacientesCLS.NombrePac;
                oPacientes.ApePac = oPacientesCLS.ApePac;
                oPacientes.DNIPac = oPacientesCLS.DNIPac;
                oPacientes.TipoDNI = oPacientesCLS.TipoDNI;
                oPacientes.EdadPac = oPacientesCLS.EdadPac;
                oPacientes.Genero = oPacientesCLS.Genero;
                oPacientes.IdUser = oPacientesCLS.IdUser;
                oPacientes.Correo = oPacientesCLS.Correo;
                oPacientes.NumeroContacto = oPacientesCLS.NumeroContacto;

            }
            return Json(oPacientesCLS, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public static List<SelectListItem> GetTipoUsuarioList()
        {
            List<SelectListItem> tipoUsuarioList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Administrador" },
        new SelectListItem { Value = "2", Text = "Usuario" },
        new SelectListItem { Value = "3", Text = "Doctor" },
        new SelectListItem { Value = "4", Text ="Deshabilitado"}
    };
        

                return tipoUsuarioList;
        }
        public static List<SelectListItem> GetGeneroUsuarioList()
        {
            List<SelectListItem> generoUsuarioList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Masculino" },
        new SelectListItem { Value = "2", Text = "Femenino" },
        new SelectListItem { Value = "3", Text = "Otro" },

    };


            return generoUsuarioList;
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

        List<SelectListItem> listaGenero;
        private void llenarGenero()
        {
            using (var db = new TurneroFaeracEntities())
            {
                listaGenero = (from Genero in db.IndiceGenero
                             where Genero.Habilitado != 0
                             select new SelectListItem
                             {
                                 Text = Genero.Descripcion,
                                 Value = Genero.IdGenero.ToString()
                             }).ToList();

                listaGenero.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });

            }
        }
        public ActionResult ListarGeneros()
        {
            using (var context = new TurneroFaeracEntities()) // Reemplaza "TuDbContext" con el nombre real de tu contexto de base de datos
            {
                var usuarios = context.Usuarios.ToList();
                var generos = context.IndiceGenero.ToList();
                var tipo = context.TipoUsuarios.ToList();

                // Mapea el IdGenero en la lista de usuarios con sus descripciones correspondientes
                var usuariosConGenero = usuarios.Select(u => new
                {
                    u.IdUser,
                    u.Usuario,
                    TipoUsuario = tipo.FirstOrDefault(g => g.IdTipo == u.IdTipo)?.Descripcion,
                    NumeroContacto = u.NumeroContacto,
                    Genero = generos.FirstOrDefault(g => g.IdGenero == u.Genero)?.Descripcion
                }).ToList();

                return View(usuariosConGenero);
            }
        }

    }

}
