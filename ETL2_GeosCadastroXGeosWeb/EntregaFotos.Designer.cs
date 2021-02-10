
namespace ETL2_GeosCadastroXGeosWeb
{
    partial class EntregaFotos
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
            this.lblTotalRegistroFotos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProjetos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPanoramica = new System.Windows.Forms.CheckBox();
            this.chkInstalacao = new System.Windows.Forms.CheckBox();
            this.chkRede = new System.Windows.Forms.CheckBox();
            this.chkUsuMutuo = new System.Windows.Forms.CheckBox();
            this.chkIP = new System.Windows.Forms.CheckBox();
            this.txtDiretorioProjeto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEntregaFotos = new System.Windows.Forms.Button();
            this.lblAcompanhamentoDownload = new System.Windows.Forms.Label();
            this.lblContagem = new System.Windows.Forms.Label();
            this.grvLog = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalRegistroFotos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbProjetos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(697, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lblTotalRegistroFotos
            // 
            this.lblTotalRegistroFotos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRegistroFotos.Location = new System.Drawing.Point(533, 45);
            this.lblTotalRegistroFotos.Name = "lblTotalRegistroFotos";
            this.lblTotalRegistroFotos.Size = new System.Drawing.Size(144, 29);
            this.lblTotalRegistroFotos.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(520, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total de registros de foto:";
            // 
            // cmbProjetos
            // 
            this.cmbProjetos.FormattingEnabled = true;
            this.cmbProjetos.Location = new System.Drawing.Point(17, 45);
            this.cmbProjetos.Name = "cmbProjetos";
            this.cmbProjetos.Size = new System.Drawing.Size(487, 24);
            this.cmbProjetos.TabIndex = 1;
            this.cmbProjetos.SelectedIndexChanged += new System.EventHandler(this.cmbProjetos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projeto";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(274, 127);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(892, 47);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.UseWaitCursor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPanoramica);
            this.groupBox2.Controls.Add(this.chkInstalacao);
            this.groupBox2.Controls.Add(this.chkRede);
            this.groupBox2.Controls.Add(this.chkUsuMutuo);
            this.groupBox2.Controls.Add(this.chkIP);
            this.groupBox2.Location = new System.Drawing.Point(712, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 77);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Realizar download de fotos das tarefas:";
            // 
            // chkPanoramica
            // 
            this.chkPanoramica.AutoSize = true;
            this.chkPanoramica.Location = new System.Drawing.Point(166, 33);
            this.chkPanoramica.Name = "chkPanoramica";
            this.chkPanoramica.Size = new System.Drawing.Size(105, 21);
            this.chkPanoramica.TabIndex = 39;
            this.chkPanoramica.Text = "Panorâmica";
            this.chkPanoramica.UseVisualStyleBackColor = true;
            // 
            // chkInstalacao
            // 
            this.chkInstalacao.AutoSize = true;
            this.chkInstalacao.Location = new System.Drawing.Point(59, 33);
            this.chkInstalacao.Name = "chkInstalacao";
            this.chkInstalacao.Size = new System.Drawing.Size(101, 21);
            this.chkInstalacao.TabIndex = 38;
            this.chkInstalacao.Text = "Instalações";
            this.chkInstalacao.UseVisualStyleBackColor = true;
            // 
            // chkRede
            // 
            this.chkRede.AutoSize = true;
            this.chkRede.Location = new System.Drawing.Point(277, 33);
            this.chkRede.Name = "chkRede";
            this.chkRede.Size = new System.Drawing.Size(64, 21);
            this.chkRede.TabIndex = 37;
            this.chkRede.Text = "Rede";
            this.chkRede.UseVisualStyleBackColor = true;
            // 
            // chkUsuMutuo
            // 
            this.chkUsuMutuo.AutoSize = true;
            this.chkUsuMutuo.Location = new System.Drawing.Point(347, 33);
            this.chkUsuMutuo.Name = "chkUsuMutuo";
            this.chkUsuMutuo.Size = new System.Drawing.Size(98, 21);
            this.chkUsuMutuo.TabIndex = 36;
            this.chkUsuMutuo.Text = "Uso Mútuo";
            this.chkUsuMutuo.UseVisualStyleBackColor = true;
            // 
            // chkIP
            // 
            this.chkIP.AutoSize = true;
            this.chkIP.Location = new System.Drawing.Point(11, 33);
            this.chkIP.Name = "chkIP";
            this.chkIP.Size = new System.Drawing.Size(42, 21);
            this.chkIP.TabIndex = 35;
            this.chkIP.Text = "IP";
            this.chkIP.UseVisualStyleBackColor = true;
            // 
            // txtDiretorioProjeto
            // 
            this.txtDiretorioProjeto.Location = new System.Drawing.Point(116, 95);
            this.txtDiretorioProjeto.Multiline = true;
            this.txtDiretorioProjeto.Name = "txtDiretorioProjeto";
            this.txtDiretorioProjeto.Size = new System.Drawing.Size(1050, 26);
            this.txtDiretorioProjeto.TabIndex = 39;
            this.txtDiretorioProjeto.Text = "D:\\eNecad\\NeoEnergia\\EntregaFotos\\ipupiara";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "Pasta destino:";
            // 
            // btnEntregaFotos
            // 
            this.btnEntregaFotos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntregaFotos.Location = new System.Drawing.Point(15, 127);
            this.btnEntregaFotos.Name = "btnEntregaFotos";
            this.btnEntregaFotos.Size = new System.Drawing.Size(237, 47);
            this.btnEntregaFotos.TabIndex = 37;
            this.btnEntregaFotos.Text = "Gerar Entrega de Fotos";
            this.btnEntregaFotos.UseVisualStyleBackColor = true;
            this.btnEntregaFotos.Click += new System.EventHandler(this.btnEntregaFotos_ClickAsync);
            // 
            // lblAcompanhamentoDownload
            // 
            this.lblAcompanhamentoDownload.AutoSize = true;
            this.lblAcompanhamentoDownload.Location = new System.Drawing.Point(283, 198);
            this.lblAcompanhamentoDownload.Name = "lblAcompanhamentoDownload";
            this.lblAcompanhamentoDownload.Size = new System.Drawing.Size(0, 17);
            this.lblAcompanhamentoDownload.TabIndex = 40;
            // 
            // lblContagem
            // 
            this.lblContagem.Location = new System.Drawing.Point(9, 321);
            this.lblContagem.Name = "lblContagem";
            this.lblContagem.Size = new System.Drawing.Size(576, 23);
            this.lblContagem.TabIndex = 41;
            this.lblContagem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grvLog
            // 
            this.grvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvLog.Location = new System.Drawing.Point(12, 198);
            this.grvLog.Name = "grvLog";
            this.grvLog.RowHeadersWidth = 51;
            this.grvLog.RowTemplate.Height = 24;
            this.grvLog.Size = new System.Drawing.Size(1154, 433);
            this.grvLog.TabIndex = 42;
            // 
            // EntregaFotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 643);
            this.Controls.Add(this.grvLog);
            this.Controls.Add(this.lblContagem);
            this.Controls.Add(this.lblAcompanhamentoDownload);
            this.Controls.Add(this.txtDiretorioProjeto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEntregaFotos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntregaFotos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrega de Fotos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProjetos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkPanoramica;
        private System.Windows.Forms.CheckBox chkInstalacao;
        private System.Windows.Forms.CheckBox chkRede;
        private System.Windows.Forms.CheckBox chkUsuMutuo;
        private System.Windows.Forms.CheckBox chkIP;
        private System.Windows.Forms.TextBox txtDiretorioProjeto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEntregaFotos;
        private System.Windows.Forms.Label lblAcompanhamentoDownload;
        private System.Windows.Forms.Label lblTotalRegistroFotos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblContagem;
        private System.Windows.Forms.DataGridView grvLog;
    }
}