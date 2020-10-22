using AutoMapper;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Helpers.DTO;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Controllers
{
    public abstract class BaseController<T, V, DTO> : Controller
        where T : Model
        where V : Model
        where DTO : DtoModel
    {
        private readonly Repository<T, V, DTO> _repo;
        protected IMapper _mapper;

        public BaseController(Repository<T, V, DTO> repository)
        {
            _repo = repository;
            _mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public async Task<ActionResult<ResponseData<DTO>>> BaseGet(Parameters resourceParameters)
        {
            var (Collection, Lenght) = await _repo.Get(resourceParameters);
            var collection = Collection.Select(i => ToDto(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO>> BaseGet(int id)
        {
            var entity = await _repo.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<DTO>> BasePost([FromBody] DTO value)
        {
            var added = await _repo.Add(FromDto(value));

            return CreatedAtAction(nameof(BaseGet), new { id = added.Id, version = "v1" }, added);
        }

        [HttpPut]
        public async Task<ActionResult<DTO>> BasePut([FromBody] DTO value)
        {
            try
            {
                var added = await _repo.Update(FromDto(value));
                return CreatedAtAction(nameof(BaseGet), new { id = added.Id, version = "v1" }, added);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.Exists(value.Id))
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
        public async Task<ActionResult<DTO>> BaseDelete(int id)
        {
            var deleted = await _repo.DeleteItem(id);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }

        protected DTO ToDto(T entity)
        {
            return _mapper.Map<DTO>(entity);
        }

        protected T FromDto(DTO dto)
        {
            return _mapper.Map<T>(dto);
        }
    }
}
