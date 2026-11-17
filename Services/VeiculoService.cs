
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;
using LocacaoVeiculos.Services.Interfaces;

namespace LocacaoVeiculos.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public VeiculoService(
            IVeiculoRepository veiculoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _veiculoRepository = veiculoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _veiculoRepository.ObterTodosAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _veiculoRepository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            if (string.IsNullOrWhiteSpace(veiculo.Placa))
                throw new Exception("A placa é obrigatória.");

            var categoria = await _categoriaRepository
                .ObterPorIdAsync(veiculo.CategoriaVeiculoId);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            await _veiculoRepository.AdicionarAsync(veiculo);
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            var existente = await _veiculoRepository
                .ObterPorIdAsync(veiculo.Id);

            if (existente == null)
                throw new Exception("Veículo não encontrado.");

            var categoria = await _categoriaRepository
                .ObterPorIdAsync(veiculo.CategoriaVeiculoId);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            existente.Marca = veiculo.Marca;
            existente.Modelo = veiculo.Modelo;
            existente.Placa = veiculo.Placa;
            existente.Ano = veiculo.Ano;
            existente.CategoriaVeiculoId = veiculo.CategoriaVeiculoId;

            await _veiculoRepository.AtualizarAsync(existente);
        }

        public async Task ExcluirAsync(int id)
        {
            var veiculo = await _veiculoRepository.ObterPorIdAsync(id);

            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (veiculo.Locacoes.Count > 0)
                throw new Exception(
                    "Não é possível excluir um veículo com histórico de locações.");

            await _veiculoRepository.ExcluirAsync(id);
        }
    }
}
