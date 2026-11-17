
using LocacaoVeiculos.Models;
using System.ComponentModel.DataAnnotations;

namespace LocacaoVeiculos.Models
{
    public class CategoriaVeiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O valor da diária é obrigatório.")]
        [Range(0.01, 999999.99,
            ErrorMessage = "O valor da diária deve ser maior que zero.")]
        public decimal ValorDiaria { get; set; }

        // Relacionamento: uma categoria possui vários veículos
        public ICollection<Veiculo> Veiculos { get; set; }
            = new List<Veiculo>();
    }
}
