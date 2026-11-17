
using Microsoft.EntityFrameworkCore;
using LocacaoVeiculos.Data;
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;

namespace LocacaoVeiculos.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly ApplicationDbContext _context;

        public VeiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos
                .Include(v => v.CategoriaVeiculo)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos
                .Include(v => v.CategoriaVeiculo)
                .Include(v => v.Locacoes)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                return false;

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
