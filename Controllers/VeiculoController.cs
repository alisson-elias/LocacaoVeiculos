
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocacaoVeiculos.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoService _veiculoService;
        private readonly ICategoriaService _categoriaService;

        public VeiculoController(
            IVeiculoService veiculoService,
            ICategoriaService categoriaService)
        {
            _veiculoService = veiculoService;
            _categoriaService = categoriaService;
        }

        // GET: Veiculo
        public async Task<IActionResult> Index()
        {
            var veiculos = await _veiculoService.ObterTodosAsync();
            return View(veiculos);
        }

        // GET: Veiculo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var veiculo = await _veiculoService.ObterPorIdAsync(id);

            if (veiculo == null)
                return NotFound();

            return View(veiculo);
        }

        // GET: Veiculo/Create
        public async Task<IActionResult> Create()
        {
            await CarregarCategorias();
            return View();
        }

        // POST: Veiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCategorias(veiculo.CategoriaVeiculoId);
                return View(veiculo);
            }

            try
            {
                await _veiculoService.AdicionarAsync(veiculo);

                TempData["Sucesso"] = "Veículo cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CarregarCategorias(veiculo.CategoriaVeiculoId);
                return View(veiculo);
            }
        }

        // GET: Veiculo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var veiculo = await _veiculoService.ObterPorIdAsync(id);

            if (veiculo == null)
                return NotFound();

            await CarregarCategorias(veiculo.CategoriaVeiculoId);
            return View(veiculo);
        }

        // POST: Veiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarCategorias(veiculo.CategoriaVeiculoId);
                return View(veiculo);
            }

            try
            {
                await _veiculoService.AtualizarAsync(veiculo);

                TempData["Sucesso"] = "Veículo atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CarregarCategorias(veiculo.CategoriaVeiculoId);
                return View(veiculo);
            }
        }

        // GET: Veiculo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var veiculo = await _veiculoService.ObterPorIdAsync(id);

            if (veiculo == null)
                return NotFound();

            return View(veiculo);
        }

        // POST: Veiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _veiculoService.ExcluirAsync(id);

                TempData["Sucesso"] = "Veículo excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarCategorias(int? categoriaSelecionada = null)
        {
            var categorias = await _categoriaService.ObterTodosAsync();

            ViewBag.Categorias = new SelectList(
                categorias,
                "Id",
                "Nome",
                categoriaSelecionada
            );
        }
    }
}
