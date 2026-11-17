
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Repositories.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<List<Veiculo>> ObterTodosAsync();

        Task<Veiculo?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Veiculo veiculo);

        Task AtualizarAsync(Veiculo veiculo);

        Task<bool> ExcluirAsync(int id);
    }
}
