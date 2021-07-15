using DAO;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class frmETL4 : Form
    {
        public frmETL4()
        {
            InitializeComponent();

            DataTable dtProjetos = (DataTable)DBAccessCadastro.ExecutarComando("select \"Id\",\"Name\" ,\"ClientId\" from sys.\"Projects\" order by \"Id\" ", CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
            for (int x = 0; x <= dtProjetos.Rows.Count - 1; x++)
            {
                cmbProjetos.Items.Add(dtProjetos.Rows[x]["Id"].ToString() + " - " + dtProjetos.Rows[x]["Name"].ToString());
            }

            DataTable dtMunicipios = (DataTable)DBAccessGeosWeb.ExecutarComando("select m.id, m.Name from Municipalities m join poles p on m.Id = p.municipalityid group by m.id, m.Name order by m.Name", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
            for (int x = 0; x <= dtMunicipios.Rows.Count - 1; x++)
            {
                cmbMunicipios.Items.Add(dtMunicipios.Rows[x]["Name"].ToString().Trim() + " - " + dtMunicipios.Rows[x]["Id"].ToString());
            }
        }

        private void btnImportarIP_Click(object sender, EventArgs e)
        {
            try
            {

                #region "Dados básicos"

                List<Poste> ListaPostes = new List<Poste>();
                DataTable dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,extid,barrament from Poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Poste poste = new Poste();
                    poste.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    poste.ExtId = Convert.ToInt32(dtDados.Rows[x][1]);
                    poste.Barrament = dtDados.Rows[x][2].ToString();
                    ListaPostes.Add(poste);
                }

                List<Transformador> ListaTransformador = new List<Transformador>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select t.id,t.poleid,t.extid,t.label from Transforms t join poles p on t.PoleId = p.id and MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Transformador transformador = new Transformador();
                    transformador.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    transformador.PoleId = Convert.ToInt32(dtDados.Rows[x][1]);
                    transformador.ExtId = Convert.ToInt32(dtDados.Rows[x][2]);
                    transformador.Label = dtDados.Rows[x][3].ToString();
                    ListaTransformador.Add(transformador);
                }

                List<PotenciaLuminaria> ListaPotencia = new List<PotenciaLuminaria>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from LuminaryPowers", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    PotenciaLuminaria potencia = new PotenciaLuminaria();
                    potencia.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    ListaPotencia.Add(potencia);
                }

                List<TipoLampadaLuminaria> ListaTipoLampada = new List<TipoLampadaLuminaria>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name,cod from LuminaryClass", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoLampadaLuminaria tipoLampada = new TipoLampadaLuminaria();
                    tipoLampada.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    tipoLampada.Name = dtDados.Rows[x][1].ToString();
                    tipoLampada.Cod = dtDados.Rows[x][2].ToString();
                    ListaTipoLampada.Add(tipoLampada);
                }

                List<TipoBraco> ListaTipoBraco = new List<TipoBraco>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from LuminaryArms", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoBraco braco = new TipoBraco();
                    braco.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    braco.Name = dtDados.Rows[x][1].ToString();
                    ListaTipoBraco.Add(braco);
                }

                List<TipoLuminaria> ListaTipoLuminaria = new List<TipoLuminaria>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from LuminaryTypes", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoLuminaria tipoLuminaria = new TipoLuminaria();
                    tipoLuminaria.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    tipoLuminaria.Name = dtDados.Rows[x][1].ToString();
                    ListaTipoLuminaria.Add(tipoLuminaria);
                }

                List<ObjetoIluminadoLuminaria> ListaObjetoIluminado = new List<ObjetoIluminadoLuminaria>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from LuminaryObjects", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    ObjetoIluminadoLuminaria objeto = new ObjetoIluminadoLuminaria();
                    objeto.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    objeto.Name = dtDados.Rows[x][1].ToString();
                    ListaObjetoIluminado.Add(objeto);
                }

                int IdTransformadorAuxi = 0;

                //Caso retorne alguma informação, indica que existe ip sem transformador no cadastro, 
                //no banco do geosweb o transformador é obrigatório, sendo assim a orientação é, incluir um transformador e associar todas as ips sem transformador a ele
                string query = "select Count(*) from sys.\"LightingPoints\" ip join sys.\"Poles\" p on ip.\"PoleId\" = p.\"Id\" ";
                query += "join sys.\"GeoPoints\" g ON g.\"Id\" = p.\"Id\" ";
                query += "where g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " and ip.\"TransformerId\" is null limit 1";
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                if (Convert.ToInt32(dtDados.Rows[0][0]) != 0)
                {
                    query = "select t.* from Transforms t join poles p on t.PoleId = p.id and MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1];
                    DataTable dtTransformador = (DataTable)DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                    if (dtTransformador.Rows.Count != 0)
                    {
                        IdTransformadorAuxi = Convert.ToInt32(dtTransformador.Rows[0]["id"]);
                    }
                    else
                    {
                        SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString);
                        connection.Open();
                        SqlCommand comandoGeos = new SqlCommand("insert into Transforms (PoleId,TypeId,Xp,Yp,DisjuntorId) select top 1 id,1,Xp,Yp,0 from poles where MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], connection);
                        comandoGeos.CommandType = CommandType.Text;

                        dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select t.id,t.poleid,t.extid,t.label from Transforms t join poles p on t.PoleId = p.id and MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                        for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                        {
                            Transformador transformador = new Transformador();
                            transformador.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                            transformador.PoleId = Convert.ToInt32(dtDados.Rows[x][1]);
                            transformador.ExtId = Convert.ToInt32(dtDados.Rows[x][2]);
                            transformador.Label = dtDados.Rows[x][3].ToString();
                            ListaTransformador.Add(transformador);
                        }

                        connection.Close();
                    }
                }
                #endregion

                #region Select
                query = "select ";
                query += "ST_X(ST_Transform(g.\"geom\", 3857)) as \"Xp\",  ";
                query += "ST_Y(ST_Transform(g.\"geom\", 3857)) as \"Yp\", ";
                query += "sum(cast(ip.\"DynamicAttributes\"->> 'LampsNumber' As integer)) as \"TotalCount\", ";
                query += "case when ip.\"DynamicAttributes\"->> 'OnAllTheTime' = 'S' then cast(ip.\"DynamicAttributes\"->> 'LampsNumber' As integer) else 0 end  as \"Hours24Count\", ";
                //query += "sum(case when ip.\"DynamicAttributes\"->> 'OnAllTheTime' = 'S' then 1 else 0 end)  as \"Hours24Count\", ";
                query += "ip.\"TransformerId\", ";
                query += "p.\"DistributorPoleCode\", p.\"DistributorPoleId\", ";
                query += "coalesce(cast(ip.\"DynamicAttributes\"->> 'IlluminatedObjectType' as varchar), 'NI') as \"IlluminatedObjectType\", ";
                query += "ip.\"DynamicAttributes\"->> 'IsMeasured' as \"Measurer\", ";
                query += "ip.\"DynamicAttributes\"->> 'Power' as \"LuminaryPower\", ";
                query += "ip.\"DynamicAttributes\"->> 'LampType' as \"LuminaryClass\", ";
                query += "coalesce(cast(ip.\"DynamicAttributes\"->> 'ArmSize' as integer), 0) as \"ArmSize\", ";
                query += "coalesce(cast(ip.\"DynamicAttributes\"->> 'LuminaryType' as integer), 0) as \"LuminaryType\", ";
                query += "ip.\"Code\",i.\"Plate\" ";
                query += "from ";
                query += "sys.\"LightingPoints\" ip ";
                query += "join sys.\"Poles\" p on ip.\"PoleId\" = p.\"Id\" ";
                query += "join sys.\"GeoPoints\" g ON g.\"Id\" = p.\"Id\" ";
                query += "left join sys.\"Transformers\" t ON t.\"Id\" = ip.\"TransformerId\" ";
                query += "left join sys.\"Installations\" i on t.\"Id\" = i.\"Id\" and i.\"InstallationType\" = 7 ";
                query += "where  ";
                query += "g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                query += " group by ";
                query += "ip.\"Code\",ST_X(ST_Transform(g.\"geom\", 3857)), ST_Y(ST_Transform(g.\"geom\", 3857)) , ";
                query += "ip.\"DynamicAttributes\"->> 'OnAllTheTime', ip.\"DynamicAttributes\"->> 'LampsNumber'  ,ip.\"TransformerId\",p.\"DistributorPoleCode\", p.\"DistributorPoleId\", ";
                query += "ip.\"DynamicAttributes\"->> 'IlluminatedObjectType',ip.\"DynamicAttributes\"->> 'IsMeasured',ip.\"DynamicAttributes\"->> 'Power', ";
                query += "ip.\"DynamicAttributes\"->> 'LampType' ,ip.\"DynamicAttributes\"->> 'ArmSize',ip.\"DynamicAttributes\"->> 'LuminaryType', ";
                query += "ip.\"DynamicAttributes\"->> 'IlluminatedObjectType',ip.\"Code\", i.\"Plate\" ";
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                #endregion

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                foreach (DataRow luminaria in dtDados.Rows)
                {

                    if (luminaria["LuminaryPower"].ToString() != "4.5")
                    {
                        if (ListaPotencia.Where(x => x.Id == Convert.ToInt32(luminaria["LuminaryPower"])).ToList().Count == 0)
                        {
                            query = "insert into LuminaryPowers (Id) values (" + luminaria["LuminaryPower"].ToString() + ")";
                            DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);
                            PotenciaLuminaria Potencia = new PotenciaLuminaria();
                            Potencia.Id = Convert.ToInt32(luminaria["LuminaryPower"]);
                            ListaPotencia.Add(Potencia);
                        }
                    }
                    else
                    {
                        if (ListaPotencia.Where(x => x.Id == 4).ToList().Count == 0)
                        {
                            query = "insert into LuminaryPowers (Id) values (4)";
                            var newPotencia = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                            PotenciaLuminaria Potencia = new PotenciaLuminaria();
                            Potencia.Id = 4;
                            ListaPotencia.Add(Potencia);
                        }
                    }

                    if (ListaTipoLampada.Where(x => x.Cod.ToUpper() == luminaria["LuminaryClass"].ToString().ToUpper()).ToList().Count == 0)
                    {
                        query = "insert into LuminaryClass (Id,Name,Cod) select max(id) + 1,'Preecher','" + luminaria["LuminaryClass"].ToString() + "' from LuminaryClass";
                        DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);

                        DataTable dtTipo = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,cod from LuminaryClass where cod ='" + luminaria["LuminaryClass"].ToString().ToUpper() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                        TipoLampadaLuminaria NewTipo = new TipoLampadaLuminaria();
                        NewTipo.Id = Convert.ToInt32(dtTipo.Rows[0][0]);
                        NewTipo.Name = "Preencher";
                        NewTipo.Cod = luminaria["LuminaryClass"].ToString().ToUpper();
                        ListaTipoLampada.Add(NewTipo);
                    }
                    if (ListaTipoBraco.Where(x => x.Id == Convert.ToInt32(luminaria["ArmSize"])).ToList().Count == 0)
                    {
                        query = "insert into LuminaryArms values (" + luminaria["ArmSize"].ToString() + ",'Preencher')";
                        var newIdArm = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                        TipoBraco NewBraco = new TipoBraco();
                        NewBraco.Id = Convert.ToInt32(newIdArm);
                        ListaTipoBraco.Add(NewBraco);
                    }
                    if (ListaTipoLuminaria.Where(x => x.Id == Convert.ToInt32(luminaria["LuminaryType"])).ToList().Count == 0)
                    {
                        query = "insert into LuminaryTypes values (" + luminaria["LuminaryType"].ToString() + ",'Preencher')";
                        var newIdTipoLuminaria = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                        TipoLuminaria NewTipoLuminaria = new TipoLuminaria();
                        NewTipoLuminaria.Id = Convert.ToInt32(newIdTipoLuminaria);
                        ListaTipoLuminaria.Add(NewTipoLuminaria);
                    }
                    if (ListaObjetoIluminado.Where(x => x.Name.ToUpper() == luminaria["IlluminatedObjectType"].ToString().ToUpper()).ToList().Count == 0)
                    {
                        query = "insert into LuminaryObjects (Id,Name) values (" + (ListaObjetoIluminado[ListaObjetoIluminado.Count - 1].Id + 1).ToString() + ",'" + luminaria["IlluminatedObjectType"].ToString().ToUpper() + "')";
                        DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);
                        ObjetoIluminadoLuminaria NewObjeto = new ObjetoIluminadoLuminaria();
                        NewObjeto.Id = ListaObjetoIluminado[ListaObjetoIluminado.Count - 1].Id + 1;
                        NewObjeto.Name = luminaria["IlluminatedObjectType"].ToString().ToUpper();
                        ListaObjetoIluminado.Add(NewObjeto);
                    }

                }

                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
                {
                    try
                    {
                        conexaoSQL.Open();

                        int contador = 0;

                        foreach (DataRow luminaria in dtDados.Rows)
                        {

                            contador++;

                            using (var comando = new SqlCommand())
                            {
                                comando.Connection = conexaoSQL;
                                comando.CommandText = "insert into luminaries (CodId,Xp,Yp,TotalCount,Hours24Count,TransformId,PoleId, LuminaryObjectId,LuminaryMeasureTypeId,Measurer,[Off],Single,LevDate,SourceFile,LuminaryPowerId,LuminaryClassId,LuminaryArmId,LuminaryTypeId,LuminaryActionId,LuminaryObsId,MunicipalityId,Code,ReleClassId,ReactorStateId,Energized,AssetId ) values ";
                                comando.CommandText += "(@CodId,@Xp,@Yp,@TotalCount,@Hours24Count,@TransformId,@PoleId, @LuminaryObjectId,@LuminaryMeasureTypeId,@Measurer,@Off1,@Single,GetDate(),@SourceFile,@LuminaryPowerId,@LuminaryClassId,@LuminaryArmId,@LuminaryTypeId,@LuminaryActionId,@LuminaryObsId,@MunicipalityId,@Code,@ReleClassId,@ReactorStateId,@Energized,@AssetId)";

                                comando.Parameters.Add(new SqlParameter("@CodId", SqlDbType.Int));
                                comando.Parameters["@CodId"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                comando.Parameters["@Xp"].Value = luminaria["Xp"];

                                comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                comando.Parameters["@Yp"].Value = luminaria["Yp"];

                                comando.Parameters.Add(new SqlParameter("@TotalCount", SqlDbType.Int));
                                comando.Parameters["@TotalCount"].Value = luminaria["TotalCount"];

                                comando.Parameters.Add(new SqlParameter("@Hours24Count", SqlDbType.Int));
                                comando.Parameters["@Hours24Count"].Value = luminaria["Hours24Count"];

                                comando.Parameters.Add(new SqlParameter("@TransformId", SqlDbType.Int));
                                comando.Parameters["@TransformId"].Value = luminaria["TransformerId"] == DBNull.Value ? IdTransformadorAuxi : ListaTransformador.Where(x => x.Label.ToUpper() == luminaria["Plate"].ToString().ToUpper()).First().Id;

                                //INSERIR AS LUMINARIAS NOS POSTES "IP" QUE FORAM INSERIDOS NA CARGA, POR ISSO PESQUISAR O BARRAMENTO PELO CÓDIGO DA IP
                                //SE NÃO ENCONTRAR A IP BOTA A LUMINARIA NO POSTE DE DISTRIBUIÇÃO
                                comando.Parameters.Add(new SqlParameter("@PoleId", SqlDbType.Int));
                                var PosteGeos = ListaPostes.Where(x => x.Barrament.ToUpper() == luminaria["Code"].ToString().ToUpper()).ToList();
                                comando.Parameters["@PoleId"].Value = PosteGeos.Count == 0 ? ListaPostes.Where(x => x.Barrament.ToUpper().Trim() == luminaria["DistributorPoleCode"].ToString().ToUpper().Trim()).First().Id : PosteGeos[0].Id;

                                comando.Parameters.Add(new SqlParameter("@LuminaryObjectId", SqlDbType.Int));
                                comando.Parameters["@LuminaryObjectId"].Value = ListaObjetoIluminado.Where(x => x.Name.ToUpper().Trim() == luminaria["IlluminatedObjectType"].ToString().ToUpper().Trim()).First().Id;

                                comando.Parameters.Add(new SqlParameter("@LuminaryMeasureTypeId", SqlDbType.Int));
                                comando.Parameters["@LuminaryMeasureTypeId"].Value = luminaria["Measurer"].ToString() == "S" ? 1 : 0;

                                comando.Parameters.Add(new SqlParameter("@Measurer", SqlDbType.VarChar));
                                if (luminaria["Measurer"].ToString() == "S")
                                {
                                    comando.Parameters["@Measurer"].Value = "000000";
                                }
                                else
                                {
                                    comando.Parameters["@Measurer"].Value = DBNull.Value;
                                }

                                comando.Parameters.Add(new SqlParameter("@Off1", SqlDbType.Int));
                                comando.Parameters["@Off1"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@Single", SqlDbType.Int));
                                comando.Parameters["@Single"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@SourceFile", SqlDbType.VarChar));
                                comando.Parameters["@SourceFile"].Value = cmbProjetos.SelectedItem.ToString().Split('-')[1];

                                comando.Parameters.Add(new SqlParameter("@LuminaryPowerId", SqlDbType.Int));
                                if (luminaria["LuminaryPower"].ToString() != "4.5")
                                {
                                    comando.Parameters["@LuminaryPowerId"].Value = ListaPotencia.Where(x => x.Id == Convert.ToInt32(luminaria["LuminaryPower"])).First().Id;
                                }
                                else
                                {
                                    comando.Parameters["@LuminaryPowerId"].Value = ListaPotencia.Where(x => x.Id == 4).First().Id;
                                }

                                comando.Parameters.Add(new SqlParameter("@LuminaryClassId", SqlDbType.Int));
                                comando.Parameters["@LuminaryClassId"].Value = ListaTipoLampada.Where(x => x.Cod.ToUpper() == luminaria["LuminaryClass"].ToString().ToUpper()).First().Id;

                                comando.Parameters.Add(new SqlParameter("@LuminaryArmId", SqlDbType.Int));
                                comando.Parameters["@LuminaryArmId"].Value = ListaTipoBraco.Where(x => x.Id == Convert.ToInt32(luminaria["ArmSize"])).First().Id;

                                comando.Parameters.Add(new SqlParameter("@LuminaryTypeId", SqlDbType.Int));
                                comando.Parameters["@LuminaryTypeId"].Value = ListaTipoLuminaria.Where(x => x.Id == Convert.ToInt32(luminaria["LuminaryType"])).First().Id;

                                comando.Parameters.Add(new SqlParameter("@LuminaryActionId", SqlDbType.Int));
                                comando.Parameters["@LuminaryActionId"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@LuminaryObsId", SqlDbType.Int));
                                comando.Parameters["@LuminaryObsId"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                                comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                                comando.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar));
                                comando.Parameters["@Code"].Value = luminaria["Code"];

                                comando.Parameters.Add(new SqlParameter("@ReleClassId", SqlDbType.Int));
                                comando.Parameters["@ReleClassId"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@ReactorStateId", SqlDbType.Int));
                                comando.Parameters["@ReactorStateId"].Value = 0;

                                comando.Parameters.Add(new SqlParameter("@Energized", SqlDbType.Int));
                                comando.Parameters["@Energized"].Value = 1;

                                comando.Parameters.Add(new SqlParameter("@AssetId", SqlDbType.Int));
                                comando.Parameters["@AssetId"].Value = 3;

                                comando.ExecuteNonQuery();

                            }

                            progressBar1.Value = progressBar1.Value + 1;

                            Console.Out.WriteLine(progressBar1.Value.ToString() + "/" + dtDados.Rows.Count.ToString() + " - " + DateTime.Now.ToLongTimeString() + "-> poste: " + luminaria["Code"].ToString());

                        }

                        conexaoSQL.Close();
                        MessageBox.Show("IPs importadas com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexaoSQL.State == ConnectionState.Open)
                        {
                            conexaoSQL.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
        private void btnImportTransformers_Click(object sender, EventArgs e)
        {
            try
            {

                #region "Dados básicos transformador"

                int IdTransformadorAuxi = 0;
                string query = "select t.id, t.PoleId, t.Label from Transforms t join poles p on t.PoleId = p.id and MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1];
                DataTable dtTransformador = (DataTable)DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                List<Transformador> ListaTransformador = new List<Transformador>();
                for (int x = 0; x <= dtTransformador.Rows.Count - 1; x++)
                {
                    IdTransformadorAuxi = Convert.ToInt32(dtTransformador.Rows[0]["id"]);

                    Transformador transformador = new Transformador();
                    transformador.Id = Convert.ToInt32(dtTransformador.Rows[x][0]);
                    transformador.PoleId = Convert.ToInt32(dtTransformador.Rows[x][1]);
                    transformador.Label = dtTransformador.Rows[x][2].ToString();
                    ListaTransformador.Add(transformador);
                }

                List<TipoTransformador> ListaTipoTransformador = new List<TipoTransformador>();
                DataTable dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,cod from TransformType", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoTransformador tipoTransformador = new TipoTransformador();
                    tipoTransformador.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    tipoTransformador.Code = dtDados.Rows[x][1].ToString();
                    ListaTipoTransformador.Add(tipoTransformador);
                }

                List<Fase> ListaFase = new List<Fase>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,cod from fase", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Fase fase = new Fase();
                    fase.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    fase.Code = dtDados.Rows[x][1].ToString();
                    ListaFase.Add(fase);
                }

                List<Poste> ListaPostes = new List<Poste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,barrament,xp,yp from poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Poste poste = new Poste();
                    poste.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    poste.Barrament = dtDados.Rows[x][1].ToString();
                    poste.Xp = Convert.ToDouble(dtDados.Rows[x][2]);
                    poste.Yp = Convert.ToDouble(dtDados.Rows[x][3]);
                    ListaPostes.Add(poste);
                }

                #endregion

                #region Query
                query = "select distinct i.\"PoleId\" as \"PoleIdCadastro\",t.\"Id\" as \"ExtId\",\"Plate\", t.\"Type\" as \"CodeType\",";
                query += "i.\"DynamicAttributes\"->> 'PhaseType' as \"CodeFase\", i.\"DynamicAttributes\"->> 'Reference' as \"Reference\", ";
                query += "ST_X(ST_Transform(i.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(i.\"geom\",3857)) as \"Yp\", p.\"DistributorPoleCode\" ";
                query += "from ";
                query += "sys.\"LightingPoints\" ip ";
                query += "join sys.\"Transformers\" t on t.\"Id\" = ip.\"TransformerId\" ";
                query += "join sys.\"Installations\" i on t.\"Id\" = i.\"Id\" and i.\"InstallationType\" = 7 ";
                query += "join sys.\"Poles\" p on i.\"PoleId\" = p.\"Id\" ";
                query += "where i.\"ProjectId\" =" + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                #endregion

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
                {
                    try
                    {
                        conexaoSQL.Open();

                        foreach (DataRow transformador in dtDados.Rows)
                        {
                            //if (Convert.ToInt32(((DataTable)DBAccessGeosWeb.ExecutarComando("select count(1) from poles p join Transforms t on p.Id=t.PoleId where MunicipalityId = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1] + " and label ='" + transformador["Plate"].ToString() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable)).Rows[0][0]) == 0)
                            if (ListaTransformador.Where(x => x.Label.ToUpper().Trim() == transformador["Plate"].ToString().ToUpper().Trim()).ToList().Count == 0)
                            {
                                if (ListaTipoTransformador.Where(x => x.Code.ToUpper() == transformador["CodeType"].ToString().ToUpper()).ToList().Count == 0)
                                {
                                    query = "insert into TransformType select max(id) + 1,'" + transformador["CodeType"].ToString() + "' from TransformType";
                                    var newIdMaterial = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                    TipoTransformador NewTipoTransformador = new TipoTransformador();
                                    NewTipoTransformador.Id = (int)newIdMaterial;
                                    NewTipoTransformador.Code = transformador["CodeType"].ToString();
                                    ListaTipoTransformador.Add(NewTipoTransformador);
                                }
                                if (ListaFase.Where(x => x.Code.ToUpper() == transformador["CodeFase"].ToString().ToUpper()).ToList().Count == 0)
                                {
                                    query = "insert into Fase select max(id) + 1,'" + transformador["CodeFase"].ToString() + "' from Fase";
                                    var newIdTipo = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                    Fase NewFase = new Fase();
                                    NewFase.Id = (int)newIdTipo;
                                    NewFase.Code = transformador["CodeFase"].ToString();
                                    ListaFase.Add(NewFase);
                                }

                                using (var comando = new SqlCommand())
                                {
                                    comando.Connection = conexaoSQL;
                                    comando.CommandText = "insert into Transforms (PoleId, ExtId,Label,TypeId,FaseId,Reference,xp,yp,InstallDate,Location) values " +
                                        "(@PoleId,@ExtId,@Label,@TypeId,@FaseId,@Reference,@xp,@yp,@InstallDate,@Location)";

                                    comando.Parameters.Add(new SqlParameter("@PoleId", SqlDbType.Int));
                                    comando.Parameters["@PoleId"].Value = ListaPostes.Where(x => x.Barrament.ToUpper() == transformador["DistributorPoleCode"].ToString().ToUpper()).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@ExtId", SqlDbType.Int));
                                    comando.Parameters["@ExtId"].Value = transformador["ExtId"];

                                    comando.Parameters.Add(new SqlParameter("@Label", SqlDbType.VarChar));
                                    comando.Parameters["@Label"].Value = transformador["Plate"];

                                    comando.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                                    comando.Parameters["@TypeId"].Value = ListaTipoTransformador.Where(x => x.Code.ToUpper() == transformador["CodeType"].ToString().ToUpper()).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@FaseId", SqlDbType.Int));
                                    comando.Parameters["@FaseId"].Value = ListaFase.Where(x => x.Code.ToUpper() == transformador["CodeFase"].ToString().ToUpper()).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@Reference", SqlDbType.VarChar));
                                    comando.Parameters["@Reference"].Value = transformador["Reference"];

                                    comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                    comando.Parameters["@Xp"].Value = transformador["Xp"];

                                    comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                    comando.Parameters["@Yp"].Value = transformador["Yp"];

                                    comando.Parameters.Add(new SqlParameter("@InstallDate", SqlDbType.DateTime));
                                    comando.Parameters["@InstallDate"].Value = DateTime.Now;

                                    comando.Parameters.Add(new SqlParameter("@DisjuntorId", SqlDbType.Int));
                                    comando.Parameters["@DisjuntorId"].Value = 0;

                                    comando.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar));
                                    comando.Parameters["@Location"].Value = "ImportGeosWebEnecad";

                                    comando.ExecuteNonQuery();

                                }
                            }

                            progressBar1.Value = progressBar1.Value + 1;
                        }

                        conexaoSQL.Close();
                        MessageBox.Show("Transformadores importados com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexaoSQL.State == ConnectionState.Open)
                        {
                            conexaoSQL.Close();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
        private void btnImportPoles_Click(object sender, EventArgs e)
        {
            //ImportePoles();

            ImportPolesLauro();
        }
        private void ImportPolesLauro()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                #region "Dados básicos postes"
                List<MaterialPoste> ListaMaterialPoste = new List<MaterialPoste>();
                DataTable dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from PoleMaterials", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    MaterialPoste material = new MaterialPoste();
                    material.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    material.Descricao = dtDados.Rows[x][1].ToString();
                    ListaMaterialPoste.Add(material);
                }

                List<TipoPoste> ListaTipoPoste = new List<TipoPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from PoleTypes", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoPoste tipo = new TipoPoste();
                    tipo.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    tipo.Descricao = dtDados.Rows[x][1].ToString();
                    ListaTipoPoste.Add(tipo);
                }

                List<AlturaPoste> ListaAlturaPoste = new List<AlturaPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleHeights", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    AlturaPoste altura = new AlturaPoste();
                    altura.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    ListaAlturaPoste.Add(altura);
                }

                List<EsforcoPoste> ListaEsforcoPoste = new List<EsforcoPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleEfforts", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    EsforcoPoste esforco = new EsforcoPoste();
                    esforco.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    ListaEsforcoPoste.Add(esforco);
                }

                List<Poste> ListaPoste = new List<Poste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,barrament,xp,yp from Poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1].ToString() + " and barrament <> '' ", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Poste poste = new Poste();
                    poste.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    poste.Barrament = dtDados.Rows[x][1].ToString();
                    poste.Xp = Convert.ToDouble(dtDados.Rows[x][2]);
                    poste.Yp = Convert.ToDouble(dtDados.Rows[x][3]);
                    ListaPoste.Add(poste);
                }
                #endregion

                #region "Postes com IP"

                string query = "";
                //postes com ip
                query = "select distinct 1 as \"Order\", p.\"Id\",p.\"DistributorPoleCode\", st_astext(g.\"geom\") as \"PoleGeometry\", st_astext(g.\"geom\") as \"PoleProjection\",";
                query += "Coalesce(p.\"Material\",'Concreto') as \"Material\", Coalesce(p.\"Height\",0) as \"Height\",Coalesce(p.\"Effort\",0) as \"Effort\", ";
                query += "Coalesce(p.\"Type\",'Circular') as \"Type\", ST_X(ST_Transform(g.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(g.\"geom\",3857)) as \"Yp\"  ";
                //query += "from sys.\"LightingPoints\" ip ";
                //query += "join sys.\"Poles\" p on ip.\"PoleId\" = p.\"Id\" ";
                query += "from sys.\"Poles\" p ";
                query += "join sys.\"GeoPoints\" g ON g.\"Id\" = p.\"Id\" ";
                query += "where g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];

                query += " union ";

                //ips que devem virar postes
                query += "select ";
                query += "distinct  2 as \"Order\", ip.\"Id\",ip.\"Code\" as \"DistributorPoleCode\", st_astext(g.\"geom\") as \"PoleGeometry\", st_astext(g.\"geom\") as \"PoleProjection\",'NI' as \"Material\",0 as \"Height\",";
                query += "0 as \"Effort\", 'NI' as \"Type\", ST_X(ST_Transform(g.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(g.\"geom\",3857)) as \"Yp\" ";
                query += "from ";
                query += "sys.\"LightingPoints\" ip ";
                query += "join sys.\"GeoPoints\" g on ip.\"Id\" = g.\"Id\" ";
                query += "where ";
                query += "g.\"Type\" = 2 and \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];

                query += " union ";

                //postes dos transformadores
                query += "select distinct 3 as \"Order\", p.\"Id\",p.\"DistributorPoleCode\", st_astext(g.\"geom\") as \"PoleGeometry\", st_astext(g.\"geom\") as \"PoleProjection\",'NI' as \"Material\",0 as \"Height\",";
                query += "0 as \"Effort\", 'NI' as \"Type\", ST_X(ST_Transform(g.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(g.\"geom\",3857)) as \"Yp\" ";
                query += "from ";
                query += "sys.\"LightingPoints\" ip ";
                query += "join sys.\"Transformers\" t on t.\"Id\" = ip.\"TransformerId\" ";
                query += "join sys.\"Installations\" i on t.\"Id\" = i.\"Id\" and i.\"InstallationType\" = 7 ";
                query += "join sys.\"Poles\" p on i.\"PoleId\" = p.\"Id\" ";
                query += "join sys.\"GeoPoints\" g on p.\"Id\" = g.\"Id\" ";
                query += "where g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                query += " order by \"Order\", \"DistributorPoleCode\" ";
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                foreach (DataRow poste in dtDados.Rows)
                {
                    if (ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).ToList().Count == 0)
                    {
                        query = "insert into PoleMaterials select max(id) + 1,'" + poste["Material"].ToString().ToUpper() + "' from PoleMaterials";
                        DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);

                        DataTable dtTipo = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleMaterials where Name ='" + poste["Material"].ToString().ToUpper() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);

                        MaterialPoste NewMaterial = new MaterialPoste();
                        NewMaterial.Id = Convert.ToInt32(dtTipo.Rows[0][0]);
                        NewMaterial.Descricao = poste["Material"].ToString().ToUpper();
                        ListaMaterialPoste.Add(NewMaterial);
                    }
                    if (ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).ToList().Count == 0)
                    {
                        query = "insert into PoleTypes select max(id) + 1,'" + poste["Type"].ToString().ToUpper() + "' from PoleTypes";
                        DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);

                        DataTable dtTipo = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleTypes where Name ='" + poste["Type"].ToString().ToUpper() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                        TipoPoste NewTipo = new TipoPoste();
                        NewTipo.Id = Convert.ToInt32(dtTipo.Rows[0][0]);
                        NewTipo.Descricao = poste["Type"].ToString().ToUpper();
                        ListaTipoPoste.Add(NewTipo);
                    }
                    if (ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).ToList().Count == 0)
                    {
                        query = "insert into PoleHeights values (" + poste["Height"].ToString() + ")";
                        var newIdAltura = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                        AlturaPoste NewAltura = new AlturaPoste();
                        NewAltura.Id = (int)newIdAltura;
                        ListaAlturaPoste.Add(NewAltura);
                    }
                    if (ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).ToList().Count == 0)
                    {
                        query = "insert into PoleEfforts values (" + poste["Effort"].ToString() + ")";
                        var newIdEsforco = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                        EsforcoPoste NewEsforco = new EsforcoPoste();
                        NewEsforco.Id = (int)newIdEsforco;
                        ListaEsforcoPoste.Add(NewEsforco);
                    }

                    progressBar1.Value = progressBar1.Value + 1;
                }

                #endregion

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                int qtdPostesLauroEmSalvador = 0;
                int orderAux = 1;

                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
                {
                    try
                    {
                        conexaoSQL.Open();
                        DataTable tabela = new DataTable();

                        foreach (DataRow posteCadastro in dtDados.Rows)
                        {

                            using (var comando = new SqlCommand())
                            {
                                comando.Parameters.Clear();
                                comando.Connection = conexaoSQL;

                                if (Convert.ToInt32(posteCadastro["Order"]) != orderAux)
                                {
                                    orderAux = Convert.ToInt32(posteCadastro["Order"]);
                                    ListaPoste = new List<Poste>();
                                    dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,barrament,xp,yp from Poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1].ToString() + " and barrament <> '' ", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                                    for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                                    {
                                        Poste NewPoste = new Poste();
                                        NewPoste.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                                        NewPoste.Barrament = dtDados.Rows[x][1].ToString();
                                        NewPoste.Xp = Convert.ToDouble(dtDados.Rows[x][2]);
                                        NewPoste.Yp = Convert.ToDouble(dtDados.Rows[x][3]);
                                        ListaPoste.Add(NewPoste);
                                    }
                                }

                                if (posteCadastro["Order"].ToString() == "1")
                                {
                                    if (posteCadastro["DistributorPoleCode"].ToString() != "X999999")
                                    {
                                        //verifica se o poste com ip também está cadastrado em Salvador, se estiver atualiza para Lauro
                                        //if (Convert.ToInt32(((DataTable)DBAccessGeosWeb.ExecutarComando("select count(1) from poles where municipalityid = 196 and barrament ='" + poste["DistributorPoleCode"].ToString() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable)).Rows[0][0]) != 0)
                                        //{
                                        //    query = "update poles set municipalityid = 192 where municipalityid = 196 and barrament = @barrament ";
                                        //    comando.CommandText = query;
                                        //    comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                                        //    comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];
                                        //    comando.ExecuteNonQuery();
                                        //    qtdPostesLauroEmSalvador++;
                                        //}

                                        var posteGeos = ListaPoste.Where(x => x.Barrament.ToUpper().Trim() == posteCadastro["DistributorPoleCode"].ToString().ToUpper().Trim()).ToList();
                                        if (posteGeos.Count == 0)
                                        {
                                            Poste newposte = new Poste();
                                            newposte.Barrament = posteCadastro["DistributorPoleCode"].ToString();
                                            newposte.Xp = Convert.ToDouble(posteCadastro["xp"]);
                                            newposte.Yp = Convert.ToDouble(posteCadastro["yp"]);
                                            ListaPoste.Add(newposte);

                                            InsertPoste(posteCadastro, ref ListaMaterialPoste, ref ListaTipoPoste, ref ListaAlturaPoste, ref ListaEsforcoPoste);

                                        }
                                        else
                                        {
                                            query = "update poles set xp = @xp,yp = @yp,point = dbo.transformWebmercatorToWGS84(@xp,@yp) where id = @id and municipalityid = @municipalityid and barrament = @barrament ";
                                            comando.CommandText = query;
                                            comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                                            comando.Parameters["@id"].Value = posteGeos[0].Id;
                                            comando.Parameters.Add(new SqlParameter("@extid", SqlDbType.Int));
                                            comando.Parameters["@extid"].Value = posteCadastro["Id"]; ;
                                            comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                                            comando.Parameters["@Barrament"].Value = posteCadastro["DistributorPoleCode"];
                                            comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                            comando.Parameters["@Xp"].Value = posteCadastro["Xp"];
                                            comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                            comando.Parameters["@Yp"].Value = posteCadastro["Yp"];
                                            comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                                            comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);
                                            comando.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        //se for x999999 inserir direto pois esse poste não existe no geosweb
                                        Poste newposte = new Poste();
                                        newposte.Barrament = posteCadastro["DistributorPoleCode"].ToString();
                                        newposte.Xp = Convert.ToDouble(posteCadastro["xp"]);
                                        newposte.Yp = Convert.ToDouble(posteCadastro["yp"]);
                                        ListaPoste.Add(newposte);

                                        InsertPoste(posteCadastro, ref ListaMaterialPoste, ref ListaTipoPoste, ref ListaAlturaPoste, ref ListaEsforcoPoste);
                                    }
                                }
                                else if (posteCadastro["Order"].ToString() == "2")
                                {
                                    if (posteCadastro["DistributorPoleCode"].ToString() == "IP-X021581")
                                    {
                                        string erro = "";
                                    }
                                    if (ListaPoste.Where(x => Math.Round(Convert.ToDouble(x.Xp), 8) == Math.Round(Convert.ToDouble(posteCadastro["Xp"]), 8) &&
                                                              Math.Round(Convert.ToDouble(x.Yp), 8) == Math.Round(Convert.ToDouble(posteCadastro["Yp"]), 8)).ToList().Count == 0)
                                    {
                                        InsertPoste(posteCadastro, ref ListaMaterialPoste, ref ListaTipoPoste, ref ListaAlturaPoste, ref ListaEsforcoPoste);

                                        Poste NewPoste = new Poste();
                                        NewPoste.Id = Convert.ToInt32(ListaPoste.Count + 1);
                                        NewPoste.Barrament = posteCadastro["DistributorPoleCode"].ToString();
                                        NewPoste.Xp = Convert.ToDouble(posteCadastro["xp"]);
                                        NewPoste.Yp = Convert.ToDouble(posteCadastro["yp"]);
                                        ListaPoste.Add(NewPoste);
                                    }
                                }
                                else if (posteCadastro["Order"].ToString() == "3")
                                {
                                    if (ListaPoste.Where(x => x.Barrament.ToUpper().Trim() == posteCadastro["DistributorPoleCode"].ToString().ToUpper().Trim()).ToList().Count == 0)
                                    {
                                        Poste newposte = new Poste();
                                        newposte.Barrament = posteCadastro["DistributorPoleCode"].ToString();
                                        newposte.Xp = Convert.ToDouble(posteCadastro["xp"]);
                                        newposte.Yp = Convert.ToDouble(posteCadastro["yp"]);
                                        ListaPoste.Add(newposte);

                                        InsertPoste(posteCadastro, ref ListaMaterialPoste, ref ListaTipoPoste, ref ListaAlturaPoste, ref ListaEsforcoPoste);
                                    }
                                }

                            }

                            progressBar1.Value = progressBar1.Value + 1;

                            Console.Out.WriteLine(progressBar1.Value.ToString() + "/" + dtDados.Rows.Count.ToString() + " - " + DateTime.Now.ToLongTimeString() + "-> poste: " + posteCadastro["DistributorPoleCode"].ToString());

                        }

                        conexaoSQL.Close();

                        MessageBox.Show("Postes que possuem IP, importados com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexaoSQL.State == ConnectionState.Open)
                        {
                            conexaoSQL.Close();
                        }
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ImportePoles()
        {
            try
            {
                #region "Dados básicos postes"
                List<MaterialPoste> ListaMaterialPoste = new List<MaterialPoste>();
                DataTable dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from PoleMaterials", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    MaterialPoste material = new MaterialPoste();
                    material.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    material.Descricao = dtDados.Rows[x][1].ToString();
                    ListaMaterialPoste.Add(material);
                }

                List<TipoPoste> ListaTipoPoste = new List<TipoPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,name from PoleTypes", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    TipoPoste tipo = new TipoPoste();
                    tipo.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    tipo.Descricao = dtDados.Rows[x][1].ToString();
                    ListaTipoPoste.Add(tipo);
                }

                List<AlturaPoste> ListaAlturaPoste = new List<AlturaPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleHeights", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    AlturaPoste altura = new AlturaPoste();
                    altura.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    ListaAlturaPoste.Add(altura);
                }

                List<EsforcoPoste> ListaEsforcoPoste = new List<EsforcoPoste>();
                dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleEfforts", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    EsforcoPoste esforco = new EsforcoPoste();
                    esforco.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    ListaEsforcoPoste.Add(esforco);
                }
                #endregion

                #region "Postes com IP"
                string query = "";
                query = "select distinct 1 as \"Order\", p.\"Id\",p.\"DistributorPoleCode\", st_astext(g.\"geom\") as \"PoleGeometry\", st_astext(g.\"geom\") as \"PoleProjection\",";
                query += "Coalesce(p.\"Material\",'Concreto') as \"Material\", Coalesce(p.\"Height\",0) as \"Height\",Coalesce(p.\"Effort\",0) as \"Effort\", ";
                query += "Coalesce(p.\"Type\",'Circular') as \"Type\", ST_X(ST_Transform(g.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(g.\"geom\",3857)) as \"Yp\"  ";
                query += "from sys.\"LightingPoints\" ip ";
                query += "join sys.\"Poles\" p on ip.\"PoleId\" = p.\"Id\" ";
                query += "join sys.\"GeoPoints\" g ON g.\"Id\" = p.\"Id\" ";
                query += "where g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                //query += " AND p.\"DistributorPoleCode\" = 'X999999' ";
                query += " union ";
                query += "select ";
                query += "distinct  2 as \"Order\", ip.\"Id\",ip.\"Code\" as \"DistributorPoleCode\", st_astext(g.\"geom\") as \"PoleGeometry\", st_astext(g.\"geom\") as \"PoleProjection\",'NI' as \"Material\",0 as \"Height\",";
                query += "0 as \"Effort\", 'NI' as \"Type\", ST_X(ST_Transform(g.\"geom\",3857)) as \"Xp\", ST_Y(ST_Transform(g.\"geom\",3857)) as \"Yp\" ";
                query += "from ";
                query += "sys.\"LightingPoints\" ip ";
                query += "join sys.\"GeoPoints\" g on ip.\"Id\" = g.\"Id\" ";
                query += "where ";
                //query += " ip.\"PoleId\" in (792628,792747,792878,794291) AND ";
                query += "g.\"Type\" = 2 and \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                query += " order by \"Order\" ";
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
                {
                    try
                    {
                        conexaoSQL.Open();

                        foreach (DataRow poste in dtDados.Rows)
                        {
                            using (var comando = new SqlCommand())
                            {
                                comando.Parameters.Clear();
                                comando.Connection = conexaoSQL;
                                if (poste["DistributorPoleCode"].ToString() == "X999999")
                                {
                                    string teste = "";
                                }
                                query = "select coalesce(sum (qte),0) from (";
                                query += "select 1 qte from poles where municipalityid = @municipalityid and barrament = @barrament ";
                                query += "union all ";
                                query += "select 2 qte from poles where municipalityid = @municipalityid and xp = @xp and yp = @yp) t";
                                comando.CommandText = query;

                                comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                                comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];
                                comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                comando.Parameters["@Xp"].Value = poste["Xp"];
                                comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                comando.Parameters["@Yp"].Value = poste["Yp"];
                                comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                                comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                                DataTable tabela = new DataTable();
                                DbDataReader leitor = comando.ExecuteReader();
                                tabela.Load(leitor);
                                Random GeradorDeNumerosAleatorios = new Random();
                                tabela.TableName = "TABELA" + GeradorDeNumerosAleatorios.Next(1, 99999999).ToString();
                                leitor.Close();

                                //if (Convert.ToInt32(((DataTable)DBAccessGeosWeb.ExecutarComando("select count(1) from poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1] + " and barrament ='" + poste["DistributorPoleCode"].ToString() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable)).Rows[0][0]) == 0)
                                if (Convert.ToInt32(tabela.Rows[0][0]) == 0 || Convert.ToInt32(tabela.Rows[0][0]) == 1)
                                {
                                    if (ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).ToList().Count == 0)
                                    {
                                        query = "insert into PoleMaterials select max(id) + 1,'" + poste["Material"].ToString() + "' from PoleMaterials";
                                        var newIdMaterial = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                        MaterialPoste NewMaterial = new MaterialPoste();
                                        NewMaterial.Id = 5;
                                        NewMaterial.Descricao = poste["Material"].ToString();
                                        ListaMaterialPoste.Add(NewMaterial);
                                    }
                                    if (ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).ToList().Count == 0)
                                    {
                                        query = "insert into PoleTypes select max(id) + 1,'" + poste["Type"].ToString() + "' from PoleTypes";
                                        var newIdTipo = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                        TipoPoste NewTipo = new TipoPoste();
                                        NewTipo.Id = 93;
                                        NewTipo.Descricao = poste["Type"].ToString();
                                        ListaTipoPoste.Add(NewTipo);
                                    }
                                    if (ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).ToList().Count == 0)
                                    {
                                        query = "insert into PoleHeights values (" + poste["Height"].ToString() + ")";
                                        var newIdAltura = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                        AlturaPoste NewAltura = new AlturaPoste();
                                        NewAltura.Id = (int)newIdAltura;
                                        ListaAlturaPoste.Add(NewAltura);
                                    }
                                    if (ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).ToList().Count == 0)
                                    {
                                        query = "insert into PoleEfforts values (" + poste["Effort"].ToString() + ")";
                                        var newIdEsforco = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                                        EsforcoPoste NewEsforco = new EsforcoPoste();
                                        NewEsforco.Id = (int)newIdEsforco;
                                        ListaEsforcoPoste.Add(NewEsforco);
                                    }

                                    comando.CommandText = "insert into poles (ExtId,Barrament,Geometry,Projection,MaterialId,TypeId,SU_Irregular,Xp,Yp,AssetBit,PoleEffortId,PoleHeightId,SU_Count,MunicipalityId) values (@ExtId,@Barrament,@Geometry,@Projection,@MaterialId,@TypeId,0,@Xp,@Yp,0,@PoleEffortId,@PoleHeightId,0,@MunicipalityId)";
                                    comando.Parameters.Clear();

                                    comando.Parameters.Add(new SqlParameter("@ExtId", SqlDbType.Int));
                                    comando.Parameters["@ExtId"].Value = poste["Id"];

                                    comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                                    comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];

                                    comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                                    comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                                    SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                                    comando.Parameters["@Geometry"].Value = geometry;

                                    comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                                    comando.Parameters["@Projection"].UdtTypeName = "geometry";
                                    SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                                    comando.Parameters["@Projection"].Value = projection;

                                    comando.Parameters.Add(new SqlParameter("@MaterialId", SqlDbType.Int));
                                    comando.Parameters["@MaterialId"].Value = ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                                    comando.Parameters["@TypeId"].Value = ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                    comando.Parameters["@Xp"].Value = poste["Xp"];

                                    comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                    comando.Parameters["@Yp"].Value = poste["Yp"];

                                    comando.Parameters.Add(new SqlParameter("@PoleEffortId", SqlDbType.Int));
                                    comando.Parameters["@PoleEffortId"].Value = ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@PoleHeightId", SqlDbType.Int));
                                    comando.Parameters["@PoleHeightId"].Value = ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).First().Id;

                                    comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                                    comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                                    comando.ExecuteNonQuery();

                                }
                                else if (Convert.ToInt32(tabela.Rows[0][0]) == 2)
                                {
                                    comando.Parameters.Clear();
                                    comando.Connection = conexaoSQL;
                                    comando.CommandText = "update poles set Geometry = @Geometry,Projection=@Projection,Xp=@Xp,Yp=@Yp where MunicipalityId= @MunicipalityId and barrament = @barrament";

                                    comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                                    comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];
                                    comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                                    comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                                    SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                                    comando.Parameters["@Geometry"].Value = geometry;
                                    comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                                    comando.Parameters["@Projection"].UdtTypeName = "geometry";
                                    SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                                    comando.Parameters["@Projection"].Value = projection;
                                    comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                                    comando.Parameters["@Xp"].Value = poste["Xp"];
                                    comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                                    comando.Parameters["@Yp"].Value = poste["Yp"];
                                    comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                                    comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);
                                    comando.ExecuteNonQuery();

                                }
                            }



                            //if (Convert.ToInt32(((DataTable)DBAccessGeosWeb.ExecutarComando("select count(1) from poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1] + " and barrament ='" + poste["DistributorPoleCode"].ToString() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable)).Rows[0][0]) == 0)
                            ////if (Convert.ToInt32(tabela.Rows[0][0]) == 0)
                            //{
                            //    if (ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).ToList().Count == 0)
                            //    {
                            //        query = "insert into PoleMaterials select max(id) + 1,'" + poste["Material"].ToString() + "' from PoleMaterials";
                            //        var newIdMaterial = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                            //        MaterialPoste NewMaterial = new MaterialPoste();
                            //        NewMaterial.Id = (int)newIdMaterial;
                            //        NewMaterial.Descricao = poste["Material"].ToString();
                            //        ListaMaterialPoste.Add(NewMaterial);
                            //    }
                            //    if (ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).ToList().Count == 0)
                            //    {
                            //        query = "insert into PoleTypes select max(id) + 1,'" + poste["Type"].ToString() + "' from PoleTypes";
                            //        var newIdTipo = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                            //        TipoPoste NewTipo = new TipoPoste();
                            //        NewTipo.Id = (int)newIdTipo;
                            //        NewTipo.Descricao = poste["Type"].ToString();
                            //        ListaTipoPoste.Add(NewTipo);
                            //    }
                            //    if (ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).ToList().Count == 0)
                            //    {
                            //        query = "insert into PoleHeights values (" + poste["Height"].ToString() + ")";
                            //        var newIdAltura = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                            //        AlturaPoste NewAltura = new AlturaPoste();
                            //        NewAltura.Id = (int)newIdAltura;
                            //        ListaAlturaPoste.Add(NewAltura);
                            //    }
                            //    if (ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).ToList().Count == 0)
                            //    {
                            //        query = "insert into PoleEfforts values (" + poste["Effort"].ToString() + ")";
                            //        var newIdEsforco = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                            //        EsforcoPoste NewEsforco = new EsforcoPoste();
                            //        NewEsforco.Id = (int)newIdEsforco;
                            //        ListaEsforcoPoste.Add(NewEsforco);
                            //    }

                            //    using (var comando = new SqlCommand())
                            //    {
                            //        comando.Connection = conexaoSQL;
                            //        comando.CommandText = "insert into poles (ExtId,Barrament,Geometry,Projection,MaterialId,TypeId,SU_Irregular,Xp,Yp,AssetBit,PoleEffortId,PoleHeightId,SU_Count,MunicipalityId) values (@ExtId,@Barrament,@Geometry,@Projection,@MaterialId,@TypeId,0,@Xp,@Yp,0,@PoleEffortId,@PoleHeightId,0,@MunicipalityId)";

                            //        comando.Parameters.Add(new SqlParameter("@ExtId", SqlDbType.Int));
                            //        comando.Parameters["@ExtId"].Value = poste["Id"];

                            //        comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                            //        comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];

                            //        comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                            //        comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                            //        SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                            //        comando.Parameters["@Geometry"].Value = geometry;

                            //        comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                            //        comando.Parameters["@Projection"].UdtTypeName = "geometry";
                            //        SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                            //        comando.Parameters["@Projection"].Value = projection;

                            //        comando.Parameters.Add(new SqlParameter("@MaterialId", SqlDbType.Int));
                            //        comando.Parameters["@MaterialId"].Value = ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).First().Id;

                            //        comando.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                            //        comando.Parameters["@TypeId"].Value = ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).First().Id;

                            //        comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                            //        comando.Parameters["@Xp"].Value = poste["Xp"];

                            //        comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                            //        comando.Parameters["@Yp"].Value = poste["Yp"];

                            //        comando.Parameters.Add(new SqlParameter("@PoleEffortId", SqlDbType.Int));
                            //        comando.Parameters["@PoleEffortId"].Value = ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).First().Id;

                            //        comando.Parameters.Add(new SqlParameter("@PoleHeightId", SqlDbType.Int));
                            //        comando.Parameters["@PoleHeightId"].Value = ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).First().Id;

                            //        comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                            //        comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                            //        comando.ExecuteNonQuery();

                            //    }

                            //}
                            //else
                            //{
                            //    using (var comando = new SqlCommand())
                            //    {
                            //        comando.Connection = conexaoSQL;
                            //        comando.CommandText = "update poles set Geometry = @Geometry,Projection=@Projection,Xp=@Xp,Yp=@Yp where MunicipalityId= @MunicipalityId and barrament = @barrament";

                            //        comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                            //        comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];

                            //        comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                            //        comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                            //        SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                            //        comando.Parameters["@Geometry"].Value = geometry;

                            //        comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                            //        comando.Parameters["@Projection"].UdtTypeName = "geometry";
                            //        SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                            //        comando.Parameters["@Projection"].Value = projection;

                            //        comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                            //        comando.Parameters["@Xp"].Value = poste["Xp"];

                            //        comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                            //        comando.Parameters["@Yp"].Value = poste["Yp"];

                            //        comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                            //        comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                            //        comando.ExecuteNonQuery();

                            //    }
                            //}

                            progressBar1.Value = progressBar1.Value + 1;
                        }

                        conexaoSQL.Close();
                        MessageBox.Show("Postes que possuem IP, importados com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexaoSQL.State == ConnectionState.Open)
                        {
                            conexaoSQL.Close();
                        }
                    }

                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bntImportarFotos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string query = "";

                List<Poste> ListaPostes = new List<Poste>();
                DataTable dtDados = (DataTable)DBAccessGeosWeb.ExecutarComando("select id,extid,barrament from Poles where municipalityid = " + cmbMunicipios.SelectedItem.ToString().Split('-')[1], CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    Poste poste = new Poste();
                    poste.Id = Convert.ToInt32(dtDados.Rows[x][0]);
                    poste.ExtId = Convert.ToInt32(dtDados.Rows[x][1]);
                    poste.Barrament = dtDados.Rows[x][2].ToString();
                    ListaPostes.Add(poste);
                }

                query = "select \"Poles\".\"Id\", \"Poles\".\"DistributorPoleCode\" ,replace(\"Path\",'" + txtPrefixoCaminhoFotos.Text + "','') as \"Path\" ";
                query += "from sys.\"Photos\" p ";
                query += "join sys.\"Tasks\" ON \"Tasks\".\"Id\" = p.\"TaskId\" ";
                query += "join sys.\"GeoPoints\" ON \"GeoPoints\".\"Id\" = \"Tasks\".\"GeoPointId\" and \"GeoPoints\".\"Type\" =1 ";
                query += "join sys.\"Poles\" ON \"Poles\".\"Id\" = \"GeoPoints\".\"Id\" ";
                query += "where  \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " and \"Poles\".\"DistributorPoleCode\" is not null ";
                //query += "where  \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " and \"Poles\".\"DistributorPoleCode\" is not null and \"Poles\".\"DistributorPoleCode\" = 'P486171' ";
                query += " union ";
                query += "select \"GeoPoints\".\"Id\", \"GeoPoints\".\"Code\" as \"DistributorPoleCode\" ,replace(\"Path\",'" + txtPrefixoCaminhoFotos.Text + "','') as \"Path\" ";
                query += "from sys.\"Photos\" p ";
                query += "join sys.\"Tasks\" ON \"Tasks\".\"Id\" = p.\"TaskId\" ";
                query += "join sys.\"GeoPoints\" ON \"GeoPoints\".\"Id\" = \"Tasks\".\"GeoPointId\" and \"GeoPoints\".\"Type\" = 2 ";
                query += "join sys.\"LightingPoints\" ip  on ip.\"Id\" = \"GeoPoints\".\"Id\" ";
                query += "where \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                //query += "where  ip.\"PoleId\" = 1094373 and \"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
                dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

                progressBar1.Value = 0;
                progressBar1.Maximum = dtDados.Rows.Count;

                using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
                {
                    try
                    {
                        conexaoSQL.Open();

                        foreach (DataRow posteCadastro in dtDados.Rows)
                        {
                            using (var comando = new SqlCommand())
                            {
                                comando.Connection = conexaoSQL;

                                List<Poste> posteGeos = new List<Poste>();
                                if (posteCadastro["Id"].ToString() == "X999999")
                                {
                                    posteGeos = ListaPostes.Where(x => x.ExtId == Convert.ToInt32(posteCadastro["Id"])).ToList();
                                }
                                else
                                {
                                    posteGeos = ListaPostes.Where(x => x.Barrament.ToUpper().Trim() == posteCadastro["DistributorPoleCode"].ToString().ToUpper().Trim()).ToList();
                                }
                                if (posteGeos.Count != 0)
                                {
                                    comando.CommandText = "insert into DocumentEntities (DocType,Uri,PoleId,UploadDate) values ('picture/jpg',@Uri,@id, getdate())";

                                    comando.Parameters.Add(new SqlParameter("@Uri", SqlDbType.VarChar));
                                    comando.Parameters["@Uri"].Value = posteCadastro["Path"];

                                    comando.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                                    comando.Parameters["@id"].Value = posteGeos[0].Id;

                                    comando.ExecuteNonQuery();

                                    Console.Out.WriteLine("id:" + posteCadastro["Id"].ToString() + " poste --> " + posteCadastro["DistributorPoleCode"].ToString() + " - " + progressBar1.Value.ToString() + "/" + progressBar1.Maximum.ToString() + " - " + DateTime.Now.ToLongTimeString());
                                }
                                else
                                {
                                    Console.Out.WriteLine("id:" + posteCadastro["Id"].ToString() + " poste --> " + posteCadastro["DistributorPoleCode"].ToString() + " - " + progressBar1.Value.ToString() + "/" + progressBar1.Maximum.ToString() + " - " + DateTime.Now.ToLongTimeString());
                                }
                            }

                            progressBar1.Value = progressBar1.Value + 1;
                        }

                        conexaoSQL.Close();

                        this.Cursor = Cursors.Default;

                        MessageBox.Show("Fotos importadas com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (conexaoSQL.State == ConnectionState.Open)
                        {
                            conexaoSQL.Close();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region "Classes"
        private class MaterialPoste
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
        }
        private class TipoPoste
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
        }
        private class AlturaPoste
        {
            public int Id { get; set; }
        }
        private class EsforcoPoste
        {
            public int Id { get; set; }
        }
        private class Transformador
        {
            public int Id { get; set; }
            public int PoleId { get; set; }
            public int ExtId { get; set; }
            public string Label { get; set; }
        }
        private class TipoTransformador
        {
            public int Id { get; set; }
            public string Code { get; set; }
        }
        private class TipoLuminaria
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private class TipoLampadaLuminaria
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Cod { get; set; }
        }
        private class PotenciaLuminaria
        {
            public int Id { get; set; }
        }
        private class ObjetoIluminadoLuminaria
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private class TipoBraco
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private class Poste
        {
            public int Id { get; set; }
            public int ExtId { get; set; }
            public string Barrament { get; set; }
            public double Xp { get; set; }
            public double Yp { get; set; }
            public int MunicipalityId { get; set; }
        }
        private class Fase
        {
            public int Id { get; set; }
            public string Code { get; set; }
        }
        private class IP
        {
            public int Id { get; set; }
            public string BarramentoPoste { get; set; }
            public int QuantidadeIPPoste { get; set; }
            public double Xp { get; set; }
            public double Yp { get; set; }

        }

        #endregion

        #region " Diversos " 
        private void InsertPoste(DataRow poste, ref List<MaterialPoste> ListaMaterialPoste, ref List<TipoPoste> ListaTipoPoste, ref List<AlturaPoste> ListaAlturaPoste, ref List<EsforcoPoste> ListaEsforcoPoste)
        {
            string query;

            if (ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).ToList().Count == 0)
            {
                query = "insert into PoleMaterials select max(id) + 1,'" + poste["Material"].ToString().ToUpper() + "' from PoleMaterials";
                DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);

                DataTable dtTipo = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleMaterials where Name ='" + poste["Material"].ToString().ToUpper() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);

                MaterialPoste NewMaterial = new MaterialPoste();
                NewMaterial.Id = Convert.ToInt32(dtTipo.Rows[0][0]);
                NewMaterial.Descricao = poste["Material"].ToString().ToUpper();
                ListaMaterialPoste.Add(NewMaterial);
            }
            if (ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).ToList().Count == 0)
            {
                query = "insert into PoleTypes select max(id) + 1,'" + poste["Type"].ToString().ToUpper() + "' from PoleTypes";
                DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteNonQuery);

                DataTable dtTipo = (DataTable)DBAccessGeosWeb.ExecutarComando("select id from PoleTypes where Name ='" + poste["Type"].ToString().ToUpper() + "'", CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteDataTable);
                TipoPoste NewTipo = new TipoPoste();
                NewTipo.Id = Convert.ToInt32(dtTipo.Rows[0][0]);
                NewTipo.Descricao = poste["Type"].ToString().ToUpper();
                ListaTipoPoste.Add(NewTipo);
            }
            if (ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).ToList().Count == 0)
            {
                query = "insert into PoleHeights values (" + poste["Height"].ToString() + ")";
                var newIdAltura = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                AlturaPoste NewAltura = new AlturaPoste();
                NewAltura.Id = (int)newIdAltura;
                ListaAlturaPoste.Add(NewAltura);
            }
            if (ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).ToList().Count == 0)
            {
                query = "insert into PoleEfforts values (" + poste["Effort"].ToString() + ")";
                var newIdEsforco = DBAccessGeosWeb.ExecutarComando(query, CommandType.Text, null, DBAccessGeosWeb.TypeCommand.ExecuteScalar);
                EsforcoPoste NewEsforco = new EsforcoPoste();
                NewEsforco.Id = (int)newIdEsforco;
                ListaEsforcoPoste.Add(NewEsforco);
            }

            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
            {
                try
                {
                    conexaoSQL.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Parameters.Clear();
                        comando.Connection = conexaoSQL;

                        comando.CommandText = "insert into poles (ExtId,Barrament,Geometry,Projection,MaterialId,TypeId,SU_Irregular,Xp,Yp,AssetBit,PoleEffortId,PoleHeightId,SU_Count,MunicipalityId,Point) values (@ExtId,@Barrament,@Geometry,@Projection,@MaterialId,@TypeId,0,@Xp,@Yp,0,@PoleEffortId,@PoleHeightId,0,@MunicipalityId, dbo.transformWebmercatorToWGS84(@xp,@yp))";
                        comando.Parameters.Clear();

                        comando.Parameters.Add(new SqlParameter("@ExtId", SqlDbType.Int));
                        comando.Parameters["@ExtId"].Value = poste["Id"];

                        comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                        comando.Parameters["@Barrament"].Value = poste["DistributorPoleCode"];

                        comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                        comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                        SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                        comando.Parameters["@Geometry"].Value = geometry;

                        comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                        comando.Parameters["@Projection"].UdtTypeName = "geometry";
                        SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                        comando.Parameters["@Projection"].Value = projection;

                        comando.Parameters.Add(new SqlParameter("@MaterialId", SqlDbType.Int));
                        comando.Parameters["@MaterialId"].Value = ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).First().Id;

                        comando.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                        comando.Parameters["@TypeId"].Value = ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).First().Id;

                        comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                        comando.Parameters["@Xp"].Value = poste["Xp"];

                        comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                        comando.Parameters["@Yp"].Value = poste["Yp"];

                        comando.Parameters.Add(new SqlParameter("@PoleEffortId", SqlDbType.Int));
                        comando.Parameters["@PoleEffortId"].Value = ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).First().Id;

                        comando.Parameters.Add(new SqlParameter("@PoleHeightId", SqlDbType.Int));
                        comando.Parameters["@PoleHeightId"].Value = ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).First().Id;

                        comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                        comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                        //comando.Parameters.Add(new SqlParameter("@Point", SqlDbType.Udt));
                        //comando.Parameters["@Point"].UdtTypeName = "geometry";
                        //SqlGeometry Point = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                        //comando.Parameters["@Point"].Value = Point;

                        comando.ExecuteNonQuery();

                    }
                    conexaoSQL.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (conexaoSQL.State == ConnectionState.Open)
                    {
                        conexaoSQL.Close();
                    }
                }
            }
        }
        private void InsertPoste(Poste newPoste)
        {
            string query;

            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            using (var conexaoSQL = new SqlConnection(DBSettingGeosWeb.ConnectionString))
            {
                try
                {
                    conexaoSQL.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Parameters.Clear();
                        comando.Connection = conexaoSQL;

                        comando.CommandText = "insert into poles (ExtId,Barrament,Geometry,Projection,MaterialId,TypeId,SU_Irregular,Xp,Yp,AssetBit,PoleEffortId,PoleHeightId,SU_Count,MunicipalityId,Point) values (@ExtId,@Barrament,@Geometry,@Projection,@MaterialId,@TypeId,0,@Xp,@Yp,0,@PoleEffortId,@PoleHeightId,0,@MunicipalityId,@Point)";
                        comando.Parameters.Clear();

                        comando.Parameters.Add(new SqlParameter("@Barrament", SqlDbType.VarChar));
                        comando.Parameters["@Barrament"].Value = newPoste.Barrament;

                        //comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                        //comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                        //SqlGeometry geometry = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                        //comando.Parameters["@Geometry"].Value = geometry;

                        //comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                        //comando.Parameters["@Projection"].UdtTypeName = "geometry";
                        //SqlGeometry projection = SqlGeometry.Parse(poste["PoleProjection"].ToString());
                        //comando.Parameters["@Projection"].Value = projection;

                        //comando.Parameters.Add(new SqlParameter("@MaterialId", SqlDbType.Int));
                        //comando.Parameters["@MaterialId"].Value = ListaMaterialPoste.Where(x => x.Descricao.ToUpper() == poste["Material"].ToString().ToUpper()).First().Id;

                        //comando.Parameters.Add(new SqlParameter("@TypeId", SqlDbType.Int));
                        //comando.Parameters["@TypeId"].Value = ListaTipoPoste.Where(x => x.Descricao.ToUpper() == poste["Type"].ToString().ToUpper()).First().Id;

                        //comando.Parameters.Add(new SqlParameter("@Xp", SqlDbType.Float));
                        //comando.Parameters["@Xp"].Value = poste["Xp"];

                        //comando.Parameters.Add(new SqlParameter("@Yp", SqlDbType.Float));
                        //comando.Parameters["@Yp"].Value = poste["Yp"];

                        //comando.Parameters.Add(new SqlParameter("@PoleEffortId", SqlDbType.Int));
                        //comando.Parameters["@PoleEffortId"].Value = ListaEsforcoPoste.Where(x => x.Id == Convert.ToInt32(poste["Effort"])).First().Id;

                        //comando.Parameters.Add(new SqlParameter("@PoleHeightId", SqlDbType.Int));
                        //comando.Parameters["@PoleHeightId"].Value = ListaAlturaPoste.Where(x => x.Id == Convert.ToInt32(poste["Height"])).First().Id;

                        //comando.Parameters.Add(new SqlParameter("@MunicipalityId", SqlDbType.Int));
                        //comando.Parameters["@MunicipalityId"].Value = Convert.ToInt32(cmbMunicipios.SelectedItem.ToString().Split('-')[1]);

                        //comando.Parameters.Add(new SqlParameter("@Point", SqlDbType.Udt));
                        //comando.Parameters["@Point"].UdtTypeName = "geometry";
                        //SqlGeometry Point = SqlGeometry.Parse(poste["PoleGeometry"].ToString());
                        //comando.Parameters["@Point"].Value = Point;

                        comando.ExecuteNonQuery();

                    }
                    conexaoSQL.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (conexaoSQL.State == ConnectionState.Open)
                    {
                        conexaoSQL.Close();
                    }
                }
            }
        }
        #endregion
    }
}
