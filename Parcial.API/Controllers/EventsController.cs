using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Parcial.API.Helpers;
using Parcial.API.Data;
using Parcial.Shared.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Parcial.API.Controllers
{
    [ApiController]
    [Route("/api/eventsControl")]
    public class EventsController : ControllerBase
    {

        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.eventControl.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.eventControl
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id_Boleta.ToString().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Id_Boleta)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.eventControl.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id_Boleta.ToString().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("full")]
        public async Task<IActionResult> GetFullAsync()
        {
            return Ok(await _context.eventControl
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var eventControl = await _context.eventControl

                .FirstOrDefaultAsync(x => x.Id_Boleta == id);
            if (eventControl == null)
            {
                return NotFound();
            }
            return Ok(eventControl);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(EventsController eventControl)
        {
            try
            {
                _context.Update(eventControl);
                await _context.SaveChangesAsync();
                return Ok(eventControl);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un ticket con el mismo código");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var eventControl = await _context.eventControl.FirstOrDefaultAsync(x => x.Id_Boleta == id);
            if (eventControl == null)
            {
                return NotFound();
            }

            _context.Remove(eventControl);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
