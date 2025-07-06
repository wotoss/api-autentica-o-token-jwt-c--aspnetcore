using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        //quais campos iremos passar para o nosso usuário
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        //vamos explicitar que este campo será "tratado" como senha
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
        //comparação de senha
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }  
    }
}
