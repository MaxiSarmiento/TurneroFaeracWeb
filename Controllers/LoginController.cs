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
        public ActionResult Registro(UsuarioCLS oUsuarioCLS)  //listo , sin confirmacion de correo electronico para mas placer
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

                        TurneroFaeracWeb.Models.Usuarios usuarioExistenteModel = new TurneroFaeracWeb.Models.Usuarios
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

                        TurneroFaeracWeb.Models.Usuarios correoExistenteModel = new TurneroFaeracWeb.Models.Usuarios
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
                        IdTipo = 6,
                        Correo = oUsuarioCLS.Correo,
                        NumeroContacto = oUsuarioCLS.NumeroContacto,
                        Descripcion = "Usuario Comun registrado",
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


        /*   public void EnviarCorreoConfirmacion(string correo, string token)
           {
               // Crear un mensaje
               var message = new MimeMessage();
               message.From.Add(new MailboxAddress("Hospicio Santa Catalina", "msarmiento1989@gmail.com")); // Cambia a tu dirección de correo y nombre
               message.To.Add(new MailboxAddress("", correo)); // El nombre del destinatario está vacío
               message.Subject = "Confirmación de registro";

               // Crear el cuerpo del mensaje (HTML)
               var builder = new BodyBuilder();
               builder.HtmlBody = $"Haga clic en el siguiente enlace para confirmar su correo: <a href='https://tusitio.com/ConfirmarCorreo?token={token}'>Confirmar</a>";

               // Agregar el cuerpo al mensaje
               message.Body = builder.ToMessageBody();

               try
               {
                   // Configurar el cliente SMTP
                   using (var client = new SmtpClient())
                   {
                       // Conectar al servidor SMTP (por ejemplo, Gmail)
                       client.Connect("smtp.gmail.com", 587, false);

                       // Autenticación
                       client.Authenticate("msarmiento1989@gmail.com", "cel540285"); // Reemplaza con tus credenciales

                       // Enviar el mensaje
                       client.Send(message);

                       // Desconectar del servidor SMTP
                       client.Disconnect(true);
                   }
               }
               catch (Exception ex)
               {
                   // Manejo de errores en caso de que falle el envío
                   Console.WriteLine($"Error al enviar el correo: {ex.Message}");
               }
           }


           public ActionResult ConfirmarCorreo(string token)
           {
               var usuario = db.Usuarios.FirstOrDefault(u => u.Token == token);

               if (usuario != null)
               {
                   // Marca al usuario como confirmado
                   usuario.Confirmado = true;
                   usuario.Token = null; // Opcional, elimina el token una vez confirmado
                   db.SaveChanges();
                   return View("CorreoConfirmado");
               }
               else
               {
                   return View("ErrorConfirmacion");
               }
           }
           public ActionResult IniciarSesion(string nombreUsuario, string contraseña)
           {
               var hashedPassword = HashPassword(contraseña);
               var usuarioEncontrado = db.Usuarios.FirstOrDefault(u => u.Usuario == nombreUsuario && u.Contraseña == hashedPassword && u.Confirmado);

               if (usuarioEncontrado != null)
               {
                   // Iniciar sesión exitosamente
                   // Puedes utilizar ASP.NET Identity o Forms Authentication para gestionar la sesión del usuario.
               }
               else
               {
                   ModelState.AddModelError("", "Inicio de sesión incorrecto.");
               }

               return View();
           }

           /*public ActionResult Registro(UsuarioCLS oUsuarioCLS)
           {
               try
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
                           Usuarios oUsuario = new Usuarios();
                           oUsuario.Usuario = oUsuarioCLS.Usuario;
                           oUsuario.Contraseña = oUsuarioCLS.Contraseña; // Hashear la contraseña
                           oUsuario.Correo = oUsuarioCLS.Correo;
                           oUsuario.Token = oUsuarioCLS.Token;
                           oUsuario.Confirmado = oUsuarioCLS.Confirmado;
                           oUsuario.Restablecer = oUsuarioCLS.Restablecer;

                           oUsuario.TipoUsuario = 6;
                           db.Usuarios.Add(oUsuario);
                           db.SaveChanges();
                       }
                   }
                   return RedirectToAction("Login");
               }
               catch (DbEntityValidationException ex)
               {
                   foreach (var entityValidationErrors in ex.EntityValidationErrors)
                   {
                       foreach (var validationError in entityValidationErrors.ValidationErrors)
                       {
                           Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                       }
                   }
                   throw; // Rethrow the exception to log it or handle it at a higher level if needed
               }
           }*/

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
        new SelectListItem { Value = "1", Text = "admin" },
        new SelectListItem { Value = "2", Text = "Jefa_Personal" },
         new SelectListItem { Value = "3", Text = "Recursos Humanos" },
        new SelectListItem { Value = "4", Text = "Atención BOX" },
        new SelectListItem { Value = "5", Text = "Doctor" },
        new SelectListItem { Value = "6", Text = "Usuario Comun"}
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

        /* public ActionResult Login(Usuarios objUser)
         {
             int idPaciente = ObtenerIdPaciente(username); // Debe buscar el IdPaciente relacionado con el usuario.
             int idUsuario = ObtenerIdUsuario(username); // Debe buscar el IdUsuario del usuario.

             if (ModelState.IsValid)
             {
                 using (var db = new TurneroFaeracEntities())
                 {
                     // Busca al usuario por nombre de usuario en la base de datos
                     var obj = db.Usuarios.FirstOrDefault(a => a.Usuario.Equals(objUser.Usuario));

                     if (obj != null)
                     {
                         // Calcula el hash SHA-256 de la contraseña ingresada
                         string hashedPassword = HashPassword(objUser.Contraseña);

                         // Compara el hash calculado con el almacenado en la base de datos
                         if (hashedPassword == obj.Contraseña)
                         {
                             // Las contraseñas coinciden, autenticación exitosa

                             Session["UserID"] = obj.IdUser.ToString();
                             Session["UserName"] = obj.Usuario.ToString();

                             // Verifica el TipoUsuario y redirige en consecuencia
                             if (obj.IdTipo == 1)
                             {
                                 return RedirectToAction("IndexAdmin", "Indices");
                             }
                             else if (obj.IdTipo == 2)
                             {
                                 return RedirectToAction("IndexDoctores", "Indices");
                             }
                             else if (obj.IdTipo == 3)
                             {
                                 return RedirectToAction("IndexAdmin", "Indices");
                             }
                             else if (obj.IdTipo == 4)
                             {
                                 return RedirectToAction("IndexRecepcion", "Indices");
                             }
                             else if (obj.IdTipo == 5)
                             {
                                 return RedirectToAction("IndiceDoctores", "Indices");
                             }
                             else if (obj.IdTipo == 6)
                             {
                                 return RedirectToAction("PanelUser", "Indices");
                             }
                         }
                     }

                     // Si llegas aquí, la autenticación falló
                     // Agrega un mensaje de error
                     ViewBag.ErrorMessage = "Verifique su usuario o contraseña";
                 }
             }
             return View(objUser);
         }


         public ActionResult Logout()
         {
             Session.Abandon();
             Session.Clear();
             return RedirectToAction("Home","LandingPage");
         }*/


        // [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios objUser)
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
                            return RedirectToAction("IndexDoctores", "Indices");
                        }
                        else if (obj.IdTipo == 3)
                        {
                            return RedirectToAction("IndexAdmin", "Indices");
                        }
                        else if (obj.IdTipo == 4)
                        {
                            return RedirectToAction("IndexRecepcion", "Indices");
                        }
                        else if (obj.IdTipo == 5)
                        {
                            return RedirectToAction("IndiceDoctores", "Indices");
                        }
                        else if (obj.IdTipo == 6)
                        {
                            return RedirectToAction("PanelUser", "Indices");
                        }
                    }

                }
            }
            return View(objUser);
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
          new SelectListItem { Value = "1", Text = "admin" },
           new SelectListItem { Value = "2", Text = "Jefa_Personal" },
            new SelectListItem { Value = "3", Text = "Recursos Humanos" },
           new SelectListItem { Value = "4", Text = "Atención BOX" },
           new SelectListItem { Value = "5", Text = "Doctor" },
           new SelectListItem { Value = "6", Text = "Usuario Comun"}
       };

            ViewBag.lista = listaUsr;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }


    }
}