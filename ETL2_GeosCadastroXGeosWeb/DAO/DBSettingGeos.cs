using System;

namespace DAO
{
    public class DBSettingGeos
    {
        private DBSettingGeos()
        {
        }
        static DBSettingGeos()
        {
            try
            {
                _connectionString = "Data Source=dbenecadprod.database.windows.net;Initial Catalog=CadastroNetAssetDb_Prod;Persist Security Info=True;User ID=fabiano.muniz;Password=DbGeos@884400";
                ////_connectionString = "Data Source=geoscadastro.database.windows.net;Initial Catalog=CadastroNetAssetDb_Develop;Persist Security Info=True;User ID=dbEnecadCad;Password=En3c4d@g30sW3b";
                //_connectionString = "Data Source=DESKTOP-E999VKR\\SRVSQLSERVER2019;Initial Catalog=CadastroNetAssetDb_Demo20201211;Integrated Security=True";
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
