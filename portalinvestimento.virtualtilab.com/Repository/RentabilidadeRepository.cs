using investminimalapi.virtualitlab.com.Repository;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class RentabilidadeRepository : DapperRepository<RentabilidadeInvestimento>, IRentabilidadeRepository
    {
        public RentabilidadeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override void Alterar(RentabilidadeInvestimento entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "update [Rentabilidade_Investimento] " +
                        "set " +
                        " [Valor_Rentabilidade] = @Valor_Rentabilidade," +
                        " [Data_Calculo] = @Data_Calculo, " +
                        " [Id_Investimento] = @Id_Investimento, " +
                        " Last_Changed = @Last_Changed" +
                        " where " +
                        " Id= @Id";

                    cmd.Parameters.AddWithValue("@Valor_Rentabilidade", entidade.Valor_Rentabilidade);
                    cmd.Parameters.AddWithValue("@Data_Calculo", entidade.Data_Operacao);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Last_Changed", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id", entidade.Id);

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

        public override void Cadastrar(RentabilidadeInvestimento entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Rentabilidade_Investimento ( " +
                        "[Id_Investimento], " +
                        "[Valor_Rentabilidade], " +
                        "[Data_Calculo], " +
                        "[Deleted], " +
                        "[Slug], " +
                        "[Publish_Date], " +
                        "[Status] " +
                        ") values (" +
                        " @Id_Investimento, " +
                        " @Valor_Rentabilidade, " +
                        " @Data_Calculo, " +
                        " @Deleted, " +
                        " @Slug, " +
                        " @Publish_Date, " +
                        " @Status " +
                        ")";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Valor_Rentabilidade", entidade.Valor_Rentabilidade);
                    cmd.Parameters.AddWithValue("@Data_Calculo", entidade.Data_Operacao);

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

        public override void Deletar(RentabilidadeInvestimento entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    //cmd.CommandText = $"update Investimento set Deleted = 0, Status = 0, Slug = '{DateTime.Now} removido da base'  where Id= @Id";
                    cmd.CommandText = "delete from Rentabilidade_Investimento where Id= @Id";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id", entidade.Id);
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

        public override RentabilidadeInvestimento ObterPorId(int id)
        {
            RentabilidadeInvestimento reg = null;

            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Rentabilidade_Investimento where Id = @Id";
                    //cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    cmd.Parameters.AddWithValue("@Id", id);

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        reg = new RentabilidadeInvestimento()
                        {
                            Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Operacao = (DateTime)rd["Data_Calculo"],
                            Valor_Rentabilidade = (Decimal)rd["Valor_Rentabilidade"],
                        };

                        reg.Investimento = new Ativo() { Id = (int)rd["Id_Investimento"] };
                        /// implementar o Slug, Deleted... 
                        reg.Deleted = (bool)rd["Deleted"];
                        reg.PublishDate = (DateTime)rd["Publish_Date"];
                        reg.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        reg.Slug = rd["Slug"].ToString();
                        reg.Status = rd["Status"].ToString() == "0" ? EntityStatus.Deactivated : EntityStatus.Active;
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

                return reg;
            }
        }



        public override IList<RentabilidadeInvestimento> ObterTodos()
        {
            List<RentabilidadeInvestimento> list = new List<RentabilidadeInvestimento>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                RentabilidadeInvestimento reg;

                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Rentabilidade_Investimento where Deleted = 0";
                    //cmd.Parameters.AddWithValue("@Id_Investimento", entidade.Investimento.Id);
                    //cmd.Parameters.AddWithValue("@Id", id);

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        reg = new RentabilidadeInvestimento()
                        {
                            Id = Int32.Parse(rd["Id"].ToString()),
                            Data_Operacao = (DateTime)rd["Data_Calculo"],
                            Valor_Rentabilidade = (Decimal)rd["Valor_Rentabilidade"],
                        };

                        reg.Investimento = new Ativo() { Id = (int)rd["Id_Investimento"] };
                        /// implementar o Slug, Deleted... 
                        reg.Deleted = (bool)rd["Deleted"];
                        reg.PublishDate = (DateTime)rd["Publish_Date"];
                        reg.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        reg.Slug = rd["Slug"].ToString();
                        reg.Status = rd["Status"].ToString() == "0" ? EntityStatus.Deactivated : EntityStatus.Active;

                        list.Add(reg);
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

        //    Rentabilidade_Investimento IRentabilidade_InvestimentoRepository.ObterRentabilidade_InvestimentoPorUserId(int user_Id)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
