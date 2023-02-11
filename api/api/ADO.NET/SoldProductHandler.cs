using api.Models;
using System.Data.SqlClient;

namespace api.ADO.NET
{
    public class SoldProductHandler : ClassHandler
    {
        // ================ metodos TIPOS DE DATOS =======================
        public static object IsLong(object obj)
        {
            long num;
            bool boolean;
            boolean = long.TryParse((string)obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = long.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            obj = num;
            return obj;
        }
        public static object IsInt(object obj)
        {
            int num;
            bool boolean;
            boolean = int.TryParse((string)obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = int.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            obj = num;
            return obj;
        }

        // ================ metodos CRUD =================================

        // metodo CREATE
        public static int AddSoldProduct(SoldProduct soldProduct)
        {
            if (soldProduct != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSER INTO ProductoVendido " +
                        "VALUES (@stock, @idProducto, @idVenta)", connection);


                    ProductHandler.UpdateStockProduct(soldProduct.ProductId, 
                        soldProduct.Quantity);
                    command.ExecuteNonQuery();
                }
                return 1;
            }
            else
                return 0;
            
        }

        // metodo READ
        public static SoldProduct GetSoldProduct(long id)
        {
            SoldProduct soldProduct;
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT prodven.* " +
                    "FROM ProductoVendido prodven " +
                    "INNER JOIN " +
                    "Venta ven " +
                    $"ON prodven.IdVenta = ven.Id AND ven.IdUsuario = {id};", connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            soldProduct = new SoldProduct(dataReader.GetInt32(0),
                                dataReader.GetInt64(1), dataReader.GetInt64(2));
                            return soldProduct;
                        }
                    }
                    else
                        return null;
                }
                return null;

            }
        }
        public static List<SoldProduct> GetSoldProduct()
        {
            List<SoldProduct> soldProducts = new List<SoldProduct>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ProductoVendido", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            soldProducts.Add(new SoldProduct(dataReader.GetInt32(0),
                                dataReader.GetInt64(1), dataReader.GetInt64(2)));
                        }
                        return soldProducts;
                    }
                    else
                        return null;
                }
                return null;

            }
        }

    }
}
