//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgendaMaster.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proyectos
    {
        public long IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; }
        public Nullable<long> IdUsuario { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
