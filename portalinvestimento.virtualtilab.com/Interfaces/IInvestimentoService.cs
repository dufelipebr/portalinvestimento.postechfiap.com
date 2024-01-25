using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces
{
    public interface IInvestimentoService
    {
        public string Create(Investimento obj);
        public Aplicacao CriarAplicacao(Usuario usuario, Investimento inv, CadastrarAplicacaoDTO dto);
    }
}
