using FilmesMoura.WebAPI.DTO;
using FilmesMoura.WebAPI.Interface;
using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FilmesMoura.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválidos!");
            }

            //Caso encontre o usuario, prossegue para a criação do token

            //1 - Criar as claims (informações do usuário que serão armazenadas no token)

            var claims = new[]
            {
                //Formato da claim 
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!)
                
                //existe a possibilidade de criar uma claim personalizada, por exemplo, para armazenar o nome do usuário
                //new Claim("nome perso", "Valor da Claim")
            };

            //2 - Gerar a chave de acesso ao token (segredo)
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //3 - Definir a credencial de acesso ao token (header)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4 - Gerar o token
            var token = new JwtSecurityToken
            (
                issuer: "api_filmes", //Emissor do token
                audience: "api_filmes", //Destinatário do token
                claims: claims, //Informações do usuário
                expires: DateTime.Now.AddMinutes(5), //Tempo de expiração do token
                signingCredentials: creds //Credenciais de acesso ao token
            );

            //5 - Retornar o token para o cliente
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
}
