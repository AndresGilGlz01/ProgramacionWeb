using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Postcategoria> Postcategoria { get; set; } = new List<Postcategoria>();
}
