using api.ADO.NET;
using System.Data.SqlClient;

namespace api.Models
{
    public class User : UserHandler
    {
        // delegados
        Verificator rightType = new Verificator(NonNullable);

        // instance variables
        private long _id;
        private string _name;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _mail;

        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = (string)rightType(value);
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = (string)rightType(value);
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = (string)rightType(value);
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = (string)rightType(value);
            }
        }
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                string nonNullableMail;
                nonNullableMail = (string)rightType(value);
                rightType = new Verificator(IsMail);
                _mail = (string)rightType(nonNullableMail);
            }
        }

        public User(long id, string name, string lastName, string userName,
            string password, string mail)
        {
            _id = id;
            _name = name;
            _lastName = lastName;
            _userName = userName;
            _password = password;
            _mail = mail;
        }

        // metodo to string
        public override string ToString()
        {
            return String.Format("{0, -5}{1, -30}{2, -30}{3, -20}{4, -20}{5, -30}",
                _id, _name, _lastName, _userName, "privado", _mail);
        }

        public static User LogIn(string userName, string password)
        {
            User user;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario" +
                    $" WHERE NombreUsuario = '{userName}' AND Contraseña = '{password}'",
                    connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows && dataReader.Read())
                    {
                        Console.WriteLine("\n %%%%% HAS INICIADO SESION %%%%%\n");
                        user = new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5));
                        return user;
                    }
                    else
                    {
                        Console.WriteLine("\n *** ERROR, NO HAY COINCIDENCIAS ***\n");
                        return null;
                    }

                }
            }
        }
    }
}
