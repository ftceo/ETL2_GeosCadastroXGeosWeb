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
    public partial class EntregaFotos : Form
    {
        int contador = 0;

        string _UrlFoto = "";
        string _PastaDestino = "";
        int _IdFoto = 0;
        LogDownloadFoto log = new LogDownloadFoto();
        List<LogDownloadFoto> listaLog = new List<LogDownloadFoto>();

        public EntregaFotos()
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

            string query = "select \"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) as \"Category\", ";
            query += "\"Photos\".\"Path\"::text as \"PhotoLinks\", \"Photos\".\"Id\" ";
            query += "from sys.\"GeoPoints\" ";
            query += "inner join sys.\"Tasks\" ON \"Tasks\".\"GeoPointId\" = \"GeoPoints\".\"Id\" ";
            query += "inner join sys.\"Photos\" ON \"Photos\".\"TaskId\" = \"Tasks\".\"Id\" ";
            query += "inner join sys.\"PhotoCategorizations\" ON \"PhotoCategorizations\".\"PhotoId\" = \"Photos\".\"Id\" ";
            query += "where \"GeoPoints\".\"ProjectId\" = " + cmbProjetos.SelectedItem.ToString().Split('-')[0] + " ";
            query += "and cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) IN (" + (TipoFoto.Substring(TipoFoto.Length - 1, 1) == "," ? TipoFoto.Substring(0, TipoFoto.Length - 1) : TipoFoto) + ") ";
            query += "and \"GeoPoints\".\"Code\" <> 'X999999' and \"GeoPoints\".\"Code\" <> '' ";
            query += "order by \"GeoPoints\".\"Code\", cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) ";
            DataTable dtPhotos = (DataTable)DBAccessCadastro.ExecutarComando(query, CommandType.Text, null, DBAccessCadastro.TypeCommand.ExecuteDataTable);

            progressBar1.Value = 0;
            progressBar1.Maximum = dtPhotos.Rows.Count;

            string barramento = dtPhotos.Rows[0]["Code"].ToString();
            string barramentoAux = "Y";

            for (int x = 0; x <= dtPhotos.Rows.Count - 1; x++)
            {
                if (barramento != barramentoAux)
                {
                    barramentoAux = barramento;
                    if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento)) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento); };
                    if (chkRede.Checked || chkInstalacao.Checked || chkPanoramica.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS"); };
                    }
                    if (chkInstalacao.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\INSTALACAO")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\INSTALACAO"); };
                    }
                    if (chkRede.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\REDE")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\REDE"); };
                    }
                    if (chkPanoramica.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\PANORAMICA")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\PANORAMICA"); };
                    }
                    if (chkIP.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\IP")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\IP"); };
                    }
                    if (chkUsuMutuo.Checked)
                    {
                        if (!Directory.Exists(txtDiretorioProjeto.Text + "\\" + barramento + "\\UM")) { Directory.CreateDirectory(txtDiretorioProjeto.Text + "\\" + barramento + "\\UM"); };
                    }
                }
                switch (dtPhotos.Rows[x]["Category"].ToString())
                {
                    case "PAN":
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\PANORAMICA\\";
                        break;
                    case "IP":
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento + "\\IP\\";
                        break;
                    case "RD":
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\REDE\\";
                        break;
                    case "UM":
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento + "\\UM\\";
                        break;
                    case "INS":
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento + "\\ATIVOS\\INSTALACAO\\";
                        break;
                    default:
                        caminhoPasta = txtDiretorioProjeto.Text + "\\" + barramento;
                        break;
                }

                startDownload(dtPhotos.Rows[x]["PhotoLinks"].ToString(), caminhoPasta, Convert.ToInt32(dtPhotos.Rows[x]["Id"]));
                if (x <= dtPhotos.Rows.Count - 2)
                {
                    barramento = dtPhotos.Rows[x + 1]["Code"].ToString();
                }
                else
                {
                    barramento = dtPhotos.Rows[x]["Code"].ToString();
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
            //query += "cfg.\"getdomaindescbycode\"(" + cmbProjetos.SelectedItem.ToString().Split('-')[0] + ", 'PhotoCategoryTypes', \"PhotoCategorizations\".\"Category\"::text) IN ()";
            query += "and \"GeoPoints\".\"Code\" <> 'X999999' and \"GeoPoints\".\"Code\" <> ''  ";
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
    }
}
