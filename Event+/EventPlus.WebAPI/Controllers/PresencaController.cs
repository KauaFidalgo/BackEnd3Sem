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

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de inscrever um usuário em um evento específico, ou seja, registrar a participação de um usuário em um evento, criando uma nova presença associada ao usuário e ao evento, permitindo que o usuário se inscreva para participar do evento
    /// </summary>
    /// <param name="presenca">Presença a ser inscrita</param>
    /// <returns>Incrição filtrada</returns>
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

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de atualizar a presença de um usuário em um evento específico, ou seja, atualizar as informações de uma presença existente, como a situação da presença, permitindo modificar o status de participação de um usuário em um evento
    /// </summary>
    /// <param name="id">id a ser filtrado</param>
    /// <param name="presenca">Presença a ser atualizada</param>
    /// <returns>Presenças atualizadas</returns>
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

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de deletar uma presença específica, ou seja, remover a participação de um usuário em um evento, excluindo a presença associada ao usuário e ao evento
    /// </summary>
    /// <param name="id">Id a ser removido</param>
    /// <returns>Status code 200</returns>
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

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de buscar uma presença específica por seu id, ou seja, retornar as informações detalhadas de uma presença específica com base no seu identificador único, permitindo visualizar os detalhes da participação de um usuário em um evento
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Informações da Presença</returns>
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

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de listar as presenças de um evento específico, ou seja, retornar uma lista de presenças associadas a um evento específico, permitindo visualizar os participantes confirmados para esse evento
    /// </summary>
    /// <param name="idEvento">Id do evento filtrado</param>
    /// <returns>As presenças de um evento</returns>
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
