
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocacaoVeiculos.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly ILocacaoService _locacaoService;
        private readonly IVeiculoService _veiculoService;
        private readonly IClienteService _clienteService;

        public LocacaoController(
            ILocacaoService locacaoService,
            IVeiculoService veiculoService,
            IClienteService clienteService)
        {
            _locacaoService = locacaoService;
            _veiculoService = veiculoService;
            _clienteService = clienteService;
        }

        // GET: Locacao
        public async Task<IActionResult> Index()
        {
            var locacoes = await _locacaoService.ObterTodosAsync();

            return View(locacoes);
        }

        // GET: Locacao/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var locacao = await _locacaoService.ObterPorIdAsync(id);

            if (locacao == null)
                return NotFound();

            return View(locacao);
        }

        // GET: Locacao/Create
        public async Task<IActionResult> Create()
        {
            await CarregarDados();
            return View();
        }

        // POST: Locacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Locacao locacao)
        {
            ModelState.Remove(nameof(Locacao.ValorTotal));
            if (!ModelState.IsValid)
            {
                await CarregarDados(locacao.VeiculoId, locacao.ClienteId);
                return View(locacao);
            }

            try
            {
                await _locacaoService.AdicionarAsync(locacao);

                TempData["Sucesso"] = "Locação registrada com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                await CarregarDados(locacao.VeiculoId, locacao.ClienteId);

                return View(locacao);
            }
        }

        // GET: Locacao/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var locacao = await _locacaoService.ObterPorIdAsync(id);

            if (locacao == null)
                return NotFound();

            await CarregarDados(locacao.VeiculoId, locacao.ClienteId);

            return View(locacao);
        }

        // POST: Locacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Locacao locacao)
        {
            ModelState.Remove(nameof(Locacao.ValorTotal));
            if (id != locacao.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarDados(locacao.VeiculoId, locacao.ClienteId);
                return View(locacao);
            }

            try
            {
                await _locacaoService.AtualizarAsync(locacao);

                TempData["Sucesso"] = "Locação atualizada com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                await CarregarDados(locacao.VeiculoId, locacao.ClienteId);

                return View(locacao);
            }
        }

        // GET: Locacao/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var locacao = await _locacaoService.ObterPorIdAsync(id);

            if (locacao == null)
                return NotFound();

            return View(locacao);
        }

        // POST: Locacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _locacaoService.ExcluirAsync(id);

                TempData["Sucesso"] = "Locação excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Locacao/RegistrarDevolucao/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarDevolucao(int id)
        {
            try
            {
                await _locacaoService.RegistrarDevolucaoAsync(id);

                TempData["Sucesso"] = "Devolução registrada com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        private async Task CarregarDados(
            int? veiculoSelecionado = null,
            int? clienteSelecionado = null)
        {
            var veiculos = await _veiculoService.ObterTodosAsync();

            // No cadastro, apresenta apenas veículos disponíveis.
            // Na edição, mantém também o veículo já selecionado.
            var veiculosDisponiveis = veiculos
                .Where(v => v.Disponivel ||
                            v.Id == veiculoSelecionado)
                .ToList();

            var clientes = await _clienteService.ObterTodosAsync();

            ViewBag.Veiculos = new SelectList(
                veiculosDisponiveis,
                "Id",
                "Modelo",
                veiculoSelecionado
            );

            ViewBag.Clientes = new SelectList(
                clientes,
                "Id",
                "Nome",
                clienteSelecionado
            );
        }
    }
}
