using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
