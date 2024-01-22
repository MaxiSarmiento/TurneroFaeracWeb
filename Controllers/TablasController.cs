using Google;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TurneroFaeracWeb.Models;


namespace FaeracT.Controllers
{
    public class TablasController : Controller
    {
        public ActionResult TablaUsuarios(UsuarioCLS oUsuarioCLS)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    string nombreUSR = oUsuarioCLS.Usuario;
                    llenarGenero();
                    GetTipoUsuarioList();
                    GetGeneroUsuarioList();
                    List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
                    using (var db = new TurneroNewEntities())
                    {
                        if (oUsuarioCLS.Usuario == null)
                        {
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
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult TablaDoctores(DoctoresCLS oDoctoresCLS)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;
                    string apeDoc = oDoctoresCLS.ApeDoc;

                    List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();
                    using (var db = new TurneroNewEntities())
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
                                             IDEspecializacion = doctores.IDEspecializacion
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
                                             IDEspecializacion = doctores.IDEspecializacion
                                         }).ToList();

                            Session["lista"] = listaDocs;
                        }
                    }
                    return View(listaDocs);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult TablaPacientes(PacientesCLS oPacientesCLS)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;
                    string dnipac = oPacientesCLS.NombrePac;

                    List<PacientesCLS> listaPacs = new List<PacientesCLS>();
                    using (var db = new TurneroNewEntities())
                    {
                        if (oPacientesCLS.NombrePac == null)
                        {
                            listaPacs = (from pacientes in db.Pacientes
                                         select new PacientesCLS
                                         {
                                             IdPaciente = pacientes.IdPaciente,
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
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult TablaAuxiliar(PacientesCLS oPacientesCLS)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    string nombreUSR = oPacientesCLS.NombrePac;
                    llenarGenero();
                    GetTipoUsuarioList();
                    GetGeneroUsuarioList();
                    List<PacientesCLS> listaUsuario = new List<PacientesCLS>();
                    using (var db = new TurneroNewEntities())
                    {
                        if (oPacientesCLS.NombrePac == null)
                        {
                            listaUsuario = (from pacientes in db.Pacientes
                                            select new PacientesCLS
                                            {
                                                IdPaciente = pacientes.IdPaciente,
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
                            Session["lista"] = listaUsuario;
                        }
                        else
                        {
                            listaUsuario = (from pacientes in db.Pacientes
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

                            Session["lista"] = listaUsuario;
                        }
                    }
                    return View(listaUsuario);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult TablaTurnos(TablaTurnosCLS oTablaTurnosCLS)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ListarEsp();
                    ObtenerListaDoctores();
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;
                    int turnosPac = oTablaTurnosCLS.IdPaciente;

                    List<TablaTurnosCLS> turnosPacs = new List<TablaTurnosCLS>();
                    using (var db = new TurneroNewEntities())
                    {
                        // Obtener la lista de especializaciones desde el método ListarEsp
                        ViewBag.Especializaciones = new SelectList(db.Especializaciones.ToList(), "IDEspecializacion", "Descripcion");

                        if (oTablaTurnosCLS.Estado == null)
                        {
                            turnosPacs = (from turnos in db.TablaTurnos
                                          select new TablaTurnosCLS
                                          {
                                              IdTurno = turnos.IdTurno,
                                              IdPaciente = turnos.IdPaciente,
                                              inicio = (DateTime)turnos.inicio,
                                              IdDoc = (int)turnos.IdDoc,

                                          }).ToList();
                            Session["lista"] = turnosPacs;
                        }
                        else
                        {
                            turnosPacs = (from turnos in db.TablaTurnos
                                          select new TablaTurnosCLS
                                          {
                                              IdTurno = turnos.IdTurno,
                                              IdPaciente = turnos.IdPaciente,
                                              inicio = (DateTime)turnos.inicio,
                                              IdDoc = (int)turnos.IdDoc,
                                          }).ToList();

                            Session["lista"] = turnosPacs;
                        }
                    }

                    // Agregar la lógica de la acción Index aquí
                    // Por ejemplo:
                    return View(turnosPacs); // Asegúrate de tener una vista llamada TablaTurnos
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View("Error"); // Asegúrate de tener una vista llamada Error
            }
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
                using (var db = new TurneroNewEntities())
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
                using (var db = new TurneroNewEntities())
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
                using (var db = new TurneroNewEntities())
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

        public ActionResult AgregarT()
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
        public ActionResult AgregarT(TablaTurnos oTablaTurnosCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    llenarGenero();
                    ViewBag.lista = listaGenero;
                    llenarEspecializacion();
                    ViewBag.Lista = listaEspecializacion;
                    return View(oTablaTurnosCLS);
                }
                else
                {
                    using (var db = new TurneroNewEntities())
                    {
                        // Verificar si el Turno ya existe
                        bool turnoExistente = db.TablaTurnos.Any(e => e.IdTurno == oTablaTurnosCLS.IdTurno && e.inicio == oTablaTurnosCLS.inicio && e.IdDoc == oTablaTurnosCLS.IdDoc);

                        if (turnoExistente)
                        {
                            // Turno existente, mostrar mensaje de error
                            ModelState.AddModelError("Turnos", "Turno ya Otorgado, intente con otra hora");
                            llenarGenero();
                            ViewBag.lista = listaGenero;
                            llenarEspecializacion();
                            ViewBag.Lista = listaEspecializacion;
                            return View(oTablaTurnosCLS);
                        }
                        else
                        {
                            // Obtener datos asociados al IdPaciente
                            Pacientes oPaciente = db.Pacientes.FirstOrDefault(p => p.IdPaciente == oTablaTurnosCLS.IdPaciente);
                            if (oPaciente == null)
                            {
                                // Manejar el caso en que no se encuentre el paciente
                                ModelState.AddModelError("Turnos", "Paciente no encontrado");
                                llenarGenero();
                                ViewBag.lista = listaGenero;
                                llenarEspecializacion();
                                ViewBag.Lista = listaEspecializacion;
                                return View(oTablaTurnosCLS);
                            }

                            // Obtener datos asociados al IdDoc
                            Doctores oDoctor = db.Doctores.FirstOrDefault(d => d.IdDoc == oTablaTurnosCLS.IdDoc);
                            if (oDoctor == null)
                            {
                                // Manejar el caso en que no se encuentre el doctor
                                ModelState.AddModelError("Turnos", "Doctor no encontrado");
                                llenarGenero();
                                ViewBag.lista = listaGenero;
                                llenarEspecializacion();
                                ViewBag.Lista = listaEspecializacion;
                                return View(oTablaTurnosCLS);
                            }

                            // Agregar el turno
                            TablaTurnos oTurnos = new TablaTurnos();
                            oTurnos.IdTurno = oTablaTurnosCLS.IdTurno;
                            oTurnos.IdPaciente = oTablaTurnosCLS.IdPaciente;
                            oTurnos.inicio = oTablaTurnosCLS.inicio;
                            oTurnos.IdDoc = oTablaTurnosCLS.IdDoc;

                            // Agregar el turno a la base de datos
                            db.TablaTurnos.Add(oTurnos);
                            db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                return RedirectToAction("Error", "Home");
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
                    using (var db = new TurneroNewEntities())
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

            using (var db = new TurneroNewEntities())
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




        private List<UsuarioCLS> ObtenerListaUsuarios()
        {
            llenarEspecializacion();
            List<UsuarioCLS> listaUsuarios = new List<UsuarioCLS>();
            using (var db = new TurneroNewEntities())

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
            using (var db = new TurneroNewEntities())
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
        private List<TablaTurnosCLS> ObtenerListaTurnos()
        {
            List<TablaTurnosCLS> listaTurnos = new List<TablaTurnosCLS>();
            using (var db = new TurneroNewEntities())
            {
                listaTurnos = (from ia in db.TablaTurnos
                               join p in db.Doctores on ia.IdDoc equals p.IdDoc
                               select new TablaTurnosCLS
                               {
                                   IdTurno = ia.IdTurno,
                                   IdPaciente = ia.IdPaciente,
                                   inicio = (DateTime)ia.inicio,
                                   Fin = (DateTime)ia.Fin,
                                   Estado = ia.Estado,
                                   IdDoc = (int)ia.IdDoc,
                                   NombrePaciente = ia.NombrePaciente,
                                   ApeDoc = ia.ApeDoc
                               }).ToList();

                return listaTurnos;
            }
        }
        public ActionResult TablaTurnosUserDoc1()
        {
            List<PacientesCLS> pacientes = ObtenerListaPacientes(); // Obtén tus datos de pacientes
            List<AnalisisPacientesCLS> analisis = ObtenerListaAnalisis(); // Obtén tus datos de análisis

            // Puedes combinar los datos como sea necesario y pasarlos a la vista parcial
            return PartialView("_TablaTurnosUserDoc", pacientes);
        }

        public ActionResult FiltrarEmpleados(UsuarioCLS oUsuariosCLS)
        {
            string nombreUsr = oUsuariosCLS.nombreFiltro;
            List<UsuarioCLS> listaUsr = new List<UsuarioCLS>();
            using (var db = new TurneroNewEntities())

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
            return PartialView("_TablaUsuarios", listaUsr);
        }
        public ActionResult Filtrar(UsuarioCLS oUsuarioCLS)
        {
            string nombreUsuario = oUsuarioCLS.nombreFiltro;
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var db = new TurneroNewEntities())
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
            return PartialView("_TablaUsuarios", listaUsuario);
        }
        public ActionResult FiltrarD(DoctoresCLS oDoctoresCLS)
        {
            string nombreDoc = oDoctoresCLS.nombreFiltro;
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();
            using (var db = new TurneroNewEntities())
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
                                 where doctores.NombreDoc.ToLower().Contains(nombreDoc.ToLower())
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
            using (var db = new TurneroNewEntities())
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
        public ActionResult FiltrarT(TurnosCLS oTurnosCLS)
        {
            string turnoPac = oTurnosCLS.nombreFiltro;
            List<TurnosCLS> turnoPacs = new List<TurnosCLS>();

            using (var db = new TurneroNewEntities())
            {
                // Obtener el IdUser del paciente logueado
                int idUser = Convert.ToInt32(Session["UserID"]);

                // Filtrar por defecto los turnos del paciente logueado
                turnoPacs = (from turnos in db.Turnos
                             where turnos.IdPaciente == idUser
                             select new TurnosCLS
                             {
                                 IdTurno = turnos.IdTurno,
                                 IdPaciente = turnos.IdPaciente,
                                 Fecha = turnos.Fecha,
                                 Hora = turnos.Hora,
                                 TurnoRealizado = turnos.TurnoRealizado,
                                 ComentarioDoctor = turnos.ComentarioDoctor,
                                 IdDoc = turnos.IdDoc,
                             }).ToList();

                // Si el filtro por nombre no está vacío, aplicar filtro adicional
                if (!string.IsNullOrEmpty(turnoPac))
                {
                    turnoPacs = turnoPacs.Where(t => t.nombreFiltro.Contains(turnoPac)).ToList();
                }


            }

            return PartialView("_TablaTurnos", turnoPacs);
        }


        public int Guardar(UsuarioCLS oUsuarioCLS, int titulo)
        {
            int rpta = 0;
            using (var db = new TurneroNewEntities())
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
            using (var db = new TurneroNewEntities())
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
            using (var db = new TurneroNewEntities())
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
        public int GuardarT(TablaTurnosCLS oTablaTurnosCLS, int titulo)
        {
            int rpta = 0;
            using (var db = new TurneroNewEntities())
            {
                if (titulo == 1)
                {
                    TablaTurnos oTurnos = new TablaTurnos
                    {
                        IdTurno = oTablaTurnosCLS.IdTurno,
                        IdPaciente = oTablaTurnosCLS.IdPaciente,
                        inicio = oTablaTurnosCLS.inicio,
                        Fin = oTablaTurnosCLS.Fin,
                        EdadPac = oTablaTurnosCLS.EdadPac,
                        IdDoc = oTablaTurnosCLS.IdDoc,
                    };
                    db.TablaTurnos.Add(oTurnos);
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
                using (var db = new TurneroNewEntities())
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
                using (var db = new TurneroNewEntities())
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
                using (var db = new TurneroNewEntities())
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
        public string EliminarT(TurnosCLS oTurnosCLS)
        {
            string rpta = "";
            try
            {
                int id = oTurnosCLS.IdTurno;
                using (var db = new TurneroNewEntities())
                {
                    Turnos oTurnos = db.Turnos.Where(p => p.IdTurno == id).First();
                    db.Turnos.Remove(oTurnos);
                    rpta = db.SaveChanges().ToString();
                }
            }
            catch (Exception)
            {
                rpta = "";
            }
            return rpta;
        }
        public string Tatendido(TurnosCLS oTurnosCLS)
        {
            string rpta = "";
            try
            {
                int id = oTurnosCLS.IdTurno;
                using (var db = new TurneroNewEntities())
                {
                    Turnos oTurnos = db.Turnos.Where(p => p.IdTurno == id).First();
                    oTurnos.TurnoRealizado = true;
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
                    using (var db = new TurneroNewEntities())
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
                    using (var db = new TurneroNewEntities())
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
                    using (var db = new TurneroNewEntities())
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
                                rpta = "Paciente no encontrado";
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

        [HttpPost]
        public string GuardarTurnos(TurnosModCLS oTurnosModCLS)
        {
            string rpta = "";

            try
            {
                int userId = GetUserIdFromSession();

                if (!ModelState.IsValid)
                {
                    // Manejo de errores de validación del modelo
                    // Puedes agregar el código necesario para manejar esto según tus requisitos
                }
                else
                {
                    using (var db = new TurneroNewEntities())
                    {
                        // Agregar nuevo turno
                        TurnosMod nuevoTurno = new TurnosMod
                        {
                            IdDoc = oTurnosModCLS.IdDoc,
                            IdPaciente = userId,
                            IdHorario = oTurnosModCLS.IdHorario, // Asegúrate de tener este campo en oTablaTurnosCLS
                            Fecha = oTurnosModCLS.Fecha, // Asegúrate de tener este campo en oTablaTurnosCLS
                             // 0 para domingo, 1 para lunes, ..., 6 para sábado
                        };

                        db.TurnosMod.Add(nuevoTurno);
                        rpta = db.SaveChanges().ToString();
                        if (rpta == "1")
                        {
                            // Turno agregado correctamente
                            rpta = "Turno agregado correctamente. Recuerda llegar 20 minutos antes del horario del turno.";
                        }
                        else
                        {
                            rpta = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "Error al agregar el turno: " + ex.Message;
            }

            return rpta;
        }
        public JsonResult GetEvents()
        {
            using (var db = new TurneroNewEntities())
            {
                int userId = GetUserIdFromSession();
                var eventos = db.TurnosMod.Select(t => new
                {
                    IdPaciente = userId,
                    IdDoctor = t.IdDoc,
                    Start = t.Fecha,
                    
                }).ToList();

                return Json(eventos, JsonRequestBehavior.AllowGet);
            }
        }


        private int GetUserIdFromSession()
        {
            if (Session["UserId"] != null)
            {
                int userId;
                if (int.TryParse(Session["UserId"].ToString(), out userId))
                {
                    return userId;
                }
            }
            // Puedes devolver un valor predeterminado o lanzar una excepción según tus necesidades
            return -1; // Valor predeterminado en caso de error
        }


    public JsonResult RellenarCampos(int titulo)

        {

            GetTipoUsuarioList();
            GetGeneroUsuarioList();
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            using (var db = new TurneroNewEntities())
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
            using (var db = new TurneroNewEntities())
            {
                
                Doctores oDoctores = db.Doctores.Where(p => p.IdDoc == titulo).First();
               
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
            using (var db = new TurneroNewEntities())
            {
                Pacientes oPacientes = db.Pacientes.Where(p => p.IdPaciente == titulo).First();

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
        public JsonResult RellenarTurno(int titulo)

        {
            ObtenerListaPacientes();
            ObtenerListaDoctores();
            TurnosCLS oTurnosCLS = new TurnosCLS();
            using (var db = new TurneroNewEntities())
            {
                Turnos oTurnos = db.Turnos.Where(p => p.IdTurno == titulo).First();

                oTurnos.IdPaciente = oTurnosCLS.IdPaciente;
                oTurnos.Fecha = oTurnosCLS.Fecha;
                oTurnos.Hora = oTurnosCLS.Hora;
                oTurnos.TurnoRealizado = oTurnosCLS.TurnoRealizado;
                oTurnos.ComentarioDoctor = oTurnosCLS.ComentarioDoctor;
                oTurnos.IdDoc = oTurnosCLS.IdDoc;

            }
            return Json(oTurnosCLS, JsonRequestBehavior.AllowGet);
        }
        private List<PacientesCLS> ObtenerListaPacientes()
        {
            List<PacientesCLS> listaPacientes = new List<PacientesCLS>();
            using (var db = new TurneroNewEntities())
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
        private List<DoctoresCLS> ObtenerListaDoctores()
        {
            List<DoctoresCLS> listaDoctores = new List<DoctoresCLS>();
            using (var db = new TurneroNewEntities())
            {
                listaDoctores = (from doctores in db.Doctores
                                 join especializacion in db.Especializaciones on doctores.IDEspecializacion equals especializacion.IDEspecializacion
                                 select new DoctoresCLS
                                 {
                                     IdDoc = doctores.IdDoc,
                                     NombreDoc = doctores.NombreDoc,
                                     ApeDoc = doctores.ApeDoc,
                                     IDEspecializacion = doctores.IDEspecializacion,
                                     Descripcion = especializacion.Descripcion
                                 }).ToList();
                return listaDoctores;
            }
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
            using (var db = new TurneroNewEntities())
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
        private object txtDateTime;

        public IEnumerable<string> Scopes { get; private set; }

        public void llenarGenero()
        {
            using (var db = new TurneroNewEntities())
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
            using (var context = new TurneroNewEntities()) // Reemplaza "TuDbContext" con el nombre real de tu contexto de base de datos
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
       /* public ActionResult ListarAnalisisUsr()
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
        }*/
        public ActionResult BuscarDocs(int idEspecializacion, string nombreFiltro)
        {
            List<DoctoresCLS> listaDocs = new List<DoctoresCLS>();

            using (var db = new TurneroNewEntities())
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



        // Acción para verificar la disponibilidad del turno
        [HttpPost]

        public JsonResult VerificarDisponibilidad(int IdDoc,DateTime fechaTurno)
        {
            using (var db = new TurneroNewEntities())
            {
                bool disponible = db.Turnos.Any(t =>
                t.IdDoc == IdDoc &&
              
                t.Fecha == fechaTurno);

                return Json(disponible);
            }
        }
        // Acción para guardar el turno
        [HttpPost]
        public JsonResult GuardarTurno( int IdDoc,int IdPaciente, DateTime fechaTurno)
        {

            using (var db = new TurneroNewEntities())
            {
                try
                {
                    // Obtiene el IdPaciente de la sesión actual
                    int idPaciente = ObtenerIdPacienteDeSesion();
                    int idDia = (int)fechaTurno.DayOfWeek;
                    // Realiza la lógica para guardar el turno en la base de datos
                    var nuevoTurno = new TurnosMod
                    {
                        IdPaciente = idPaciente, // Asigna el IdPaciente de la sesión actual
                        IdDoc = IdDoc,
                        IdDia = idDia,
                        Fecha = fechaTurno
                        // Otros campos del turno según sea necesario
                    };

                    db.TurnosMod.Add(nuevoTurno);
                    db.SaveChanges();

                    return Json("El turno se guardó correctamente.");
                }
                catch (Exception ex)
                {
                    return Json($"Hubo un error al guardar el turno: {ex.Message}");
                }
            }
        }

        private int ObtenerIdPacienteDeSesion()
        {
            // Verifica si el usuario está autenticado
            if (User.Identity.IsAuthenticated)
            {
                // Accede a la identidad del usuario actual para obtener información adicional
                var claimsIdentity = (ClaimsIdentity)User.Identity;

                // Busca el claim que contiene el IdPaciente
                var idPacienteClaim = claimsIdentity.FindFirst("IdPaciente");

                // Verifica si el claim fue encontrado y si su valor es un entero válido
                if (idPacienteClaim != null && int.TryParse(idPacienteClaim.Value, out int idPaciente))
                {
                    return idPaciente;
                }
            }

            // Si no se encuentra la información o no es válida, puedes devolver un valor predeterminado o lanzar una excepción
            // En este ejemplo, se devuelve -1 para indicar que no se encontró el IdPaciente
            return -1;
        }
        [HttpPost]
        public ActionResult ObtenerDoctores(int idEspecializacion)
        {
            using (var db = new TurneroNewEntities())
            {
                var doctores = db.Doctores
                                 .Where(d => d.IDEspecializacion == idEspecializacion)
                                 
                                 .Select(d => new SelectListItem
                                 {
                                     Value = d.IdDoc.ToString(),
                                     Text = d.ApeDoc
                                 })
                                 .ToList();

                return Json(doctores);
            }
        }
        public ActionResult ObtenerDoctores2(int idEspecializacion)
        {
            using (var db = new TurneroNewEntities())
            {
                var doctores = db.Doctores.Where(d => d.IDEspecializacion == idEspecializacion).ToList();
                return PartialView("_ListaDoctores", doctores);
            }
        }


        [HttpPost]
        public ActionResult ObtenerEspecializaciones()
        {
            using (var db = new TurneroNewEntities())
            {
                var especializaciones = db.Especializaciones
                                           .Select(e => new SelectListItem
                                           {
                                               Value = e.IDEspecializacion.ToString(),
                                               Text = e.Descripcion
                                           })
                                           .ToList();

                return Json(especializaciones);
            }
        }
        [HttpGet]
        public ActionResult ListarDocs(int idEspecializacion)
        {
            using (var db = new TurneroNewEntities())
            {
                var doctores = db.Doctores
                                 .Where(d => d.IDEspecializacion == idEspecializacion)
                                 .Select(d => new
                                 {
                                     d.IdDoc,
                                     d.ApeDoc,
                                     d.NombreDoc
                                 })
                                 .ToList();

                return Json(doctores);
            }
        }


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
        public ActionResult AgregarTurno(int idDoctor, DateTime inicio, string hora)
        {
            try
            {
                using (var db = new TurneroNewEntities())
                {
                    // Por ejemplo:
                    var nuevoTurno = new TablaTurnos
                    {
                        IdDoc = idDoctor,
                        inicio = inicio,
                        Fin = inicio.AddMinutes(20), // Establecer la duración del turno (ajusta según sea necesario)
                        Estado = "Activo", // O cualquier otro estado que desees asignar al nuevo turno
                    };

                    db.TablaTurnos.Add(nuevoTurno);
                    db.SaveChanges();

                    return RedirectToAction("Tablas", "TablaTurnos");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return RedirectToAction("Error", "Turnos");
            }
        }

        // Otras acciones según sea necesario
    }





}



