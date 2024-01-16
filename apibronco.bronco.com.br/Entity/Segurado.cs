namespace apibronco.bronco.com.br.Entity
{
    public class Segurado
    {
        public string Nome { get; set; }
        public string CPF_CNPJ{ get; set; }
        public char Tipo_Segurado { get; set; } // J - Juridica P- Fisica 
        public string RG{ get; set; }

        public DateTime Data_Nascimento { get; set; }
        public Endereco Endereco_Segurado { get; set; }
        public string Telefone_Comercial { get; set; }

        public string Telefone_Residencial { get; set; }

        public string Celular { get; set; }
    }
}
