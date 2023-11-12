using TurneroFaeracWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurneroFaeracWeb.Datos
{
    public class DBUsuario
    {
        public static bool Registrar(UsuarioCLS oUsuarioCLS)
        {
            // Verifica si el correo ya está registrado
            var usuarioExistente = Obtener(oUsuarioCLS.Correo);
            if (usuarioExistente != null)
            {
                return false; // El correo ya está registrado
            }

            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    Usuarios oUsuario = new Usuarios
                    {
                        Usuario = oUsuarioCLS.Usuario,
                        Correo = oUsuarioCLS.Correo,
                        Contraseña = oUsuarioCLS.Contraseña,

                        Token = oUsuarioCLS.Token,
                        IdTipo = 6
                    };
                    db.Usuarios.Add(oUsuario);
                    db.SaveChanges();
                    return true; // Registro exitoso
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UsuarioCLS Validar(string correo, string clave)
        {
            UsuarioCLS usuario = null;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuarioDB = db.Usuarios
                        .Where(u => u.Correo == correo && u.Contraseña == clave)
                        .FirstOrDefault();

                    if (usuarioDB != null)
                    {
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
                        usuario = new UsuarioCLS
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

        public static bool RestablecerActualizar(string clave, string token)
        {
            bool respuesta = false;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuarioDB = db.Usuarios
                        .Where(u => u.Token == token)
                        .FirstOrDefault();

                    if (usuarioDB != null)
                    {

                        usuarioDB.Contraseña = clave;
                        db.SaveChanges();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

        public static bool Confirmar(string token)
        {
            bool respuesta = false;
            try
            {
                using (var db = new TurneroFaeracEntities())
                {
                    var usuarioDB = db.Usuarios
                        .Where(u => u.Token == token)
                        .FirstOrDefault();

                    if (usuarioDB != null)
                    {

                        db.SaveChanges();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

    }
}