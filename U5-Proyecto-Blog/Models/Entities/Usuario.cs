using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public ulong EmailConfirmed { get; set; }

    public ulong Activo { get; set; }

    public string Password { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public int? IdRol { get; set; }

    public virtual ICollection<Follow> FollowIdSeguidoNavigation { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowIdSeguidorNavigation { get; set; } = new List<Follow>();

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Post> Post { get; set; } = new List<Post>();
}
