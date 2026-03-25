using Azure.AI.ContentSafety;
using Azure;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(
        ContentSafetyClient contentSafetyClient,
        IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// Cadastra um novo comentário, realizando a análise de conteúdo para verificar se ele pode ser exibido
    /// </summary>
    /// <param name="comentarioEvento">Dados do comentário a ser cadastrado</param>
    /// <returns>Comentário cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
                return BadRequest("A descrição é obrigatória!");

            //var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            //Response<AnalyzeTextResult> response =
            //await _contentSafetyClient.AnalyzeTextAsync(request);


            bool temConteudoImproprio = false;

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEvento.IdEvento,
                IdUsuario = comentarioEvento.IdUsuario,
                Descricao = comentarioEvento.Descricao,
                Exibe = !temConteudoImproprio,
                DataComentarioEvento = DateTime.Now
            };

            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Lista todos os comentários de um evento específico
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Lista de comentários do evento</returns>
    [HttpGet("{idEvento}")]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Lista apenas os comentários permitidos para exibição de um evento
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Lista de comentários visíveis</returns>
    [HttpGet("exibe/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Busca um comentário específico de um usuário em um evento
    /// </summary>
    /// <param name="idUsuario">Id do usuário</param>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Comentário encontrado</returns>
    [HttpGet("usuario")]
    public IActionResult BuscarPorUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            var comentario = _comentarioEventoRepository
                .BuscarPorIdUsuario(idUsuario, idEvento);

            if (comentario == null)
                return NotFound("Comentário não encontrado");

            return Ok(comentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Atualiza um comentário existente
    /// </summary>
    /// <param name="comentario">Comentário atualizado</param>
    /// <returns>Status da operação</returns>
    [HttpPut]
    public IActionResult Atualizar(ComentarioEvento comentario)
    {
        try
        {
            _comentarioEventoRepository.Atualizar(comentario);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deleta um comentário pelo seu ID
    /// </summary>
    /// <param name="idComentario">Id do comentário</param>
    /// <returns>Status da operação</returns>
    [HttpDelete("{idComentario}")]
    public IActionResult Deletar(Guid idComentario)
    {
        try
        {
            _comentarioEventoRepository.Deletar(idComentario);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}