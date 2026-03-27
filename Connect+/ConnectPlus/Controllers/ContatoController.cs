using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using ConnectPlus.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IWebHostEnvironment _env;

    public ContatoController(IContatoRepository contatoRepository, IWebHostEnvironment env)
    {
        _contatoRepository = contatoRepository;
        _env = env;
    }

    [HttpPost]
    public IActionResult Cadastrar([FromForm] ContatoDTO Contato)
    {
        try
        {
            string nomeArquivo = string.Empty;

            // Lógica para salvar a imagem física
            if (Contato.CaminhoImagem != null)
            {
                var pasta = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);

                nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(Contato.CaminhoImagem.FileName);
                var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    Contato.CaminhoImagem.CopyTo(stream);
                }
            }

            var novoContato = new Contato
            {
                Nome = Contato.Nome!,
                FormaContato = Contato.FormaContato,
                CaminhoImagem = nomeArquivo, // Aqui salva APENAS a string (o nome da foto)
                IdTipoContato = Contato.IdTipoContato // Não esqueça da chave estrangeira do DTO
            };

            _contatoRepository.Cadastrar(novoContato);
            return StatusCode(201, novoContato);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_contatoRepository.Listar());
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
            return Ok(_contatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id,[FromForm] ContatoDTO contatoAtualizado)
    {
        var contato = _contatoRepository.BuscarPorId(id);
        if (contato == null)
        {
            return NotFound();
        }
        _contatoRepository.Atualizar(id, contato);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _contatoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
