using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace diPasswords.Infrastructure.Data
{
    // Свернутые запросы к базам данных
    // (нет нужды заново прописывать однообразный код)
    public class DataBaseManager : IDataBaseManager
    {
        private ILogger _logger; // Связывание с логгингом
        private string _connectionString; // Строка подключения к серверу БД

        public DataBaseManager(ILogger logger, string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;")
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        // Запрос без вывода
        public void Request(string commandString, Dictionary<string, object>? sqlInjetion = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(commandString, connection))
                    {
                        if (sqlInjetion != null)
                        {
                            foreach (var parameter in sqlInjetion) command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    _logger.Fatal("Incorrect SQL-request: " + e.Message);
                }
            }
        }
        // Запрос с чтением и выводом
        public List<EncryptedData> SelectData(string commandString, Dictionary<string, object>? sqlInjetion = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(commandString, connection))
                    {
                        List<EncryptedData> dataList = new List<EncryptedData>();                        

                        if (sqlInjetion != null)
                        {
                            foreach (var parameter in sqlInjetion) command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                        }

                        SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                EncryptedData data = new EncryptedData();

                                data.Name = sqlDataReader.IsDBNull(2) ? "" : sqlDataReader.GetString(2);
                                data.Favorite = sqlDataReader.GetBoolean(3);
                                data.EncryptedLogin = sqlDataReader.IsDBNull(4) ? "" : sqlDataReader.GetString(4);
                                data.EncryptedPassword = sqlDataReader.IsDBNull(5) ? "" : sqlDataReader.GetString(5);
                                data.EncryptedEmail = sqlDataReader.IsDBNull(6) ? "" : sqlDataReader.GetString(6);
                                data.EncryptedPhone = sqlDataReader.IsDBNull(7) ? "" : sqlDataReader.GetString(7);
                                data.EncryptedDescription = sqlDataReader.IsDBNull(8) ? "" : sqlDataReader.GetString(8);
                                data.IVector = sqlDataReader.IsDBNull(9) ? null : (byte[])sqlDataReader.GetValue(9);

                                dataList.Add(data);
                            }
                        }
                        else return null; //dataList.Add(new EncryptedData());

                        return dataList;
                    }
                }
                catch (Exception e)
                {
                    _logger.Fatal("Incorrect SQL-request: " + e.Message);
                    return null;
                }
            }
        }
        // Запрос с чтением и выводом для логина и пароля пользователя
        public List<MasterData> SelectBaseData(string commandString, Dictionary<string, object>? sqlInjetion = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(commandString, connection))
                    {
                        List<MasterData> dataList = new List<MasterData>();

                        if (sqlInjetion != null)
                        {
                            foreach (var parameter in sqlInjetion) command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                        }

                        SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                MasterData data = new MasterData();

                                data.Login = sqlDataReader.GetString(1);
                                data.Password = sqlDataReader.GetString(2);

                                dataList.Add(data);
                            }
                        }
                        else dataList.Add(new MasterData());

                        return dataList;
                    }
                }
                catch (Exception e)
                {
                    _logger.Fatal("Incorrect SQL-request: " + e.Message);
                    return null;
                }
            }
        }
        // Запрос с подсчетом по условию
        public object GetCount(string commandString, Dictionary<string, object>? sqlInjetion = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(commandString, connection))
                    {
                        if (sqlInjetion != null)
                        {
                            foreach (var parameter in sqlInjetion) command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                        }
                        return command.ExecuteScalar();
                    }
                }
                catch (Exception e)
                {
                    _logger.Fatal("Incorrect SQL-request: " + e.Message);
                    return 0;
                }
            }
        }
    }
}
