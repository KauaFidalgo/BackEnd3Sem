namespace EventPlus.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public Guid IdUsuario { get; set; }
    public Guid IdEvento { get; set; }
    public string? Descricao { get; set; }
}
