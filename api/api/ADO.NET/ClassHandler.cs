namespace api.ADO.NET
{
    public abstract class ClassHandler
    {
        // delegado para verificar los tipos de dato de acuerdo a la base de datos
        public delegate object Verificator(object obj);

        // connection string
        protected static string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        

    }
}
