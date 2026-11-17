
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Services.Interfaces
{
    public interface ILocacaoService
    {
        Task<List<Locacao>> ObterTodosAsync();

        Task<Locacao?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Locacao locacao);

        Task AtualizarAsync(Locacao locacao);

        Task ExcluirAsync(int id);

        Task RegistrarDevolucaoAsync(int id);
    }
}
