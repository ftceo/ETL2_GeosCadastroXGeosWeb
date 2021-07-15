
namespace ETL2_GeosCadastroXGeosWeb
{
    partial class frmGerarSQLite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbUF = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIdMunicipioGeosWeb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProjetos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportPostes = new System.Windows.Forms.Button();
            this.btmImportBT = new System.Windows.Forms.Button();
            this.btnImportMT = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnImportCentroDespacho = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCaminhoBancoSQLite = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbUF);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbIdMunicipioGeosWeb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbProjetos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(807, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projeto Cadastro";
            // 
            // cmbUF
            // 
            this.cmbUF.FormattingEnabled = true;
            this.cmbUF.Location = new System.Drawing.Point(70, 73);
            this.cmbUF.Name = "cmbUF";
            this.cmbUF.Size = new System.Drawing.Size(171, 24);
            this.cmbUF.TabIndex = 38;
            this.cmbUF.SelectedIndexChanged += new System.EventHandler(this.cmbUF_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "UF";
            // 
            // cmbIdMunicipioGeosWeb
            // 
            this.cmbIdMunicipioGeosWeb.FormattingEnabled = true;
            this.cmbIdMunicipioGeosWeb.Location = new System.Drawing.Point(389, 73);
            this.cmbIdMunicipioGeosWeb.Name = "cmbIdMunicipioGeosWeb";
            this.cmbIdMunicipioGeosWeb.Size = new System.Drawing.Size(412, 24);
            this.cmbIdMunicipioGeosWeb.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "Município GeosWeb";
            // 
            // cmbProjetos
            // 
            this.cmbProjetos.FormattingEnabled = true;
            this.cmbProjetos.Location = new System.Drawing.Point(70, 34);
            this.cmbProjetos.Name = "cmbProjetos";
            this.cmbProjetos.Size = new System.Drawing.Size(731, 24);
            this.cmbProjetos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projeto";
            // 
            // btnImportPostes
            // 
            this.btnImportPostes.Location = new System.Drawing.Point(241, 235);
            this.btnImportPostes.Name = "btnImportPostes";
            this.btnImportPostes.Size = new System.Drawing.Size(186, 36);
            this.btnImportPostes.TabIndex = 30;
            this.btnImportPostes.Text = "Importar Postes";
            this.btnImportPostes.UseVisualStyleBackColor = true;
            // 
            // btmImportBT
            // 
            this.btmImportBT.Location = new System.Drawing.Point(471, 236);
            this.btmImportBT.Name = "btmImportBT";
            this.btmImportBT.Size = new System.Drawing.Size(144, 35);
            this.btmImportBT.TabIndex = 31;
            this.btmImportBT.Text = "Importar Rede BT";
            this.btmImportBT.UseVisualStyleBackColor = true;
            // 
            // btnImportMT
            // 
            this.btnImportMT.Location = new System.Drawing.Point(659, 235);
            this.btnImportMT.Name = "btnImportMT";
            this.btnImportMT.Size = new System.Drawing.Size(154, 35);
            this.btnImportMT.TabIndex = 32;
            this.btnImportMT.Text = "Importar Rede MT";
            this.btnImportMT.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 300);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(804, 45);
            this.progressBar1.TabIndex = 33;
            // 
            // btnImportCentroDespacho
            // 
            this.btnImportCentroDespacho.Location = new System.Drawing.Point(10, 235);
            this.btnImportCentroDespacho.Name = "btnImportCentroDespacho";
            this.btnImportCentroDespacho.Size = new System.Drawing.Size(187, 36);
            this.btnImportCentroDespacho.TabIndex = 34;
            this.btnImportCentroDespacho.Text = "Importar Centro Despacho";
            this.btnImportCentroDespacho.UseVisualStyleBackColor = true;
            this.btnImportCentroDespacho.Click += new System.EventHandler(this.btnImportCentroDespacho_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Caminho Banco";
            // 
            // txtCaminhoBancoSQLite
            // 
            this.txtCaminhoBancoSQLite.Location = new System.Drawing.Point(134, 188);
            this.txtCaminhoBancoSQLite.Name = "txtCaminhoBancoSQLite";
            this.txtCaminhoBancoSQLite.Size = new System.Drawing.Size(679, 22);
            this.txtCaminhoBancoSQLite.TabIndex = 36;
            this.txtCaminhoBancoSQLite.Text = "D:\\eNecad\\BancoMobile\\MapaServico\\TesteCarga\\GeosMobile_MapaServico.db";
            // 
            // GerarSQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 357);
            this.Controls.Add(this.txtCaminhoBancoSQLite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnImportCentroDespacho);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnImportMT);
            this.Controls.Add(this.btmImportBT);
            this.Controls.Add(this.btnImportPostes);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GerarSQLite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GerarSQLite";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProjetos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportPostes;
        private System.Windows.Forms.Button btmImportBT;
        private System.Windows.Forms.Button btnImportMT;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnImportCentroDespacho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaminhoBancoSQLite;
        private System.Windows.Forms.ComboBox cmbUF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbIdMunicipioGeosWeb;
        private System.Windows.Forms.Label label4;
    }
}