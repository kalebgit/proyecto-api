using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // metodos http get
        [HttpGet("Usuario/{usuario}/{contraseña}")]
        public User LogIn(string usuario, string contraseña)
        {
            return UserHandler.LogIn(usuario, contraseña);
        }

        [HttpGet("Usuario/{usuario}")]
        public User FindUser(string usuario)
        {
            return UserHandler.GetUser(usuario);
        }

        [HttpGet("Usuario/{id}")]
        public User FindUser(long id)
        {
            return UserHandler.GetUser(id);
        }

        [HttpGet("Usuarios")]
        public List<User> FindUser()
        {
            return UserHandler.GetUsers();
        }

        // metodos http post
        [HttpPost("Usuario")]
        public void CreateUser([FromBody]User user)
        {
            UserHandler.AddUsers(user);
        }

        // metodos http put
        [HttpPut("Usuario")]
        public string UpdateUser([FromBody]User user)
        {
            return UserHandler.UpdateUser(user) == 1 ? "se ha actualizado el usuario" :
                "no se pudo actualizar el usuario";
        }

        // metodo http delete
        [HttpDelete("Usuario/{id}")]
        public void DeleteUser(long id)
        {
            UserHandler.DeleteUser(id);
        }

    }
}
