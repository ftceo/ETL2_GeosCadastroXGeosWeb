using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class frmEntregaFotos : Form
    {
        int contador = 0;

        string _UrlFoto = "";
        string _PastaDestino = "";
        int _IdFoto = 0;
        LogDownloadFoto log = new LogDownloadFoto();
        List<LogDownloadFoto> listaLog = new List<LogDownloadFoto>();

        public frmEntregaFotos()
        {
            InitializeComponent();

            if (DBSettingCadastro.TipoBanco == "ePgSQL")
            {
                DataTable dtProjetos = (DataTable)DBAccessCadastro.ExecutarComando("select \"Id\",\"Name\" ,\"ClientId\" from sys.\"Projects\" order by \"Id\" ", CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                for (int x = 0; x <= dtProjetos.Rows.Count - 1; x++)
                {
                    cmbProjetos.Items.Add(dtProjetos.Rows[x]["Id"].ToString() + " - " + dtProjetos.Rows[x]["Name"].ToString());
                }

                chkInstalacao.Checked = true;
                chkIP.Checked = true;
                chkPanoramica.Checked = true;
                chkRede.Checked = true;
                chkUsuMutuo.Checked = true;
                chkImportarPostesX9999.Checked = true;
            }
        }
        private void btnEntregaFotos_ClickAsync(object sender, EventArgs e)
        {
            if (cmbProjetos.SelectedItem == null)
            {
                MessageBox.Show("Projeto deve ser preenchido.");
                return;
            }

            if (string.IsNullOrEmpty(txtDiretorioProjeto.Text))
            {
                MessageBox.Show("Diretório destino deve ser preenchido.");
                return;
            }

            string caminhoPasta;
            string TipoFoto = (chkIP.Checked ? "'IP'," : "") +
                              (chkInstalacao.Checked ? "'INS'," : "") +
                              (chkPanoramica.Checked ? "'PAN,'," : "") +
                              (chkRede.Checked ? "'RD'," : "") +
                              (chkUsuMutuo.Checked ? "'UM'" : "");

            listaLog = new List<LogDownloadFoto>();

            //FOTOS DO POSTE 
            string query = "select \"GeoPoints\".\"Code\" as \"CodePoleDerivation\",\"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) as \"Category\", ";
            query += "\"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\", 1 \"TaskTargetId\", null \"TaskId\",\"GeoPoints\".\"Id\" as \"TargetId\" ";
            query += "from sys.\"GeoPoints\" ";
            query += "inner join sys.\"Poles\" ON \"Poles\".\"Id\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) IN (" + (TipoFoto.Substring(TipoFoto.Length - 1, 1) == "," ? TipoFoto.Substring(0, TipoFoto.Length - 1) : TipoFoto) + ") ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) <>'Excluída' ";
            if (chkImportarPostesX9999.Checked)
            {
                query += "and \"GeoPoints\".\"Code\" <> '' ";
                query += "and \"GeoPoints\".\"Type\" = 1 and \"Poles\".\"Id\" in  ";
                query += "(1057565,1057531,1058051,1058218,1058031,1059108,1059159,1058078,1052482,1054278,1054278,1054401,1053767,1058128,1054718,1059125, ";
                query += "1055058,1057158,1052308,1054184,1054078,1055669,1055602,1056834,1051961,1052572,1055667,1053081,1054828,1058360,1053240,1056586, ";
                query += "1054266,1054266,1055722,1054387,1054276,1053415,1052374,1055373,1053935,1054220,1055756,1054818,1052867,1056671,1056210,1056101, ";
                query += "1054726,1054726,1057981,1057986,1054690,1056385,1056009,1056085,1055709,1055998,1055377,1057142,1056076,1053278,1054840,1054383,1054383,1054205,1052854, ";
                query += "1054237,1058166,1055699,1055699,1051912,1056619,1054168,1054045,1051894,1056692,1055515,1053687,1054052,1054738,1053673,1053884,1052098,1056547,1054306, ";
                query += "1057406,1058734,1057014,1056773,1058120,1052567,1052000,1054188,1052719,1055921,1058154,1055341,1058731,1054147,1055587,1054752,1054591,1056858,1053596, ";
                query += "1056199,1055666,1053073,1057257,1051944,1056145,1058105,1056867,1052407,1057447,1055728,1054944,1053200,1053098,1057166,1055312,1052838,1056592,1053867, ";
                query += "1053528,1057036,1056924,1056159,1056277,1055625,1054212,1053808,1051867,1053881,1052745,1054309,1055584,1055214,1055817,1053414,1053511,1056136,1052140, ";
                query += "1056070,1052401,1059152,1051893,1057172,1054257,1053243,1058024,1057933,1053017,1055440,1053652,1052506,1054950,1055197,1055191,1054836,1052220,1054661, ";
                query += "1052284,1054294,1056983,1057344,1056251,1052655,1052551,1058403,1053547,1053548,1053543,1055038,1054253,1056015,1056019,1057086,1057133,1056023,1056667, ";
                query += "1056667,1057164,1056668,1056316,1056764,1058426,1058808,1057141,1058412,1056309,1052360,1057769,1053987,1057786,1056300,1058810,1055153,1057099,1056833) ";
            }
            else
            {
                query += "and \"GeoPoints\".\"Code\" <> 'X999999' and \"GeoPoints\".\"Code\" <> '' ";
            }
            query += "and \"Tasks\".\"TaskTargetId\" <> 7 ";

            query += "union all ";

            ////FOTOS DE IP
            //query += "select g.\"Code\" as \"CodePoleDerivation\" , p.\"Code\" , \"Category\" ,\"PhotoLinks\" ,p.\"Id\" ,p.\"TaskTargetId\" , p.\"TaskId\",g.\"Id\" as \"TargetId\"  from sys.\"GeoPoints\" g ";
            //query += "inner join sys.\"Tasks\" t ON t.\"GeoPointId\" = g.\"Id\" ";
            //query += "inner join (select \"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) as \"Category\", ";
            //query += "\"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\", \"TaskTargetId\", \"Tasks\".\"TaskId\"";
            //query += "from sys.\"GeoPoints\" ";
            //query += "inner join sys.\"Poles\" ON \"Poles\".\"Id\" = \"GeoPoints\".\"Id\" ";
            //query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            //query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            //query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            //query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            //query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) <>'Excluída' ";
            //if (chkImportarPostesX9999.Checked)
            //{
            //    query += "and \"GeoPoints\".\"Code\" <> '' ";
            //    query += "and \"GeoPoints\".\"Type\" = 1 ";
            //    query += "and \"GeoPoints\".\"Type\" = 1 and \"Poles\".\"Id\" in  ";
            //    query += "(1057565,1057531,1058051) ";
            //    //query += "(1057565,1057531,1058051,1058218,1058031,1059108,1059159,1058078,1052482,1054278,1054278,1054401,1053767,1058128,1054718,1059125, ";
            //    //query += "1055058,1057158,1052308,1054184,1054078,1055669,1055602,1056834,1051961,1052572,1055667,1053081,1054828,1058360,1053240,1056586, ";
            //    //query += "1054266,1054266,1055722,1054387,1054276,1053415,1052374,1055373,1053935,1054220,1055756,1054818,1052867,1056671,1056210,1056101, ";
            //    //query += "1054726,1054726,1057981,1057986,1054690,1056385,1056009,1056085,1055709,1055998,1055377,1057142,1056076,1053278,1054840,1054383,1054383,1054205,1052854, ";
            //    //query += "1054237,1058166,1055699,1055699,1051912,1056619,1054168,1054045,1051894,1056692,1055515,1053687,1054052,1054738,1053673,1053884,1052098,1056547,1054306, ";
            //    //query += "1057406,1058734,1057014,1056773,1058120,1052567,1052000,1054188,1052719,1055921,1058154,1055341,1058731,1054147,1055587,1054752,1054591,1056858,1053596, ";
            //    //query += "1056199,1055666,1053073,1057257,1051944,1056145,1058105,1056867,1052407,1057447,1055728,1054944,1053200,1053098,1057166,1055312,1052838,1056592,1053867, ";
            //    //query += "1053528,1057036,1056924,1056159,1056277,1055625,1054212,1053808,1051867,1053881,1052745,1054309,1055584,1055214,1055817,1053414,1053511,1056136,1052140, ";
            //    //query += "1056070,1052401,1059152,1051893,1057172,1054257,1053243,1058024,1057933,1053017,1055440,1053652,1052506,1054950,1055197,1055191,1054836,1052220,1054661, ";
            //    //query += "1052284,1054294,1056983,1057344,1056251,1052655,1052551,1058403,1053547,1053548,1053543,1055038,1054253,1056015,1056019,1057086,1057133,1056023,1056667, ";
            //    //query += "1056667,1057164,1056668,1056316,1056764,1058426,1058808,1057141,1058412,1056309,1052360,1057769,1053987,1057786,1056300,1058810,1055153,1057099,1056833) ";
            //}
            //else
            //{
            //    query += "and \"GeoPoints\".\"Code\" <> 'X999999' and \"GeoPoints\".\"Code\" <> '' ";
            //}
            //query += "and \"Tasks\".\"TaskTargetId\" = 7 ) p on p.\"TaskId\" = t.\"Id\" ";

            //query += "union all ";

            //FOTOS DAS IPS LIGADAS AO POSTE
            query += "select  ";
            query += "	g.\"Code\"  as \"CodePoleDerivation\",l.\"Code\",  cfg.\"getdomaindescbycode\"(52 , 'PhotoCategoryTypes', fc.\"Category\"::text) as \"Category\", ";
            query += "	f.\"Path\"::text as \"PhotoLinks\", f.\"Id\", 3 \"TaskTargetId\", f.\"TaskId\",g.\"Id\" as \"TargetId\"  ";
            query += "from  ";
            query += "	sys.\"Poles\" p  ";
            query += "	join sys.\"GeoPoints\" g on g.\"Id\" = p.\"Id\" and g.\"Type\" = 1 ";
            query += "	join sys.\"LightingPoints\" l on p.\"Id\" = l.\"PoleId\" ";
            query += "	join sys.\"Tasks\" t on t.\"TargetId\" = p.\"Id\" ";
            query += "	join sys.\"Photos\" f on f.\"TaskId\" = t.\"Id\" ";
            query += "	inner join sys.\"PhotoCategorizations\" fc ON fc.\"PhotoId\" = f.\"Id\" ";
            query += "where ";
            query += "	g.\"ProjectId\" = 52  and \"p\".\"Id\" in ";
            query += "(1057565,1057531,1058051,1058218,1058031,1059108,1059159,1058078,1052482,1054278,1054278,1054401,1053767,1058128,1054718,1059125, ";
            query += "1055058,1057158,1052308,1054184,1054078,1055669,1055602,1056834,1051961,1052572,1055667,1053081,1054828,1058360,1053240,1056586, ";
            query += "1054266,1054266,1055722,1054387,1054276,1053415,1052374,1055373,1053935,1054220,1055756,1054818,1052867,1056671,1056210,1056101, ";
            query += "1054726,1054726,1057981,1057986,1054690,1056385,1056009,1056085,1055709,1055998,1055377,1057142,1056076,1053278,1054840,1054383,1054383,1054205,1052854, ";
            query += "1054237,1058166,1055699,1055699,1051912,1056619,1054168,1054045,1051894,1056692,1055515,1053687,1054052,1054738,1053673,1053884,1052098,1056547,1054306, ";
            query += "1057406,1058734,1057014,1056773,1058120,1052567,1052000,1054188,1052719,1055921,1058154,1055341,1058731,1054147,1055587,1054752,1054591,1056858,1053596, ";
            query += "1056199,1055666,1053073,1057257,1051944,1056145,1058105,1056867,1052407,1057447,1055728,1054944,1053200,1053098,1057166,1055312,1052838,1056592,1053867, ";
            query += "1053528,1057036,1056924,1056159,1056277,1055625,1054212,1053808,1051867,1053881,1052745,1054309,1055584,1055214,1055817,1053414,1053511,1056136,1052140, ";
            query += "1056070,1052401,1059152,1051893,1057172,1054257,1053243,1058024,1057933,1053017,1055440,1053652,1052506,1054950,1055197,1055191,1054836,1052220,1054661, ";
            query += "1052284,1054294,1056983,1057344,1056251,1052655,1052551,1058403,1053547,1053548,1053543,1055038,1054253,1056015,1056019,1057086,1057133,1056023,1056667, ";
            query += "1056667,1057164,1056668,1056316,1056764,1058426,1058808,1057141,1058412,1056309,1052360,1057769,1053987,1057786,1056300,1058810,1055153,1057099,1056833) ";
            query += "order by \"CodePoleDerivation\", \"TaskTargetId\" ";

            DataTable dtPhotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            progressBar1.Value = 0;
            progressBar1.Maximum = dtPhotos.Rows.Count;

            string barramentoDerivacao = dtPhotos.Rows[0]["CodePoleDerivation"].ToString();
            string barramentoDerivacaoAux = "Y";
            string barramentoIP = dtPhotos.Rows[0]["Code"].ToString();
            string barramentoIPAux = "X";

            for (int x = 0; x <= dtPhotos.Rows.Count - 1; x++)
            {
                if (barramentoDerivacao != barramentoDerivacaoAux)
                {
                    barramentoDerivacaoAux = barramentoDerivacao;
                    if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao))
                    {
                        Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao);
                    }
                    if (chkRede.Checked || chkInstalacao.Checked || chkPanoramica.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS"); };
                    }
                    if (chkInstalacao.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\INSTALACAO")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\INSTALACAO"); };
                    }
                    if (chkRede.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\REDE")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\REDE"); };
                    }
                    if (chkPanoramica.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\PANORAMICA")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\PANORAMICA"); };
                    }
                    if (chkIP.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP"); };
                    }
                    if (chkUsuMutuo.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\UM")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\UM"); };
                    }
                }

                if (barramentoIP != barramentoIPAux)
                {
                    barramentoIPAux = barramentoIP;
                    if (dtPhotos.Rows[x]["TaskTargetId"].ToString() == "3")
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP))
                        {
                            Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP);
                        }
                    }
                }

                if (dtPhotos.Rows[x]["TaskTargetId"].ToString() != "3")
                {
                    switch (dtPhotos.Rows[x]["Category"].ToString())
                    {
                        case "PAN":
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\PANORAMICA\\";
                            break;
                        case "IP":
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP\\";
                            break;
                        case "RD":
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\REDE\\";
                            break;
                        case "UM":
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\UM\\";
                            break;
                        case "INS":
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\ATIVOS\\INSTALACAO\\";
                            break;
                        default:
                            caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao;
                            break;
                    }
                }
                else
                {
                    caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP + "\\";
                }

                startDownload(dtPhotos.Rows[x]["PhotoLinks"].ToString(), caminhoPasta, Convert.ToInt32(dtPhotos.Rows[x]["Id"]));

                if (x <= dtPhotos.Rows.Count - 2)
                {
                    if (dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString() == "X999999")
                    {
                        barramentoDerivacao = dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString() + "_" + dtPhotos.Rows[x + 1]["TargetId"].ToString();
                        barramentoIP = dtPhotos.Rows[x + 1]["Code"].ToString() + "_" + dtPhotos.Rows[x + 1]["TargetId"].ToString();
                    }
                    else
                    {
                        barramentoDerivacao = dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString();
                        barramentoIP = dtPhotos.Rows[x + 1]["Code"].ToString();
                    }
                }
                else
                {
                    if (dtPhotos.Rows[x]["CodePoleDerivation"].ToString() == "X999999")
                    {
                        barramentoDerivacao = dtPhotos.Rows[x]["CodePoleDerivation"].ToString() + "_" + dtPhotos.Rows[x]["TargetId"].ToString();
                        barramentoIP = dtPhotos.Rows[x]["Code"].ToString() + "_" + dtPhotos.Rows[x]["TargetId"].ToString();
                    }
                    else
                    {
                        barramentoDerivacao = dtPhotos.Rows[x]["CodePoleDerivation"].ToString();
                        barramentoIP = dtPhotos.Rows[x]["Code"].ToString();
                    }
                }

                progressBar1.Value = progressBar1.Value + 1;
                contador = x;
            }

            MessageBox.Show("Download concluído com sucesso!");

            grvLog.DataSource = listaLog;

        }
        public async void DownloadAsync(string url, string pastaDestino)
        {
            await Task.Run(() => Download(url, pastaDestino, 0));
        }
        public void Download(string url, string pastaDestino, int IdPhoto)
        {
            using (WebClient cliente = new WebClient())
            {
                string nomeArquivo = Path.GetFileName(url);
                var pathfinal = pastaDestino + nomeArquivo;
                var uri = new Uri(url);

                cliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                cliente.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                cliente.DownloadFile(uri, pathfinal);
            }
        }
        private void startDownload(string url, string pastaDestino, int IdPhoto)
        {
            try
            {
                string nomeArquivo = Path.GetFileName(url);
                if (!File.Exists(pastaDestino + nomeArquivo))
                {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                    _UrlFoto = url;
                    _PastaDestino = pastaDestino;
                    _IdFoto = IdPhoto;

                    client.DownloadFile(new Uri(url), @pastaDestino + nomeArquivo);
                    //DBAccessCadastro.ExecutarComando("update sys.\"Photos\" set "IsDelivered" = true where \"Id\" = " + IdPhoto.ToString(), CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
                }
            }
            catch (Exception ex)
            {
                log = new LogDownloadFoto();
                log.FotoId = _IdFoto;
                log.UrlAzure = _UrlFoto;
                log.Mensagem = ex.Message;
                log.ErroCode = ex.HResult.ToString();
                log.Date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
                listaLog.Add(log);
            }
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                //    //double bytesIn = double.Parse(e.BytesReceived.ToString());
                //    //double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                //    //double percentage = bytesIn / totalBytes * 100;
                //    //lblAcompanhamentoDownload.Text = "Baixado: " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                //    //progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                //    //progressBar1.Value = progressBar1.Value + 1;
                lblContagem.Text = (contador + 1).ToString();
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //BeginInvoke((MethodInvoker)delegate
            //{
            //    lblContagem.Text = (contador + 1).ToString();
            //});
        }
        private void cmbProjetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select count(*) from sys.\"GeoPoints\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) <>'Excluída' ";
            query += "and \"GeoPoints\".\"Code\" <> 'X999999' ";
            DataTable dtFotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);
            lblTotalRegistroFotos.Text = Convert.ToInt32(dtFotos.Rows[0][0]).ToString("N0");
        }
        private class LogDownloadFoto
        {
            public int FotoId { get; set; }
            public string UrlAzure { get; set; }
            public string Mensagem { get; set; }
            public string ErroCode { get; set; }
            public string Date { get; set; }
        }
        private void btnIndiceFoto_Click(object sender, EventArgs e)
        {
            string caminhoPasta = txtDiretorioProjeto.Text;
            string categoria = "";
            string linha, barramento, barramentoAux = "";
            string query = "select \"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) as \"Category\", ";
            query += "\"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\" ";
            query += "from sys.\"GeoPoints\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) <>'Excluída' ";
            if (chkImportarPostesX9999.Checked)
            {
                query += "and \"GeoPoints\".\"Code\" <> '' ";
            }
            else
            {
                query += "and \"GeoPoints\".\"Code\" <> 'X999999' and \"GeoPoints\".\"Code\" <> '' ";
            }
            query += "and \"Tasks\".\"TaskTargetId\" = 7 "; //fotos de IPs
            query += "order by \"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) ";
            DataTable dtPhotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            using (StreamWriter file = new StreamWriter(@txtDiretorioProjeto.Text + "\\Index.html"))
            {
                barramento = dtPhotos.Rows[0]["Code"].ToString();
                barramentoAux = dtPhotos.Rows[0]["Code"].ToString();
                linha = "<div class=\"container\">";
                file.WriteLine(linha);
                linha = "<div class=\"row\">";
                file.WriteLine(linha);
                linha = "<div class=\"col -md-4 col-md-offset-4\">";
                file.WriteLine(linha);
                linha = "<ul id = \"treeview\">";
                file.WriteLine(linha);
                linha = "<li data-expanded=\"false\">" + dtPhotos.Rows[0]["Code"].ToString();
                file.WriteLine(linha);
                linha = "<ul>";
                file.WriteLine(linha);
                for (int x = 0; x <= dtPhotos.Rows.Count - 1; x++)
                {
                    string nomeArquivo = Path.GetFileName(dtPhotos.Rows[x]["PhotoLinks"].ToString());
                    if (barramento != barramentoAux)
                    {
                        barramentoAux = barramento;
                        linha = "</ul>";
                        file.WriteLine(linha);
                        linha = "</li>";
                        file.WriteLine(linha);
                        linha = "<li data-expanded=\"false\">" + dtPhotos.Rows[x]["Code"].ToString();
                        file.WriteLine(linha);
                        linha = "<ul>";
                        file.WriteLine(linha);
                    }
                    switch (dtPhotos.Rows[x]["Category"].ToString())
                    {
                        case "PAN":
                            categoria = "Panorâmica";
                            caminhoPasta = barramento + "/ATIVOS/PANORAMICA/";
                            break;
                        case "IP":
                            categoria = "IP";
                            caminhoPasta = barramento + "/IP/";
                            break;
                        case "RD":
                            categoria = "Rede";
                            caminhoPasta = barramento + "/ATIVOS/REDE/";
                            break;
                        case "UM":
                            categoria = "Uso Mútuo";
                            caminhoPasta = barramento + "/UM/";
                            break;
                        case "INS":
                            categoria = "Instalação";
                            caminhoPasta = barramento + "/ATIVOS/INSTALACAO/";
                            break;
                        default:
                            caminhoPasta = barramento;
                            break;
                    }
                    linha = "<li><a href=" + "\"" + caminhoPasta + nomeArquivo + "\">" + categoria + "</a></li>";
                    file.WriteLine(linha);
                    if (x <= dtPhotos.Rows.Count - 2)
                    {
                        barramento = dtPhotos.Rows[x + 1]["Code"].ToString();
                    }
                    else
                    {
                        barramento = dtPhotos.Rows[x]["Code"].ToString();
                    }

                }
                linha = "</ul>";
                file.WriteLine(linha);
                linha = "</li>";
                file.WriteLine(linha);
                linha = "</ul>";
                file.WriteLine(linha);
                linha = "</div>";
                file.WriteLine(linha);
                linha = "</div>";
                file.WriteLine(linha);
                linha = "</div>";
                file.WriteLine(linha);
                file.Close();
            }
            MessageBox.Show("Arquivo criado com sucesso.");
        }
        private void btnImportFotosIP_Click(object sender, EventArgs e)
        {
            if (cmbProjetos.SelectedItem == null)
            {
                MessageBox.Show("Projeto deve ser preenchido.");
                return;
            }

            if (string.IsNullOrEmpty(txtDiretorioProjeto.Text))
            {
                MessageBox.Show("Diretório destino deve ser preenchido.");
                return;
            }

            string caminhoPasta;
            string TipoFoto = (chkIP.Checked ? "'IP'," : "") +
                              (chkInstalacao.Checked ? "'INS'," : "") +
                              (chkPanoramica.Checked ? "'PAN,'," : "") +
                              (chkRede.Checked ? "'RD'," : "") +
                              (chkUsuMutuo.Checked ? "'UM'" : "");

            listaLog = new List<LogDownloadFoto>();
            string query;

            //FOTOS DO POSTE 
            query = "select \"GeoPoints\".\"Code\" as \"CodePoleDerivation\",\"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) as \"Category\", ";
            query += "\"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\", 1 \"TaskTargetId\", null \"TaskId\",\"GeoPoints\".\"Id\" as \"TargetId\" ";
            query += "from sys.\"GeoPoints\" ";
            query += "inner join sys.\"Poles\" ON \"Poles\".\"Id\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) ='IP' ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) <>'Excluída' ";
            query += "and \"GeoPoints\".\"Code\" <> '' ";
            query += "and \"GeoPoints\".\"Type\" = 1 and \"Poles\".\"Id\" in  ";
            query += "(1066742,1064300,1063709,1063557,1061582,1068377,1059373,1062870,1062693,1059318,1059318,1070956,1063498,1063416,1070831,1068879,1068879,1069521,1073491,1066057, ";
            query += "1063430,1061810,1063048,1065256,1067033,1059480,1067803,1060741,1066232,1073379,1065994,1061629,1062667,1067748,1065086,1059570,1069807,1060620,1061790,1068860, ";
            query += "1068586,1068103,1062199,1064528,1061504,1070097,1069049,1062931,1061862,1068472,1068721,1066336,1064302,1059856,1067715,1064008,1069502,1059511,1060302,1062750, ";
            query += "1059334,1068832,1061791,1072886,1071862,1073511,1074987,1073838,1059564,1075131,1066540,1075622,1073841,1075695,1075722,1076203,1115658,1067422,1065002,1060464, ";
            query += "1069589,1062928,1068830,1059886,1066426,1069056,1071620,1071511,1059971,1064583,1060448,1060846,1060846,1060846,1060846,1060846,1060695,1059428,1066427,1064298, ";
            query += "1064012,1062955,1069559,1064000,1071638,1066113,1066747,1067914,1060105,1067570,1067244,1062190,1069821,1072447,1072393,1065157,1065933,1065904,1066661,1062637, ";
            query += "1066731,1060220,1075756,1063410,1063096,1063466,1063332,1059714,1061273,1059695,1070320,1069354,1063707,1064557,1059988,1067907,1063914,1061526,1059422,1059422, ";
            query += "1069786,1060843,1061125,1060260,1062852,1060147,1062856,1060150,1061241,1060624,1061970,1068252,1061903,1059469,1070819,1070654,1070671,1070615,1070579,1070579, ";
            query += "1063773,1065610,1065212,1065214,1065446,1061507,1061507,1061539,1063986,1061515,1062309,1071265,1063408,1074414,1069578,1064547,1074436,1074453,1065843,1067801, ";
            query += "1067799,1064719,1069459,1069461,1067950,1062252,1068395,1068390,1060210,1062250,1072799,1072799,1072646,1071597,1071395,1071473,1072638,1071481,1070197,1067177) ";
            //query += "and \"Tasks\".\"TaskTargetId\" = 7 ";

            query += "union all ";

            //FOTOS DAS IPS LIGADAS AO POSTE
            query += "select  ";
            query += "	g.\"Code\"  as \"CodePoleDerivation\",l.\"Code\",  cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " , 'PhotoCategoryTypes', fc.\"Category\"::text) as \"Category\", ";
            query += "	f.\"Path\"::text as \"PhotoLinks\", f.\"Id\", 3 \"TaskTargetId\", f.\"TaskId\",g.\"Id\" as \"TargetId\"  ";
            query += "from  ";
            query += "	sys.\"Poles\" p  ";
            query += "	join sys.\"GeoPoints\" g on g.\"Id\" = p.\"Id\" and g.\"Type\" = 1 ";
            query += "	join sys.\"LightingPoints\" l on p.\"Id\" = l.\"PoleId\" ";
            query += "	join sys.\"Tasks\" t on t.\"TargetId\" = l.\"Id\" ";
            query += "	join sys.\"Photos\" f on f.\"TaskId\" = t.\"Id\" ";
            query += "	inner join sys.\"PhotoCategorizations\" fc ON fc.\"PhotoId\" = f.\"Id\" ";
            query += "where ";
            query += "	g.\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + "  and \"p\".\"Id\" in ";
            query += "(1066742,1064300,1063709,1063557,1061582,1068377,1059373,1062870,1062693,1059318,1059318,1070956,1063498,1063416,1070831,1068879,1068879,1069521,1073491,1066057, ";
            query += "1063430,1061810,1063048,1065256,1067033,1059480,1067803,1060741,1066232,1073379,1065994,1061629,1062667,1067748,1065086,1059570,1069807,1060620,1061790,1068860, ";
            query += "1068586,1068103,1062199,1064528,1061504,1070097,1069049,1062931,1061862,1068472,1068721,1066336,1064302,1059856,1067715,1064008,1069502,1059511,1060302,1062750, ";
            query += "1059334,1068832,1061791,1072886,1071862,1073511,1074987,1073838,1059564,1075131,1066540,1075622,1073841,1075695,1075722,1076203,1115658,1067422,1065002,1060464, ";
            query += "1069589,1062928,1068830,1059886,1066426,1069056,1071620,1071511,1059971,1064583,1060448,1060846,1060846,1060846,1060846,1060846,1060695,1059428,1066427,1064298, ";
            query += "1064012,1062955,1069559,1064000,1071638,1066113,1066747,1067914,1060105,1067570,1067244,1062190,1069821,1072447,1072393,1065157,1065933,1065904,1066661,1062637, ";
            query += "1066731,1060220,1075756,1063410,1063096,1063466,1063332,1059714,1061273,1059695,1070320,1069354,1063707,1064557,1059988,1067907,1063914,1061526,1059422,1059422, ";
            query += "1069786,1060843,1061125,1060260,1062852,1060147,1062856,1060150,1061241,1060624,1061970,1068252,1061903,1059469,1070819,1070654,1070671,1070615,1070579,1070579, ";
            query += "1063773,1065610,1065212,1065214,1065446,1061507,1061507,1061539,1063986,1061515,1062309,1071265,1063408,1074414,1069578,1064547,1074436,1074453,1065843,1067801, ";
            query += "1067799,1064719,1069459,1069461,1067950,1062252,1068395,1068390,1060210,1062250,1072799,1072799,1072646,1071597,1071395,1071473,1072638,1071481,1070197,1067177) ";
            query += "order by \"CodePoleDerivation\", \"TaskTargetId\" ";
            DataTable dtPhotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            progressBar1.Value = 0;
            progressBar1.Maximum = dtPhotos.Rows.Count;

            string barramentoDerivacao = dtPhotos.Rows[0]["CodePoleDerivation"].ToString();
            string barramentoDerivacaoAux = "Y";
            string barramentoIP = dtPhotos.Rows[0]["Code"].ToString();
            string barramentoIPAux = "X";

            for (int x = 0; x <= dtPhotos.Rows.Count - 1; x++)
            {
                if (barramentoDerivacao != barramentoDerivacaoAux)
                {
                    barramentoDerivacaoAux = barramentoDerivacao;
                    if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao))
                    {
                        Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao);
                    }
                    if (chkIP.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP"); };
                    }
                }

                if (barramentoIP != barramentoIPAux)
                {
                    barramentoIPAux = barramentoIP;
                    if (dtPhotos.Rows[x]["TaskTargetId"].ToString() == "3" && !barramentoIP.Contains(barramentoDerivacao))
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP))
                        {
                            Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP);
                        }
                    }
                }

                if (dtPhotos.Rows[x]["TaskTargetId"].ToString() != "3")
                {
                    caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\IP\\";
                }
                else
                {
                    caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\" + barramentoIP + "\\";
                }

                startDownload(dtPhotos.Rows[x]["PhotoLinks"].ToString(), caminhoPasta, Convert.ToInt32(dtPhotos.Rows[x]["Id"]));

                if (x <= dtPhotos.Rows.Count - 2)
                {
                    if (dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString() == "X999999")
                    {
                        barramentoDerivacao = dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString() + "_" + dtPhotos.Rows[x + 1]["TargetId"].ToString();
                        barramentoIP = dtPhotos.Rows[x + 1]["Code"].ToString() + "_" + dtPhotos.Rows[x + 1]["TargetId"].ToString();
                    }
                    else
                    {
                        barramentoDerivacao = dtPhotos.Rows[x + 1]["CodePoleDerivation"].ToString();
                        barramentoIP = dtPhotos.Rows[x + 1]["Code"].ToString();
                    }
                }
                else
                {
                    if (dtPhotos.Rows[x]["CodePoleDerivation"].ToString() == "X999999")
                    {
                        barramentoDerivacao = dtPhotos.Rows[x]["CodePoleDerivation"].ToString() + "_" + dtPhotos.Rows[x]["TargetId"].ToString();
                        barramentoIP = dtPhotos.Rows[x]["Code"].ToString() + "_" + dtPhotos.Rows[x]["TargetId"].ToString();
                    }
                    else
                    {
                        barramentoDerivacao = dtPhotos.Rows[x]["CodePoleDerivation"].ToString();
                        barramentoIP = dtPhotos.Rows[x]["Code"].ToString();
                    }
                }

                progressBar1.Value = progressBar1.Value + 1;
                contador = x;
            }

            MessageBox.Show("Download concluído com sucesso!");

            grvLog.DataSource = listaLog;
        }
        private void btnIP24Hs_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiretorioProjeto.Text))
            {
                MessageBox.Show("Diretório destino deve ser preenchido.");
                return;
            }

            string caminhoPasta;

            listaLog = new List<LogDownloadFoto>();

            string query = "select \"LightingPoints\".\"Code\" || '_' || \"GeoPoints\".\"ProjectId\"::text || '_' || \"GeoPoints\".\"Id\"::text as \"CodeIP\", \"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\", \"GeoPoints\".\"Id\" as \"TargetId\" ";
            query += "from sys.\"LightingPoints\" ";
            query += "inner join sys.\"GeoPoints\" ON \"GeoPoints\".\"Id\" = \"LightingPoints\".\"PoleId\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "where \"LightingPoints\".\"DynamicAttributes\" ->> 'OnAllTheTime' = 'S' ";
            query += "order by \"GeoPoints\".\"ProjectId\" ";
            DataTable dtPhotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            progressBar1.Value = 0;
            progressBar1.Maximum = dtPhotos.Rows.Count;

            string barramentoDerivacao = dtPhotos.Rows[0]["CodeIP"].ToString();
            string barramentoDerivacaoAux = "Y";

            for (int x = 0; x <= dtPhotos.Rows.Count - 1; x++)
            {
                if (barramentoDerivacao != barramentoDerivacaoAux)
                {
                    barramentoDerivacaoAux = barramentoDerivacao;
                    if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao))
                    {
                        Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramentoDerivacao);
                    }
                }

                caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramentoDerivacao + "\\";

                startDownload(dtPhotos.Rows[x]["PhotoLinks"].ToString(), caminhoPasta, Convert.ToInt32(dtPhotos.Rows[x]["Id"]));

                if (x <= dtPhotos.Rows.Count - 2)
                {
                    barramentoDerivacao = dtPhotos.Rows[x + 1]["CodeIP"].ToString();
                }
                else
                {
                    barramentoDerivacao = dtPhotos.Rows[x]["CodeIP"].ToString();
                }

                progressBar1.Value = progressBar1.Value + 1;
                contador = x;
            }

            MessageBox.Show("Download concluído com sucesso!");

            grvLog.DataSource = listaLog;
        }
    }
}
