using api.Models;
using Microsoft.AspNetCore.Connections.Features;
using System.Data;
using System.Data.SqlClient;

namespace api.ADO.NET
{
    public class ProductHandler : ClassHandler
    {

        // ======================= metodos TIPOS DE DATO =======================
        public static object IsDecimal(object obj)
        {
            decimal num;
            bool boolean;
            boolean = decimal.TryParse((string)obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = decimal.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            obj = num;
            return obj;
        }
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
        public static object NonNullable(object obj)
        {
            while (String.IsNullOrWhiteSpace((string)obj))
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                obj = Console.ReadLine();
            }

            return obj;
        }


        // ======================= metodos CRUD =====================

        // metodos CREATE
        public static int AddProducts(Product product)
        {
            if (product != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Producto VALUES " +
                        "(@descripciones, @costo, @precioVenta, @stock, @idUsuario)",
                        connection);

                    command.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar)
                    { Value = product.Description });
                    command.Parameters.Add(new SqlParameter("costo", SqlDbType.Money)
                    { Value = product.Cost });
                    command.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Money)
                    { Value = product.SellingPrice });
                    command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int)
                    { Value = product.Stock });
                    command.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt)
                    { Value = product.UserId });

                    command.ExecuteNonQuery();
                }
                return 1;
            }
            else
                return 0;

        }
        public static int AddProducts(params Product[] products)
        {
            if (products.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string values = "";
                    for (int i = 0; i < products.Length; i++)
                    {
                        values += $"('{products[i].Description}', {products[i].Cost}," +
                            $" {products[i].SellingPrice}, {products[i].Stock}, " +
                            $"{products[i].UserId})";
                        values += i == products.Length - 1 ? "" : ", ";
                    }

                    SqlCommand command = new SqlCommand("INSERT INTO Producto VALUES " + values,
                        connection);
                    command.ExecuteNonQuery();
                }
                return 1;
            }
            else
                return 0;
        }

        // metodos READ
        public static Product GetProduct(long id)
        {
            Product product;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Producto" +
                    $" WHERE Id = {id}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            product = new Product(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetDecimal(2), dataReader.GetDecimal(3),
                                dataReader.GetInt32(4), dataReader.GetInt64(5));
                            return product;
                        }
                    }
                    else
                        return null;
                }
                return null;
            }
        }
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Producto", connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            products.Add(new Product(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetDecimal(2), dataReader.GetDecimal(3),
                                dataReader.GetInt32(4), dataReader.GetInt64(5)));
                        }
                        return products;
                    }
                    else
                        return null;
                }
                return null;
            }
        }

        // metodos UPDATE
        public static int UpdateProduct(Product product)
        {
            if (product != null)
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Producto SET " +
                        $"Descripciones = '{product.Description}', Costo = {product.Cost}, " +
                        $"PrecioVenta = {product.SellingPrice}, Stock = {product.Stock}, " +
                        $"IdUsuario = {product.UserId} " +
                        $"WHERE Id = {product.Id}", connection);
                    command.ExecuteNonQuery();
                }
                return 1;
            }
            else
                return 0;
        }
        public static void UpdateStockProduct(long id, int quantitySold)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SoldProduct soldProduct = SoldProductHandler.GetSoldProduct(id);
                Product product = ProductHandler.GetProduct(id);
                SqlCommand command = new SqlCommand($"UPDATE Producto SET Stock = " +
                    $"@stock WHERE Id = {product.Id}");

                command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int)
                { Value = product.Stock - soldProduct.Quantity });

                command.ExecuteNonQuery();
            }
        }

        // metodos DELETE
        public static void DeleteProduct(long id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Producto " +
                    $"WHERE Id = {id}", connection);
                command.ExecuteNonQuery();
            }
        }

        public static void ShowProducts(List<Product> products)
        {
            if (products.Count > 0)
            {
                Console.WriteLine("{0, -5}{1, -30}{2, -20}{3, -20}{4, -20}",
                "Id", "Description", "Cost", "Selling Price", "Stock");
                foreach (Product product in products)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine("\n\n");
            }
            else
                Console.WriteLine("%%% NO HAY PRODUCTOS %%%\n\n");

        }
    }
}
