using System.Globalization;
using System.Runtime.ConstrainedExecution;
using portalinvestimento.virtualtilab.com.DTO;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Ativo : Entidade
    {
        public Ativo()
        {

        }
        public Ativo(enTipoInvestimento tipo, string nome, string descricao, string codigo, decimal taxaADM, decimal aporteMinimo, decimal rent_3, decimal rent_12, decimal rent_24)
        {
            Tipo = tipo;
            Nome = nome;
            this.Descricao = descricao;
            Codigo = codigo;
            TaxaADM = taxaADM;
            AporteMinimo = aporteMinimo;
            RentabilidadeUltimo_3meses = rent_3;
            Rentabilidade_Ultimo_12meses = rent_12;
            Rentabilidade_Ultimo_24meses = rent_24;
            

            ValidateEntity();

        }

        

        public Ativo(CadastrarAtivoDTO cad_dto)
        {
        
            this.Nome = cad_dto.Nome;
            this.Tipo = cad_dto.TipoInvestimento;
            this.AporteMinimo = cad_dto.AporteMinimo;
            this.Codigo = cad_dto.Codigo;
            this.TaxaADM = cad_dto.TaxaADM;
            this.RentabilidadeUltimo_3meses = cad_dto.RentabilidadeUltimo_3meses;
            this.Rentabilidade_Ultimo_12meses = cad_dto.Rentabilidade_Ultimo_12meses;
            this.Rentabilidade_Ultimo_24meses = cad_dto.Rentabilidade_Ultimo_24meses;
            this.Descricao = cad_dto.Descricao;

            //ValidateEntity(); 
        }

        public Ativo(ModificarAtivoDTO cad_dto)
        {

            this.Nome = cad_dto.Nome;
            this.Tipo = cad_dto.TipoInvestimento;
            this.AporteMinimo = cad_dto.AporteMinimo;
            this.Codigo = cad_dto.Codigo;
            this.TaxaADM = cad_dto.TaxaADM;
            this.RentabilidadeUltimo_3meses = cad_dto.RentabilidadeUltimo_3meses;
            this.Rentabilidade_Ultimo_12meses = cad_dto.Rentabilidade_Ultimo_12meses;
            this.Rentabilidade_Ultimo_24meses = cad_dto.Rentabilidade_Ultimo_24meses;
            this.Descricao = cad_dto.Descricao;
            this.Id = cad_dto.Id;

            if (Id == 0)
                throw  new ArgumentException("ID precisa ser preenchido. Obrigatorio");

            //ValidateEntity(); 
        }

        public enum enTipoInvestimento
        {
            CDB, Acoes, CDI, LDI_LDA, Tesouro, Cripto
        }
        public enTipoInvestimento Tipo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

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
            AssertionConcern.AssertArgumentLength(Codigo, 50, "Codigo do Investimento precisa ter no maximo 50 caracteres.");

            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            AssertionConcern.AssertArgumentLength(Nome, 100, "Nome do Investimento precisa ter no maximo 100 caracteres.");

            AssertionConcern.AssertArgumentNotEquals(Tipo, 0, "Tipo Investimento precisa ser preenchido");
            
            AssertionConcern.AssertArgumentRange((double)TaxaADM, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
            AssertionConcern.AssertArgumentRange((double)AporteMinimo, 0.1, 1000000, "Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00");
            
            
            //throw new NotImplementedException();
        }


    }
}
