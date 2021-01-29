using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAO
{
    public class DBAccessGeos
    {
        public static DbCommand CriarComando(string cmbText, CommandType cmbType, List<DbParameter> listParameter)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory(DBSettingGeos.ProviderName);
            DbConnection conexao = factory.CreateConnection();
            conexao.ConnectionString = DBSettingGeos.ConnectionString;
            DbCommand comando = conexao.CreateCommand();
            comando.CommandText = cmbText;
            comando.CommandType = cmbType;

            if (!(listParameter == null))
            {
                foreach (DbParameter parametro in listParameter)
                {
                    comando.Parameters.Add(parametro);
                }

            }

            return comando;
        }
        public static DbParameter CriarParametro(string nomeParametro, DbType tipoParametro, object valorParametro)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(DBSettingGeos.ProviderName);
            DbParameter parametro = factory.CreateParameter();
            if (!(parametro == null))
            {
                parametro.ParameterName = DBSettingGeos.SufixoBanco + nomeParametro;
                parametro.DbType = tipoParametro;
                parametro.Value = valorParametro;
            }

            return parametro;
        }
        public static object ExecutarComando(string cmbText, CommandType cmbType, List<DbParameter> listParameters, TypeCommand tipoRetorno)
        {

            DbCommand comando = CriarComando(cmbText, cmbType, listParameters);
            object objRetorno = null;
            try
            {
                comando.Connection.Open();
                switch (tipoRetorno)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objRetorno = comando.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objRetorno = comando.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objRetorno = comando.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        if (cmbType == CommandType.StoredProcedure)
                        {
                            var returnValue = comando.ExecuteReader();
                        }
                        DataTable tabela = new DataTable();
                        DbDataReader leitor = comando.ExecuteReader();
                        tabela.Load(leitor);
                        Random GeradorDeNumerosAleatorios = new Random();
                        tabela.TableName = "TABELA" + GeradorDeNumerosAleatorios.Next(1, 99999999).ToString();
                        leitor.Close();
                        objRetorno = tabela;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if ((tipoRetorno != TypeCommand.ExecuteReader))
                {
                    if ((comando.Connection.State == ConnectionState.Open))
                    {
                        comando.Connection.Close();
                        comando.Connection.Dispose();
                    }

                }

            }

            return objRetorno;
        }
        public static object ExecutarComando(string cmbText, TypeCommand tipoRetorno)
        {

            DbCommand comando = CriarComando(cmbText, CommandType.Text, null);
            object objRetorno = null;
            try
            {
                comando.Connection.Open();
                switch (tipoRetorno)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objRetorno = comando.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objRetorno = comando.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objRetorno = comando.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        DataTable tabela = new DataTable();
                        DbDataReader leitor = comando.ExecuteReader();
                        tabela.Load(leitor);
                        Random GeradorDeNumerosAleatorios = new Random();
                        tabela.TableName = "TABELA" + GeradorDeNumerosAleatorios.Next(1, 99999999).ToString();
                        leitor.Close();
                        objRetorno = tabela;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if ((tipoRetorno != TypeCommand.ExecuteReader))
                {
                    if ((comando.Connection.State == ConnectionState.Open))
                    {
                        comando.Connection.Close();
                        comando.Connection.Dispose();
                    }

                }

            }

            return objRetorno;
        }
        private async void ExecutarComando(string cmbText, CommandType cmbType, List<DbParameter> listParameters)
        {
            SqlConnection conexao = new SqlConnection(DBSettingGeos.ConnectionString);
            try
            {
                SqlCommand comando = new SqlCommand(cmbText, conexao);
                foreach (SqlParameter parametro in listParameters)
                {
                    comando.Parameters.Add(parametro);
                }
                await conexao.OpenAsync();
                await comando.ExecuteNonQueryAsync();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        public enum TypeCommand
        {

            ExecuteNonQuery,
            ExecuteReader,
            ExecuteScalar,
            ExecuteDataTable,
        }
        

    }
}
