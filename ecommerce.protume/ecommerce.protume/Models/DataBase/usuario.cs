//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ecommerce.protume.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuario
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public Nullable<int> dni { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
    }
}