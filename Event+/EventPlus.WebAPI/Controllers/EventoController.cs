using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// Lista todos os eventos cadastrados, retornando uma resposta HTTP 200 (OK)
    /// </summary>
    /// <returns>Eventos listados</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Busca um evento por ID, verificando se ele existe antes de tentar retornar os dados
    /// </summary>
    /// <param name="id">id do evento a ser buscado</param>
    /// <returns>Evento buscado por ID</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            Evento evento = _eventoRepository.BuscarPorId(id);

            if (evento == null)
                return NotFound("Evento não encontrado");

            return Ok(evento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo evento, validando os dados de entrada e retornando o status apropriado
    /// </summary>
    /// <param name="novoEvento">Novo evento a ser cadastrado</param>
    /// <returns>Evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(EventoDTO eventoDTO)
    {

        try
        {

            var novoEvento = new Evento
            {
                Nome = eventoDTO.Nome!,
                Descricao = eventoDTO.Descricao!,
                DataEvento = eventoDTO.DataEvento!,
                IdInstituicao = eventoDTO.IdInstituicao!,
                IdTipoEvento = eventoDTO.IdTipoEvento!
            };

            _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201, novoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Atualiza um evento existente por ID, verificando se ele existe antes de tentar atualizar
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="eventoAtualizado">Evento atualizado</param>
    /// <returns>Evento Atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, EventoDTO evento)
    {
        try
        {

            var eventoAtualizado = new Evento
            {
                Nome = evento.Nome!,
                Descricao = evento.Descricao!,
                DataEvento = evento.DataEvento!,
                IdInstituicao = evento.IdInstituicao!,
                IdTipoEvento = evento.IdTipoEvento!
            };


            _eventoRepository.Atualizar(id, eventoAtualizado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Deleta um evento por ID, verificando se ele existe antes de tentar deletar
    /// </summary>
    /// <param name="id">Id do evento a ser deletado</param>
    /// <returns>Evento Id Deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Lista os eventos de um usuário específico
    /// </summary>
    /// <param name="idUsuario">Id do usuario para ver seus eventos</param>
    /// <returns>Lista dos Eventos deste IdUsuario</returns>
    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid idUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Lista os próximos eventos (com data futura)
    /// </summary>
    /// <returns>Lista dos proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}