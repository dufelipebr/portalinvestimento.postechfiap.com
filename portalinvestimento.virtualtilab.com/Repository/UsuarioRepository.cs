using investminimalapi.virtualitlab.com.Repository;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Data.SqlClient;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class UsuarioRepository : DapperRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(Usuario entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = dbConnection.CreateCommand();
            //dbConnection.Query("");
            cmd.CommandText = "update carteira set nome = @Nome where Id = @Id";
            cmd.Parameters.AddWithValue("@Nome", entidade.Nome.ToString());
            cmd.Parameters.AddWithValue("@Id", entidade.Id);

            cmd.ExecuteNonQuery();
        }

        public override void Cadastrar(Usuario entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = dbConnection.CreateCommand();
            //dbConnection.Query("");
            cmd.CommandText = "insert into Usuario (Id, Nome) values (@Id, @Nome)";
            cmd.Parameters.AddWithValue("@Nome", entidade.Nome.ToString());
            cmd.Parameters.AddWithValue("@Id", entidade.Id);

            cmd.ExecuteNonQuery();
        }

        public override void Deletar(Usuario entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);
            SqlCommand cmd = dbConnection.CreateCommand();
            //dbConnection.Query("");
            cmd.CommandText = "delete from Usuario where id = @Id";
//            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
            cmd.Parameters.AddWithValue("@Id", entidade.Id);

            cmd.ExecuteNonQuery();
        }

        public override Usuario ObterPorId(int id)
        {
            return ObterUsuarios(id).FirstOrDefault();
        }

        public override IList<Usuario> ObterTodos()
        {
            return ObterUsuarios(null);
        }

        protected IList<Usuario> ObterUsuarios(int? id)
        {
            IList<Usuario> list = new List<Usuario>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Usuario where (Id = @Id or @Id is null) ";

                    //if (id != null)
                        cmd.Parameters.AddWithValue("@Id", id);
                    

                    var rd = cmd.ExecuteReader();

                    while (!rd.Read())
                    {
                        list.Add(new Usuario(Int32.Parse(rd["Id"].ToString()), rd["Nome"].ToString()));
                    }
                    return list;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
    }
}
