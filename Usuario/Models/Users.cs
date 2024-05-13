using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Usuario.Models
{
    public class Users
    {
        [Key]
        [Column("userId")]
        [Display(Name = "Id")]
        public int UserId { get; set; }

        [Column("userNome")]
        [Display(Name = "Nome")]
        public string? UserNome { get; set; }

        [Column("userCpf")]
        [Display(Name = "Cpf")]
        public string? UserCpf { get; set; }

        [Column("userTelefone")]
        [Display(Name = "Telefone")]
        public string? UserTelefone { get; set; }

        [Column("UserLogin")]
        [Display(Name = "Login")]
        public string? UserLogin { get; set; }

        [Column("userSenha")]
        [Display(Name = "Senha")]
        public string? UserSenha { get; set; }

        [Column("userNivel")]
        [Display(Name = "Nivel")]
        public string? UserNivel { get; set; }

        [Column("userStatus")]
        [Display(Name = "Status")]
        public string? UserStatus { get; set; }

    }
}
