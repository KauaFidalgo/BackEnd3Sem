using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    //Injeção de dependência do contexto
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    ///  Atualiza uma instituição existente no banco de dados, buscando-a pelo ID e atualizando seu nome, caso seja encontrada.
    /// </summary>
    /// <param name="id">id da instituicao a atualizar</param>
    /// <param name="instituicao">Nivis dados da instituicao</param>

    public void Atualizar(Guid IdInstituicao, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(IdInstituicao);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.Cnpj = instituicao.Cnpj;
            instituicaoBuscada.Endereco = instituicao.Endereco;
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de instituicao pelo id
    /// </summary>
    /// <param name="id">id da instituicao a ser buscado</param>
    /// <returns>Objeto instituicao com as informacoes da instituicao buscada</returns>

    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de instituicao
    /// </summary>
    /// <param name="instituicao">tipo de evento a ser cadastrado</param>

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta uma instituição existente no banco de dados, buscando-a pelo ID e removendo-a caso seja encontrada.
    /// </summary>
    /// <param name="id">id da instituicao a ser deletada</param>

    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos
            .OrderBy(Instituicao => Instituicao.NomeFantasia)
            .ToList();
    }
}