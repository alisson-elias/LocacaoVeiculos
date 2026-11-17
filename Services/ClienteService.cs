
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;
using LocacaoVeiculos.Services.Interfaces;

namespace LocacaoVeiculos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new Exception("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cliente.CPF))
                throw new Exception("O CPF é obrigatório.");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                throw new Exception("O e-mail é obrigatório.");

            await _repository.AdicionarAsync(cliente);
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            var existente = await _repository.ObterPorIdAsync(cliente.Id);

            if (existente == null)
                throw new Exception("Cliente não encontrado.");

            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new Exception("O nome é obrigatório.");

            existente.Nome = cliente.Nome;
            existente.CPF = cliente.CPF;
            existente.Telefone = cliente.Telefone;
            existente.Email = cliente.Email;

            await _repository.AtualizarAsync(existente);
        }

        public async Task ExcluirAsync(int id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (cliente.Locacoes.Count > 0)
                throw new Exception(
                    "Não é possível excluir um cliente com histórico de locações.");

            await _repository.ExcluirAsync(id);
        }
    }
}
