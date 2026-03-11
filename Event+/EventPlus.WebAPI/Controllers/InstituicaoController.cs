using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamadas para o método de lista as instituições
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamadas para o método de buscar uma instituição especifica
    /// </summary>
    /// <param name="id">Id da instituição a ser buscada</param>
    /// <returns>Status code 200 e a instituição encontrada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamadas para o método de cadastrar uma nova instituição
    /// </summary>
    /// <param name="instituicao">Tipo de instituição a ser cadastrada</param>
    /// <returns>Status code 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Endereco = instituicao.Endereco!,
                Cnpj = instituicao.Cnpj!
            };

            _instituicaoRepository.Cadastrar(novaInstituicao);

            return StatusCode(201, novaInstituicao);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamadas para o método de atualizar uma instituição especifica
    /// </summary>
    /// <param name="id">Id da instituição a ser atualizada</param>
    /// <param name="instituicao">Dados da instituição a ser atualizada</param>
    /// <returns>Status code 204</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Endereco = instituicao.Endereco!,
                Cnpj = instituicao.Cnpj!
            };
            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamadas para o método de deletar uma instituição especifica
    /// </summary>
    /// <param name="id">Id da instituição a ser excluida</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
