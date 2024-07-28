using investminimalapi.virtualitlab.com.Repository;
using System.Data.SqlClient;
using portalinvestimento.virtualtilab.com.Entity;
using System.Collections.Generic;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class PortfolioRepository : DapperRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(Portfolio entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "Update  Portfolio set " +
                        " Nome=@Nome, " +
                        " Descricao=@Descricao, " +
                        " Id_Usuario=@Id_Usuario, " +
                        " Codigo=@Codigo, " +
                        " Slug=@Slug, " +
                        " Last_Changed=@Last_Changed " +
                        " Where Id = @Id";
                     
                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome);
                    cmd.Parameters.AddWithValue("@Descricao", entidade.Descricao);
                    cmd.Parameters.AddWithValue("@Id_Usuario", entidade.Id_Usuario);
                    cmd.Parameters.AddWithValue("@Codigo", entidade.Codigo);
                    cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Slug", entidade.Slug);
                    cmd.Parameters.AddWithValue("@Last_Changed", entidade.LastChanged);
                    cmd.Parameters.AddWithValue("@Status", (int)EntityStatus.Active);

                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public override void Cadastrar(Portfolio entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                { 
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Portfolio " +
                        "(" +
                        "Nome, " +
                        "Descricao, " +
                        "Id_Usuario, " +
                        "Codigo, " +
                        "Deleted, " +
                        "Slug, " +
                        "Publish_Date, " +
                        "Status " +
                        ") " +
                        "       values        (" +
                        "@Nome, " +
                        "@Descricao, " +
                        "@Id_Usuario, " +
                        "@Codigo, " +
                        "@Deleted, " +
                        "@Slug, " +
                        "@Publish_Date, " +
                        "@Status" +
                     ")";
                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome);
                    cmd.Parameters.AddWithValue("@Descricao", entidade.Descricao);
                    cmd.Parameters.AddWithValue("@Id_Usuario", entidade.Id_Usuario);
                    cmd.Parameters.AddWithValue("@Codigo", entidade.Codigo);

                    cmd.Parameters.AddWithValue("@Deleted", 0);
                    cmd.Parameters.AddWithValue("@Slug", $"{DateTime.Now} registro criado");
                    cmd.Parameters.AddWithValue("@Publish_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", (int)EntityStatus.Active);
                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally 
            {
                dbConnection.Close();
            }
        }

        public override void Deletar(Portfolio entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.CommandText = "delete from Portfolio where Id = @Id";
                cmd.Parameters.AddWithValue("@Id", entidade.Id);

                dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch 
            {
            
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public override Portfolio ObterPorId(int id)
        {
            var result = ObterPortfolios().Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public override IList<Portfolio> ObterTodos()
        {
            return ObterPortfolios();
        }

        protected IList<Portfolio> ObterPortfolios()
        {
            IList<Portfolio> list = new List<Portfolio>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Portfolio where Deleted=0";

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        list.Add(new Portfolio() {
                            Id = Int32.Parse(rd["Id"].ToString()),
                            Nome = rd["Nome"].ToString(),
                            Descricao = rd["Descricao"].ToString(),
                            Codigo = rd["Codigo"].ToString(),
                             Id_Usuario = (int) rd["Id_Usuario"]
                        });
                    }
                }
                finally
                {
                    dbConnection.Close();
                }

                return list;
            }
        }

        public Portfolio ObterPorNomePortfolioESenha(
            string email,
            string senha)
        {

            Portfolio user = null;
            using var dbConnection = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.CommandText = "select * from Portfolio where Email = @Email and Senha = @senha";
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                dbConnection.Open();
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    user = new Portfolio()
                    {
                        Id = Int32.Parse(rd["Id"].ToString()),
                        Nome = rd["Nome"].ToString(),
                        Descricao = rd["Descricao"].ToString(),
                        Codigo = rd["Codigo"].ToString(),
                        Id_Usuario = (int)rd["Id_Usuario"]
                    };
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }

            return user;
        }

    }
}
