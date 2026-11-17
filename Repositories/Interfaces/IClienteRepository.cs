
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterTodosAsync();

        Task<Cliente?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Cliente cliente);

        Task AtualizarAsync(Cliente cliente);

        Task<bool> ExcluirAsync(int id);
    }
}
