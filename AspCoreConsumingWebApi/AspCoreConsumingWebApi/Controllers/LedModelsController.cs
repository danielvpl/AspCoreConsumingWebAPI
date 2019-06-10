using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCoreModels;

namespace AspCoreConsumingWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/LedModels")]
    public class LedModelsController : Controller
    {
        private readonly ModelContext _context;

        public LedModelsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/LedModels
        [HttpGet]
        public IEnumerable<LedModel> GetLedModels()
        {
            return _context.LedModels.OrderBy(m => m.desc);
        }

        // GET: api/LedModels
        [HttpGet("{id}")]
        public IEnumerable<LedModel> GetLedModels(string id)
        {
            return _context.LedModels.Where(m => m.id == id);
        }

        // GET: api/LedModels/5
        [HttpGet("{id}/{gpio}")]
        public async Task<IActionResult> GetLedModel([FromRoute] string id, int gpio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ledModel = await _context.LedModels.SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);

            if (ledModel == null)
            {
                return NotFound();
            }

            return Ok(ledModel);
        }

        // PUT: api/LedModels/5/2
        [HttpPut("{id}/{gpio}")]
        public async Task<IActionResult> PutLedModel([FromRoute] string id, int gpio, [FromBody] LedModel ledModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ledModel.id || gpio != ledModel.gpio)
            {
                return BadRequest();
            }

            _context.Entry(ledModel).State = EntityState.Modified;
            if (string.IsNullOrEmpty(ledModel.desc))
                _context.Entry(ledModel).Property(p => p.desc).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LedModelExists(id, gpio))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LedModels
        [HttpPost]
        public async Task<IActionResult> PostLedModel([FromBody] LedModel ledModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LedModels.Add(ledModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LedModelExists(ledModel.id, ledModel.gpio))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLedModel", new { id = ledModel.id, gpio = ledModel.gpio }, ledModel);
        }

        // DELETE: api/LedModels/5
        [HttpDelete("{id}/{gpio}")]
        public async Task<IActionResult> DeleteLedModel([FromRoute] string id, int gpio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ledModel = await _context.LedModels.SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
            if (ledModel == null)
            {
                return NotFound();
            }

            _context.LedModels.Remove(ledModel);
            await _context.SaveChangesAsync();

            return Ok(ledModel);
        }

        private bool LedModelExists(string id, int gpio)
        {
            return _context.LedModels.Any(e => e.id == id && e.gpio == gpio);
        }
    }
}