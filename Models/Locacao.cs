
using LocacaoVeiculos.Models;
using System.ComponentModel.DataAnnotations;

namespace LocacaoVeiculos.Models
{
    public class Locacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        public DateTime DataFim { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Range(0.01, 99999999.99)]
        public decimal ValorTotal { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pendente";

        // Chaves estrangeiras
        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        // Relacionamentos
        public Veiculo? Veiculo { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
