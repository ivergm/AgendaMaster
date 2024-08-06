using AgendaMaster.Entidades;
using AgendaMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaMaster.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioM = new UsuarioModel();

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(Usuario entidad)
        {
            var usuario = usuarioM.IniciarSesion(entidad);

            if (usuario != null)
            {
                if (usuario.Rol == "usuario")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (usuario.Rol == "admin")
                {
                    return RedirectToAction("indexAdmin", "Home");
                }
            }
            ViewBag.msj = "Su información no es correcta";
            return View();
        }

        [HttpGet]
            public ActionResult Registro()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Registro(Usuario entidad)
            {
                var respuesta = usuarioM.RegistrarUsuario(entidad);

                if (respuesta)
                    return RedirectToAction("IniciarSesion", "Usuario");
                else
                {
                    ViewBag.msj = "Su información no se ha registrado";
                    return View();
                }
            }


        }
    }
