using System;

namespace DAO
{
    public class DBSettingGeosWeb
    {

        private DBSettingGeosWeb()
        {
        }
        static DBSettingGeosWeb()
        {
            try
            {
                //servidor\banco produção
                //_connectionString = "Data Source=dbenecadprod.database.windows.net ;Initial Catalog=NeoenergiaNetAssetDB_Prod;Persist Security Info=True;User ID=fabiano.muniz;Password=DbGeos@884400";
                //servidor\banco homologação
                _connectionString = "Data Source=dbenecaddev.database.windows.net;Initial Catalog=NeoenergiaNetAssetDB_Homologa;Persist Security Info=True;User ID=fabiano.muniz;Password=DbGeos@884400";

                //Local
                //_connectionString = "Data Source=DESKTOP-E999VKR\\SRVSQLSERVER2019;Initial Catalog=NeoenergiaNetAssetDB_Homologa_I;Persist Security Info=True;User ID=sa;Password=123456";
                _providerName = "System.Data.SqlClient";
                _tipoBanco = "eSQL";
                _sufixoBanco = "@";
            }
            catch (Exception)
            {
                throw new Exception("Erro ao acessar ConnectionString.");
            }
        }

        private static string _connectionString;
        public string pconnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
        private static string _providerName;
        public string pproviderName
        {
            get
            {
                return _providerName;
            }
            set
            {
                _providerName = value;
            }
        }
        public static string ProviderName
        {
            get
            {
                return _providerName;
            }
        }
        private static string _tipoBanco;
        public string ptipoBanco
        {
            get
            {
                return _tipoBanco;
            }
            set
            {
                _tipoBanco = value;
            }
        }
        public static string TipoBanco
        {
            get
            {
                return _tipoBanco;
            }
        }
        private static string _sufixoBanco;
        public string psufixoBanco
        {
            get
            {
                return _sufixoBanco;
            }
            set
            {
                _sufixoBanco = value;
            }
        }
        public static string SufixoBanco
        {
            get
            {
                return _sufixoBanco;
            }
        }
    }
}
