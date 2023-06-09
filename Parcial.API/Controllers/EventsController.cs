﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Parcial.API.Helpers;
using Parcial.API.Data;
using Parcial.Shared.DTOs;

namespace Parcial.API.Controllers
{
    [ApiController]
    [Route("/api/systemControl")]
    public class systemController : ControllerBase
    {

        private readonly DataContext _context;

        public systemController(DataContext context)
        {
            _context = context;
        }
     
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
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Id)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.eventControl.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
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

                .FirstOrDefaultAsync(x => x.Id == id);
            if (eventControl == null)
            {
                return NotFound();
            }
            return Ok(eventControl);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(systemController ticket)
        {
            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                return Ok(ticket);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
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
            var eventControl = await _context.eventControl.FirstOrDefaultAsync(x => x.Id == id);
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
