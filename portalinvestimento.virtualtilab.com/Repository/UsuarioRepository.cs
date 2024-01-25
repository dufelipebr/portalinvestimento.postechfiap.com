using investminimalapi.virtualitlab.com.Repository;
using System.Data.SqlClient;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Collections.Generic;

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

            try {

                SqlCommand cmd = dbConnection.CreateCommand();
                //dbConnection.Query("");
                cmd.CommandText = "update Usuario set " +
                    //"  Nome = @Nome " +
                    //" ,Email = @Email " +
                    //" ,Senha = @Senha " +
                    //" ,Tipo_Acesso = @Tipo_Acesso " +
                    //" ,CPF = @CPF " +
                    //" ,Codigo_Conta = @Codigo_Conta " +
                    //" ,Digito_Conta = @Digito_Conta " +
                    " Saldo_Carteira = @Saldo_Carteira" +
                    //" ,Deleted = @Deleted" +
                    //" ,Slug = @Slug " +
                    " , Last_Changed = @Last_Changed" +
                    " where Id = @Id";
                cmd.Parameters.AddWithValue("@Saldo_Carteira", entidade.Saldo_Carteira);
                cmd.Parameters.AddWithValue("@Last_Changed", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id", entidade.Id);
                //cmd.Parameters.AddWithValue("@Email", entidade.Email);
                //cmd.Parameters.AddWithValue("@Senha", entidade.Senha);
                //cmd.Parameters.AddWithValue("@Tipo_Acesso", (int)entidade.TipoPermissao);
                
                //cmd.Parameters.AddWithValue("@Slug", $"{DateTime.Now} registro modificado");
                
                //cmd.Parameters.AddWithValue("@Status", (int)EntityStatus.Active);

                dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch {
                dbConnection.Close();
                throw;
            }
            
        }

        public override void Cadastrar(Usuario entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                { 
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Usuario " +
                        "(" +
                        "Nome, " +
                        "Email, " +
                        "Senha, " +
                        "Tipo_Acesso, " +
                        "CPF, " +
                        "Codigo_Conta, " +
                        "Digito_Conta, " +
                        "Saldo_Carteira, " +
                        "Deleted, " +
                        "Slug, " +
                        "Publish_Date, " +
                        "Status " +
                        ") " +
                        "       values        (" +
                        "@Nome, " +
                        "@Email, " +
                        "@Senha, " +
                        "@Tipo_Acesso," +
                        "@CPF, " +
                        "@Codigo_Conta, " +
                        "@Digito_Conta, " +
                        "@Saldo_Carteira, " +
                        "@Deleted, " +
                        "@Slug, " +
                        "@Publish_Date, " +
                        "@Status" +
                     ")";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome);
                    cmd.Parameters.AddWithValue("@Email", entidade.Email);
                    cmd.Parameters.AddWithValue("@Senha", entidade.Senha);
                    cmd.Parameters.AddWithValue("@Tipo_Acesso", (int)entidade.TipoPermissao);
                    cmd.Parameters.AddWithValue("@CPF", entidade.CPF);
                    cmd.Parameters.AddWithValue("@Codigo_Conta", entidade.Codigo_Conta);
                    cmd.Parameters.AddWithValue("@Digito_Conta", entidade.Digito_Conta);
                    cmd.Parameters.AddWithValue("@Saldo_Carteira", entidade.Saldo_Carteira);

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

        public override void Deletar(Usuario entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand cmd = dbConnection.CreateCommand();
                //dbConnection.Query("");
                cmd.CommandText = "delete from Usuario where Id = @Id";
                //            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
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
                        cmd.Parameters.AddWithValue("@Id",  (id == null ? DBNull.Value : id));
                    

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        list.Add(new Usuario() { 
                            Id = Int32.Parse(rd["Id"].ToString()), 
                            Nome = rd["Nome"].ToString(), 
                            Email = rd["Email"].ToString(),
                            //Senha = rd["Senha"].ToString(),
                            TipoPermissao = (EnTipoAcesso) Int32.Parse(rd["Tipo_Acesso"].ToString()),
                            Codigo_Conta = (int) rd["Codigo_Conta"],
                            Digito_Conta = (int)rd["Digito_Conta"],
                            Saldo_Carteira = (decimal)rd["Saldo_Carteira"],
                            CPF = rd["CPF"].ToString()
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

        public Usuario ObterPorNomeUsuarioESenha(
            string email,
            string senha)
        {

            Usuario user = null;
            using var dbConnection = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand cmd = dbConnection.CreateCommand();
                //dbConnection.Query("");
                cmd.CommandText = "select * from Usuario where Email = @Email and Senha = @senha";
                //            cmd.Parameters.AddWithValue("@Nome", entidade.NomeBeneficiario.ToString());
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                dbConnection.Open();
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    user = new Usuario()
                    {
                        Id = Int32.Parse(rd["Id"].ToString()),
                        Nome = rd["Nome"].ToString(),
                        Email = rd["Email"].ToString(),
                        Senha = rd["Senha"].ToString(),
                        TipoPermissao = (EnTipoAcesso)Int32.Parse(rd["Tipo_Acesso"].ToString())
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
