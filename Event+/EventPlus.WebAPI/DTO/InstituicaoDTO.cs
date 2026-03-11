using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O nome da instituição é obrigatório!")]
    public string? NomeFantasia { get; set; }


    [Required(ErrorMessage = "O CNPJ da instituição é obrigatório!")]
    public string? Cnpj { get; set; }


    [Required(ErrorMessage = "O endereço da instituição é obrigatório!")]
    public string? Endereco { get; set; }
}