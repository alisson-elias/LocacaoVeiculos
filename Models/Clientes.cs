
using LocacaoVeiculos.Models;
using System.ComponentModel.DataAnnotations;

namespace LocacaoVeiculos.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14)]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(15)]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Relacionamento: cliente pode realizar várias locações
        public ICollection<Locacao> Locacoes { get; set; }
            = new List<Locacao>();
    }
}
