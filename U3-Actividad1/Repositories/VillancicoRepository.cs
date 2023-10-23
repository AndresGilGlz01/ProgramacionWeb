using Villancicos.Models;

namespace U3_Actividad1.Repositories;

public class VillancicoRepository
{
    villancicosContext _context;

    public VillancicoRepository(villancicosContext context) { _context = context; }

    public void Add(Villancico villancico)
    {
        _context.Add(villancico);
        _context.SaveChanges();
    }

    public IEnumerable<Villancico> Get() => _context.Villancico.OrderBy(v => v.Nombre);

    public Villancico? Get(int Id) => _context.Villancico.Find(Id);

    public Villancico? Get(string Nombre) => _context.Villancico.FirstOrDefault(v => v.Nombre == Nombre);

    public void Update(Villancico villancico)
    {
        _context.Update(villancico);
        _context.SaveChanges();
    }

    public void Delete(Villancico villancico)
    {
        _context.Remove(villancico);
        _context.SaveChanges();
    }
}
