using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface ITipoContatoRepository
{
        void Cadastrar(TipoContato tipoContato);
        List<TipoContato> Listar();
        TipoContato BuscarPorId(Guid id);
        void Atualizar(Guid id, TipoContato tipoContato);
        void Deletar(Guid id);
}
