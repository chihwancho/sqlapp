using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "appserver21213.database.windows.net";
        private static string db_user = "az204";
        private static string db_password = "bg7TpnZfk@otq!aEXJB^4G!S8R6naMq";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _product_list = new List<Product>();

            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };

                    _product_list.Add(product);
                }
            }

            conn.Close();
            return _product_list;

        }
    }
}
