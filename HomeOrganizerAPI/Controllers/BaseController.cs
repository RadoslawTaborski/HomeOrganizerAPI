using AutoMapper;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Helpers.DTO;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Controllers
{
    public abstract class BaseController<T, V, DTO> : Controller
        where T : Model
        where V : Model
        where DTO : DtoModel
    {
        protected readonly Repository<T, V, DTO> _repo;
        protected Mapper<T, DTO> _mapper;

        public BaseController(Repository<T, V, DTO> repository)
        {
            _repo = repository;
            _mapper = new Mapper<T, DTO>();
        }

        public async Task<ActionResult<ResponseData<DTO>>> BaseGet(Parameters resourceParameters)
        {
            var (Collection, Lenght) = await _repo.Get(resourceParameters);
            var collection = Collection.Select(i => _mapper.ToDto(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO>> BaseGet(string id)
        {
            var byteUuid = Guid.Parse(id).ToByteArray();
            var entity = await _repo.Get(byteUuid);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<DTO>> BasePost([FromBody] DTO value)
        {
            var added = await _repo.Add(_mapper.FromDto(value));

            return CreatedAtAction(nameof(BaseGet), new { uuid = new Guid(added.Uuid).ToString(), version = "v1" }, _mapper.ToDto(added));
        }

        [HttpPut]
        public async Task<ActionResult<DTO>> BasePut([FromBody] DTO value)
        {
            try
            {
                var added = await _repo.Update(_mapper.FromDto(value));
                return CreatedAtAction(nameof(BaseGet), new { uuid = new Guid(added.Uuid).ToString(), version = "v1" }, _mapper.ToDto(added));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.Exists(value.Uuid))
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
        public async Task<ActionResult<DTO>> BaseDelete(string id)
        {
            var byteUuid = Guid.Parse(id).ToByteArray();
            var deleted = await _repo.DeleteItem(byteUuid);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
