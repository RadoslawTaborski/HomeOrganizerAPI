using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using DtoOut = HomeOrganizerAPI.Helpers.DTO.Expenses;
using DtoIn = HomeOrganizerAPI.Helpers.DTO.CreateExpenses;
using ExpenseDetail = HomeOrganizerAPI.Helpers.DTO.ExpenseDetails;
using UserDto = HomeOrganizerAPI.Helpers.DTO.User;
using System.Collections.Generic;
using System.Linq;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExpensesController : BaseController<Expenses, Expenses, DtoOut, DtoIn>
    {
        private HomeOrganizerContext _context;
        public ExpensesController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService) : base(new ExpensesRepository(context, propertyMappingService))
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<DtoOut>>> Get([FromQuery] DefaultParameters resourceParameters)
        {
            return await BaseGet(resourceParameters);
        }

        protected async override Task<DtoOut> MapInToOut(DtoIn value)
        {
            var result = new DtoOut
            {
                Name = value.Name,
                GroupUuid = value.GroupUuid
            };

            var userMap = new Dictionary<byte[], float>();
            var totalCoefficient = 0f;
            if (value.FiftyFifty)
            {
                userMap = CreateDictionary(value.Recipients, value.GroupUuid);
                totalCoefficient = userMap.Sum(x => x.Value);
            }

            var details = new List<ExpenseDetail>();
            
            foreach (var user in value.Recipients)
            {
                var detail = new ExpenseDetail
                {
                    Payer = await _context.User.FindAsync(value.Payer),
                    Recipient = await _context.User.FindAsync(user)
                };
                if (value.FiftyFifty)
                {
                    detail.Value = value.Amount / value.Recipients.Count();
                } else
                {
                    detail.Value = value.Amount * ((decimal)userMap[user] / (decimal)totalCoefficient);
                }
                details.Add(detail);
            }
            result.ExpenseDetails = details.ToArray();
            return result;
        }

        private Dictionary<byte[], float> CreateDictionary(List<byte[]> users, byte[] groupUuid)
        {
            var result = new Dictionary<byte[], float>();
            foreach(var user in users)
            {
                var val = _context.ExpensesSettings.Where(s => s.UserGroups.UserUuid == user && s.UserGroups.GroupUuid == groupUuid).FirstOrDefault().Value;
                result.Add(user, val);
            }
            return result;
        }
    }


}

