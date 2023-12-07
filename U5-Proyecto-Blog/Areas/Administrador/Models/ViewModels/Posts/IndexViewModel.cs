namespace U5_Proyecto_Blog.Areas.Administrador.Models.ViewModels.Posts;

public class IndexViewModel
{
    public IEnumerable<PostModel> Posts { get; set; } = Enumerable.Empty<PostModel>();
}
