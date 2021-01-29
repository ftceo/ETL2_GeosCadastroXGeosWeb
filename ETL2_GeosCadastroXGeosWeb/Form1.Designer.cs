namespace ETL2_GeosCadastroXGeosWeb
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDadosPrimarios = new System.Windows.Forms.Button();
            this.chkExcluirPoste = new System.Windows.Forms.CheckBox();
            this.cmbProjetos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnInstacaoa = new System.Windows.Forms.Button();
            this.btnLevantamentoEspecial = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.btnExportarRedeMT = new System.Windows.Forms.Button();
            this.btnExportarRedeBT = new System.Windows.Forms.Button();
            this.cmbUF = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIdMunicipioGeosWeb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProgresso = new System.Windows.Forms.Label();
            this.lblPorcento = new System.Windows.Forms.Label();
            this.btnETL3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 320);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(991, 45);
            this.progressBar1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDadosPrimarios);
            this.groupBox1.Controls.Add(this.chkExcluirPoste);
            this.groupBox1.Controls.Add(this.cmbProjetos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(990, 95);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projeto Cadastro";
            // 
            // btnDadosPrimarios
            // 
            this.btnDadosPrimarios.Location = new System.Drawing.Point(718, 26);
            this.btnDadosPrimarios.Name = "btnDadosPrimarios";
            this.btnDadosPrimarios.Size = new System.Drawing.Size(232, 52);
            this.btnDadosPrimarios.TabIndex = 29;
            this.btnDadosPrimarios.Text = "Importar Dados Primários";
            this.btnDadosPrimarios.UseVisualStyleBackColor = true;
            this.btnDadosPrimarios.Click += new System.EventHandler(this.btnDadosPrimarios_Click);
            // 
            // chkExcluirPoste
            // 
            this.chkExcluirPoste.AutoSize = true;
            this.chkExcluirPoste.Location = new System.Drawing.Point(574, 43);
            this.chkExcluirPoste.Name = "chkExcluirPoste";
            this.chkExcluirPoste.Size = new System.Drawing.Size(129, 21);
            this.chkExcluirPoste.TabIndex = 28;
            this.chkExcluirPoste.Text = "Excluir postes ?";
            this.chkExcluirPoste.UseVisualStyleBackColor = true;
            // 
            // cmbProjetos
            // 
            this.cmbProjetos.FormattingEnabled = true;
            this.cmbProjetos.Location = new System.Drawing.Point(74, 40);
            this.cmbProjetos.Name = "cmbProjetos";
            this.cmbProjetos.Size = new System.Drawing.Size(470, 24);
            this.cmbProjetos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projeto";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnInstacaoa);
            this.groupBox2.Controls.Add(this.btnLevantamentoEspecial);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnSincronizar);
            this.groupBox2.Controls.Add(this.btnExportarRedeMT);
            this.groupBox2.Controls.Add(this.btnExportarRedeBT);
            this.groupBox2.Controls.Add(this.cmbUF);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbIdMunicipioGeosWeb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(990, 144);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados GeosWeb";
            // 
            // btnInstacaoa
            // 
            this.btnInstacaoa.Location = new System.Drawing.Point(796, 86);
            this.btnInstacaoa.Name = "btnInstacaoa";
            this.btnInstacaoa.Size = new System.Drawing.Size(175, 38);
            this.btnInstacaoa.TabIndex = 40;
            this.btnInstacaoa.Text = "Exportar Instalações";
            this.btnInstacaoa.UseVisualStyleBackColor = true;
            this.btnInstacaoa.Click += new System.EventHandler(this.btnInstacaoa_Click);
            // 
            // btnLevantamentoEspecial
            // 
            this.btnLevantamentoEspecial.Location = new System.Drawing.Point(240, 86);
            this.btnLevantamentoEspecial.Name = "btnLevantamentoEspecial";
            this.btnLevantamentoEspecial.Size = new System.Drawing.Size(165, 38);
            this.btnLevantamentoEspecial.TabIndex = 39;
            this.btnLevantamentoEspecial.Text = "Exportar Lev. Especial";
            this.btnLevantamentoEspecial.UseVisualStyleBackColor = true;
            this.btnLevantamentoEspecial.Click += new System.EventHandler(this.btnLevantamentoEspecial_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(796, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Location = new System.Drawing.Point(74, 86);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(147, 38);
            this.btnSincronizar.TabIndex = 37;
            this.btnSincronizar.Text = "Exportar Postes";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click_1);
            // 
            // btnExportarRedeMT
            // 
            this.btnExportarRedeMT.Location = new System.Drawing.Point(610, 86);
            this.btnExportarRedeMT.Name = "btnExportarRedeMT";
            this.btnExportarRedeMT.Size = new System.Drawing.Size(167, 38);
            this.btnExportarRedeMT.TabIndex = 36;
            this.btnExportarRedeMT.Text = "Exportar Rede MT";
            this.btnExportarRedeMT.UseVisualStyleBackColor = true;
            this.btnExportarRedeMT.Click += new System.EventHandler(this.btnExportarRedeMT_Click);
            // 
            // btnExportarRedeBT
            // 
            this.btnExportarRedeBT.Location = new System.Drawing.Point(424, 86);
            this.btnExportarRedeBT.Name = "btnExportarRedeBT";
            this.btnExportarRedeBT.Size = new System.Drawing.Size(167, 38);
            this.btnExportarRedeBT.TabIndex = 35;
            this.btnExportarRedeBT.Text = "Exportar Rede BT";
            this.btnExportarRedeBT.UseVisualStyleBackColor = true;
            this.btnExportarRedeBT.Click += new System.EventHandler(this.btnExportarRedeBT_Click);
            // 
            // cmbUF
            // 
            this.cmbUF.FormattingEnabled = true;
            this.cmbUF.Location = new System.Drawing.Point(74, 41);
            this.cmbUF.Name = "cmbUF";
            this.cmbUF.Size = new System.Drawing.Size(268, 24);
            this.cmbUF.TabIndex = 34;
            this.cmbUF.SelectedIndexChanged += new System.EventHandler(this.cmbUF_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "UF";
            // 
            // cmbIdMunicipioGeosWeb
            // 
            this.cmbIdMunicipioGeosWeb.FormattingEnabled = true;
            this.cmbIdMunicipioGeosWeb.Location = new System.Drawing.Point(502, 41);
            this.cmbIdMunicipioGeosWeb.Name = "cmbIdMunicipioGeosWeb";
            this.cmbIdMunicipioGeosWeb.Size = new System.Drawing.Size(470, 24);
            this.cmbIdMunicipioGeosWeb.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 17);
            this.label2.TabIndex = 31;
            this.label2.Text = "Município GeosWeb";
            // 
            // lblProgresso
            // 
            this.lblProgresso.AutoSize = true;
            this.lblProgresso.Location = new System.Drawing.Point(449, 380);
            this.lblProgresso.Name = "lblProgresso";
            this.lblProgresso.Size = new System.Drawing.Size(0, 17);
            this.lblProgresso.TabIndex = 4;
            // 
            // lblPorcento
            // 
            this.lblPorcento.AutoSize = true;
            this.lblPorcento.BackColor = System.Drawing.Color.Transparent;
            this.lblPorcento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcento.Location = new System.Drawing.Point(483, 331);
            this.lblPorcento.Name = "lblPorcento";
            this.lblPorcento.Size = new System.Drawing.Size(0, 25);
            this.lblPorcento.TabIndex = 5;
            // 
            // btnETL3
            // 
            this.btnETL3.Location = new System.Drawing.Point(87, 265);
            this.btnETL3.Name = "btnETL3";
            this.btnETL3.Size = new System.Drawing.Size(147, 37);
            this.btnETL3.TabIndex = 6;
            this.btnETL3.Text = "ETL3";
            this.btnETL3.UseVisualStyleBackColor = true;
            this.btnETL3.Click += new System.EventHandler(this.btnETL3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 414);
            this.Controls.Add(this.btnETL3);
            this.Controls.Add(this.lblPorcento);
            this.Controls.Add(this.lblProgresso);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ETL2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProjetos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExcluirPoste;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbUF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbIdMunicipioGeosWeb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportarRedeBT;
        private System.Windows.Forms.Button btnExportarRedeMT;
        private System.Windows.Forms.Button btnDadosPrimarios;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLevantamentoEspecial;
        private System.Windows.Forms.Button btnInstacaoa;
        private System.Windows.Forms.Label lblProgresso;
        private System.Windows.Forms.Label lblPorcento;
        private System.Windows.Forms.Button btnETL3;
    }
}

