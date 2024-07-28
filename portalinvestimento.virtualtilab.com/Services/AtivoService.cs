using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Service;

namespace portalinvestimento.virtualtilab.com.Services
{
    public class AtivoService : IAtivoService
    {
        public string Create(Ativo obj)
        {
            if (obj.Codigo == null || obj.Codigo.Length > 10)
                return "O codigo precisa ser preenchido e deve ter no maximo 50 caracteres";

            if (obj.Nome == null || obj.Nome.Length > 100)
                return "O nome não pode ser maior que 100 caracteres";


            if (obj.Descricao == null || obj.Descricao.Length > 100)
                return "Descrição do Fundo não pode ser maior que 500 caracteres";

            return "Ok";
        }

        public string Modify(Ativo obj)
        {
            if (obj.Codigo == null || obj.Codigo.Length > 10)
                return "O codigo precisa ser preenchido e deve ter no maximo 50 caracteres";

            if (obj.Nome == null || obj.Nome.Length > 100)
                return "O nome não pode ser maior que 100 caracteres";


            if (obj.Descricao == null || obj.Descricao.Length > 100)
                return "Descrição do Fundo não pode ser maior que 500 caracteres";

            if (obj.Id == 0)
                return "Id precisa ser preenchido";

            return "Ok";
        }
    }
}
