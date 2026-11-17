
using LocacaoVeiculos.Models;

namespace LocacaoVeiculos.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterTodosAsync();

        Task<Cliente?> ObterPorIdAsync(int id);

        Task AdicionarAsync(Cliente cliente);

        Task AtualizarAsync(Cliente cliente);

        Task ExcluirAsync(int id);
    }
}
