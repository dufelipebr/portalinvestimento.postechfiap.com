namespace apibronco.bronco.com.br.Entity
{
    public class Proposta : Entidade
    {
        public StatusProposta StatusProposta { get; set; }
        public string Codigo_Interno { get; }
        public Ramo Ramo { get; set; }

        public IList<Cobertura> Coberturas { get; set; } // lista de coberturas coberta na apolice/proposta
        public string Codigo_Empresa { get; set; }

        public string Codigo_Apolice { get; set; }

        public Segurado Segurado { get; set; }

        public Endereco Endereco_Segurado { get; set; }


        public string Forma_Pagamento { get; set; } // DebitoEmConta, Boleto, Credito ?? 

        public DateTime Data_Emissao { get; set; }

        public DateTime Data_Inicio_Vigencia { get; set; }

        public DateTime Data_Fim_Vigencia { get; set; }

        public DateTime Data_Assinatura_Proposta { get; set; }

        public string Moeda { get; set; } // Default BRL 
        
        public string Codigo_Produto { get; set; } // codigo produto do seguro por exemplo VIDA01 
        
        public string UF_Risco_Principal { get; set; } // provalmente vai seguir endereço do segurado

        public string Codigo_Interno_Susep { get; set; }

        public decimal Valor_LMG { get; set; }




        public bool IsValid()
        {
            return base.IsValid();
        }

    }
}
