using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.Interfaces
{
    public interface IRepository<T> where T: Entidade
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(T entidade);
    }
}
