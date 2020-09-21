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
    public class SaldoController : Controller
    {
        private readonly HomeOrganizerContext _context;

        public SaldoController(HomeOrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get()
        {
            var data = await _context.Saldo.ToArrayAsync();
            var response = new ResponseData
            {
                data = data,
                total = data.Length,
                message = "ok",
                error = ""
            };
            return Ok(response);
        }
    }
}

