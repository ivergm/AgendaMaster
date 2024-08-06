using AgendaMaster.Entidades;
using AgendaMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaMaster.Controllers
{
    public class ProyectoController : Controller

        
    {
        ProyectoModel proyectoM = new ProyectoModel();

        [HttpGet]
        public ActionResult RegistroProyecto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroProyecto(Proyecto entidad)
        {
            if (ModelState.IsValid)
            {
                var respuesta = proyectoM.RegistrarProyecto(entidad);

                if (respuesta)
                {
                    ViewBag.Message = "Proyecto registrado exitosamente.";
                }
                else
                {
                    ModelState.AddModelError("NombreProyecto", "El proyecto no ha sido registrado");

                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarProyecto()
        {
            var respuesta = proyectoM.ConsultarProyecto();
            return View(respuesta);
        }
    }
}
