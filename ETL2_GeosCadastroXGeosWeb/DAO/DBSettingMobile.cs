using System;

namespace DAO
{
    public class DBSettingMobile
    {

        private DBSettingMobile()
        {
        }
        static DBSettingMobile()
        {
            try
            {
                _connectionString = "Server=191.232.160.227;Port=25437;User Id=postgres;Password=stXYZ321#@;Database=dbgeoscad;Timeout=300;CommandTimeout = 300;";
                _providerName = "System.Data.SQLite";
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
            //get
            //{
            //    return _connectionString;
            //}
             get; set; 
        }

        public string ConectionStringSQLite { get; set; }

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
