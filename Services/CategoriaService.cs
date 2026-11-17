
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;
using LocacaoVeiculos.Services.Interfaces;

namespace LocacaoVeiculos.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IVeiculoRepository _veiculoRepository;

        public CategoriaService(
            ICategoriaRepository repository,
            IVeiculoRepository veiculoRepository)
        {
            _repository = repository;
            _veiculoRepository = veiculoRepository;
        }

        public async Task<List<CategoriaVeiculo>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<CategoriaVeiculo?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(CategoriaVeiculo categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
                throw new Exception("O nome é obrigatório.");

            if (categoria.ValorDiaria <= 0)
                throw new Exception("O valor da diária deve ser positivo.");

            await _repository.AdicionarAsync(categoria);
        }

        public async Task AtualizarAsync(CategoriaVeiculo categoria)
        {
            var existente = await _repository.ObterPorIdAsync(categoria.Id);

            if (existente == null)
                throw new Exception("Categoria não encontrada.");

            if (string.IsNullOrWhiteSpace(categoria.Nome))
                throw new Exception("O nome é obrigatório.");

            if (categoria.ValorDiaria <= 0)
                throw new Exception("O valor da diária deve ser positivo.");

            existente.Nome = categoria.Nome;
            existente.Descricao = categoria.Descricao;
            existente.ValorDiaria = categoria.ValorDiaria;

            await _repository.AtualizarAsync(existente);
        }

        public async Task ExcluirAsync(int id)
        {
            var categoria = await _repository.ObterPorIdAsync(id);

            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            var veiculos = await _veiculoRepository.ObterTodosAsync();

            if (veiculos.Any(v => v.CategoriaVeiculoId == id))
                throw new Exception(
                    "Não é possível excluir uma categoria que possui veículos vinculados.");

            await _repository.ExcluirAsync(id);
        }
    }
}
