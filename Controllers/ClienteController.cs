
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocacaoVeiculos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObterTodosAsync();

            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _clienteService.AdicionarAsync(cliente);

                TempData["Sucesso"] = "Cliente cadastrado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(cliente);
            }
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _clienteService.AtualizarAsync(cliente);

                TempData["Sucesso"] = "Cliente atualizado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(cliente);
            }
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _clienteService.ExcluirAsync(id);

                TempData["Sucesso"] = "Cliente excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
