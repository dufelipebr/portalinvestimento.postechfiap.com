using investminimalapi.virtualitlab.com.Interfaces;
using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces
{
    public interface IAplicacaoRepository : IRepository<Aplicacao>
    {
        public Aplicacao ObterAplicacao(int user_Id, int investimento_Id);

        public List<Aplicacao> ObterAplicacaoPorUserId(int user_Id);
    }
}
