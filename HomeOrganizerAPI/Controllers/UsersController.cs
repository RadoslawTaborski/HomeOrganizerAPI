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
    public class UsersController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public UsersController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get()
        {
            var data = await _context.User.Select(i => Dto.FromObject(i)).ToArrayAsync();
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
            var entity = await _context.User.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Dto.FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<Dto>> Post([FromBody] Dto value)
        {
            _context.User.Add(Dto.ToObject(value));
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(User), new { id = value.Id }, value);
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
            var entity = await _context.User.FindAsync(id);
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
            var entity = await _context.User.FindAsync(id);
            return entity != null;
        }

        public class Dto
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public DateTimeOffset CreateTime { get; set; }
            public DateTimeOffset? UpdateTime { get; set; }
            public DateTimeOffset? DeleteTime { get; set; }

            public static Dto FromObject(User entity)
            {
                return new Dto
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                    CreateTime = entity.CreateTime,
                    UpdateTime = entity.UpdateTime,
                    DeleteTime = entity.DeleteTime,
                };
            }

            public static User ToObject(Dto dto)
            {
                return new User
                {
                    Id = dto.Id,
                    Username = dto.Username,
                    Email = dto.Email,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    DeleteTime = dto.DeleteTime
                };
            }
        }
    }
}