namespace ConnectPlus.DTO;

public class ContatoDTO
{
    public string? Nome { get; set; }
    public string? FormaContato { get; set; }
    public IFormFile CaminhoImagem { get; set; }
    public Guid IdTipoContato { get; set; }
}
