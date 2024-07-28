using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces.Repository
{
    public interface IAplicacaoRepository : IRepository<Transacao>
    {
        public Transacao ObterAplicacao(int user_Id, int investimento_Id);

        public List<Transacao> ObterAplicacaoPorUserId(int user_Id);
    }
}
