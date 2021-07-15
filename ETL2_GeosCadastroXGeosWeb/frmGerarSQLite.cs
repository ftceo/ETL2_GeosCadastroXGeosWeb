using DAO;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class frmGerarSQLite : Form
    {
        string query;
        public frmGerarSQLite()
        {
            InitializeComponent();

            DBSettingMobile.ConnectionString = txtCaminhoBancoSQLite.Text + ";Version=3;";

            DataTable dtProjetos = (DataTable)DBAccessGeos.ExecutarComando("select id,name,case when special = 1 then 'Sim' else 'Não' end special  from DispatchCenters where status =1 order by id", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            for (int x = 0; x <= dtProjetos.Rows.Count - 1; x++)
            {
                cmbProjetos.Items.Add(dtProjetos.Rows[x]["Id"].ToString() + " - " + dtProjetos.Rows[x]["Name"].ToString() + " - SPecial:" + dtProjetos.Rows[x]["special"].ToString());
            }

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

        private void btnImportCentroDespacho_Click(object sender, EventArgs e)
        {

            DataTable dtCentroDispacho = (DataTable)DBAccessGeos.ExecutarComando("select id,name from DispatchCenters where id =" + cmbProjetos.SelectedItem.ToString().Split('-')[0], CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
            if (dtCentroDispacho.Rows.Count > 0)
            {
                query = "insert into DispatchCenters (id,Name,CentreId) values (@id,@Name,@CentreId)";

                SQLiteConnection connection = new SQLiteConnection(DBSettingMobile.ConnectionString);
                connection.Open();
                SQLiteCommand comando = new SQLiteCommand(query, connection);
                comando.Parameters.AddWithValue("@id", dtCentroDispacho.Rows[0]["id"]);
                comando.Parameters.AddWithValue("@Name", dtCentroDispacho.Rows[0]["name"]);
                comando.Parameters.AddWithValue("@CentreId", 1);
                comando.CommandType = CommandType.Text;
                comando.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Centro de despacho não encontrado");
            }

        }
    }
}
