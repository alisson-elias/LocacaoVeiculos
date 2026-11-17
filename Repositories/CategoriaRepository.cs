
using Microsoft.EntityFrameworkCore;
using LocacaoVeiculos.Data;
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;

namespace LocacaoVeiculos.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaVeiculo>> ObterTodosAsync()
        {
            return await _context.CategoriasVeiculo
                .ToListAsync();
        }

        public async Task<CategoriaVeiculo?> ObterPorIdAsync(int id)
        {
            return await _context.CategoriasVeiculo
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(CategoriaVeiculo categoria)
        {
            await _context.CategoriasVeiculo.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(CategoriaVeiculo categoria)
        {
            _context.CategoriasVeiculo.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var categoria = await _context.CategoriasVeiculo
                .FindAsync(id);

            if (categoria == null)
                return false;

            _context.CategoriasVeiculo.Remove(categoria);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
