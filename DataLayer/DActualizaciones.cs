using CommonLayer;
using CommonLayer.Logs;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataLayer
{
    public class DActualizaciones
    {
        private bool runSqlScriptFile(string script, string connectionString)
        {
            try
            {

                // split script on GO command
                System.Collections.Generic.IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                                         RegexOptions.Multiline | RegexOptions.IgnoreCase);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (string commandString in commandStrings)
                    {
                        if (commandString.Trim() != "")
                        {
                            using (var command = new SqlCommand(commandString, connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    string spError = commandString.Length > 100 ? commandString.Substring(0, 100) + " ...\n..." : commandString;
                                    clsEvento evento = new clsEvento(string.Format("Favor verificar el SqlServer script.\nArchivo: script.sql \nLinea: {0} \nError: {1} \nSQL Command: \n{2}", ex.LineNumber, ex.Message, spError), "1");
                                    return false;
                                }
                            }
                        }
                    }
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento("Error ejecutar script.sql" + ex.Message, "1");
                return false;
            }
        }
        public bool updateDataBase(string query)
        {
            return runSqlScriptFile(query, Global.conexionReportes);

            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(
            //   Global.conexionReportes))
            //    {
            //        SqlCommand command = new SqlCommand(query, connection);
            //        command.Connection.Open();
            //        command.ExecuteNonQuery();
            //    }
            //    return true;

            //}
            //catch (Exception ex)
            //{

            //    return false;
            //}
        }

    }
}
