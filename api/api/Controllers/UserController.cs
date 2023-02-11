using api.ADO.NET;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // metodos http get
        [HttpGet("Usuario/{usuario}/{contraseña}")]
        public User LogIn(string usuario, string contraseña)
        {
            User user = UserHandler.LogIn(usuario, contraseña);
            return user;
        }

        [HttpGet("Usuario/{usuario}")]
        public User FindUser(string usuario)
        {
            User user = UserHandler.GetUser(usuario);
            return user;
        }

        // metodos http post
        [HttpPost("Usuario")]
        public void CreateUser([FromBody]User user)
        {
            UserHandler.AddUsers(user);
        }

        // metodos http put
        [HttpPut("Usuario")]
        public void UpdateUser([FromBody]User user)
        {
            UserHandler.UpdateUser(user);
        }

        // metodo http delete
        [HttpDelete("Usuario/{id}")]
        public void DeleteUser(long id)
        {
            UserHandler.DeleteUser(id);
        }

    }
}
