using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class PresencaDTO
{
    [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
    public bool Situacao { get; set; }

    [Required(ErrorMessage = "O campo 'IdUsuario' é obrigatório.")]
    public Guid IdUsuario { get; set; }

    [Required(ErrorMessage = "O campo 'IdEvento' é obrigatório.")]
    public Guid IdEvento { get; set; }
}
