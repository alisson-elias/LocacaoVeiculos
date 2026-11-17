
using Microsoft.EntityFrameworkCore;
using LocacaoVeiculos.Data;
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Repositories.Interfaces;

namespace LocacaoVeiculos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Locacoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
