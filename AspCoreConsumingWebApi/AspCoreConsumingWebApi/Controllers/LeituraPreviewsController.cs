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
    [Route("api/LeituraPreviews")]
    public class LeituraPreviewsController : Controller
    {
        private readonly PlayItContext _context;

        public LeituraPreviewsController(PlayItContext context)
        {
            _context = context;
        }

        // GET: api/LeituraPreviews
        [HttpGet]
        public IEnumerable<LeituraPreviewModel> GetLeituraPreviews()
        {
            return _context.LeituraPreviews;
        }

        // GET: api/LeituraPreviews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeituraPreviewModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leituraPreviewModel = await _context.LeituraPreviews.SingleOrDefaultAsync(m => m.ltp_codigo == id);

            if (leituraPreviewModel == null)
            {
                return NotFound();
            }

            return Ok(leituraPreviewModel);
        }

        // PUT: api/LeituraPreviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeituraPreviewModel([FromRoute] long id, [FromBody] LeituraPreviewModel leituraPreviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leituraPreviewModel.ltp_codigo)
            {
                return BadRequest();
            }

            _context.Entry(leituraPreviewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeituraPreviewModelExists(id))
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

        // POST: api/LeituraPreviews
        [HttpPost]
        public async Task<IActionResult> PostLeituraPreviewModel([FromBody] LeituraPreviewModel leituraPreviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LeituraPreviews.Add(leituraPreviewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeituraPreviewModel", new { id = leituraPreviewModel.ltp_codigo }, leituraPreviewModel);
        }

        // DELETE: api/LeituraPreviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeituraPreviewModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leituraPreviewModel = await _context.LeituraPreviews.SingleOrDefaultAsync(m => m.ltp_codigo == id);
            if (leituraPreviewModel == null)
            {
                return NotFound();
            }

            _context.LeituraPreviews.Remove(leituraPreviewModel);
            await _context.SaveChangesAsync();

            return Ok(leituraPreviewModel);
        }

        private bool LeituraPreviewModelExists(long id)
        {
            return _context.LeituraPreviews.Any(e => e.ltp_codigo == id);
        }
    }
}