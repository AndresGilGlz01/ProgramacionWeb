﻿using System;
using System.Collections.Generic;

namespace U3_Actividad2.Models.Entities
{
    public partial class Productos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Precio { get; set; }
        public string? UnidadMedida { get; set; }
        public string? Descripcion { get; set; }

        public virtual Categorias? IdCategoriaNavigation { get; set; }
    }
}
