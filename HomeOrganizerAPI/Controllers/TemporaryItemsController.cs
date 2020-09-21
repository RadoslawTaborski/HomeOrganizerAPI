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
    public class TemporaryItemsController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public TemporaryItemsController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get()
        {
            var data = await _context.TemporaryItem.ToArrayAsync();
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
        public async Task<ActionResult<TemporaryItem>> Get(int id)
        {
            var entity = await _context.Item.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<TemporaryItem>> Post([FromBody] TemporaryItem value)
        {
            _context.Item.Add(ToObject(value));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TemporaryItem), new { id = value.Id }, value);
        }

        [HttpPut]
        public async Task<ActionResult<TemporaryItem>> Put([FromBody] TemporaryItem value)
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
        public async Task<ActionResult<TemporaryItem>> Delete(int id)
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

        public static TemporaryItem FromObject(Item entity)
        {
            return new TemporaryItem
            {
                Id = entity.Id,
                Name = entity.Name,
                ShoppingListId = entity.ShoppingListId,
                Quantity = entity.Quantity,
                CategoryId = entity.CategoryId,
                Bought = entity.Bought,
                CreateTime = entity.CreateTime,
                UpdateTime = entity.UpdateTime,
                DeleteTime = entity.DeleteTime,
            };
        }

        public static Item ToObject(TemporaryItem dto)
        {
            return new Item
            {
                Id = dto.Id,
                Name = dto.Name,
                ShoppingListId = dto.ShoppingListId,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId,
                Bought = dto.Bought,
                CreateTime = dto.CreateTime,
                UpdateTime = dto.UpdateTime,
                DeleteTime = dto.DeleteTime
            };
        }
    }
}

