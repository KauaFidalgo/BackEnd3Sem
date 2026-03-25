namespace ConnectPlus.DTO;

public class ContatoDTO
{
    public string? Nome { get; set; }
    public string? DadosDoContato { get; set; }
    public IFormFile CaminhoImagem { get; set; }
    public int IdTipoContato { get; set; }
}
