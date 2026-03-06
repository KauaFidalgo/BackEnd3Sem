using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Interface
{
    public interface IFilmesRepository
    {
        void Cadastrar(Filme novoFilme);
        List<Filme> Listar();
        void AtualizarIdCorpo(Filme filmeAtualizado);
        void AtualizarIdUrl(Guid id, Filme filmeAtualizado);
        void Deletar(Guid id);
        Filme BuscarPorId(Guid id);
    }
}
