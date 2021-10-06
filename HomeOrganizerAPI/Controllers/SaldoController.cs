using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using HomeOrganizerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
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
        private IPermissionChecker _checker;

        public SaldoController(SaldoRepository repo, IPermissionChecker checker)
        {
            _repo = repo;
            _mapper = new Mapper<Saldo, Dto>();
            _checker = checker;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<Dto>>> Get([FromQuery] SaldoResourceParameters resourceParameters)
        {
            if (!User.IsInRole("admin") && ! await HasAccessGet(User, resourceParameters))
            {
                return Forbid();
            }
            var (Collection, Lenght) = await _repo.Get(resourceParameters);
            var collection = Collection.Select(i => _mapper.ToDto(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
        }

        private Task<bool> HasAccessGet(ClaimsPrincipal user, SaldoResourceParameters resourceParameters)
        {
            return _checker.IsAtLeast(user, Guid.Parse(resourceParameters.GroupUuid).ToByteArray(), GroupRole.Member);
        }
    }
}

