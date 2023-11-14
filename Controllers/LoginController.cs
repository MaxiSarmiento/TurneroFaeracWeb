using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TurneroFaeracWeb.Models;
using System.Text;
using System.Security.Cryptography;


namespace Progweb1.Controllers
{
    public class LoginController : Controller
    {
        TurneroFaeracEntities db = new TurneroFaeracEntities();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            Agregartipo();
            
            return View();
        }
        public ActionResult Restablecer()
        {

            return View();
        }

        public ActionResult Confirmar()
        {
            return View();
        }
        public ActionResult Actualizar()
        {
            return View();
        }
        [HttpPost]
        /* public ActionResult Registro(UsuarioCLS oUsuarioCLS)  //listo , sin confirmacion de correo electronico para mas placer
         {
             if (!ModelState.IsValid)
             {


                 Agregartipo();
                 ViewBag.lista = listaUsr;
                 return View(oUsuarioCLS);
             }
             else
             {
                 using (var db = new TurneroFaeracEntities())
                 {
                     // Verifica si ya existe un usuario con el mismo nombre de usuario
                     var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Usuario == oUsuarioCLS.Usuario);

                     if (usuarioExistente != null)
                     {
                         // Usuario ya existe en la base de datos
                         ViewBag.MensajeUsuario = "El usuario ya está registrado";
                         Agregartipo();
                         ViewBag.lista = listaUsr;

                         Usuarios usuarioExistenteModel = new Usuarios
                         {
                             Usuario = usuarioExistente.Usuario,

                         };
                         return View(usuarioExistenteModel);
                     }

                     // Verifica si ya existe un usuario con el mismo correo
                     var correoExistente = db.Usuarios.FirstOrDefault(u => u.Correo == oUsuarioCLS.Correo);

                     if (correoExistente != null)
                     {
                         // Correo ya existe en la base de datos
                         ViewBag.MensajeCorreo = "Correo ya existente";
                         Agregartipo();
                         ViewBag.lista = listaUsr;

                         Usuarios correoExistenteModel = new Usuarios
                         {
                             Correo = correoExistente.Correo,

                         };
                         return View(correoExistenteModel);
                     }

                     // Si no existen registros con el mismo usuario ni correo, procede a registrar al nuevo usuario
                     Usuarios nuevoUsuario = new Usuarios
                     {
                         Usuario = oUsuarioCLS.Usuario,
                         Contraseña = HashPassword(oUsuarioCLS.Contraseña),
                         IdTipo = 2,
                         Correo = oUsuarioCLS.Correo,
                         NumeroContacto = oUsuarioCLS.NumeroContacto,
                         Descripcion = "Usuario Registrado",
                         Genero = oUsuarioCLS.IdGenero,
                         Token = Guid.NewGuid().ToString(),
                     };

                     // Agrega el nuevo usuario a la base de datos
                     db.Usuarios.Add(nuevoUsuario);
                     db.SaveChanges();

                     // Envía un correo electrónico de confirmación aquí
                     //EnviarCorreoConfirmacion(nuevoUsuario.Correo, nuevoUsuario.Token);
                 }
             }

             return RedirectToAction("Login");
         }
        */
        public ActionResult Registro(UsuarioCLS oUsuarioCLS)
        {
          using (var db = new TurneroFaeracEntities())
            {
                // Verifica si ya existe un usuario con el mismo nombre de usuario o correo
                var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Usuario == oUsuarioCLS.Usuario || u.Correo == oUsuarioCLS.Correo);

                if (usuarioExistente != null)
                {
                    // Usuario ya existe en la base de datos
                    if (usuarioExistente.Usuario == oUsuarioCLS.Usuario)
                    {
                        ViewBag.MensajeUsuario = "El usuario ya está registrado";
                        Agregartipo();
                        ViewBag.lista = listaUsr;

                        Usuarios usuarioExistenteModel = new Usuarios
                        {
                            Usuario = usuarioExistente.Usuario,
                        };
                        return View(usuarioExistenteModel);
                    }
                    else if (usuarioExistente.Correo == oUsuarioCLS.Correo)
                    {
                        // Correo ya existe en la base de datos
                        ViewBag.MensajeCorreo = "Correo ya existente";
                        Agregartipo();
                        ViewBag.lista = listaUsr;

                        Usuarios correoExistenteModel = new Usuarios
                        {
                            Correo = usuarioExistente.Correo,
                        };
                        return View(correoExistenteModel);
                    }
                }

                // Si no existen registros con el mismo usuario ni correo, procede a registrar al nuevo usuario
                Usuarios nuevoUsuario = new Usuarios
                {
                    Usuario = oUsuarioCLS.Usuario,
                    Contraseña = HashPassword(oUsuarioCLS.Contraseña),
                    IdTipo = 2,
                    Correo = oUsuarioCLS.Correo,
                    NumeroContacto = oUsuarioCLS.NumeroContacto,
                    Descripcion = "Usuario Registrado",
                    Genero = oUsuarioCLS.Genero,
                    Token = Guid.NewGuid().ToString(),
                };

                // Agrega el nuevo usuario a la base de datos
                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();

                // Envía un correo electrónico de confirmación aquí
                //EnviarCorreoConfirmacion(nuevoUsuario.Correo, nuevoUsuario.Token);
            }

            return RedirectToAction("Login");
        }


            public UsuarioCLS Validar(UsuarioCLS oUsuarioCLS)
        {
            UsuarioCLS usuario = null;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuarioDB = db.Usuarios
                        .Where(u => u.Correo == oUsuarioCLS.Correo && u.Contraseña == oUsuarioCLS.Contraseña)
                        .FirstOrDefault();

                    if (usuarioDB != null)
                    {
                        // El usuario se encontró en la base de datos, puedes asignar propiedades
                        // del usuario a usuarioCLS o cualquier otra acción necesaria.
                        usuario = new UsuarioCLS
                        {
                            Usuario = usuarioDB.Usuario,

                        };
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return usuario;
        }
        public static UsuarioCLS Obtener(string correo)
        {
            UsuarioCLS usuario = null;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuarioDB = db.Usuarios
                        .Where(u => u.Correo == correo)
                        .FirstOrDefault();

                    if (usuarioDB != null)
                    {
                        usuario = new UsuarioCLS()
                        {
                            Usuario = usuarioDB.Usuario,
                            Contraseña = usuarioDB.Contraseña,

                            Token = usuarioDB.Token
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }
        public static bool RestablecerActualizar(bool restablecer, string clave, string token)
        {
            bool respuesta = false;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuario = db.Usuarios
                        .Where(u => u.Token == token)
                        .FirstOrDefault();

                    if (usuario != null)
                    {

                        usuario.Contraseña = clave;

                        db.SaveChanges(); // Guarda los cambios en la base de datos

                        respuesta = true;
                    }
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Confirmar(string token)
        {
            bool respuesta = false;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuario = db.Usuarios
                        .Where(u => u.Token == token)
                        .FirstOrDefault();

                    if (usuario != null)
                    {


                        db.SaveChanges(); // Guarda los cambios en la base de datos

                        respuesta = true;
                    }
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        private void Agregartipo()
        {
            List<SelectListItem> listaUsr = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Administrador" },
        new SelectListItem { Value = "2", Text = "Usuario" },
        new SelectListItem { Value = "3", Text = "Doctor" },
        new SelectListItem { Value = "4", Text ="Deshabilitado"}
    };

            ViewBag.lista = listaUsr;
        }
        [HttpPost]
        public ActionResult Agregartipo(UsuarioCLS oUsuarioCLS)
        {
            if (!ModelState.IsValid)
            {
                TipoUsr();
                ViewBag.lista = listaUsr;
                return View(oUsuarioCLS);
            }
            else
            {
                using (var db = new TurneroFaeracEntities())
                {
                    Usuarios oUsuario = new Usuarios();
                    oUsuario.Usuario = oUsuarioCLS.Usuario;
                    oUsuario.Contraseña = HashPassword(oUsuarioCLS.Contraseña); // Hashear la contraseña
                    oUsuario.IdTipo = (int)oUsuarioCLS.IdTipo;
                    db.Usuarios.Add(oUsuario);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]



        // [ValidateAntiForgeryToken]
        /*  public ActionResult Login(Usuarios objUser)
          {

              if (ModelState.IsValid)
              {
                  using (var db = new TurneroFaeracEntities())
                  {
                      var obj = db.Usuarios.Where(a => a.Usuario.Equals(objUser.Usuario) && a.Contraseña.Equals(objUser.Contraseña)).FirstOrDefault();
                      if (obj != null)
                      {
                          Session["UserID"] = obj.IdUser.ToString();
                          Session["UserName"] = obj.Usuario.ToString();
                          // Verifica el TipoUsuario y redirige en consecuencia
                          if (obj.IdTipo == 1)
                          {
                              return RedirectToAction("IndexAdmin", "Indices");
                          }
                          else if (obj.IdTipo == 2)
                          {
                              return RedirectToAction("PanelUser", "Indices");
                          }
                          else if (obj.IdTipo == 3)
                          {
                              return RedirectToAction("IndexDoctores", "Indices");
                          }

                      }

                  }
              }
              return View(objUser);
          }*/
        public ActionResult Login(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TurneroFaeracEntities())
                {
                    // Hasheamos la contraseña ingresada por el usuario
                    string hashedPassword = HashPassword(objUser.Contraseña);

                    // Buscamos el usuario en la base de datos usando la contraseña hasheada
                    var obj = db.Usuarios
                        .Where(a => a.Usuario.Equals(objUser.Usuario) && a.Contraseña.Equals(hashedPassword))
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        // Almacenamos el ID y nombre del usuario en la sesión
                        Session["UserID"] = obj.IdUser.ToString();
                        Session["UserName"] = obj.Usuario.ToString();

                        // Verifica el TipoUsuario y redirige en consecuencia
                        if (obj.IdTipo == 1)
                        {
                            return RedirectToAction("IndexAdmin", "Indices");
                        }
                        else if (obj.IdTipo == 2)
                        {
                            return RedirectToAction("PanelUser", "Indices");
                        }
                        else if (obj.IdTipo == 3)
                        {
                            return RedirectToAction("IndexDoctores", "Indices");
                        }
                    }
                }
            }
            return View(objUser);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public ActionResult Logout()
        {
            // Limpia la sesión al cerrar sesión.
            Session.Clear();
            return RedirectToAction("Index");
        }

        /* private int ObtenerIdPaciente(int username)
         {
             // Implementa la lógica para buscar el IdPaciente relacionado con el usuario.
             // Esto dependerá de la estructura de tu base de datos y cómo se relacionan los usuarios con los pacientes.
             // Debe devolver el IdPaciente si se encuentra, o 0 si no se encuentra.
             var usuario = db.Pacientes.FirstOrDefault(u => u.IdUser = username);
             if (usuario != null)
             {
                 return usuario.IdUser;
             }
             return 0;
         }*/

        private int ObtenerIdUsuario(string username)
        {
            // Implementa la lógica para buscar el IdUsuario del usuario.
            // Esto dependerá de la estructura de tu base de datos.
            // Debe devolver el IdUsuario si se encuentra, o 0 si no se encuentra.
            var usuario = db.Usuarios.FirstOrDefault(u => u.Usuario == username);
            if (usuario != null)
            {
                return usuario.IdUser;
            }
            return 0;
        }

        List<SelectListItem> listaUsr;
        private void TipoUsr()
        {
            List<SelectListItem> listaUsr = new List<SelectListItem>
       {
      new SelectListItem { Value = "1", Text = "Administrador" },
        new SelectListItem { Value = "2", Text = "Usuario" },
        new SelectListItem { Value = "3", Text = "Doctor" },
        new SelectListItem { Value = "4", Text ="Deshabilitado"}
       };

            ViewBag.lista = listaUsr;
        }
       /* private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }*/

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
        /*     public ActionResult ListarGeneros()
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
             }*/

    }

}
