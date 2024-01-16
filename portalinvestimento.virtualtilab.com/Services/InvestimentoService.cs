using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;

namespace portalinvestimento.virtualtilab.com.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        public string Create(Investimento obj)
        {
            if (obj.Codigo == null || obj.Codigo.Length < 9)
                return "O codigo não pode ser menor 8 caracteres";

            if(obj.TaxaADM <= 0)
                return "A Taxa de AM não pode ser menor que 0";

            if (obj.TipoInvestimento == 0)
                return "A Tipo de Investimento não foi informado";

            if(obj.AporteMinimo == 0)
                return "Aporte minimo deve ser informado";

            if (obj.Nome == "" || obj.Nome.Length <10)
                return "O nome do investimento deve ser maior que 10 caracteres";

            return "";
        }

    }
}
