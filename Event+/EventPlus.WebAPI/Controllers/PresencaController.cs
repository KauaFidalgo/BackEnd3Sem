using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de listar as presenças de um usuário específico
    /// </summary>
    /// <param name="idUsuario">Id do usuario para filtragem</param>
    /// <returns>Status code 200 e uma lista de presença</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento
            };

            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presenca)
    {
        try
        {
            var presencaExistente = _presencaRepository.BuscarPorId(id);
            if (presencaExistente == null)
            {
                return NotFound("Presença não encontrada.");
            }
            presencaExistente.Situacao = presenca.Situacao;
            _presencaRepository.Atualizar(id);

            return Ok(presencaExistente);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            var presencaExistente = _presencaRepository.BuscarPorId(id);
            
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            var presenca = _presencaRepository.BuscarPorId(id);
            
            return Ok(presenca);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListarMinhas/{idEvento}")]
    public IActionResult ListarMinhas(Guid idEvento)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
