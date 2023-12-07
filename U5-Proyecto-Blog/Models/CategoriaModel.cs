using U5_Proyecto_Blog.Areas.Administrador.Models;

namespace U5_Proyecto_Blog.Models;

public class CategoriaModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = string.Empty;
    public IEnumerable<PostModel> Posts { get; set; } = Enumerable.Empty<PostModel>();
}
