using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um evento existente no banco de dados, buscando pelo id
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="evento">Evento</param>
    public void Atualizar(Guid id, Evento evento)
    {
        Evento eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento;
            eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
            eventoBuscado.IdInstituicao = evento.IdInstituicao;

            _context.Eventos.Update(eventoBuscado);
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Busca um evento pelo seu id, incluindo as informações de tipo de evento e instituição relacionadas
    /// </summary>
    /// <param name="id">Id do evento a ser buscado</param>
    /// <returns>Tipo de evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .FirstOrDefault(e => e.IdEvento == id);
    }

    /// <summary>
    /// Cadastra um novo evento no banco de dados, adicionando o evento ao contexto e salvando as alterações.
    /// </summary>
    /// <param name="evento">Nome do Evento a ser cadastrado</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um evento do banco de dados, buscando pelo id e removendo o evento encontrado do contexto, em seguida salvando as alterações
    /// </summary>
    /// <param name="id">Id do evento a ser deletado</param>
    public void Deletar(Guid id)
    {
        Evento eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista todos os eventos do banco de dados
    /// </summary>
    /// <returns>Eventos Listados</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .ToList();
    }


    /// <summary>
    /// Metodo que lista os eventos que o usuario esta presente, ou seja, os eventos que ele confirmou presença
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuario</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }

    /// <summary>
    /// Método que lista os eventos futuros, ou seja, os eventos que ainda não aconteceram, ordenados pela data do evento
    /// </summary>
    /// <returns>Lista de próximos eventos</returns>
    public List<Evento> ListarProximos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
