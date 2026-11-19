
using System.ComponentModel.DataAnnotations;

namespace LocacaoVeiculos.ViewModels
{
    public class VeiculoFormViewModel
    {
        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7,
            ErrorMessage = "A placa deve possuir 7 caracteres.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "Informe um ano válido.")]
        public int? Ano { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public int? CategoriaVeiculoId { get; set; }

        public bool Disponivel { get; set; } = true;
    }
}
