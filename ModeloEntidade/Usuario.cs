using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.ModeloEntidade
{
    /*
       Identity é responsavel 
       => Criação de usuário
       => Controller de Identificação 
      ( cadastrar senha no banco e criptografia )
     */
    public class Usuario : IdentityUser
    {
        /*
         * Além de herdar todas as propriedade que estão dentro de IdentityUser
         * [UserName,  Email, PasswordHash e outros]
         * ao usar o class Usuario eu consigo (personalizar/adicionar)
         * propriedades que não temos no (IdentityUser) como a prop (DataNascimento)
         */
        public DateTime DataNascimento { get; set; }
        /*
          => Quando for instanciado um usuário você vai vazer uma chamada do
          cosntrutor da (super classe / base).
         */
        public Usuario(): base( ) { }

    }
}
