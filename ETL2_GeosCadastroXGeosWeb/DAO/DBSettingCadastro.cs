﻿using System;

namespace DAO
{
    public class DBSettingCadastro
    {

        private DBSettingCadastro()
        {
        }
        static DBSettingCadastro()
        {
            try
            {
                //Provider = PostgreSQLOLEDBProvider; Data Source = myServerAddress; location = myDataBase; User ID = myUsername; password = myPassword; timeout = 1000;
               // _connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=dbgeoscad_mirror;Timeout=1000;CommandTimeout = 1000;";
                _connectionString = "Server=191.232.160.227;Port=25437;User Id=postgres;Password=stXYZ321#@;Database=dbgeoscad;Timeout=300;CommandTimeout = 300;";
                _providerName = "Npgsql";
                _tipoBanco = "ePgSQL";
                _sufixoBanco = "@";

                //_connectionString = "Data Source=DESKTOP-E999VKR\\SRVSQLSERVER2019;Initial Catalog=CADASTRO_IMPORT_UM;Integrated Security=True";
                //_providerName = "System.Data.SqlClient";
                //_tipoBanco = "eSQL";
                //_sufixoBanco = "@";
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