using Microsoft.EntityFrameworkCore;
using U5_Proyecto_Blog.Models.Entities;

namespace U5_Proyecto_Blog.Repositories;

public class PostRepository : Repository<Post>
{
    public PostRepository(BlogContext context) : base(context) { }

    public override IEnumerable<Post> GetAll() => Context.Post.Include(p => p.IdCreadorNavigation);

    public Post? GetByTitulo(string titulo) => Context.Post
        .Include(p => p.Postcategoria)
        .ThenInclude(p => p.IdCategoriaNavigation)
        .FirstOrDefault(p => p.Titulo == titulo);

    public IEnumerable<Post> Search(string titulo) => Context.Post
        .Include(p => p.IdCreadorNavigation)
        .Where(p => p.Titulo.Contains(titulo));

    public bool Exist(string titulo) => Context.Post.Any(p => p.Titulo == titulo);
}
