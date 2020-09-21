using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public ItemsController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get()
        {
            var data = await _context.Item.Select(i => Dto.FromObject(i)).ToArrayAsync();
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
        public async Task<ActionResult<Dto>> Get(int id)
        {
            var entity = await _context.Item.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Dto.FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<Dto>> Post([FromBody] Dto value)
        {
            _context.Item.Add(Dto.ToObject(value));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Item), new { id = value.Id }, value);
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

        public class Dto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ShoppingListId { get; set; }
            public int? StateId { get; set; }
            public string Quantity { get; set; }
            public int CategoryId { get; set; }
            public DateTimeOffset? Bought { get; set; }
            public DateTimeOffset CreateTime { get; set; }
            public DateTimeOffset? UpdateTime { get; set; }
            public DateTimeOffset? DeleteTime { get; set; }

            public static Dto FromObject(Item entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ShoppingListId = entity.ShoppingListId,
                    StateId = entity.StateId,
                    Quantity = entity.Quantity,
                    CategoryId = entity.CategoryId,
                    Bought = entity.Bought,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static Item ToObject(Dto dto)
            {
                return new Item
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    ShoppingListId = dto.ShoppingListId,
                    StateId = dto.StateId,
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
}
