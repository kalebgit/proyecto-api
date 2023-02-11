using api.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;

namespace api.ADO.NET
{
    public class UserHandler : ClassHandler
    {
        
        // ============== metodos TIPOS DE DATOS ==================
        public static object IsMail(object obj)
        {
            string mail = (string)obj;
            while (!mail.Contains("@"))
            {
                Console.Write("\n---------- No ingresaste correo, debe " +
                    "tener @ ----------\n" +
                    "\nPon un correo: ");
                obj = Console.ReadLine();
            }
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

        // =============== metodos CRUD =================

        // metodos CREATE
        public static int AddUsers(User user)
        {
            if (user != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Usuario VALUES " +
                        "(@nombre, @apellido, @nombreUsuario, @contrasenia, @mail)", connection);

                    command.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar)
                    { Value = user.Name });
                    command.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar)
                    { Value = user.LastName });
                    command.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar)
                    { Value = user.UserName });
                    command.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar)
                    { Value = user.Password });
                    command.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar)
                    { Value = user.Mail });

                    command.ExecuteNonQuery();
                }

                return 1;
            }
            else
                return 0;
            
        }
        public static int AddUsers(params User[] users)
        {
            if (users.Length != 0)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string values = "";
                    for(int i = 0; i < users.Length; i++)
                    {
                        values += $"('{users[i].Name}', '{users[i].LastName}', '{users[i].UserName}', " +
                            $"'{users[i].Password}', '{users[i].Mail}')";
                        values += i == users.Length - 1 ? "" : ", ";
                    }
                    SqlCommand command = new SqlCommand("INSERT INTO Usuario VALUES " + values,
                        connection);
                    command.ExecuteNonQuery();
                }

                return 1;
            }
            else
                return 0;

        }

        // metodos READ
        public static User GetUser(long id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                User user;
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario " +
                    $"WHERE Id = {id}");
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            user = new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5));
                            return user;
                        }
                    }
                    else
                        return null;
                }
            }
            return null;
        }
        public static User GetUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                User user;
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario " +
                    $"WHERE NombreUsuario = {userName}");
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            user = new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5));
                            return user;
                        }
                    }
                    else
                        return null;
                }
            }
            return null;
        }
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            users.Add(new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5)));
                        }
                        return users;
                    }
                    return null;
                }
                return null;
            }
        }

        // metodos UPDATE
        public static int UpdateUser(User user)
        {
            if (user != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Usuario SET " +
                        $"Nombre = '{user.Name}', Apellido = '{user.LastName}', " +
                        $"NombreUsuario = '{user.UserName}', " +
                        $"Contraseña = '{user.Password}', Mail = '{user.Mail}' " +
                        $"WHERE Id = {user.Id}", connection);
                    command.ExecuteNonQuery();
                }
                return 1;
            }
            else
                return 0;
            
        }

        // metodos DELETE
        public static void DeleteUser(long id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Usuario " +
                    $"WHERE Id = {id}");
                command.ExecuteNonQuery();
            }
        }


        // =============== metodos ESPECIALES ===================
        public static User LogIn(string userName, string password)
        {
            User user;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario WHERE " +
                    $"NombreUsuario = {userName} AND Contraseña = {password}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            user = new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5));
                            return user;
                        }

                    }
                    else
                        return null;

                }
            }
            return null;
        }
    }
}
