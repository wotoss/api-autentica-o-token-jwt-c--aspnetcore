using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Service;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        //verbo post - estamos criando um usuario ou um recurso no sistema



        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            await _usuarioService.Cadastra(usuarioDto);
            return Ok("Usuário cadastrado com sucesso! ");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto loginUsuarioDto)
        {
           var token = await _usuarioService.Login(loginUsuarioDto);
           return Ok(token);
        }
    }
}
