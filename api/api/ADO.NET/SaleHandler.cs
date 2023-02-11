using api.Models;
using System.Data.SqlClient;

namespace api.ADO.NET
{
    public class SaleHandler : ClassHandler
    {
        // ================== metodos TIPOS DE DATO ====================
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


        // ================== metodos CRUD =============================

        // metodo CREATE
        public static long AddSale(Sale sale)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) " +
                    $"VALUES ('{sale.Comments}', {sale.UserId}); SELECT @@IDENTITY", connection);
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        public static void ChargeSale(long idUsuario, List<SoldProduct> soldPorducts)
        {
            Sale sale = new Sale();
            sale.Comments = "";
            sale.UserId = idUsuario;

            long idVenta = AddSale(sale);
            
            foreach(SoldProduct soldproduct in soldPorducts)
            {
                soldproduct.SaleId = idVenta;
                SoldProductHandler.AddSoldProduct(soldproduct);
            }
        }

        // metodo READ
        public static Sale GetSale(long id)
        {
            Sale sale;
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Venta " +
                    $"WHERE Id = {id}", connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            sale = new Sale(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetInt64(2));
                            return sale;
                        }
                    }
                    return null;
                }
                return null;
            }
        }
        public static List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Venta", connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            sales.Add(new Sale(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetInt64(2)));
                        }
                        return sales;
                    }
                    return null;
                }
                return null;
            }
        }
    }
}
