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
        return View();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
