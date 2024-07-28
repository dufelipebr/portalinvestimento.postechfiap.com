using portalinvestimento.virtualtilab.com.DTO;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class RentabilidadeInvestimento : Entidade
    {
        public Ativo Investimento { get; set; }
        public DateTime Data_Operacao { get; set; }
        public Decimal Valor_Rentabilidade { get; set; }

        public RentabilidadeInvestimento()
        {

        }

        public RentabilidadeInvestimento(CadastrarRentabilidadeDTO cad) 
        {
            Investimento = new Ativo() { Id = cad.Id_Investimento };
            Data_Operacao = cad.Data_Operacao;
            Valor_Rentabilidade = cad.Valor_Rentabilidade;
        }

        public override void ValidateEntity()
        {

            //AssertionConcern.AssertArgumentNotEmpty(Codigo, "Codigo precisa ser preenchido.");
            //AssertionConcern.AssertArgumentLength(Codigo, 50, "Codigo do Investimento precisa ter no maximo 50 caracteres.");

            //AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            //AssertionConcern.AssertArgumentLength(Nome, 100, "Nome do Investimento precisa ter no maximo 100 caracteres.");

            //AssertionConcern.AssertArgumentNotEquals(TipoInvestimento, 0, "Tipo Investimento precisa ser preenchido");

            //AssertionConcern.AssertArgumentRange((double)TaxaADM, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
            //AssertionConcern.AssertArgumentRange((double)AporteMinimo, 0.1, 1000000, "Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00");


            //throw new NotImplementedException();
        }

    }
}
