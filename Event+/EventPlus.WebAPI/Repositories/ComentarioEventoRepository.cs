using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;

    // Injeção de dependência do contexto
    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }


    public void Atualizar(ComentarioEvento comentarioEvento)
    {
        var comentarioExistente = _context.ComentarioEventos.Find(comentarioEvento.IdComentarioEvento);

        if (comentarioExistente != null)
        {
            comentarioExistente.Descricao = comentarioEvento.Descricao;
            comentarioExistente.DataComentarioEvento = comentarioEvento.DataComentarioEvento;
            comentarioExistente.Exibe = comentarioEvento.Exibe;
            comentarioExistente.IdEvento = comentarioEvento.IdEvento;
            comentarioExistente.IdUsuario = comentarioEvento.IdUsuario;

            _context.ComentarioEventos.Update(comentarioExistente);
            _context.SaveChanges();
        }
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid IdComentarioEvento)
    {
        var comentarioBuscado = _context.ComentarioEventos.Find(IdComentarioEvento);

        if (comentarioBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioBuscado);
            _context.SaveChanges();
        }
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .Where(c => c.IdEvento == IdEvento)
            .ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .Where(c => c.IdEvento == IdEvento && c.Exibe == true)
            .ToList();
    }
}