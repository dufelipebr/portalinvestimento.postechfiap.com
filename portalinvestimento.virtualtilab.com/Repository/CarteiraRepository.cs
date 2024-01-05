using investminimalapi.virtualitlab.com.Repository;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using static portalinvestimento.virtualtilab.com.Entity.Investimento;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class CarteiraRepository : DapperRepository<Carteira>, ICarteiraRepository
    {
        public CarteiraRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(Carteira entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "update carteira set nome = @Nome where Id = @Id";
            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
            cmd.Parameters.AddWithValue("@Id", entidade.Id);
            
            cmd.ExecuteNonQuery();
            
        }

        public override void Cadastrar(Carteira entidade)
        {
            throw new NotImplementedException();
        }

        public override void Deletar(Carteira entidade)
        {
            throw new NotImplementedException();
        }

        public override Carteira ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Carteira> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
