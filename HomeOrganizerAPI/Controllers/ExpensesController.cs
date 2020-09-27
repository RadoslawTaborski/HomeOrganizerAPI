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
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeOrganizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expenses, Expenses, ExpensesController.Dto>
    {
        public ExpensesController(HomeOrganizerContext context) : base(new ExpensesRepository(context))
        {
        }

        protected override Dto FromObject(Expenses obj) => Dto.FromObject(obj);

        protected override Expenses ToObject(Dto obj) => Dto.ToObject(obj);

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        public class Dto : Model
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int PayerId { get; set; }
            public int RecipientId { get; set; }


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

