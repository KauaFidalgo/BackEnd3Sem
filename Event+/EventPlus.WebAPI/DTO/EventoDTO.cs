using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O nome do evento é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição do evento é obrigatória!")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A data do evento é obrigatória!")]
    public DateTime DataEvento { get; set; }

    [Required(ErrorMessage = "O tipo do evento é obrigatório!")]
    public Guid IdTipoEvento { get; set; }  

    [Required(ErrorMessage = "A instituição é obrigatória!")]
    public Guid IdInstituicao { get; set; }
}