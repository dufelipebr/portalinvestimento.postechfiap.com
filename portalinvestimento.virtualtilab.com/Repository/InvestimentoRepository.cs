using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Collections;
using System.Collections.Generic;
using static portalinvestimento.virtualtilab.com.Entity.Investimento;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        //private IList<Investimento> _listaInvestimento = new List<Investimento>();
        //public void InsertInvestimento(Investimento item)
        //{
        //    _listaInvestimento.Add(item);
        //}

        //public void AlterInvestimento(Investimento investimento)
        //{
        //    Investimento i = GetInvestimentoById(investimento.Codigo);
        //    if (i != null)
        //    {
        //        i.Nome = investimento.Nome;
        //    }
        //}

        //public void DeleteInvestimento(string id)
        //{
        //    Investimento i = GetInvestimentoById(id);
        //    if (i != null)
        //    {
        //        //_listaInvestimento.Remove(inv => inv.Codigo== id);
        //        _listaInvestimento.Remove(i);
        //    }
        //}

        //public Investimento GetInvestimentoById(string id)
        //{
        //    return _listaInvestimento.FirstOrDefault(investimento => investimento.Codigo == id);
        //}

        //public IList<Investimento> GetInvestimentoList()
        //{
        //    if (_listaInvestimento.Count ==0)
        //    {
        //        _listaInvestimento.Add(new Investimento(enTipoInvestimento.Acoes, "ações Petrobras", "0001", 3, 5000, 7.1M, 5.2M, 4.1M));
        //        _listaInvestimento.Add(new Investimento(enTipoInvestimento.CDI, "CDI FIAP", "0002", 0.1M, 0, 2.1M, 12.1M, 25.7M));
        //        _listaInvestimento.Add(new Investimento(enTipoInvestimento.Tesouro, "Tesouro Selic", "0003", 0, 0, 1.8M, 10.6M, 19.1M));
        //        _listaInvestimento.Add(new Investimento(enTipoInvestimento.CDB, "CDB FIAP PLUS 5", "0004", 0.5M, 1000, 3.1M, 18.2M, 23.4M));
        //        _listaInvestimento.Add(new Investimento(enTipoInvestimento.LDI_LDA, "LCI LCA FIAP", "0005", 0, 100, 1.0M, 12.0M, 20.1M));
        //    }

        //    return _listaInvestimento;
        //}

        public IList<Investimento> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Investimento ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Investimento entidade)
        {
            throw new NotImplementedException();
        }

        public void Alterar(Investimento entidade)
        {
            throw new NotImplementedException();
        }

        public void Deletar(Investimento entidade)
        {
            throw new NotImplementedException();
        }
    }
}
