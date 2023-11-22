using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Postcategoria
{
    public int Id { get; set; }

    public int IdCategoria { get; set; }

    public int IdPost { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Post IdPostNavigation { get; set; } = null!;
}
