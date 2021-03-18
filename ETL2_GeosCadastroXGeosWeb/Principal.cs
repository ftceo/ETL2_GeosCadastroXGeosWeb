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
            Form1 frmETL2 = new Form1();
            frmETL2.ShowDialog();
        }

        private void entregaDeFotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntregaFotos EntregaFotos = new EntregaFotos();
            EntregaFotos.ShowDialog();
        }

        private void gerarSQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerarSQLite GerarSQLite = new GerarSQLite();
            GerarSQLite.ShowDialog();
        }
    }
}
