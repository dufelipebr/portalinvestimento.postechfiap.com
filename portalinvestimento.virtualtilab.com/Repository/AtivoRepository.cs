using investminimalapi.virtualitlab.com.Repository;
using System.Data.SqlClient;
using Microsoft.OpenApi.Models;
using portalinvestimento.virtualtilab.com.Entity;
using System.Collections;
using System.Collections.Generic;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;
using portalinvestimento.virtualtilab.com.Interfaces.Repository;

namespace portalinvestimento.virtualtilab.com.Repository
{
    public class AtivoRepository : DapperRepository<Ativo>, IAtivoRepository
    {
        public AtivoRepository(IConfiguration configuration) : base(configuration)
        {

        }


        public override void Alterar(Ativo entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "update Investimento " +
                        "set " +
                        " Id_Tipo_Investimento = @Id_Tipo_Investimento," +
                        " Nome = @Nome, " +
                        " Codigo = @Codigo, " +
                        " Taxa_ADM = @Taxa_ADM, " +
                        " Aporte_Minimo = @Aporte_Minimo, " +
                        " Rentabilidade_Ultimo_3meses = @Rentabilidade_Ultimo_3meses, " +
                        " Rentabilidade_Ultimo_12meses = @Rentabilidade_Ultimo_12meses, " +
                        " Rentabilidade_Ultimo_24meses = @Rentabilidade_Ultimo_24meses, " +
                        " Deleted = @Deleted," +
                        " Slug = @Slug, " +
                        " Last_Changed = @Last_Changed," +
                        " Status = @Status " +
                        " where Id = @Id";
                    
                    cmd.Parameters.AddWithValue("@Id_Tipo_Investimento", (int) entidade.Tipo);
                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome);
                    cmd.Parameters.AddWithValue("@Codigo", entidade.Codigo);
                    cmd.Parameters.AddWithValue("@Taxa_ADM", entidade.TaxaADM);
                    cmd.Parameters.AddWithValue("@Aporte_Minimo", entidade.AporteMinimo);
                    cmd.Parameters.AddWithValue("@Rentabilidade_Ultimo_3meses", entidade.RentabilidadeUltimo_3meses);
                    cmd.Parameters.AddWithValue("@Rentabilidade_Ultimo_12meses", entidade.Rentabilidade_Ultimo_12meses);
                    cmd.Parameters.AddWithValue("@Rentabilidade_Ultimo_24meses", entidade.Rentabilidade_Ultimo_24meses);
                    cmd.Parameters.AddWithValue("@Id", entidade.Id);

                    cmd.Parameters.AddWithValue("@Deleted", 0);
                    cmd.Parameters.AddWithValue("@Slug", $"{DateTime.Now} registro modificado");
                    cmd.Parameters.AddWithValue("@Last_Changed", DateTime.Now);
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

        public override void Cadastrar(Ativo entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    cmd.CommandText = "insert into Investimento (Id_Tipo_Investimento, " +
                        "Nome, " +
                        "Codigo, " +
                        "Descricao, " +
                        "Taxa_ADM, " + 
                        "Aporte_Minimo, " +
                        "Rentabilidade_Ultimo_3meses, "+
                        "Rentabilidade_Ultimo_12meses, "+
                        "Rentabilidade_Ultimo_24meses, " +
                        "Deleted,  "+
                        "Slug,  " +
                        "Publish_Date," +
                        "Status" +
                        ") values (" +
                        "@Id_Tipo_Investimento, " +
                        "@Nome, " +
                        "@Codigo, " +
                        "@Descricao, " +
                        "@TaxaADM, " +
                        "@AporteMinimo, " +
                        "@RentabilidadeUltimo_3meses, " +
                        "@Rentabilidade_Ultimo_12meses, " +
                        "@Rentabilidade_Ultimo_24meses, " +
                        "@Deleted, " +
                        "@Slug, " +
                        "@Publish_Date, " +
                        "@Status)";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id_Tipo_Investimento", (int) entidade.Tipo);
                    cmd.Parameters.AddWithValue("@Nome", entidade.Nome.ToString());
                    cmd.Parameters.AddWithValue("@Codigo", entidade.Codigo.ToString());
                    cmd.Parameters.AddWithValue("@Descricao", entidade.Descricao.ToString());
                    cmd.Parameters.AddWithValue("@TaxaADM", (decimal)entidade.TaxaADM);
                    cmd.Parameters.AddWithValue("@AporteMinimo", (decimal)entidade.AporteMinimo);
                    cmd.Parameters.AddWithValue("@RentabilidadeUltimo_3meses", (decimal)entidade.RentabilidadeUltimo_3meses);
                    cmd.Parameters.AddWithValue("@Rentabilidade_Ultimo_12meses", (decimal)entidade.Rentabilidade_Ultimo_12meses);
                    cmd.Parameters.AddWithValue("@Rentabilidade_Ultimo_24meses", (decimal)entidade.Rentabilidade_Ultimo_24meses);
                    
                    cmd.Parameters.AddWithValue("@Deleted", 0);
                    cmd.Parameters.AddWithValue("@Slug", $"{DateTime.Now} registro criado");
                    cmd.Parameters.AddWithValue("@Publish_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", (int) EntityStatus.Active);
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

        public override void Deletar(Ativo entidade)
        {
            using var dbConnection = new SqlConnection(ConnectionString);


            try
            {
                using (SqlCommand cmd = dbConnection.CreateCommand())
                {
                    //dbConnection.Query("");
                    //cmd.CommandText = $"update Investimento set Deleted = 0, Status = 0, Slug = '{DateTime.Now} removido da base'  where Id= @Id";
                    cmd.CommandText = $"delete from Investimento where Id= @Id";
                    //cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    cmd.Parameters.AddWithValue("@Id", (int)entidade.Id) ;
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

        public override Ativo ObterPorId(int id)
        {
            return this.ObterTodos().Where(x => x.Id == id).FirstOrDefault();
        }

        public override IList<Ativo> ObterTodos()
        {
            IList<Ativo> list = new List<Ativo>();
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    dbConnection.Open();
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "select * from  Investimento";

                   var rd = cmd.ExecuteReader();
 
                    while (rd.Read())
                    {
                        Ativo investimento = new Ativo()
                        {
                            Id = Int32.Parse(rd["Id"].ToString()),
                            Codigo = rd["Codigo"].ToString(),
                            Nome = rd["Nome"].ToString(),
                            Descricao = rd["Descricao"].ToString(),
                            AporteMinimo = (decimal)rd["Aporte_Minimo"],
                            Tipo = (enTipoInvestimento)Int32.Parse(rd["Id_Tipo_Investimento"].ToString()),
                            TaxaADM = (decimal)rd["Taxa_Adm"],
                            RentabilidadeUltimo_3meses = (decimal)rd["Rentabilidade_Ultimo_3meses"],
                            Rentabilidade_Ultimo_12meses = (decimal)rd["Rentabilidade_Ultimo_12meses"],
                            Rentabilidade_Ultimo_24meses = (decimal)rd["Rentabilidade_Ultimo_24meses"]
                        };
                        /// implementar o Slug, Deleted... 
                        investimento.Deleted = (bool) rd["Deleted"];
                        investimento.PublishDate = (DateTime)rd["Publish_Date"];
                        investimento.LastChanged = ((rd["Last_Changed"] == DBNull.Value) ? DateTime.MinValue : (DateTime)rd["Last_Changed"]);
                        investimento.Slug = rd["Slug"].ToString();
                        investimento.Status =  rd["Status"].ToString()=="0"?EntityStatus.Deactivated: EntityStatus.Active;
                        list.Add(investimento);
                    }
                }
                catch(Exception ex)
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

    }
}
