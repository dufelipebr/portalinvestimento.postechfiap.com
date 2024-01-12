using System.Net;

namespace apibronco.bronco.com.br.Entity
{
    public class Cobertura : Entidade
    {
        public TipoCobertura Tipo_Cobertura { get; set; } //  96001- Básica-Incêndio, raio, explosão, implosão e fumaça
        public string Descricao { get; set; }
        /*        Básica-Incêndio, raio, explosão, implosão e fumaça
Vendaval, furacão, ciclone, tornardo, granizo, impacto de veículos e queda de aeronaves
Danos elétricos
Equipamentos eletrônicos*/

        public int Codigo_Sequencial { get; set; }
        public decimal Cobertura_DanoMaximo { get; set; }

        public Decimal Valor_Premio { get; set; }
        public Decimal Valor_IOF { get; set; }
        public Decimal Valor_Custo_Emiss { get; set; }
        public Decimal Valor_Add_Fraq { get; set; }
        public Decimal Valor_Cosseg_Cedido { get; set; }
        public Decimal Valor_LMI { get; set; }
        public Decimal Valor_Is { get; set; }
        public Decimal Valor_Comiss { get; set; }

        public String Codigo_Moeda { get; set; }

        public String Codigo_Cobertura_SUSEP { get; set; }


        public bool IsValid()
        {
            return base.IsValid();
        }
    }
}
