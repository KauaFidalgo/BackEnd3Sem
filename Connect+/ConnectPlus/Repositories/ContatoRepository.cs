using ConnectPlus.BdContextEvent;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly EventContext _context;

    public ContatoRepository(EventContext context)
    {
        _context = context;
    }

    public void Cadastrar(Contato contato)
    {
        _context.Contatos.Add(contato);
        _context.SaveChanges();
    }

    public List<Contato> Listar()
    {
        return _context.Contatos.ToList();
    }

    public void Atualizar(Guid id, Contato contatoAtualizado)
    {
        var contato = _context.Contatos.Find(id);

        if (contato != null)
        {
            contato.Nome = contatoAtualizado.Nome!;
            contato.FormaContato = contatoAtualizado.FormaContato!;
            contato.CaminhoImagem = contatoAtualizado.CaminhoImagem!;
            contato.IdTipoContato = contatoAtualizado.IdTipoContato;

            _context.Contatos.Update(contato);
            _context.SaveChanges();
        }
    }

    public void Deletar(Guid id)
    {
        var contato = _context.Contatos.Find(id);

        if (contato != null)
        {
            _context.Contatos.Remove(contato);
            _context.SaveChanges();
        }
    }

    public Contato BuscarPorId(Guid id)
    {
        return _context.Contatos.Find(id)!;
    }
}
