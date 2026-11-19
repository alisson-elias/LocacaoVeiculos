
using System.ComponentModel.DataAnnotations;

namespace LocacaoVeiculos.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7)]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(1900, 2100,
            ErrorMessage = "Ano inválido.")]
        public int Ano { get; set; }

        public bool Disponivel { get; set; } = true;

        // Chave estrangeira
        [Required(ErrorMessage = "Selecione uma categoria.")]     
        public int CategoriaVeiculoId { get; set; }

        // Relacionamento: veículo pertence a uma categoria
        [Required(ErrorMessage = "Selecione uma categoria.")]
        public CategoriaVeiculo? CategoriaVeiculo { get; set; }

        // Relacionamento: veículo pode possuir várias locações
        public ICollection<Locacao> Locacoes { get; set; }
            = new List<Locacao>();
    }
}
