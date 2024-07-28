using System.ComponentModel.DataAnnotations;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.DTO
{
    public class CadastrarAtivoDTO
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

    public class ModificarAtivoDTO
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
