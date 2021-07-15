
namespace ETL2_GeosCadastroXGeosWeb
{
    partial class frmETL4
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
            this.cmbProjetos = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbMunicipios = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnImportTransformers = new System.Windows.Forms.Button();
            this.btnImportarIP = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnImportPoles = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblProgresso = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPercentual = new System.Windows.Forms.Label();
            this.lblContador = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.bntImportarFotos = new System.Windows.Forms.Button();
            this.txtPrefixoCaminhoFotos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCont = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbProjetos);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projeto Cadastro - Origem";
            // 
            // cmbProjetos
            // 
            this.cmbProjetos.FormattingEnabled = true;
            this.cmbProjetos.Location = new System.Drawing.Point(7, 30);
            this.cmbProjetos.Name = "cmbProjetos";
            this.cmbProjetos.Size = new System.Drawing.Size(490, 24);
            this.cmbProjetos.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbMunicipios);
            this.groupBox2.Location = new System.Drawing.Point(521, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Múnicípio GeosWeb - Destino";
            // 
            // cmbMunicipios
            // 
            this.cmbMunicipios.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMunicipios.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMunicipios.FormattingEnabled = true;
            this.cmbMunicipios.Location = new System.Drawing.Point(7, 30);
            this.cmbMunicipios.Name = "cmbMunicipios";
            this.cmbMunicipios.Size = new System.Drawing.Size(490, 24);
            this.cmbMunicipios.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.btnImportTransformers);
            this.groupBox3.Controls.Add(this.btnImportarIP);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.btnImportPoles);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(16, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1007, 115);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Passo 1";
            // 
            // btnImportTransformers
            // 
            this.btnImportTransformers.Location = new System.Drawing.Point(262, 24);
            this.btnImportTransformers.Name = "btnImportTransformers";
            this.btnImportTransformers.Size = new System.Drawing.Size(220, 42);
            this.btnImportTransformers.TabIndex = 8;
            this.btnImportTransformers.Text = "Importar Transformadores";
            this.btnImportTransformers.UseVisualStyleBackColor = true;
            this.btnImportTransformers.Click += new System.EventHandler(this.btnImportTransformers_Click);
            // 
            // btnImportarIP
            // 
            this.btnImportarIP.Location = new System.Drawing.Point(506, 24);
            this.btnImportarIP.Name = "btnImportarIP";
            this.btnImportarIP.Size = new System.Drawing.Size(220, 42);
            this.btnImportarIP.TabIndex = 7;
            this.btnImportarIP.Text = "Importar IP";
            this.btnImportarIP.UseVisualStyleBackColor = true;
            this.btnImportarIP.Click += new System.EventHandler(this.btnImportarIP_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(18, 73);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(361, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Converter pontos IP em Postes Distribuidora?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnImportPoles
            // 
            this.btnImportPoles.Location = new System.Drawing.Point(18, 24);
            this.btnImportPoles.Name = "btnImportPoles";
            this.btnImportPoles.Size = new System.Drawing.Size(220, 42);
            this.btnImportPoles.TabIndex = 2;
            this.btnImportPoles.Text = "Importar Postes";
            this.btnImportPoles.UseVisualStyleBackColor = true;
            this.btnImportPoles.Click += new System.EventHandler(this.btnImportPoles_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblCont);
            this.groupBox4.Controls.Add(this.lblProgresso);
            this.groupBox4.Controls.Add(this.progressBar1);
            this.groupBox4.Controls.Add(this.lblPercentual);
            this.groupBox4.Controls.Add(this.lblContador);
            this.groupBox4.Location = new System.Drawing.Point(13, 315);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1014, 99);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // lblProgresso
            // 
            this.lblProgresso.AutoSize = true;
            this.lblProgresso.Location = new System.Drawing.Point(643, 22);
            this.lblProgresso.Name = "lblProgresso";
            this.lblProgresso.Size = new System.Drawing.Size(0, 17);
            this.lblProgresso.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(983, 39);
            this.progressBar1.TabIndex = 2;
            // 
            // lblPercentual
            // 
            this.lblPercentual.AutoSize = true;
            this.lblPercentual.Location = new System.Drawing.Point(506, 18);
            this.lblPercentual.Name = "lblPercentual";
            this.lblPercentual.Size = new System.Drawing.Size(0, 17);
            this.lblPercentual.TabIndex = 1;
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Location = new System.Drawing.Point(409, 18);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(0, 17);
            this.lblContador.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.bntImportarFotos);
            this.groupBox5.Controls.Add(this.txtPrefixoCaminhoFotos);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(11, 223);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1012, 86);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Passo 2";
            // 
            // bntImportarFotos
            // 
            this.bntImportarFotos.Location = new System.Drawing.Point(17, 21);
            this.bntImportarFotos.Name = "bntImportarFotos";
            this.bntImportarFotos.Size = new System.Drawing.Size(152, 45);
            this.bntImportarFotos.TabIndex = 8;
            this.bntImportarFotos.Text = "Importar Fotos";
            this.bntImportarFotos.UseVisualStyleBackColor = true;
            this.bntImportarFotos.Click += new System.EventHandler(this.bntImportarFotos_Click);
            // 
            // txtPrefixoCaminhoFotos
            // 
            this.txtPrefixoCaminhoFotos.Location = new System.Drawing.Point(175, 40);
            this.txtPrefixoCaminhoFotos.Name = "txtPrefixoCaminhoFotos";
            this.txtPrefixoCaminhoFotos.Size = new System.Drawing.Size(625, 22);
            this.txtPrefixoCaminhoFotos.TabIndex = 7;
            this.txtPrefixoCaminhoFotos.Text = "https://geosweb.blob.core.windows.net/pictures/PREFEITURA_CARDEAL_DA_SILVA/BA/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Prefixo do caminho das fotos no GeosCadastro:";
            // 
            // lblCont
            // 
            this.lblCont.AutoSize = true;
            this.lblCont.Location = new System.Drawing.Point(483, 17);
            this.lblCont.Name = "lblCont";
            this.lblCont.Size = new System.Drawing.Size(0, 17);
            this.lblCont.TabIndex = 6;
            // 
            // frmETL4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 426);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmETL4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ETL4";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProjetos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbMunicipios;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPercentual;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Button btnImportPoles;
        private System.Windows.Forms.Label lblProgresso;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnImportTransformers;
        private System.Windows.Forms.Button btnImportarIP;
        private System.Windows.Forms.Button bntImportarFotos;
        private System.Windows.Forms.TextBox txtPrefixoCaminhoFotos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCont;
    }
}