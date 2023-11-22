using System;
using System.Collections.Generic;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class Post
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public DateTime FechaPublicacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public string Contenido { get; set; } = null!;

    public int IdCreador { get; set; }

    public virtual ICollection<Actividad> Actividad { get; set; } = new List<Actividad>();

    public virtual Usuario IdCreadorNavigation { get; set; } = null!;

    public virtual ICollection<Postcategoria> Postcategoria { get; set; } = new List<Postcategoria>();

    public virtual ICollection<Reporte> Reporte { get; set; } = new List<Reporte>();
}
