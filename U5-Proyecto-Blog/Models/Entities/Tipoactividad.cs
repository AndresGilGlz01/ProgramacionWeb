using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Tipoactividad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Actividad> Actividad { get; set; } = new List<Actividad>();
}
