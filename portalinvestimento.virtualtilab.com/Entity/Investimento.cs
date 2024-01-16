using System.Globalization;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Investimento : Entidade
    {
        public Investimento()
        {

        }
        public Investimento(enTipoInvestimento tipo, string nome, string codigo, decimal taxaADM, decimal aporteMinimo, decimal rent_3, decimal rent_12, decimal rent_24)
        {
            TipoInvestimento = tipo;
            Nome = nome;
            Codigo = codigo;
            TaxaADM = taxaADM;
            AporteMinimo = aporteMinimo;
            RentabilidadeUltimo_3meses = rent_3;
            Rentabilidade_Ultimo_12meses = rent_12;
            Rentabilidade_Ultimo_24meses = rent_24;

            ValidateEntity();

        }
        public enum enTipoInvestimento
        {
            CDB, Acoes, CDI, LDI_LDA, Tesouro
        }
        public enTipoInvestimento TipoInvestimento { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public decimal TaxaADM { get; set; }

        public decimal AporteMinimo { get; set; }

        public decimal RentabilidadeUltimo_3meses { get; set; }
        public decimal Rentabilidade_Ultimo_12meses { get; set; }
        public decimal Rentabilidade_Ultimo_24meses { get; set; }

        // Patrimonio Liquido - Ações 
        // Movimentação - quanto pode movimentar minimo
        //Pagamento Resgate: D+3
        //valor minimo permancia: abaixo desse valor não pode ficar
        //

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Codigo, "Codigo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            AssertionConcern.AssertArgumentLength(Codigo, 10, "Investimento precisa ter no maximo 10 caracteres.");
            AssertionConcern.AssertArgumentLength(Nome, 50, "Nome do Investimento precisa ter no maximo 50 caracteres.");
            AssertionConcern.AssertArgumentRange((double)TaxaADM, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
            AssertionConcern.AssertArgumentRange((double)AporteMinimo, 0.1, 1000000, "Taxa ADM precisa estar entre 0.1 e 10.");
            AssertionConcern.AssertArgumentNotEquals(TipoInvestimento, 0, "Tipo Investimento precisa ser preenchido");
            AssertionConcern.AssertArgumentTrue(Nome.Trim() == "", "Nome precisa ser preechido");
            //throw new NotImplementedException();
        }


    }
}
