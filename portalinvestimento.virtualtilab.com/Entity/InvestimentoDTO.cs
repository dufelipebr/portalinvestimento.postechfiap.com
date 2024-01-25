using System.ComponentModel.DataAnnotations;
using static portalinvestimento.virtualtilab.com.Entity.Investimento;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class CadastrarInvestimentoDTO
    {
        public int Id;
        public enTipoInvestimento TipoInvestimento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public decimal TaxaADM { get; set; }
        public decimal AporteMinimo { get; set; }
        public decimal RentabilidadeUltimo_3meses { get; set; }
        public decimal Rentabilidade_Ultimo_12meses { get; set; }
        public decimal Rentabilidade_Ultimo_24meses { get; set; }
    }

    public class ModificarInvestimentoDTO
    {
        public int Id { get; set; } 
        public enTipoInvestimento TipoInvestimento { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal TaxaADM { get; set; }
        public decimal AporteMinimo { get; set; }
        public decimal RentabilidadeUltimo_3meses { get; set; }
        public decimal Rentabilidade_Ultimo_12meses { get; set; }
        public decimal Rentabilidade_Ultimo_24meses { get; set; }
    }
}
