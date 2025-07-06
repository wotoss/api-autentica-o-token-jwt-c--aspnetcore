using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.ModeloEntidade;

namespace UsuariosApi.Service
{
    public class UsuarioService
    {
        private IMapper _mapper; 
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task Cadastra(CreateUsuarioDto usuarioDto)
        {
            /*usuarioDto é o usauario que esta vindo na requisição pelo via client/navegador
            //ou seja foi digitado pelo usuario
            //não iremos fazer a logica neste momento
              => passo o  (usuarioDto - da requisição) para minha entidade ou class usuario*/
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            //eu uso ele para criar o usuario através do _userManager
            IdentityResult resultado = await _userManager.CreateAsync(usuario,
                usuarioDto.Password);

            //este usuário foi bem sucessedido = como se fosse if e else
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
            
        }
        public async Task<string> Login(LoginUsuarioDto loginUsuarioDto)
        {
          var usuarioLogado =  await _signInManager.PasswordSignInAsync(loginUsuarioDto.Username,
                loginUsuarioDto.Password, false, false);

            if (!usuarioLogado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado");
            }
            //vou rexuperar meu usuario atraves do _signInManager porque o dto não tem id e outros
            //vou pegar uma lista de usuários
            var usuario = _signInManager
                   .UserManager
                   .Users
                   .FirstOrDefault(user => user.NormalizedUserName ==
                   loginUsuarioDto.Username.ToUpper());

            //se o login foi bem sucedido nós vamos gerar um token e retornar para o usuário posteiormente
            //desta forma eu consigo enviar para _tokenService.GenerateToken o usuario completo
            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
