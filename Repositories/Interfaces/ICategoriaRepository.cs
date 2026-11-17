
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<CategoriaVeiculo>> ObterTodosAsync();

        Task<CategoriaVeiculo?> ObterPorIdAsync(int id);

        Task AdicionarAsync(CategoriaVeiculo categoria);

        Task AtualizarAsync(CategoriaVeiculo categoria);

        Task<bool> ExcluirAsync(int id);
    }
}
