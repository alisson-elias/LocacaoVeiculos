
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;
using LocacaoVeiculos.Services.Interfaces;

namespace LocacaoVeiculos.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IClienteRepository _clienteRepository;

        public LocacaoService(
            ILocacaoRepository locacaoRepository,
            IVeiculoRepository veiculoRepository,
            IClienteRepository clienteRepository)
        {
            _locacaoRepository = locacaoRepository;
            _veiculoRepository = veiculoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Locacao>> ObterTodosAsync()
        {
            return await _locacaoRepository.ObterTodosAsync();
        }

        public async Task<Locacao?> ObterPorIdAsync(int id)
        {
            return await _locacaoRepository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Locacao locacao)
        {
            // Validar período
            if (locacao.DataFim.Date <= locacao.DataInicio.Date)
                throw new Exception(
                    "A data final deve ser posterior à data inicial.");

            // Verificar veículo
            var veiculo = await _veiculoRepository
                .ObterPorIdAsync(locacao.VeiculoId);

            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (!veiculo.Disponivel)
                throw new Exception("Veículo indisponível para locação.");

            // Verificar cliente
            var cliente = await _clienteRepository
                .ObterPorIdAsync(locacao.ClienteId);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            // Verificar conflitos de datas
            bool existeConflito =
                await _locacaoRepository.ExisteConflitoAsync(
                    locacao.VeiculoId,
                    locacao.DataInicio,
                    locacao.DataFim
                );

            if (existeConflito)
                throw new Exception(
                    "O veículo já possui uma locação nesse período.");

            // Calcular valor
            int quantidadeDiarias =
                (locacao.DataFim.Date - locacao.DataInicio.Date).Days;

            decimal valorDiaria =
                veiculo.CategoriaVeiculo!.ValorDiaria;

            locacao.ValorTotal =
                quantidadeDiarias * valorDiaria;

            locacao.Status = "Pendente";
            locacao.DataDevolucao = null;

            await _locacaoRepository.AdicionarAsync(locacao);
        }

        public async Task AtualizarAsync(Locacao locacao)
        {
            var existente = await _locacaoRepository
                .ObterPorIdAsync(locacao.Id);

            if (existente == null)
                throw new Exception("Locação não encontrada.");

            if (existente.Status != "Pendente")
                throw new Exception(
                    "Somente locações pendentes podem ser alteradas.");

            if (locacao.DataFim.Date <= locacao.DataInicio.Date)
                throw new Exception(
                    "A data final deve ser posterior à data inicial.");

            var veiculo = await _veiculoRepository
                .ObterPorIdAsync(locacao.VeiculoId);

            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (!veiculo.Disponivel)
                throw new Exception("Veículo indisponível.");

            var cliente = await _clienteRepository
                .ObterPorIdAsync(locacao.ClienteId);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            bool existeConflito =
                await _locacaoRepository.ExisteConflitoAsync(
                    locacao.VeiculoId,
                    locacao.DataInicio,
                    locacao.DataFim,
                    locacao.Id
                );

            if (existeConflito)
                throw new Exception(
                    "O veículo já possui uma locação nesse período.");

            int diarias =
                (locacao.DataFim.Date - locacao.DataInicio.Date).Days;

            existente.VeiculoId = locacao.VeiculoId;
            existente.ClienteId = locacao.ClienteId;
            existente.DataInicio = locacao.DataInicio;
            existente.DataFim = locacao.DataFim;
            existente.ValorTotal =
                diarias * veiculo.CategoriaVeiculo!.ValorDiaria;

            await _locacaoRepository.AtualizarAsync(existente);
        }

        public async Task ExcluirAsync(int id)
        {
            var locacao = await _locacaoRepository.ObterPorIdAsync(id);

            if (locacao == null)
                throw new Exception("Locação não encontrada.");

            if (locacao.Status != "Pendente")
                throw new Exception(
                    "Somente locações pendentes podem ser excluídas.");

            await _locacaoRepository.ExcluirAsync(id);
        }

        public async Task RegistrarDevolucaoAsync(int id)
        {
            var locacao = await _locacaoRepository.ObterPorIdAsync(id);

            if (locacao == null)
                throw new Exception("Locação não encontrada.");

            if (locacao.Status != "Pendente")
                throw new Exception(
                    "Esta locação não pode receber uma devolução.");

            locacao.DataDevolucao = DateTime.Now;
            locacao.Status = "Concluida";

            await _locacaoRepository.AtualizarAsync(locacao);
        }
    }
}
