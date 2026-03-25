using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO;

public class TipoContatoDTO
{
    [Required(ErrorMessage ="O Titulo é obrigatorio!")]
    public string? Titulo { get; set; }
}
