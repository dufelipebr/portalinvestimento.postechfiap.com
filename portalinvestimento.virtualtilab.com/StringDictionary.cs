using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com
{
    public class StringDictionary
    {
        //public abstract List<KeyValuePair> _dictionary = new List<KeyValuePair>();        
        private Dictionary<string, string> _dictionary;
        public StringDictionary()
        {
            _dictionary  = new Dictionary<string, string>();
            _dictionary.Add("INVESTIMENTO_VALIDACAO_VAZIO_NOME", "Nome precisa ser preenchido.");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_VAZIO_CODIGO", "Codigo precisa ser preenchido.");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_VAZIO_TIPO_INVESTIMENTO", "Tipo Investimento precisa ser preenchido");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_MAXLENGTH_CODIGO", "Codigo do Investimento precisa ter no maximo 10 caracteres.");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_MAXLENGTH_NOME", "Nome do Investimento precisa ter no maximo 50 caracteres.");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_MAXLENGHT_TAXA_ADM", "Taxa ADM precisa estar entre 0.1 e 10.");
            _dictionary.Add("INVESTIMENTO_VALIDACAO_MAXLENGHT_APORTE_MINIMO", "Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00");

        }

        public string GetByKey(string key) 
        {
            return _dictionary.FirstOrDefault(x => x.Key == key).Value;
        }
    }
}
