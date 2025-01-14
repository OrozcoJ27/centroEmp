using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using centroEm.Models;
using Microsoft.EntityFrameworkCore;

namespace centroEm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Empcontext _context;
    public HomeController(ILogger<HomeController> logger, Empcontext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var emprendedores = await _context.Emprendedores.ToListAsync();  // Obtienes los datos
        return View(emprendedores);  // Pasa los datos a la vista
    }
    // Acción para mostrar el formulario de creación
    public IActionResult Create()
    {
        return View(new Emprendedor());
    }

    // Acción para manejar el POST del formulario de creación
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("nombre,email,telefono,nombre_proyecto,descripcion_proyecto,sector,fecha_registro")] Emprendedor emprendedor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(emprendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // Redirige al Index después de la creación
        }
        return View(emprendedor);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    // GET: Emprendedores/Edit/1
    public async Task<IActionResult> Edit(int id)
    {


        var emprendedor = await _context.Emprendedores.FindAsync(id);
        if (emprendedor == null)
        {
            return NotFound();
        }
        return View(emprendedor);
    }


    // Acción POST: Procesar la actualización del emprendedor
    // POST: Emprendedores/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("id_emprendedor,nombre,email,telefono,nombre_proyecto,descripcion_proyecto,sector,fecha_registro")] Emprendedor emprendedor)
    {
        if (id != emprendedor.id_emprendedor)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(emprendedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprendedorExists(emprendedor.id_emprendedor))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index)); // Redirige al Index después de la actualización
        }
        return View(emprendedor); // Si no es válido, vuelve a la vista de edición
    }


    private bool EmprendedorExists(int id)
    {
        return _context.Emprendedores.Any(e => e.id_emprendedor == id);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var emprendedor = await _context.Emprendedores.FindAsync(id);
        if (emprendedor == null)
        {
            return NotFound();
        }

        return View(emprendedor);  // Pasa el emprendedor a la vista para que se muestre la confirmación
    }

    // POST: Emprendedores/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var emprendedor = await _context.Emprendedores.FindAsync(id);
        if (emprendedor == null)
        {
            return NotFound();
        }

        _context.Emprendedores.Remove(emprendedor);  // Elimina el emprendedor de la base de datos
        await _context.SaveChangesAsync();  // Guarda los cambios

        return RedirectToAction(nameof(Index));  // Redirige al Index después de la eliminación
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }





}
