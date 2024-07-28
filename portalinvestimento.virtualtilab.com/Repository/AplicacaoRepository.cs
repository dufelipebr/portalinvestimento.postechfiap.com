using investminimalapi.virtualitlab.com.Repository;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class AplicacaoRepository : DapperRepository<Transacao>, IAplicacaoRepository
    {
        public AplicacaoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(Transacao entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {

                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "update [Aplicacao] " +
                        "set " +
                        " [Preco] = @Preco," +
                        " [Data_Transacao] = @Data_Transacao, " +
                        " [Rentabilidade] = @Rentabilidade, " +
                        " Last_Changed = @Last_Changed" +
                        " where Id_Investimento = @Id_Investimento " +
                        " and Id_Portfolio = @Id_Portfolio ";

                    cmd.Parameters.AddWithValue("@Preco", entidade.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", entidade.Quantidade);
                    cmd.Parameters.AddWithValue("@Data_Transacao", entidade.Data_Transacao);
                    //cmd.Parameters.AddWithValue("@Rentabilidade", entidade.Rentabilidade);
                    cmd.Parameters.AddWithValue("@Last_Changed", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id_Portfolio", entidade.Id_Portfolio);

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

        public override void Cadastrar(Transacao entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Aplicacao ([Id_Portfolio], " +
                        "[Id_Investimento], " +
                        "[Preco], " +
                        "[Data_Transacao], " +
                        "[Rentabilidade], " +
                        "[Deleted], " +
                        "[Slug], " +
                        "[Publish_Date], " +
                        "[Status] " +
                        ") values (" +
                        " @Id_Usuario, " +
                        " @Id_Investimento, " +
                        " @Preco, " +
                        " @Data_Transacao, " +
                        " @Rentabilidade, " +
                        " @Deleted, " +
                        " @Slug, " +
                        " @Publish_Date, " +
                        " @Status " +
                        ")";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id_Portfolio", entidade.Id_Portfolio);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Preco", entidade.Preco);
                    cmd.Parameters.AddWithValue("@Data_Transacao", entidade.Data_Transacao);
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

        public override void Deletar(Transacao entidade)
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
                    cmd.Parameters.AddWithValue("@Id_Portfolio", entidade.Id_Portfolio);
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

        public Transacao ObterAplicacao(int user_Id, int investimento_Id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                Transacao ap = null;

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
                        ap = new Transacao()
                        {
                            //Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Transacao = (DateTime)rd["Data_Transacao"],
                            Preco = (Decimal)rd["Preco"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"], 
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        //ap.Usuario = new Portfolio() { Id = (int)rd["Id_Usuario"] };
                        ap.Id_Portfolio = (int)rd["Id_Portfolio"];
                        ap.Investimento = new Ativo() { Id = (int)rd["Id_Investimento"] };
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

        public List<Transacao> ObterAplicacaoPorUserId(int user_Id)
        {
            List<Transacao> list = new List<Transacao>();
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
                        Transacao ap = new Transacao()
                        {
                            //Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Transacao = (DateTime)rd["Data_Transacao"],
                            Preco = (Decimal)rd["Preco"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"],
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        ap.Id_Portfolio = (int)rd["Id_Portfolio"];                         
                        ap.Investimento = new Ativo() { Id = (int)rd["Id_Investimento"] };
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

        public override Transacao ObterPorId(int id)
        {
            throw new NotImplementedException();
        }



        public override IList<Transacao> ObterTodos()
        {
            List<Transacao> list = new List<Transacao>();
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
                        Transacao ap = new Transacao()
                        {
                            //Id = (int) rd["Id"], -- não tem ID nessa tabela
                            Data_Transacao = (DateTime)rd["Data_Transacao"],
                            Preco = (Decimal)rd["Preco"],
                            Rentabilidade = (Decimal)rd["Rentabilidade"],
                            Ultima_Rentabilidade_Calculada = ((rd["Ultima_Rentabilidade_Calculada"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"])
                        };

                        //ap.Usuario = new Portfolio() { Id = (int)rd["Id_Usuario"] };
                        ap.Id_Portfolio = (int)rd["Id_Portfolio"];
                        ap.Investimento = new Ativo() { Id = (int)rd["Id_Investimento"] };
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
