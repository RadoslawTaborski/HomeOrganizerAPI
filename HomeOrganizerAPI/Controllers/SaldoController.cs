using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SaldoController : BaseController<Saldo, Saldo, Saldo>
    {
        public SaldoController(HomeOrganizerContext context) : base(new SaldoRepository(context))
        {
        }

        protected override Saldo FromObject(Saldo obj) => obj;

        protected override Saldo ToObject(Saldo obj) => obj;

        [HttpGet]
        public async Task<ActionResult<ResponseData<Saldo>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }
    }
}

