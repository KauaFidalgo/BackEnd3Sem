using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    [Required (ErrorMessage = "O nome do usuario é obrigatorio!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email do usuario é obrigatorio!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha do usuario é obrigatoria!")]
    public string? Senha { get; set; }

    public Guid IdTipoUsuario { get; set; }
}
