using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private readonly ITipoContatoRepository _tipoContatoRepository;

    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoContatoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoContatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
     [HttpPost]
     public IActionResult Cadastrar(TipoContatoDTO tipoContato)
     {
            try
            {
                var novoTipoContato = new TipoContato
                {
                    Titulo = tipoContato.Titulo!
                };
    
                _tipoContatoRepository.Cadastrar(novoTipoContato);
    
                return StatusCode(201, novoTipoContato);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
     }
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContato)
    {
        try
        {
            var TipoContatoAtualizado = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };

            _tipoContatoRepository.Atualizar(id, TipoContatoAtualizado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoContatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpGet("contatos/{id}")]
    public IActionResult ListarContatosPorTipoContato(Guid id)
    {
        try
        {
            var tipoContato = _tipoContatoRepository.BuscarPorId(id);
            if (tipoContato == null)
            {
                return NotFound("Tipo de contato não encontrado.");
            }
            return Ok(tipoContato.Contatos);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
