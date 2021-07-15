using System;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void eTL2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmETL2 frmETL2 = new frmETL2();
            frmETL2.ShowDialog();
        }

        private void entregaDeFotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEntregaFotos EntregaFotos = new frmEntregaFotos();
            EntregaFotos.ShowDialog();
        }

        private void gerarSQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGerarSQLite GerarSQLite = new frmGerarSQLite();
            GerarSQLite.ShowDialog();
        }

        private void eTL4PostGreXSqlServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmETL4 frmETL4 = new frmETL4();
            frmETL4.ShowDialog();
        }

        private void importarShapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportEquatorial importShape = new frmImportEquatorial();
            importShape.ShowDialog();
        }
    }
}
