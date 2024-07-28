using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;

namespace investminimalapi.virtualitlab.com.Repository
{
    public abstract class DapperRepository<T> : IRepository<T> where T : Entidade
    {
        private readonly string? _connectionString;
        protected string ConnectionString => _connectionString;

        public DapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:ConnectionString");
        }
        public abstract void Alterar(T entidade);
        public abstract void Cadastrar(T entidade);
        public abstract void Deletar(T entidade);
        public abstract T ObterPorId(int id);
        public abstract IList<T> ObterTodos();
        
    }
}
