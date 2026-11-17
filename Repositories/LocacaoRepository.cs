
using Microsoft.EntityFrameworkCore;
using LocacaoVeiculos.Data;
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;

namespace LocacaoVeiculos.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public LocacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Locacao>> ObterTodosAsync()
        {
            return await _context.Locacoes
                .Include(l => l.Veiculo)
                    .ThenInclude(v => v!.CategoriaVeiculo)
                .Include(l => l.Cliente)
                .ToListAsync();
        }

        public async Task<Locacao?> ObterPorIdAsync(int id)
        {
            return await _context.Locacoes
                .Include(l => l.Veiculo)
                    .ThenInclude(v => v!.CategoriaVeiculo)
                .Include(l => l.Cliente)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AdicionarAsync(Locacao locacao)
        {
            await _context.Locacoes.AddAsync(locacao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Locacao locacao)
        {
            _context.Locacoes.Update(locacao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);

            if (locacao == null)
                return false;

            _context.Locacoes.Remove(locacao);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExisteConflitoAsync(
            int veiculoId,
            DateTime dataInicio,
            DateTime dataFim,
            int? locacaoIgnoradaId = null)
        {
            return await _context.Locacoes.AnyAsync(l =>
                l.VeiculoId == veiculoId &&
                l.Status != "Cancelada" &&
                (!locacaoIgnoradaId.HasValue ||
                 l.Id != locacaoIgnoradaId.Value) &&
                dataInicio < l.DataFim &&
                dataFim > l.DataInicio
            );
        }
    }
}
