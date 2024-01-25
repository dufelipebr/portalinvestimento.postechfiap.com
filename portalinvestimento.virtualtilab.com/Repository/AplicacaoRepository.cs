using investminimalapi.virtualitlab.com.Repository;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using static portalinvestimento.virtualtilab.com.Entity.Investimento;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class AplicacaoRepository : DapperRepository<Aplicacao>, IAplicacaoRepository
    {
        public AplicacaoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(Aplicacao entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "update [Aplicacao] " +
                        "set " +
                        " [Valor_Aplicacao] = @Valor_Aplicacao," +
                        " [Data_Aplicacao] = @Data_Aplicacao, " +
                        " [Rentabilidade] = @Rentabilidade, " +
                        " Last_Changed = @Last_Changed" +
                        " where Id_Investimento = @Id_Investimento " +
                        " and Id_Usuario = @Id_Usuario ";

                    cmd.Parameters.AddWithValue("@Valor_Aplicacao", entidade.Valor_Aplicacao);
                    cmd.Parameters.AddWithValue("@Data_Aplicacao", entidade.Data_Aplicacao);
                    cmd.Parameters.AddWithValue("@Rentabilidade", entidade.Rentabilidade);
                    cmd.Parameters.AddWithValue("@Last_Changed", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", entidade.Usuario.Id);

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

        public override void Cadastrar(Aplicacao entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Aplicacao ([Id_Usuario], " +
                        "[Id_Investimento], " +
                        "[Valor_Aplicacao], " +
                        "[Data_Aplicacao], " +
                        "[Rentabilidade], " +
                        "[Deleted], " +
                        "[Slug], " +
                        "[Publish_Date], " +
                        "[Status] " +
                        ") values (" +
                        " @Id_Usuario, " +
                        " @Id_Investimento, " +
                        " @Valor_Aplicacao, " +
                        " @Data_Aplicacao, " +
                        " @Rentabilidade, " +
                        " @Deleted, " +
                        " @Slug, " +
                        " @Publish_Date, " +
                        " @Status " +
                        ")";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", entidade.Usuario.Id);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Valor_Aplicacao", entidade.Valor_Aplicacao);
                    cmd.Parameters.AddWithValue("@Data_Aplicacao", entidade.Data_Aplicacao);
                    cmd.Parameters.AddWithValue("@Rentabilidade", entidade.Rentabilidade);
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

        public override void Deletar(Aplicacao entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    //cmd.CommandText = $"update Investimento set Deleted = 0, Status = 0, Slug = '{DateTime.Now} removido da base'  where Id= @Id";
                    cmd.CommandText = $"delete from Aplicacao where Id_Investimento= @Id_Investimento and Id_Usuario = @Id_Usuario";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", entidade.Usuario.Id);
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

        public Aplicacao ObterAplicacao(int user_Id, int investimento_Id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                Aplicacao ap = null;

                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Aplicacao where Id_Usuario = @Id_Usuario and Id_Investimento = @Id_Investimento";
                    //cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id_Investimento", investimento_Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", user_Id);

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        ap = new Aplicacao()
                        {
                            //Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Aplicacao = (DateTime)rd["Data_Aplicacao"],
                            Valor_Aplicacao = (Decimal)rd["Valor_Aplicacao"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"], 
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        ap.Usuario = new Usuario() { Id = (int)rd["Id_Usuario"] };
                        ap.Investimento = new Investimento() { Id = (int)rd["Id_Investimento"] };
                        /// implementar o Slug, Deleted... 
                        ap.Deleted = (bool)rd["Deleted"];
                        ap.PublishDate = (DateTime)rd["Publish_Date"];
                        ap.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        ap.Slug = rd["Slug"].ToString();
                        ap.Status = rd["Status"].ToString() == "0" ? EntityStatus.Deactivated : EntityStatus.Active;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }

                return ap;
            }
        }

        public List<Aplicacao> ObterAplicacaoPorUserId(int user_Id)
        {
            List<Aplicacao> list = new List<Aplicacao>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Aplicacao where Id_Usuario = @Id_Usuario";
                    //cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id_Usuario", user_Id);

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Aplicacao ap = new Aplicacao()
                        {
                            //Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Aplicacao = (DateTime)rd["Data_Aplicacao"],
                            Valor_Aplicacao = (Decimal)rd["Valor_Aplicacao"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"],
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        ap.Usuario = new Usuario() { Id = (int)rd["Id_Usuario"] };
                        ap.Investimento = new Investimento() { Id = (int)rd["Id_Investimento"] };
                        /// implementar o Slug, Deleted... 
                        ap.Deleted = (bool)rd["Deleted"];
                        ap.PublishDate = (DateTime)rd["Publish_Date"];
                        ap.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        ap.Slug = rd["Slug"].ToString();
                        ap.Status = rd["Status"].ToString() == "0" ? EntityStatus.Deactivated : EntityStatus.Active;
                        list.Add(ap);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }

                return list;
            }
        }

        public override Aplicacao ObterPorId(int id)
        {
            throw new NotImplementedException();
        }



        public override IList<Aplicacao> ObterTodos()
        {
            List<Aplicacao> list = new List<Aplicacao>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Aplicacao where Deleted = 0";
                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Aplicacao ap = new Aplicacao()
                        {
                            //Id = (int) rd["Id"], -- não tem ID nessa tabela
                            Data_Aplicacao = (DateTime)rd["Data_Aplicacao"],
                            Valor_Aplicacao = (Decimal)rd["Valor_Aplicacao"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"],
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        ap.Usuario = new Usuario() { Id = (int)rd["Id_Usuario"] };
                        ap.Investimento = new Investimento() { Id = (int)rd["Id_Investimento"] };
                        /// implementar o Slug, Deleted... 
                        ap.Deleted = (bool)rd["Deleted"];
                        ap.PublishDate = (DateTime)rd["Publish_Date"];
                        ap.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        ap.Slug = rd["Slug"].ToString();
                        ap.Status = rd["Status"].ToString() == "0" ? EntityStatus.Deactivated : EntityStatus.Active;
                        list.Add(ap);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    dbConnection.Close();
                }

                return list;
            }
        }

        //    Aplicacao IAplicacaoRepository.ObterAplicacaoPorUserId(int user_Id)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
