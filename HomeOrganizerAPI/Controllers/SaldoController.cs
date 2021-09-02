using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

using Dto = HomeOrganizerAPI.Helpers.DTO.Saldo;

namespace HomeOrganizerAPI.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = "ApiReader")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SaldoController : Controller
    {
        private SaldoRepository _repo;
        private Mapper<Saldo, Dto> _mapper;

        public SaldoController(HomeOrganizerContext context, IPropertyMappingService propertyMappingService)
        {
            _repo = new SaldoRepository(context, propertyMappingService);
            _mapper = new Mapper<Saldo, Dto>();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] SaldoResourceParameters resourceParameters)
        {
            var (Collection, Lenght) = await _repo.Get(resourceParameters);
            var collection = Collection.Select(i => _mapper.ToDto(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
        }
    }
}

