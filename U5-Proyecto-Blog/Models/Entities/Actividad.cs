using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Actividad
{
    public int Id { get; set; }

    public int IdPost { get; set; }

    public int IdTipo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual Tipoactividad IdTipoNavigation { get; set; } = null!;
}
