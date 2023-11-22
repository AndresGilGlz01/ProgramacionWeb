using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Reporte
{
    public int Id { get; set; }

    public string Motivo { get; set; } = null!;

    public int IdPost { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;
}
