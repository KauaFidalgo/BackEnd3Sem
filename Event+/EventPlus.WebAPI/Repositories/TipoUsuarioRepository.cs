using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    //Injeção de dependência do contexto
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de usuário existente no banco de dados
    /// </summary>
    /// <param name="id">id do tipo usuario a ser atualizado</param>
    /// <param name="tipoUsuario">Novos dados do tipo usuario</param>
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de usuário por seu id
    /// </summary>
    /// <param name="id">id do usuario procurado</param>
    /// <returns>Objeto do tipoUsuario com o tipo usuario buscado</returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de usuário no banco de dados
    /// </summary>
    /// <param name="tipoUsuario">tipo usuario a ser cadastrado</param>

    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um tipo de usuário do banco de dados com base no id fornecido
    /// </summary>
    /// <param name="id">id do tipoUsuario a ser deletado</param>

    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios
            .OrderBy(TipoUsuario => TipoUsuario.Titulo)
            .ToList();
    }
}
