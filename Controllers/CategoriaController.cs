
using LocacaoVeiculos.Models;
using LocacaoVeiculos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocacaoVeiculos.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObterTodosAsync();

            return View(categorias);
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaVeiculo categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            try
            {
                await _categoriaService.AdicionarAsync(categoria);

                TempData["Sucesso"] = "Categoria cadastrada com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(categoria);
            }
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CategoriaVeiculo categoria)
        {
            if (id != categoria.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(categoria);

            try
            {
                await _categoriaService.AtualizarAsync(categoria);

                TempData["Sucesso"] = "Categoria atualizada com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(categoria);
            }
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoriaService.ExcluirAsync(id);

                TempData["Sucesso"] = "Categoria excluída com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
