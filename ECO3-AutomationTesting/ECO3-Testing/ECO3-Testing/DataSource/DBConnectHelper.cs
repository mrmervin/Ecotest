using ECO3_Testing.CustomException;
using ECO3_Testing.Settings;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ECO3_Testing.DataSource
{
    public class DBConnectHelper
    {
        public static List <DatabaseResult> GetQueryResult(string query,DatabaseName nameOfDatabase)
        {
            string connectionString = SelectDatabase(nameOfDatabase);
            
            List<DatabaseResult> outPut = new List<DatabaseResult>();
           
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection connection = EstablishConnection(connectionString))
                {
                    OpenConnection(connection);
                    SqlDataReader dataReader;
                    SqlCommand command = new SqlCommand(query, connection);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                      DatabaseResult result = new DatabaseResult();
                      result.GetDataOfSpecifiedColumns(dataReader);
                      outPut.Add(result);
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return outPut;
            }
            catch (ECO3TestException)
            {
                throw new ECO3TestException("Issue encountered while trying to access database or Check columns specified in SQL query are within range");
            }
            
        }
        public static bool DeleteRow(string Table, string columnName,object condition,DatabaseName databaseName)
        {
            string connectionString = SelectDatabase(databaseName);
            try
            {
                int rowsAffected;
                bool isDeleted =false;
                using (SqlConnection connectionForDeleteQuery = EstablishConnection(connectionString))
                {
                    OpenConnection(connectionForDeleteQuery);
                    SqlCommand sqlCommand;

                    if (condition.GetType() == typeof(int))
                    {
                        sqlCommand = new SqlCommand("DELETE FROM " + Table + " WHERE " + columnName + " = " + condition, connectionForDeleteQuery);
                    }
                    else
                    {
                        sqlCommand = new SqlCommand("DELETE FROM " + Table + " WHERE " + columnName + " = '" + condition + "'", connectionForDeleteQuery);
                    }
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                if (rowsAffected >= 1)
                {
                    isDeleted = true;
                }
                return isDeleted;
            }
            catch (ECO3TestException)
            {
                throw new ECO3TestException("Issue encountered while trying to do a delete operation");
            }
        }
        public static int RowReturnedQuery(string query,DatabaseName databaseName)
        {
            string connectionString = SelectDatabase(databaseName);
            int numberOfRows;
            try
            {
                using (SqlConnection simpleQueryConnection = EstablishConnection(connectionString))
                {
                    OpenConnection(simpleQueryConnection);
                    SqlCommand command = new SqlCommand(query, simpleQueryConnection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    DataTable rows = new DataTable();
                    rows.Load(dataReader);
                    numberOfRows = rows.Rows.Count;
                    return numberOfRows;
                }
            }
            catch (ECO3TestException)
            {
                throw new ECO3TestException("Error retrieving rows for the query "+query);
            }
        }
        private static SqlConnection EstablishConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
         private static void OpenConnection(SqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }
            connection.Open();
        }
        private static string SelectDatabase(DatabaseName nameOfDatabase)
        {
            string connectionString= null;
            switch (nameOfDatabase)
            {
                case DatabaseName.Documents:
                    connectionString = ObjectRepository.Config.GetDocumentsSqlConnection;
                    break;
                case DatabaseName.ECO:
                    connectionString = ObjectRepository.Config.GetECOSqlConnection;
                    break;
            }
            return connectionString;
        }
    }
}
