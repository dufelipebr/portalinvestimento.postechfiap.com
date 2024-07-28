using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces.Repository
{
    public interface IRepository<T> where T : Entidade
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(T entidade);
    }
}
