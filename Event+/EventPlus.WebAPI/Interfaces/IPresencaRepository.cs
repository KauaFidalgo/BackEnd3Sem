using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
        void Inscrever(Presenca presenca);
        void Atualizar(Guid id);
        void Deletar(Guid id);
        Presenca BuscarPorId(Guid id);
        List<Presenca> ListarMinhas(Guid IdUsuario);
        List<Presenca> Listar(Guid IdUsuario);
}
