using System.Data.SqlClient;

namespace CommonLayer
{
    public class SqlHelper
    {
        SqlConnection cnx;

        public SqlHelper(string connection)
        {
            cnx = new SqlConnection(connection);
        }

        public bool isConnected
        {

            get
            {
                if (cnx.State == System.Data.ConnectionState.Closed)
                    cnx.Open();
                return true;
            }
        }

    }
}
