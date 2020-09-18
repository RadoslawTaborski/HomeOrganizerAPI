using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public ExpensesController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dto>>> Get()
        {
            return Ok(await _context.Expenses.Select(i => Dto.FromObject(i)).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dto>> Get(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Dto.FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<Dto>> Post([FromBody] Dto value)
        {
            _context.Expenses.Add(Dto.ToObject(value));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Expenses), new { id = value.Id }, value);
        }

        [HttpPut]
        public async Task<ActionResult<Dto>> Put([FromBody] Dto value)
        {
            value.UpdateTime = DateTimeOffset.Now;
            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EntityExists(value.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dto>> Delete(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.UpdateTime = DateTimeOffset.Now;
            entity.DeleteTime = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> EntityExists(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            return entity != null;
        }

        public class Dto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int PayerId { get; set; }
            public int RecipientId { get; set; }
            public DateTimeOffset CreateTime { get; set; }
            public DateTimeOffset? UpdateTime { get; set; }
            public DateTimeOffset? DeleteTime { get; set; }


            public static Dto FromObject(Expenses entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Value = entity.Value,
                    PayerId = entity.PayerId,
                    RecipientId = entity.RecipientId,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static Expenses ToObject(Dto dto)
            {
                return new Expenses
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Value = dto.Value,
                    PayerId = dto.PayerId,
                    RecipientId = dto.RecipientId,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}

