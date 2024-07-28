using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario ObterPorNomeUsuarioESenha(string email, string senha);
    }
}
