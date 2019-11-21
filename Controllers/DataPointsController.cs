using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomApi.Models;
using RandomApi.Helpers;

namespace RandomDataProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataPointsController : ControllerBase
    {
        private readonly RandomContext _context;

        public DataPointsController(RandomContext context)
        {
            _context = context;
        }

        // GET: api/DataPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataPoint>>> GetDataPoints()
        {
            return await _context.DataPoints.ToListAsync();
        }

        // GET: api/DataPoints/random
        [HttpGet("random")]
        public async Task<ActionResult<IEnumerable<DataPoint>>> GetRandomDataPoints()
        {
            Random random = new Random();
            return await _context.DataPoints.OrderByRandom().Take(random.Next(1, 15)).ToListAsync();
        }

        // GET: api/DataPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataPoint>> GetDataPoint(long id)
        {
            var dataPoint = await _context.DataPoints.FindAsync(id);

            if (dataPoint == null)
            {
                return NotFound();
            }

            return dataPoint;
        }

        // PUT: api/DataPoints/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataPoint(long id, DataPoint dataPoint)
        {
            if (id != dataPoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataPointExists(id))
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

        // POST: api/DataPoints
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataPoint>> PostDataPoint(DataPoint dataPoint)
        {
            _context.DataPoints.Add(dataPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDataPoint), new { id = dataPoint.Id }, dataPoint);
        }

        // DELETE: api/DataPoints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataPoint>> DeleteDataPoint(long id)
        {
            var dataPoint = await _context.DataPoints.FindAsync(id);
            if (dataPoint == null)
            {
                return NotFound();
            }

            _context.DataPoints.Remove(dataPoint);
            await _context.SaveChangesAsync();

            return dataPoint;
        }

        private bool DataPointExists(long id)
        {
            return _context.DataPoints.Any(e => e.Id == id);
        }
    }
}
