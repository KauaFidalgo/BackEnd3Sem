using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca o usuário pelo email e senha fornecidos, incluindo a navegação para o tipo de usuário associado
    /// </summary>
    /// <param name="Email">Email do usuario</param>
    /// <param name="Senha">Senha do usuario</param>
    /// <returns>Usuario buscado e validado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        // Primeiro, buscamos o usuário pelo email
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation) // Inclui a navegação para o tipo de usuário
            .FirstOrDefault(usuario => usuario.Email == Email);

        //Verifica se o usuário foi encontrado e se a senha corresponde
        if (usuarioBuscado != null)
        {
            bool confere = Criptografia.CompararHash(Senha,
                usuarioBuscado.Senha); //Comparamos o hash da senha fornecida com o hash armazenado

            if (confere)
            {
                return usuarioBuscado; //Se a senha estiver correta, retornamos o usuário
            }
        }

        return null!; //Se o usuário não for encontrado ou a senha estiver incorreta, retornamos null
    }

    /// <summary>
    /// Busca um usuário por seu ID, incluindo a navegação para o tipo de usuário associado.
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser buscado</param>
    /// <returns>Usuario Buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
    }

    /// <summary>
    /// Cadastra um novo usuário no banco de dados, criptografando a senha antes de salvar.
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
       usuario.Senha = Criptografia.GerarHash(usuario.Senha);

       _context.Usuarios.Add(usuario);
       _context.SaveChanges();
    }
}
