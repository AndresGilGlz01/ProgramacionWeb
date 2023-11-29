using System;
using System.Collections.Generic;

namespace U3_Actividad2.Models.Entities
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int Rol { get; set; }
    }
}
