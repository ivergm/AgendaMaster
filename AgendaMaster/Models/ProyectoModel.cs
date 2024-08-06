using AgendaMaster.BaseDatos;
using AgendaMaster.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaMaster.Models
{
    public class ProyectoModel
    {

        public bool RegistrarProyecto(Proyecto entidad)
        {
            using (var context = new AgendaMasterEntities())
            {


                var tabla = new BaseDatos.Proyectos();
                tabla.NombreProyecto = entidad.NombreProyecto;
                tabla.Descripcion = entidad.Descripcion;
                tabla.FechaCreacion = entidad.FechaCreacion;
                tabla.Estatus = entidad.Estatus;


                context.Proyectos.Add(tabla);
                context.SaveChanges();
            }
            return true;
        }
        public List<BaseDatos.Proyectos> ConsultarProyecto()
        {
            using (var context = new AgendaMasterEntities())
            {
                return context.Proyectos                     
                      .ToList();
            }
        }
    }
}