
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<List<Veiculo>> ObterTodosAsync();

        Task<Veiculo?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Veiculo veiculo);

        Task AtualizarAsync(Veiculo veiculo);

        Task ExcluirAsync(int id);
    }
}
