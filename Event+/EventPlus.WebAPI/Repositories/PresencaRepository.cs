using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{

    private readonly EventContext _context;

    public PresencaRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metodo que alterna a situção da presença
    /// </summary>
    /// <param name="id">id da presenca a ser alterada</param>
    public void Atualizar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    /// <summary>
    /// Deleta uma presença por id
    /// </summary>
    /// <param name="id">id da Presenca a ser deletado</param>
    public void Deletar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            _context.Presencas.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Inscreve um usuário em um evento, verificando se o usuário já está inscrito para evitar duplicidade
    /// </summary>
    /// <param name="presenca"> Nome a ser inscrito </param>
    public void Inscrever(Presenca presenca)
    {
        // Verifica se o usuário já está inscrito no evento
        var presencaExistente = _context.Presencas
            .FirstOrDefault(p => p.IdUsuario == presenca.IdUsuario
                              && p.IdEvento == presenca.IdEvento);

        if (presencaExistente != null)
        {
            throw new Exception("Usuário já inscrito neste evento.");
        }

        // Define situação padrão (ex: inscrito = true)
        presenca.Situacao = true;

        _context.Presencas.Add(presenca);
        _context.SaveChanges();
    }

    /// <summary>
    /// Metodo que lista as presença de um usuario especifico
    /// </summary>
    /// <param name="IdUsuario">id do usuario para filtragem</param>
    /// <returns>Lista de presenças de um usuario</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }

    /// <summary>
    /// Lista todas as presenças de um usuário específico, incluindo detalhes do evento e da instituição associada a cada presença.
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser listado</param>
    /// <returns></returns>
    public List<Presenca> Listar(Guid IdUsuario)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
