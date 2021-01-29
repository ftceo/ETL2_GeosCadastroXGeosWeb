using DAO;
using Microsoft.SqlServer.Types;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class Form1 : Form
    {
        #region " Inicio "
        public Form1()
        {
            InitializeComponent();
            if (DBSettingCadastro.TipoBanco == "ePgSQL")
            {
                DataTable dtProjetos = (DataTable)DBAccessCadastro.ExecutarComando("select \"Id\",\"Name\" ,\"ClientId\" from sys.\"Projects\" order by \"Id\" ", CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtProjetos.Rows.Count - 1; x++)
                {
                    cmbProjetos.Items.Add(dtProjetos.Rows[x]["Id"].ToString() + " - " + dtProjetos.Rows[x]["Name"].ToString());
                }
            }

            //DataTable dtUF = (DataTable)DBAccessGeos.ExecutarComando("select Id,Name from Municipalities order by name ", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            DataTable dtUF = (DataTable)DBAccessGeos.ExecutarComando("select id, name,cod from States order by name", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            for (int x = 0; x <= dtUF.Rows.Count - 1; x++)
            {
                cmbUF.Items.Add(dtUF.Rows[x]["Id"].ToString() + " - " + dtUF.Rows[x]["Name"].ToString() + " - " + dtUF.Rows[x]["cod"].ToString());
            }
        }
        private void cmbUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIdMunicipioGeosWeb.Items.Clear();
            DataTable dtMunicipios = (DataTable)DBAccessGeos.ExecutarComando("select Id,Name from Municipalities where stateid =" + cmbUF.SelectedItem.ToString().Split('-')[0] + " order by name ", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            for (int x = 0; x <= dtMunicipios.Rows.Count - 1; x++)
            {
                cmbIdMunicipioGeosWeb.Items.Add(dtMunicipios.Rows[x]["Id"].ToString() + " - " + dtMunicipios.Rows[x]["Name"].ToString());
            }
        }
        #endregion

        #region " Lixo "
        //private void btnSincronizar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {


        //        #region " Atualização UM "

        //        //DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando("select 163 municipalityid, de_barrame barrament, geom.STX xp,geom.STY yp from Abaira_Poles where de_barrame is not null ", CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
        //        //UpdateCoordenadas_CadastroXGeosWebAsync(dtDados);

        //        #endregion


        //        //if (chkExcluirPoste.Checked)
        //        //{
        //        //    DBAccessGeos.ExecutarComando("delete from documententities f join poles p on f.poleid=p.id and municipalityid = " + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);
        //        //    DBAccessGeos.ExecutarComando("delete sharedusepole from sharedusepole sp join poles p on sp.poleid=p.id and p.municipalityid = " + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);
        //        //    DBAccessGeos.ExecutarComando("delete from transforms from transforms t join poles p on t.poleid=p.id and municipalityid =  " + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);
        //        //    DBAccessGeos.ExecutarComando("delete from Luminaries where municipalityid =  " + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);
        //        //    DBAccessGeos.ExecutarComando("delete from poles where municipalityid = " + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);
        //        //}

        //        //progressBar1.Value = 0;
        //        //progressBar1.Maximum = 100;
        //        //progressBar1.Value = progressBar1.Value + 45;
        //        ////ATUALIZAÇÃO DOS POSTES DO GEOSWEB COM AS COORDENADAS DOS POSTES NO CADASTRO
        //        ////string query = "select * from sys.\"exportPolesToGeosweb\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ")";
        //        //string query = "select \"Id\",\"DistributorPoleId\",\"DistributorPoleCode\",\"Type\",\"InstallationDate\", \"Material\",\"Height\",\"Effort\",ST_X(ST_AsText(ST_Transform(\"geom\",3857))) xp,ST_Y(ST_AsText(ST_Transform(\"geom\",3857))) yp from sys.\"getPoles\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ")";
        //        //DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
        //        //progressBar1.Value = progressBar1.Value + 55;

        //        //this.Cursor = Cursors.Default;

        //        this.Cursor = Cursors.WaitCursor;

        //        progressBar1.Value = 0;


        //        this.Cursor = Cursors.Default;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Falha na sincronização");
        //    }
        //}
        #endregion

        #region " Dados Primários"
        private void CarregarDadosPrimarios()
        {

            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            DataTable dtDados = (DataTable)DBAccessGeos.ExecutarComando("select name from DispatchCenters where name = '" + cmbProjetos.SelectedItem.ToString().Split('-')[1] + "'", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            //if (dtDados.Rows.Count != 0)
            //{
            //    throw new Exception("Já existe uma Dispatch Center com este nome");
            //}

            #region " Centro de Despacho "

            //progressBar1.Value = progressBar1.Value + 20;

            //SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString);
            //connection.Open();

            //string query = "insert into DispatchCenters (OrganizationId,Name,Status,Special,NumberHighAvailability,NumberLowAvailability,NumberMediumAvailability) ";
            //query += "output inserted.id values (@OrganizationId,@Name,@Status,@Special,@NumberHighAvailability,@NumberLowAvailability,@NumberMediumAvailability)";

            //SqlCommand comandoGeos = new SqlCommand(query, connection);
            //comandoGeos.Parameters.AddWithValue("@OrganizationId", 1);
            //comandoGeos.Parameters.AddWithValue("@Name", dtDados.Rows[0]["Name"].ToString());
            //comandoGeos.Parameters.AddWithValue("@Status", 1);
            //comandoGeos.Parameters.AddWithValue("@Special", 1);
            //comandoGeos.Parameters.AddWithValue("@NumberHighAvailability", DBNull.Value);
            //comandoGeos.Parameters.AddWithValue("@NumberLowAvailability", DBNull.Value);
            //comandoGeos.Parameters.AddWithValue("@NumberMediumAvailability", DBNull.Value);
            //comandoGeos.CommandType = CommandType.Text;
            //var newIdDispatchCenter = comandoGeos.ExecuteScalar();

            //progressBar1.Value = progressBar1.Value + 25;

            //query = "insert into DispatchCenterPolygons (DispatchCenterId,PolygonId,Active,SuperArea,ElementaryArea,MunicipalityId) values ";
            //query += "(@DispatchCenterId,@PolygonId,@Active,@SuperArea,@ElementaryArea,@MunicipalityId)";
            //comandoGeos = new SqlCommand(query, connection);
            //comandoGeos.Parameters.AddWithValue("@DispatchCenterId", newIdDispatchCenter);
            //comandoGeos.Parameters.AddWithValue("@PolygonId", cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]);
            //comandoGeos.Parameters.AddWithValue("@Active", 1);
            //comandoGeos.Parameters.AddWithValue("@SuperArea", 0);
            //comandoGeos.Parameters.AddWithValue("@ElementaryArea", "S");
            //comandoGeos.Parameters.AddWithValue("@MunicipalityId", DBNull.Value);
            //comandoGeos.CommandType = CommandType.Text;
            //comandoGeos.ExecuteNonQuery();
            //connection.Close();

            //progressBar1.Value = progressBar1.Value + 25;

            #endregion

        }
        #endregion

        #region " Postes "
        private async Task ExportarDados_SQLCadastro_SQLGeos_LOCAL_Async(DataTable dados)
        {

            await InserirPostes_CadastroXGeosWebAsync(dados, Convert.ToInt32(cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]));

        }
        private async Task InserirPostes_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, int IdMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {

                        string query = "insert into poles (extid, barrament, materialid, typeid, xp, yp, poleeffortid, poleheightid, municipalityid, organizationid, assetbit, su_count, su_irregular) ";
                        query += " values (@extid, @barrament, @materialid, @typeid, @xp, @yp, @poleeffortid, @poleheightid, @municipalityid, @organizationid, @assetbit, @su_count, @su_irregular)";

                        int materialId = 0;
                        switch (listaPostesCADASTRO.Rows[x]["Material"])
                        {
                            case "Fibra":
                                materialId = 4;
                                break;
                            case "Ferro":
                                materialId = 2;
                                break;
                            case "Madeira":
                                materialId = 3;
                                break;
                            case "Concreto":
                                materialId = 1;
                                break;
                            default:
                                materialId = 0;
                                break;
                        }

                        int TypeId = 0;
                        switch (listaPostesCADASTRO.Rows[x]["Type"])
                        {
                            case "IP":
                                TypeId = 5;
                                break;
                            case "Circular":
                                TypeId = 1;
                                break;
                            case "Fly":
                                TypeId = 90;
                                break;
                            case "Torre Triangular":
                                TypeId = 8;
                                break;
                            case "Duplo T":
                                TypeId = 2;
                                break;
                            case "Fantasma":
                                TypeId = 91;
                                break;
                            default:
                                TypeId = 0;
                                break;
                        }

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.Parameters.AddWithValue("@extid", listaPostesCADASTRO.Rows[x]["Id"]);
                        comandoGeos.Parameters.AddWithValue("@barrament", listaPostesCADASTRO.Rows[x]["DistributorPoleCode"]);
                        comandoGeos.Parameters.AddWithValue("@materialid", materialId);
                        comandoGeos.Parameters.AddWithValue("@typeid", TypeId);
                        comandoGeos.Parameters.AddWithValue("@xp", listaPostesCADASTRO.Rows[x]["xp"]);
                        comandoGeos.Parameters.AddWithValue("@yp", listaPostesCADASTRO.Rows[x]["yp"]);
                        comandoGeos.Parameters.AddWithValue("@poleeffortid", listaPostesCADASTRO.Rows[x]["effort"]);
                        comandoGeos.Parameters.AddWithValue("@poleheightid", listaPostesCADASTRO.Rows[x]["height"]);
                        comandoGeos.Parameters.AddWithValue("@municipalityid", IdMunicipio);
                        comandoGeos.Parameters.AddWithValue("@organizationid", 1);
                        comandoGeos.Parameters.AddWithValue("@assetbit", 0);
                        comandoGeos.Parameters.AddWithValue("@su_count", 0);
                        comandoGeos.Parameters.AddWithValue("@su_irregular", 0);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;

                        lblProgresso.Text = progressBar1.Value.ToString() + "/" + listaPostesCADASTRO.Rows.Count.ToString();
                        lblPorcento.Text = Convert.ToString((progressBar1.Value * 100) / listaPostesCADASTRO.Rows.Count) + "%";

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }
        private async Task InserirUM_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, int IdMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {

                        string query = "insert into poles (extid, barrament, materialid, typeid, xp, yp, poleeffortid, poleheightid, municipalityid, organizationid, assetbit, su_count, su_irregular) ";
                        query += " values (@extid, @barrament, @materialid, @typeid, @xp, @yp, @poleeffortid, @poleheightid, @municipalityid, @organizationid, @assetbit, @su_count, @su_irregular)";

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.Parameters.AddWithValue("@extid", listaPostesCADASTRO.Rows[x]["extid"]);
                        comandoGeos.Parameters.AddWithValue("@barrament", listaPostesCADASTRO.Rows[x]["barrament"]);
                        comandoGeos.Parameters.AddWithValue("@materialid", listaPostesCADASTRO.Rows[x]["materialid"] == DBNull.Value ? 0 : listaPostesCADASTRO.Rows[x]["materialid"]);
                        comandoGeos.Parameters.AddWithValue("@typeid", listaPostesCADASTRO.Rows[x]["typeid"] == DBNull.Value ? 0 : listaPostesCADASTRO.Rows[x]["typeid"]);
                        comandoGeos.Parameters.AddWithValue("@xp", listaPostesCADASTRO.Rows[x]["xp"]);
                        comandoGeos.Parameters.AddWithValue("@yp", listaPostesCADASTRO.Rows[x]["yp"]);
                        comandoGeos.Parameters.AddWithValue("@poleeffortid", listaPostesCADASTRO.Rows[x]["effort"]);
                        comandoGeos.Parameters.AddWithValue("@poleheightid", listaPostesCADASTRO.Rows[x]["height"]);
                        comandoGeos.Parameters.AddWithValue("@municipalityid", IdMunicipio);
                        comandoGeos.Parameters.AddWithValue("@organizationid", 1);
                        comandoGeos.Parameters.AddWithValue("@assetbit", 0);
                        comandoGeos.Parameters.AddWithValue("@su_count", 0);
                        comandoGeos.Parameters.AddWithValue("@su_irregular", 0);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }
        #endregion

        #region " Levantamento Especial "

        private void btnLevantamentoEspecial_Click(object sender, EventArgs e)
        {

            string query = "select \"Code\"->> 'value' as \"Code\", \"DomainDataTypes\".\"Description\" from cfg.\"DomainDataTypes\" ";
            query += "inner join cfg.\"DataTypes\" ON \"DataTypes\".\"Id\" = \"DomainDataTypes\".\"DataTypeId\" ";
            query += "where \"DataTypes\".\"Name\" = 'SpecialSurveysTypes' and \"DataTypes\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
            DataTable dtDadosLevantamentosEspeciais = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            int IdCetroDespacho = Convert.ToInt32(((DataTable)DBAccessCadastro.ExecutarComando("select \"ExtId\" from sys.\"Projects\" where \"Id\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable)).Rows[0]["ExtId"]);

            query = "select \"ExtId\",\"Barrament\",\"SpecialSurveys\" from  sys.\"exportPolesToGeosweb\" ( " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ") where \"SpecialSurveys\" is not null";
            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
            progressBar1.Maximum = dtDados.Rows.Count;
            ExportarLevantamentoEspecial_Async(dtDados, dtDadosLevantamentosEspeciais, IdCetroDespacho);
        }
        private async Task ExportarLevantamentoEspecial_Async(DataTable dados, DataTable dadosLevantamento, int IdCentroDespach)
        {

            await InserirPostesLevantamentoEspeciais_CadastroXGeosWebAsync(dados, dadosLevantamento, IdCentroDespach);

        }
        private async Task InserirPostesLevantamentoEspeciais_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, DataTable listaLevantamentoCADASTRO, int IdCentroDespacho)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    string LevantamentoCompleto = "";
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {
                        for (int y = 0; y <= listaLevantamentoCADASTRO.Select(" Code in (" + listaPostesCADASTRO.Rows[x]["SpecialSurveys"].ToString().Replace("[", "").Replace("]", "") + ")").Length - 1; y++)
                        {
                            LevantamentoCompleto += ((DataRow)listaLevantamentoCADASTRO.Select(" Code in (" + listaPostesCADASTRO.Rows[x]["SpecialSurveys"].ToString().Replace("[", "").Replace("]", "") + ")").GetValue(y)).ItemArray[0] + "-" + ((DataRow)listaLevantamentoCADASTRO.Select(" Code in (2,3,4)").GetValue(y)).ItemArray[1];
                            LevantamentoCompleto += (y < listaLevantamentoCADASTRO.Select(" Code in (" + listaPostesCADASTRO.Rows[x]["SpecialSurveys"].ToString().Replace("[", "").Replace("]", "") + ")").Length - 1) ? "," : "";
                        }

                        string query = "Insert into DispatchCenterSpecialSurveys select " + IdCentroDespacho.ToString() + ",Id,'" + LevantamentoCompleto + "' from poles where ExtId = " + listaPostesCADASTRO.Rows[x]["ExtId"].ToString();
                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.CommandType = CommandType.Text;
                        LevantamentoCompleto = "";
                        await comandoGeos.ExecuteNonQueryAsync();
                        progressBar1.Value = progressBar1.Value + 1;

                        lblProgresso.Text = progressBar1.Value.ToString() + "/" + listaPostesCADASTRO.Rows.Count.ToString();
                        lblPorcento.Text = Convert.ToString((progressBar1.Value * 100) / listaPostesCADASTRO.Rows.Count) + "%";

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }

        #endregion

        #region " Redes "

        private void btnExportarRedeBT_Click(object sender, EventArgs e)
        {
            string query = "SELECT nt.\"Id\" AS \"NetworkStrecthId\", pf.\"Id\" AS \"PoleIdFrom\", pt.\"Id\" AS \"PoleIdTo\"";
            query += " FROM sys.\"NetworkStrecths\" nt  ";
            query += " INNER JOIN sys.\"LowVoltageWires\" bt ON nt.\"Id\" = bt.\"Id\"";
            query += " INNER JOIN sys.\"Nodes\" ndf ON ndf.\"Id\" = nt.\"NodeIdFrom\"";
            query += " INNER JOIN sys.\"Poles\" pf ON pf.\"Id\" = ndf.\"GeoPointId\"";
            query += " INNER JOIN sys.\"Nodes\" ndt ON ndt.\"Id\" = nt.\"NodeIdTo\"";
            query += " INNER JOIN sys.\"Poles\" pt ON pt.\"Id\" = ndt.\"GeoPointId\"";
            query += " WHERE nt.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            ExportarDados_RedeBT_Async(dtDados, Convert.ToInt32(cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]));

        }
        private async Task ExportarDados_RedeBT_Async(DataTable dados, int IdMunicipio)
        {
            await InserirRedeBT_CadastroXGeosWebAsync(dados, IdMunicipio);
        }
        private async Task InserirRedeBT_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, int IdMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {
                        string query = "insert into LowTensionWire (extid, PoleStartId,PoleEndId,x1,y1,x2,y2) ";
                        query += " select " + listaPostesCADASTRO.Rows[x]["NetworkStrecthId"].ToString() + ",t1.id,t2.id, t1.xp,t1.yp,t2.xp,t2.yp from ";
                        query += " (select id, xp,yp from poles where MunicipalityId = " + IdMunicipio.ToString() + " and extid =" + listaPostesCADASTRO.Rows[x]["PoleIdFrom"].ToString() + ")t1, ";
                        query += " (select id, xp,yp from poles where MunicipalityId = " + IdMunicipio.ToString() + " and extid =" + listaPostesCADASTRO.Rows[x]["PoleIdTo"].ToString() + ")t2 ";

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;

                        lblProgresso.Text = progressBar1.Value.ToString() + "/" + listaPostesCADASTRO.Rows.Count.ToString();
                        lblPorcento.Text = Convert.ToString((progressBar1.Value * 100) / listaPostesCADASTRO.Rows.Count) + "%";

                    }

                    SqlCommand comandoGeosUpdate = new SqlCommand("update LowTensionWire set projection = geometry::STGeomFromText('LINESTRING(' + STR(x1, 20, 8) + ' ' + STR(y1, 20, 8) + ',' + STR(x2, 20, 8) + ' ' + STR(y2, 20, 8) + ')', 0)", connection);
                    comandoGeosUpdate.CommandType = CommandType.Text;
                    await comandoGeosUpdate.ExecuteNonQueryAsync();

                    progressBar1.Value = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }

        private void btnExportarRedeMT_Click(object sender, EventArgs e)
        {
            string query = "SELECT nt.\"Id\" AS \"NetworkStrecthId\", pf.\"Id\" AS \"PoleIdFrom\", pt.\"Id\" AS \"PoleIdTo\"";
            query += " FROM sys.\"NetworkStrecths\" nt  ";
            query += " INNER JOIN sys.\"MediumVoltageWires\" bt ON nt.\"Id\" = bt.\"Id\"";
            query += " INNER JOIN sys.\"Nodes\" ndf ON ndf.\"Id\" = nt.\"NodeIdFrom\"";
            query += " INNER JOIN sys.\"Poles\" pf ON pf.\"Id\" = ndf.\"GeoPointId\"";
            query += " INNER JOIN sys.\"Nodes\" ndt ON ndt.\"Id\" = nt.\"NodeIdTo\"";
            query += " INNER JOIN sys.\"Poles\" pt ON pt.\"Id\" = ndt.\"GeoPointId\"";
            query += " WHERE nt.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0];
            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            ExportarDados_RedeMT_Async(dtDados, Convert.ToInt32(cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]));
        }
        private async Task ExportarDados_RedeMT_Async(DataTable dados, int IdMunicipio)
        {
            await InserirRedeMT_CadastroXGeosWebAsync(dados, IdMunicipio);
        }
        private async Task InserirRedeMT_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, int IdMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {
                        string query = "insert into HightTensionWire (extid, PoleStartId,PoleEndId,x1,y1,x2,y2) ";
                        query += " select " + listaPostesCADASTRO.Rows[x]["NetworkStrecthId"].ToString() + ",t1.id,t2.id, t1.xp,t1.yp,t2.xp,t2.yp from ";
                        query += " (select id, xp,yp from poles where MunicipalityId = " + IdMunicipio.ToString() + " and extid =" + listaPostesCADASTRO.Rows[x]["PoleIdFrom"].ToString() + ")t1, ";
                        query += " (select id, xp,yp from poles where MunicipalityId = " + IdMunicipio.ToString() + " and extid =" + listaPostesCADASTRO.Rows[x]["PoleIdTo"].ToString() + ")t2 ";

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;

                        lblProgresso.Text = progressBar1.Value.ToString() + "/" + listaPostesCADASTRO.Rows.Count.ToString();
                        lblPorcento.Text = Convert.ToString((progressBar1.Value * 100) / listaPostesCADASTRO.Rows.Count) + "%";

                    }

                    SqlCommand comandoGeosUpdate = new SqlCommand("update HightTensionWire set projection = geometry::STGeomFromText('LINESTRING(' + STR(x1, 20, 8) + ' ' + STR(y1, 20, 8) + ',' + STR(x2, 20, 8) + ' ' + STR(y2, 20, 8) + ')', 0)", connection);
                    comandoGeosUpdate.CommandType = CommandType.Text;
                    await comandoGeosUpdate.ExecuteNonQueryAsync();

                    progressBar1.Value = 0;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }

        #endregion

        #region " Atualização UM "

        private async Task UpdateCoordenadas_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {
                        string query = "update poles set flgAlterado = 1,";
                        query += "xp = @xp, ";
                        query += "yp = @yp ";
                        query += "where municipalityid = @municipalityid and barrament = @barrament";

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.Parameters.AddWithValue("@xp", listaPostesCADASTRO.Rows[x]["xp"]);
                        comandoGeos.Parameters.AddWithValue("@yp", listaPostesCADASTRO.Rows[x]["yp"]);
                        comandoGeos.Parameters.AddWithValue("@barrament", listaPostesCADASTRO.Rows[x]["barrament"]);
                        comandoGeos.Parameters.AddWithValue("@municipalityid", listaPostesCADASTRO.Rows[x]["municipalityid"]);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }
        private async Task InserirPostesNovo_UM_CadastroXGeosWebAsync(DataTable listaPostesCADASTRO, int IdMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listaPostesCADASTRO.Rows.Count;

                    for (int x = 0; x <= listaPostesCADASTRO.Rows.Count - 1; x++)
                    {

                        string query = "insert into poles (extid, barrament, materialid, typeid, xp, yp, poleeffortid, poleheightid, municipalityid, organizationid, assetbit, su_count, su_irregular) ";
                        query += " values (@extid, @barrament, @materialid, @typeid, @xp, @yp, @poleeffortid, @poleheightid, @municipalityid, @organizationid, @assetbit, @su_count, @su_irregular)";

                        SqlCommand comandoGeos = new SqlCommand(query, connection);
                        comandoGeos.Parameters.AddWithValue("@extid", listaPostesCADASTRO.Rows[x]["extid"]);
                        comandoGeos.Parameters.AddWithValue("@barrament", listaPostesCADASTRO.Rows[x]["barrament"]);
                        comandoGeos.Parameters.AddWithValue("@materialid", listaPostesCADASTRO.Rows[x]["materialid"] == DBNull.Value ? 0 : listaPostesCADASTRO.Rows[x]["materialid"]);
                        comandoGeos.Parameters.AddWithValue("@typeid", listaPostesCADASTRO.Rows[x]["typeid"] == DBNull.Value ? 0 : listaPostesCADASTRO.Rows[x]["typeid"]);
                        comandoGeos.Parameters.AddWithValue("@xp", listaPostesCADASTRO.Rows[x]["xp"]);
                        comandoGeos.Parameters.AddWithValue("@yp", listaPostesCADASTRO.Rows[x]["yp"]);
                        comandoGeos.Parameters.AddWithValue("@poleeffortid", listaPostesCADASTRO.Rows[x]["effort"]);
                        comandoGeos.Parameters.AddWithValue("@poleheightid", listaPostesCADASTRO.Rows[x]["height"]);
                        comandoGeos.Parameters.AddWithValue("@municipalityid", IdMunicipio);
                        comandoGeos.Parameters.AddWithValue("@organizationid", 1);
                        comandoGeos.Parameters.AddWithValue("@assetbit", 0);
                        comandoGeos.Parameters.AddWithValue("@su_count", 0);
                        comandoGeos.Parameters.AddWithValue("@su_irregular", 0);
                        comandoGeos.CommandType = CommandType.Text;
                        await comandoGeos.ExecuteNonQueryAsync();

                        progressBar1.Value = progressBar1.Value + 1;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }


        #endregion

        private void btnDadosPrimarios_Click(object sender, EventArgs e)
        {

            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando("select \"Id\",\"MunicipalityId\" from sys.\"Projects\" where \"Id\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
            if (dtDados.Rows.Count != 0)
            {
                Int64 IdProjeto = Convert.ToInt64(dtDados.Rows[0]["Id"]);
                Int64 IdMunicipio = Convert.ToInt64(dtDados.Rows[0]["MunicipalityId"]);

                #region " Área Geográfica "

                dtDados = (DataTable)DBAccessCadastro.ExecutarComando("select \"Name\",\"InitialDate\",st_astext(\"geom\") geom from sys.\"Projects\" where \"Id\" = " + IdProjeto.ToString(), CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeos.ConnectionString))
                {
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexaoSQL;
                        comando.CommandText = "insert into GeographicAreas (GeographicAreaTypeId,Name,Polygon,CreationDate) values (1,@name,@poligono,@creationdate)";

                        comando.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                        comando.Parameters["@name"].Value = dtDados.Rows[0]["Name"].ToString();

                        comando.Parameters.Add(new SqlParameter("@poligono", SqlDbType.Udt));
                        comando.Parameters["@poligono"].UdtTypeName = "geometry";
                        SqlGeometry poligono = SqlGeometry.Parse(dtDados.Rows[0]["Geom"].ToString());
                        comando.Parameters["@poligono"].Value = poligono;

                        comando.Parameters.Add(new SqlParameter("@creationdate", SqlDbType.DateTime));
                        comando.Parameters["@creationdate"].Value = dtDados.Rows[0]["InitialDate"].ToString();

                        conexaoSQL.Open();
                        comando.ExecuteNonQuery();
                        conexaoSQL.Close();
                    }
                }

                DBAccessGeos.ExecutarComando("UPDATE GeographicAreas SET polygon.STSrid = 4326 where name ='" + dtDados.Rows[0]["Name"].ToString() + "'", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteScalar);

                #endregion

                #region " Município "

                dtDados = (DataTable)DBAccessGeos.ExecutarComando("select id from GeographicAreas where name ='" + dtDados.Rows[0]["Name"].ToString() + "'", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
                Int64 IdAreaGeografica = Convert.ToInt64(dtDados.Rows[0][0]);

                dtDados = (DataTable)DBAccessCadastro.ExecutarComando("select m.\"Id\", m.\"IbgeCode\",m.\"Name\",st_astext(m.\"geom\") geom from sys.\"Municipalities\" m join sys.\"Projects\" p on p.\"MunicipalityId\" = m.\"Id\" where p.\"Id\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
                using (var conexaoSQL = new SqlConnection(DBSettingGeos.ConnectionString))
                {
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexaoSQL;
                        comando.CommandText = "insert into Municipalities (Name,Geometry,Projection,StateId,ExtId,CodMunicIBGE,GeographicAreaId) values (@Name,@Geometry,@Projection,1,@ExtId,@CodMunicIBGE,@GeographicAreaId)";

                        comando.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                        comando.Parameters["@name"].Value = dtDados.Rows[0]["Name"].ToString();

                        comando.Parameters.Add(new SqlParameter("@Geometry", SqlDbType.Udt));
                        comando.Parameters["@Geometry"].UdtTypeName = "geometry";
                        SqlGeometry poligono = SqlGeometry.Parse(dtDados.Rows[0]["Geom"].ToString());
                        comando.Parameters["@Geometry"].Value = poligono;

                        comando.Parameters.Add(new SqlParameter("@Projection", SqlDbType.Udt));
                        comando.Parameters["@Projection"].UdtTypeName = "geometry";
                        SqlGeometry Projection = SqlGeometry.Parse(dtDados.Rows[0]["Geom"].ToString());
                        comando.Parameters["@Projection"].Value = Projection;

                        comando.Parameters.Add(new SqlParameter("@ExtId", SqlDbType.Int));
                        comando.Parameters["@ExtId"].Value = dtDados.Rows[0]["Id"].ToString();

                        comando.Parameters.Add(new SqlParameter("@CodMunicIBGE", SqlDbType.Int));
                        comando.Parameters["@CodMunicIBGE"].Value = dtDados.Rows[0]["ibgeCode"].ToString();

                        comando.Parameters.Add(new SqlParameter("@GeographicAreaId", SqlDbType.Int));
                        comando.Parameters["@GeographicAreaId"].Value = IdAreaGeografica;

                        conexaoSQL.Open();
                        comando.ExecuteNonQuery();
                        conexaoSQL.Close();
                    }
                }

                DBAccessGeos.ExecutarComando("UPDATE municipalities SET geometry.STSrid=4326 where name ='" + dtDados.Rows[0]["Name"].ToString() + "'", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteScalar);

                #endregion

                MessageBox.Show("Dados Primários cadastrados com sucesso!");
            }

        }

        private void btnSincronizar_Click_1(object sender, EventArgs e)
        {
            string query = "";

            #region " Organização "

            DataTable dtDadosMunicipio = (DataTable)DBAccessGeos.ExecutarComando("select name from Municipalities where id =" + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            query = "Insert Into Organizations ([Name],[OrganizationTypeId],[ExtId],[LongName],[CNPJ],[Rua],[No],[Complementary],[City],[State],[CEP],[Telephone1],[DDD1],[Telephone2],[DDD2],[Email1],[Email2],[Barrio],[Active],[ExpirationDate],[AccountContract],[CreationDate],[MunicipalityId]) ";
            query += "output inserted.OrganizationId values (";
            query += "'Pref." + dtDadosMunicipio.Rows[0][0].ToString() + "',5,0,";
            query += "'Pref." + dtDadosMunicipio.Rows[0][0].ToString() + "',";
            query += "null, NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,";
            query += cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0] + ")";
            var newIdOrganization = DBAccessGeos.ExecutarComando(query, CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteScalar);

            progressBar1.Value = progressBar1.Value + 10;

            query = "INSERT INTO OrganizationMunicipalities(OrganizationId, MunicipalityId, Active) ";
            query += "VALUES(" + newIdOrganization.ToString() + "," + cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0] + ", 1)";
            DBAccessGeos.ExecutarComando(query, CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteNonQuery);

            progressBar1.Value = progressBar1.Value + 20;

            #endregion

            #region " Centro de Despacho "

            progressBar1.Value = progressBar1.Value + 20;

            SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString);
            connection.Open();

            query = "insert into DispatchCenters (OrganizationId,Name,Status,Special,NumberHighAvailability,NumberLowAvailability,NumberMediumAvailability) ";
            query += "output inserted.id values (@OrganizationId,@Name,@Status,@Special,@NumberHighAvailability,@NumberLowAvailability,@NumberMediumAvailability)";

            SqlCommand comandoGeos = new SqlCommand(query, connection);
            comandoGeos.Parameters.AddWithValue("@OrganizationId", 1);
            comandoGeos.Parameters.AddWithValue("@Name", "Cadastro da Rede Elétrica de " + dtDadosMunicipio.Rows[0][0].ToString());
            comandoGeos.Parameters.AddWithValue("@Status", 1);
            comandoGeos.Parameters.AddWithValue("@Special", 1);
            comandoGeos.Parameters.AddWithValue("@NumberHighAvailability", 12);
            comandoGeos.Parameters.AddWithValue("@NumberLowAvailability", 13);
            comandoGeos.Parameters.AddWithValue("@NumberMediumAvailability", 15);
            comandoGeos.CommandType = CommandType.Text;
            var newIdDispatchCenter = comandoGeos.ExecuteScalar();

            progressBar1.Value = progressBar1.Value + 25;

            query = "insert into DispatchCenterPolygons (DispatchCenterId,PolygonId,Active,SuperArea,ElementaryArea,MunicipalityId) values ";
            query += "(@DispatchCenterId,@PolygonId,@Active,@SuperArea,@ElementaryArea,@MunicipalityId)";
            comandoGeos = new SqlCommand(query, connection);
            comandoGeos.Parameters.AddWithValue("@DispatchCenterId", newIdDispatchCenter);
            comandoGeos.Parameters.AddWithValue("@PolygonId", cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]);
            comandoGeos.Parameters.AddWithValue("@Active", 1);
            comandoGeos.Parameters.AddWithValue("@SuperArea", 0);
            comandoGeos.Parameters.AddWithValue("@ElementaryArea", "S");
            comandoGeos.Parameters.AddWithValue("@MunicipalityId", DBNull.Value);
            comandoGeos.CommandType = CommandType.Text;
            comandoGeos.ExecuteNonQuery();
            connection.Close();

            DBAccessCadastro.ExecutarComando("update sys.\"Projects\" set \"ExtId\" = " + newIdDispatchCenter.ToString() + " where \"Id\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteNonQuery);

            progressBar1.Value = progressBar1.Value + 25;

            #endregion

            #region " Postes "

            query = "select \"Id\",\"DistributorPoleId\",\"DistributorPoleCode\",\"Type\",\"InstallationDate\", \"Material\",\"Height\",\"Effort\",ST_X(ST_AsText(ST_Transform(\"geom\",3857))) xp,ST_Y(ST_AsText(ST_Transform(\"geom\",3857))) yp from sys.\"getPoles\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ")";
            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
            progressBar1.Maximum = dtDados.Rows.Count;
            ExportarDados_SQLCadastro_SQLGeos_LOCAL_Async(dtDados);

            #endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {

                string query = "select  ";
                query += "ts.\"Id\" as \"Tid\",  ";
                query += "ts.\"ServiceOrderId\" as \"ServiceOrderIdInvalida\", ";
                query += "ts.\"ExtNumber\" as \"ExtNumberInvalido\",  ";
                query += "so.\"Id\" as \"ServiceOrderIdValida\", ";
                query += "so.\"ExtNumber\" as \"ExtNumberValido\" ";
                query += "from  ";
                query += "sys.\"ServiceOrders\" so, ";
                query += "(select  ";
                query += "\"GeoPoints\".\"Code\", \"ServiceOrders\".\"ExtNumber\", \"Tasks\".\"TargetId\", \"Tasks\".\"Id\", \"Tasks\".\"ServiceOrderId\", \"GeoPoints\".geom ";
                query += "from sys.\"Tasks\"  ";
                query += "inner join sys.\"GeoPoints\" ON \"GeoPoints\".\"Id\" = \"Tasks\".\"TargetId\" ";
                query += "inner join sys.\"ServiceOrders\" ON \"ServiceOrders\".\"Id\" = \"Tasks\".\"ServiceOrderId\" ";
                query += "where \"Tasks\".\"TaskTargetId\" = 1 and \"GeoPoints\".\"ProjectId\"=30) ts ";
                query += "where ";
                query += "st_contains(so.geom, ts.geom) ";
                query += "and so.\"Id\" <> ts.\"ServiceOrderId\" ";
                DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

                for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                {
                    query = "UPDATE sys.\"Tasks\" SET \"ServiceOrderId\" = " + dtDados.Rows[x]["ServiceOrderIdValida"].ToString() + " WHERE \"Id\" = " + dtDados.Rows[x]["Tid"].ToString();
                    DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteNonQuery);
                }

                #region " Diversos "

                //string query = "SELECT DISTINCT BARRAMENTO,XP,YP FROM Fotos_Postes_MunizFerreira_Mercator WHERE BARRAMENTO IS NOT NULL ORDER BY BARRAMENTO,XP";
                //DataTable dtDados = (DataTable)DBAccessGeos.ExecutarComando(query, CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);

                //string barramento = "";
                //double xp = 0;
                //int cont = 0;
                //for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                //{
                //    if (dtDados.Rows[x]["BARRAMENTO"].ToString() == barramento && Convert.ToDouble(dtDados.Rows[x]["XP"]) != xp)
                //    {
                //        cont++;

                //        query = "UPDATE Fotos_Postes_MunizFerreira_Mercator SET BARRAMENTO = 'IP" + cont.ToString() + "_" + dtDados.Rows[x]["BARRAMENTO"].ToString() + "' ";
                //        query += "WHERE BARRAMENTO = @barrament AND XP = @xp";

                //        SqlCommand comandoGeos = new SqlCommand(query, connection);
                //        comandoGeos.Parameters.AddWithValue("@barrament", dtDados.Rows[x]["BARRAMENTO"].ToString());
                //        comandoGeos.Parameters.AddWithValue("@xp", dtDados.Rows[x]["xp"]);
                //        comandoGeos.CommandType = CommandType.Text;
                //        comandoGeos.ExecuteNonQuery();
                //    }
                //    else
                //    {
                //        cont = 0;
                //    }

                //    barramento = dtDados.Rows[x]["BARRAMENTO"].ToString();
                //    xp = Convert.ToDouble(dtDados.Rows[x]["xp"]);
                //}

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erro");
            }


            //using (SqlConnection connection = new SqlConnection(DBSettingCadastro.ConnectionString))
            //{
            //    connection.Open();

            //    SqlCommand command = connection.CreateCommand();
            //    command.Connection = connection;

            //    try
            //    {

            //        //string query = "SELECT DISTINCT BARRAMENTO,XP,YP FROM Fotos_Postes_MunizFerreira_Mercator WHERE BARRAMENTO IS NOT NULL ORDER BY BARRAMENTO,XP";
            //        //DataTable dtDados = (DataTable)DBAccessGeos.ExecutarComando(query, CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);

            //        //string barramento = "";
            //        //double xp = 0;
            //        //int cont = 0;
            //        //for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
            //        //{
            //        //    if (dtDados.Rows[x]["BARRAMENTO"].ToString() == barramento && Convert.ToDouble(dtDados.Rows[x]["XP"]) != xp)
            //        //    {
            //        //        cont++;

            //        //        query = "UPDATE Fotos_Postes_MunizFerreira_Mercator SET BARRAMENTO = 'IP" + cont.ToString() + "_" + dtDados.Rows[x]["BARRAMENTO"].ToString() + "' ";
            //        //        query += "WHERE BARRAMENTO = @barrament AND XP = @xp";

            //        //        SqlCommand comandoGeos = new SqlCommand(query, connection);
            //        //        comandoGeos.Parameters.AddWithValue("@barrament", dtDados.Rows[x]["BARRAMENTO"].ToString());
            //        //        comandoGeos.Parameters.AddWithValue("@xp", dtDados.Rows[x]["xp"]);
            //        //        comandoGeos.CommandType = CommandType.Text;
            //        //        comandoGeos.ExecuteNonQuery();
            //        //    }
            //        //    else
            //        //    {
            //        //        cont = 0;
            //        //    }

            //        //    barramento = dtDados.Rows[x]["BARRAMENTO"].ToString();
            //        //    xp = Convert.ToDouble(dtDados.Rows[x]["xp"]);
            //        //}

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "erro");
            //    }
            //}


        }

        private void btnInstacaoa_Click(object sender, EventArgs e)
        {

            string query = "select \"Id\",\"poleid\",\"Plate\",\"SubType\",ST_X(ST_AsText(ST_Transform(\"geom\",3857))) xp,ST_Y(ST_AsText(ST_Transform(\"geom\",3857))) yp,\"phasetype\" ";
            query += "from sys.\"exportInstallationsToGeosweb\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ")";
            DataTable dtDados = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            ExportarDados_Equipamentos_Async(dtDados, Convert.ToInt32(cmbIdMunicipioGeosWeb.SelectedItem.ToString().Split('-')[0]));
        }
        private async Task ExportarDados_Equipamentos_Async(DataTable dtDados, int IdMunicipio)
        {
            await InserirEquipamentos_CadastroXGeosWebAsync(dtDados, IdMunicipio);
        }
        private async Task InserirEquipamentos_CadastroXGeosWebAsync(DataTable dtDados, int idMunicipio)
        {
            using (SqlConnection connection = new SqlConnection(DBSettingGeos.ConnectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;

                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = dtDados.Rows.Count;

                    for (int x = 0; x <= dtDados.Rows.Count - 1; x++)
                    {

                        int IdFase = 8; //NI
                        DataTable dtFase = (DataTable)DBAccessGeos.ExecutarComando("select id from fase where cod = '" + dtDados.Rows[x]["PhaseType"].ToString() + "'", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
                        if (dtFase.Rows.Count != 0)
                        {
                            IdFase = Convert.ToInt32(dtFase.Rows[0][0]);
                        }

                        int IdTipo = 0;//NI
                        DataTable dtTipo = (DataTable)DBAccessGeos.ExecutarComando("select top 1 id from TransformType where cod = '" + dtDados.Rows[x]["SubType"].ToString() + "' order by id", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
                        if (dtTipo.Rows.Count != 0)
                        {
                            IdTipo = Convert.ToInt32(dtTipo.Rows[0][0]);
                        }

                        DataTable dtPoste = (DataTable)DBAccessGeos.ExecutarComando("select id from poles where municipalityid = " + idMunicipio.ToString() + " and extid =" + dtDados.Rows[x]["PoleId"].ToString(), CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
                        if (dtPoste.Rows.Count != 0)
                        {
                            Int64 IdPoste = Convert.ToInt64(dtPoste.Rows[0][0]);
                            string query = "insert into transforms (poleid,extid,label,typeid,faseid,xp,yp,InstallDate,DisjuntorId) values (@poleid,@extid,@Plate,@typeid,@faseid,@xp,@yp,getdate(),0) ";
                            SqlCommand comandoGeos = new SqlCommand(query, connection);
                            comandoGeos.Parameters.AddWithValue("@poleid", IdPoste);
                            comandoGeos.Parameters.AddWithValue("@extid", dtDados.Rows[x]["Id"]);
                            comandoGeos.Parameters.AddWithValue("@Plate", dtDados.Rows[x]["Plate"]);
                            comandoGeos.Parameters.AddWithValue("@typeid", IdTipo);
                            comandoGeos.Parameters.AddWithValue("@faseid", IdFase);
                            comandoGeos.Parameters.AddWithValue("@xp", dtDados.Rows[x]["xp"]);
                            comandoGeos.Parameters.AddWithValue("@yp", dtDados.Rows[x]["yp"]);
                            comandoGeos.CommandType = CommandType.Text;
                            comandoGeos.ExecuteNonQuery();
                        }

                        progressBar1.Value = progressBar1.Value + 1;

                        lblProgresso.Text = progressBar1.Value.ToString() + "/" + dtDados.Rows.Count.ToString();
                        lblPorcento.Text = Convert.ToString((progressBar1.Value * 100) / dtDados.Rows.Count) + "%";

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "erro");
                }
            }
        }

        private void btnETL3_Click(object sender, EventArgs e)
        {
            //string query = "select id from serviceorders so ";
            //query += "where id in (select serviceorderid from DispatchCenterServiceOrders where dispatchcenterid in  ";
            //query += "and secondarystateid = 4";
            //DataTable dtDados = (DataTable)DBAccessGeos.ExecutarComando(query, CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);

            //RunAsync().Wait();
        }
        static async Task RunAsync(DataTable dtOSEntregues)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://apigeoscad.azurewebsites.net/api/SO/ETL/72/2240");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/produtos/3");
                //if (response.IsSuccessStatusCode)
                //{  //GET
                //    Produto produto = await response.Content.ReadAsAsync<Produto>();
                //    Console.WriteLine("{0}\tR${1}\t{2}", produto.Nome, produto.Preco, produto.Categoria);
                //    Console.WriteLine("Produto acessado e exibido.  Tecle algo para incluir um novo produto.");
                //    Console.ReadKey();
                //}
                ////POST
                //var cha = new Produto() { Nome = "Chá Verde", Preco = 1.50M, Categoria = "Bebidas" };
                //response = await client.PostAsJsonAsync("api/produtos", cha);
                //Console.WriteLine("Produto cha verde incluído. Tecle algo para atualizar o preço do produto.");
                //Console.ReadKey();
                //if (response.IsSuccessStatusCode)
                //{   //PUT
                //    Uri chaUrl = response.Headers.Location;
                //    cha.Preco = 2.55M;   // atualiza o preco do produto
                //    response = await client.PutAsJsonAsync(chaUrl, cha);
                //    Console.WriteLine("Produto preço do atualizado. Tecle algo para excluir o produto");
                //    Console.ReadKey();
                //    //DELETE
                //    response = await client.DeleteAsync(chaUrl);
                //    Console.WriteLine("Produto deletado");
                //    Console.ReadKey();
                //}
            }
        }
    }
}
