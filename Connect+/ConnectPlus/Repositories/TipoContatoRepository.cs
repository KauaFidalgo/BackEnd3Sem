using ConnectPlus.BdContextEvent;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly EventContext _context;

    public TipoContatoRepository(EventContext context)
    {
        _context = context;
    }

    public void Cadastrar(TipoContato tipoContato)
    {
        _context.TipoContatos.Add(tipoContato);
        _context.SaveChanges();
    }

    public List<TipoContato> Listar()
    {
        return _context.TipoContatos.ToList();
    }

    public TipoContato BuscarPorId(Guid id)
    {
        return _context.TipoContatos.Find(id)!;
    }

    public void Atualizar(Guid id, TipoContato tipoContatoAtualizado)
    {
        var tipoContato = _context.TipoContatos.Find(id);

        if (tipoContato != null)
        {
            tipoContato.Titulo = tipoContatoAtualizado.Titulo;

            _context.TipoContatos.Update(tipoContato);
            _context.SaveChanges();
        }
    }

    public void Deletar(Guid id)
    {
        var tipoContato = _context.TipoContatos.Find(id);

        if (tipoContato != null)
        {
            _context.TipoContatos.Remove(tipoContato);
            _context.SaveChanges();
        }
    }
}
