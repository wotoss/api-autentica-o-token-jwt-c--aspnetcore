using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


using UsuariosApi.ModeloEntidade;

namespace UsuariosApi.Service
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            //vamos preencher (atraves das Claims) este token 
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, 
                usuario.DataNascimento.ToString()),
                //momento em que foi criado a (login/claim) - loginTimesstamp
                new Claim("loginTimestamp",
                DateTime.UtcNow.ToString()),
            };

            //chave para gerar o token
            var chave = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes("wotoy8fW3mXzP2sT9rQ5uV7eY1bL6aK0dJnO"));

            var signingCredentials =
                new SigningCredentials(chave,
                SecurityAlgorithms.HmacSha256);

            //as configurações presentes no token como cliclo de vida
            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );
            //aqui eu converto o token para uma string usando JwtSecurityTokenHandler
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
