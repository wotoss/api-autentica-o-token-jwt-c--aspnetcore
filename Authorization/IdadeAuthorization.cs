using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UsuariosApi.ModeloEntidade;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        /* o proprio - HandleRequirementAsync - nos "prover" o context
           através do context nós conseguimos acessar as informações do usuário.*/
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            //atraves deste context nós temos as informações do usuário
            var dataNascimentoClaim = context
            //atraves da User.FindFirst eu chego no link da claim =>
                .User.FindFirst(claim =>
                claim.Type == ClaimTypes.DateOfBirth);

            //lembre que aqui é um if e else
            /*se for nulo não autorizamos e damos um Task.CompletedTask; */
            if (dataNascimentoClaim is null)
                return Task.CompletedTask;
            /*se não ou seja entramos no else
              neste caso estamos pegando a dataNascimentoClaim e convertendo 
              para um date time */
            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            var idadeUsuario = 
                DateTime.Today.Year - dataNascimento.Year;

            /*
              Vamos entender com um exemplo:
              Hoje é 06/07/2025
              2025 - 2000 = 25 anos
              Usuário nasceu em 31/12/2000
              Mas o usuário ainda não fez aniversário em 2025! Ele fará em dezembro.
            */
            if (dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
                idadeUsuario--;

            if (idadeUsuario >= requirement.Idade)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
