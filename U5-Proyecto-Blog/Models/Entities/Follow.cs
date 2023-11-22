using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Follow
{
    public int Id { get; set; }

    public int IdSeguido { get; set; }

    public int IdSeguidor { get; set; }

    public virtual Usuario IdSeguidoNavigation { get; set; } = null!;

    public virtual Usuario IdSeguidorNavigation { get; set; } = null!;
}
