using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmprendedoresController : Controller
{
    private readonly Empcontext _context;

    public EmprendedoresController(Empcontext context)
    {
        _context = context;
    }
 
    // GET: Emprendedores
    public async Task<IActionResult> Index()
    {
        return View(await _context.Emprendedores.ToListAsync());
    }

    // GET: Emprendedores/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var emprendedor = await _context.Emprendedores
            .FirstOrDefaultAsync(m => m.id_emprendedor == id);
        if (emprendedor == null)
        {
            return NotFound();
        }

        return View(emprendedor);
    }

    // GET: Emprendedores/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Emprendedores/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id_emprendedor,nombre,email,telefono,nombre_proyecto,descripcion_proyecto,sector,fecha_registro")] Emprendedor emprendedor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(emprendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(emprendedor);
    }

    // GET: Emprendedores/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var emprendedor = await _context.Emprendedores.FindAsync(id);
        if (emprendedor == null)
        {
            return NotFound();
        }
        return View(emprendedor);
    }

    // POST: Emprendedores/Edit/5
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
            return RedirectToAction(nameof(Index));
        }
        return View(emprendedor);
    }

    // GET: Emprendedores/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var emprendedor = await _context.Emprendedores
            .FirstOrDefaultAsync(m => m.id_emprendedor == id);
        if (emprendedor == null)
        {
            return NotFound();
        }

        return View(emprendedor);
    }

    // POST: Emprendedores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var emprendedor = await _context.Emprendedores.FindAsync(id);
        _context.Emprendedores.Remove(emprendedor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool EmprendedorExists(int id)
    {
        return _context.Emprendedores.Any(e => e.id_emprendedor == id);
    }

}
