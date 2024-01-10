using investminimalapi.virtualitlab.com.Interfaces;
using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario ObterPorNomeUsuarioESenha(string email, string senha);
    }
}
