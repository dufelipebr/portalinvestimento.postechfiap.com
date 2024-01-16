using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
