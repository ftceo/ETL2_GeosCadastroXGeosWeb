using DAO;
using System.Data;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class GerarSQLite : Form
    {
        public GerarSQLite()
        {
            InitializeComponent();
            if (DBSettingCadastro.TipoBanco == "ePgSQL")
            {
                DataTable dtProjetos = (DataTable)DBAccessGeos.ExecutarComando("select * from DispatchCenters order by id ", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtProjetos.Rows.Count - 1; x++)
                {
                    cmbProjetos.Items.Add(dtProjetos.Rows[x]["Id"].ToString() + " - " + dtProjetos.Rows[x]["Name"].ToString());
                }
            }
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            DataTable dtCentro = (DataTable)DBAccessGeos.ExecutarComando("select * from DispatchCenters  ", CommandType.Text, null, DBAccessGeos.TypeCommand.ExecuteDataTable);
        }
    }
}
