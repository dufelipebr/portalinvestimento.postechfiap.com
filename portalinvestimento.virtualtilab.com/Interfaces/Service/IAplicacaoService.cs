using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces.Service
{
    public interface IAplicacaoService
    {
        public string Create(Ativo obj);
        public Transacao CriarAplicacao(Portfolio usuario, Ativo inv, CadastrarAplicacaoDTO dto);
    }
}
