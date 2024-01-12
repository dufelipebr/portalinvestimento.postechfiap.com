using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections;
using System.Collections.Generic;
//using static apibronco.bronco.com.br.Entity.Proposta;

namespace apibronco.bronco.com.br.Repository
{
    public class PropostaRepository : IPropostaRepository
    {
        //private IList<Proposta> _listaProposta = new List<Proposta>();
        //public void InsertProposta(Proposta item)
        //{
        //    _listaProposta.Add(item);
        //}

        //public void AlterProposta(Proposta investimento)
        //{
        //    Proposta i = GetPropostaById(investimento.Codigo);
        //    if (i != null)
        //    {
        //        i.Nome = investimento.Nome;
        //    }
        //}

        //public void DeleteProposta(string id)
        //{
        //    Proposta i = GetPropostaById(id);
        //    if (i != null)
        //    {
        //        //_listaProposta.Remove(inv => inv.Codigo== id);
        //        _listaProposta.Remove(i);
        //    }
        //}

        //public Proposta GetPropostaById(string id)
        //{
        //    return _listaProposta.FirstOrDefault(investimento => investimento.Codigo == id);
        //}

        //public IList<Proposta> GetPropostaList()
        //{
        //    if (_listaProposta.Count ==0)
        //    {
        //        _listaProposta.Add(new Proposta(enTipoProposta.Acoes, "ações Petrobras", "0001", 3, 5000, 7.1M, 5.2M, 4.1M));
        //        _listaProposta.Add(new Proposta(enTipoProposta.CDI, "CDI FIAP", "0002", 0.1M, 0, 2.1M, 12.1M, 25.7M));
        //        _listaProposta.Add(new Proposta(enTipoProposta.Tesouro, "Tesouro Selic", "0003", 0, 0, 1.8M, 10.6M, 19.1M));
        //        _listaProposta.Add(new Proposta(enTipoProposta.CDB, "CDB FIAP PLUS 5", "0004", 0.5M, 1000, 3.1M, 18.2M, 23.4M));
        //        _listaProposta.Add(new Proposta(enTipoProposta.LDI_LDA, "LCI LCA FIAP", "0005", 0, 100, 1.0M, 12.0M, 20.1M));
        //    }

        //    return _listaProposta;
        //}

        public IList<Proposta> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Proposta ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Proposta entidade)
        {
            entidade.CreatedOn = DateTime.Now;

            throw new NotImplementedException();
        }

        public void Alterar(Proposta entidade)
        {
            throw new NotImplementedException();
        }

        public void Deletar(Proposta entidade)
        {
            throw new NotImplementedException();
        }

        IList<Proposta> IRepository<Proposta>.ObterTodos()
        {
            throw new NotImplementedException();
        }

        Proposta IRepository<Proposta>.ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

    }
}
