using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuario e obrigatorio!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha do usuario e obrigatoria!")]
    public string? Senha { get; set; }
}
