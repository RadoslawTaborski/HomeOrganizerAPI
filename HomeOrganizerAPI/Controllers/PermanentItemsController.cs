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
    public class PermanentItemsController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public PermanentItemsController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseData>>> Get()
        {
            var data = await _context.PermanentItem.ToArrayAsync();
            var response = new ResponseData
            {
                data = data,
                total = data.Length,
                message = "ok",
                error = ""
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermanentItem>> Get(int id)
        {
            var entity = await _context.Item.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<PermanentItem>> Post([FromBody] PermanentItem value)
        {
            _context.Item.Add(ToObject(value));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PermanentItem), new { id = value.Id }, value);
        }

        [HttpPut]
        public async Task<ActionResult<PermanentItem>> Put([FromBody] PermanentItem value)
        {
            value.UpdateTime = DateTimeOffset.Now;
            _context.Entry(ToObject(value)).State = EntityState.Modified;

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
        public async Task<ActionResult<PermanentItem>> Delete(int id)
        {
            var entity = await _context.Item.FindAsync(id);
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
            var entity = await _context.Item.FindAsync(id);
            return entity != null;
        }

        public static PermanentItem FromObject(Item entity)
        {
            return new PermanentItem
            {
                Id = entity.Id,
                Name = entity.Name,
                StateId = entity.StateId,
                CategoryId = entity.CategoryId,
                CreateTime = entity.CreateTime,
                UpdateTime = entity.UpdateTime,
                DeleteTime = entity.DeleteTime,
            };
        }

        public static Item ToObject(PermanentItem dto)
        {
            return new Item
            {
                Id = dto.Id,
                Name = dto.Name,
                StateId = dto.StateId,
                CategoryId = dto.CategoryId,
                CreateTime = dto.CreateTime,
                UpdateTime = dto.UpdateTime,
                DeleteTime = dto.DeleteTime
            };
        }
    }
}
