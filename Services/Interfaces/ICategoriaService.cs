
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaVeiculo>> ObterTodosAsync();

        Task<CategoriaVeiculo?> ObterPorIdAsync(int id);

        Task AdicionarAsync(CategoriaVeiculo categoria);

        Task AtualizarAsync(CategoriaVeiculo categoria);

        Task ExcluirAsync(int id);
    }
}
