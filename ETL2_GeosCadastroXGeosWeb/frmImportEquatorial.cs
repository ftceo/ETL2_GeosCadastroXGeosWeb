using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL2_GeosCadastroXGeosWeb
{
    public partial class frmImportEquatorial : Form
    {
        public frmImportEquatorial()
        {
            InitializeComponent();
        }

        public static void CreateWorkForThreads()
        {
            DataSet ds = new DataSet();// CreateNewDataSet();
            DataTable dtNWS = ds.Tables[0];

            //// Parse the shapefile into a DataTable, grabbing the columns we are interested in
            //using (Shapefile shapefile = new Shapefile(Path.Combine(weatherFileDir, "nws_precip_1day_observed_" + dateToLoad.ToString("yyyyMMdd") + ".shp")))
            //{
            //    foreach (Shape shape in shapefile)
            //    {
            //        string[] metadataNames = shape.GetMetadataNames();
            //        decimal lat = 0m;
            //        decimal lon = 0m;
            //        decimal globvalue = 0m;

            //        if (metadataNames != null)
            //        {
            //            foreach (string metadataName in metadataNames)
            //            {
            //                if (metadataName == "lat")
            //                    lat = decimal.Parse(shape.GetMetadata(metadataName));
            //                else if (metadataName == "lon")
            //                    lon = decimal.Parse(shape.GetMetadata(metadataName));
            //                else if (metadataName == "globvalue")
            //                    globvalue = decimal.Parse(shape.GetMetadata(metadataName));
            //            }
            //        }

            //        DataRow drNWS = dtNWS.NewRow();
            //        drNWS["lat"] = lat;
            //        drNWS["lon"] = lon;
            //        drNWS["globalvalue"] = globvalue;
            //        drNWS["precipDate"] = dateToLoad;
            //        drNWS["XAxis"] = Math.Cos(ConvertDegreesToRadians((double)lat)) * Math.Cos(ConvertDegreesToRadians((double)lon));
            //        drNWS["YAxis"] = Math.Cos(ConvertDegreesToRadians((double)lat)) * Math.Sin(ConvertDegreesToRadians((double)lon)); ;
            //        drNWS["ZAxis"] = Math.Sin(ConvertDegreesToRadians((double)lat));
            //        dtNWS.Rows.Add(drNWS);
            //    }
            //}

            //List; listOfDataSetsForThreads = new List();
            //DataSet dsCur = CreateNewDataSet();

            //// Create a list of datasets, each containing the rows the thread will import
            //foreach (DataRow dr in dtNWS.Rows)
            //{
            //    if (dsCur.Tables[0].Rows.Count % 3000 == 0)
            //    {
            //        listOfDataSetsForThreads.Add(dsCur);
            //        dsCur = CreateNewDataSet();
            //    }

            //    dsCur.Tables[0].ImportRow(dr);
            //}

            //if (dsCur.Tables[0].Rows.Count > 0)
            //    listOfDataSetsForThreads.Add(dsCur);

            //// Spawn off the threads to import our datasets in parallel
            //foreach (DataSet dsThreadWork in listOfDataSetsForThreads)
            //{
            //    WaitCallback wcb = new WaitCallback(ImportDataSet);
            //    ThreadPool.QueueUserWorkItem(wcb, dsThreadWork);
            //}
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

        }
    }
}
