
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Repositories.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<List<Locacao>> ObterTodosAsync();

        Task<Locacao?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Locacao locacao);

        Task AtualizarAsync(Locacao locacao);

        Task<bool> ExcluirAsync(int id);

        Task<bool> ExisteConflitoAsync(
            int veiculoId,
            DateTime dataInicio,
            DateTime dataFim,
            int? locacaoIgnoradaId = null
        );
    }
}
