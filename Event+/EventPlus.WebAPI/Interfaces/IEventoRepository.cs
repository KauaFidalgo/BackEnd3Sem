using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
        void Cadastrar(Evento evento);
        void Atualizar(Guid id, Evento evento);
        void Deletar(Guid id);
        Evento BuscarPorId(Guid id);
        List<Evento> Listar();
        List<Evento> ListarProximos();
        List<Evento> ListarPorId(Guid IdUsuario);
}
