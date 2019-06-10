using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCoreModels;

namespace AspCoreConsumingWebApi.Controllers
{
    public class LedModelController : Controller
    {
        private readonly ModelContext _context;

        public LedModelController(ModelContext context)
        {
            _context = context;
        }

        // GET: LedModel
        public async Task<IActionResult> Index()
        {
            return View(await _context.LedModels.OrderBy(m => m.desc).ToListAsync());
        }

        // GET: LedModel/Details/PLSC-84-0D-8E-81-D5-3C/5
        public async Task<IActionResult> Details(string id, int gpio)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledModel = await _context.LedModels
                .SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
            if (ledModel == null)
            {
                return NotFound();
            }

            return View(ledModel);
        }

        // GET: LedModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LedModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,gpio,status,desc")] LedModel ledModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ledModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ledModel);
        }

        // GET: LedModel/Edit/5
        public async Task<IActionResult> Edit(string id, int gpio)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledModel = await _context.LedModels.SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
            if (ledModel == null)
            {
                return NotFound();
            }
            return View(ledModel);
        }

        // POST: LedModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,gpio,status,desc")] LedModel ledModel)
        {
            if (id != ledModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ledModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LedModelExists(ledModel.id, ledModel.gpio))
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
            return View(ledModel);
        }

        // GET: LedModel/Delete/5
        public async Task<IActionResult> Delete(string id, int gpio)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ledModel = await _context.LedModels
                .SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
            if (ledModel == null)
            {
                return NotFound();
            }

            return View(ledModel);
        }

        // POST: LedModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, int gpio)
        {
            var ledModel = await _context.LedModels.SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
            _context.LedModels.Remove(ledModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LedModelExists(string id, int gpio)
        {
            return _context.LedModels.Any(e => e.id == id && e.gpio == gpio);
        }
    }
}
