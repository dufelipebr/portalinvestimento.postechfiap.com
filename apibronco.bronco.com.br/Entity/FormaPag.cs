namespace apibronco.bronco.com.br.Entity
{
    public class FormaPag : Entidade
    {
        public string CodigoFormaPag { get; set; } // Debito, Credito, Boleto
        public int NumeroBanco { get; set; } // Debito, Credito, Boleto
        public int NumeroAgencia { get; set; } // Debito, Credito, Boleto
        public int NumeroAgenciaDigito { get; set; } // Debito, Credito, Boleto
        public string TipoConta { get; set; } // Poupança, Corrente
        public int NumeroConta { get; set; } // Debito, Credito, Boleto
        public int ContaDigito { get; set; } // Debito, Credito, Boleto
    }
}
