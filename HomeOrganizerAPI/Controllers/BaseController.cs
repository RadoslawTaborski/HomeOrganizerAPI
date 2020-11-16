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
    public abstract class BaseController<T, V, OUT, IN> : Controller
        where T : Model
        where V : Model
        where OUT : DtoModel
        where IN : class, IDtoEntity
    {
        protected readonly Repository<T, V, OUT> _repo;
        protected Mapper<T, OUT> _mapperTOut;
        protected MapperOutIn<OUT, IN> _mapperOutIn;

        public BaseController(Repository<T, V, OUT> repository)
        {
            _repo = repository;
            _mapperTOut = new Mapper<T, OUT>();
            _mapperOutIn = new MapperOutIn<OUT, IN>();
        }

        public async Task<ActionResult<ResponseData<OUT>>> BaseGet(Parameters resourceParameters)
        {
            var (Collection, Lenght) = await _repo.Get(resourceParameters);
            var collection = Collection.Select(i => _mapperTOut.ToDto(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OUT>> BaseGet(string id)
        {
            var byteUuid = Guid.Parse(id).ToByteArray();
            var entity = await _repo.Get(byteUuid);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapperTOut.ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<T>> BasePost([FromBody] IN value)
        {
            OUT outModel = await MapInToOut(value);
            var added = await _repo.Add(_mapperTOut.FromDto(outModel));

            return CreatedAtAction(nameof(BaseGet), new { uuid = new Guid(added.Uuid).ToString(), version = "v1" }, _mapperTOut.ToDto(added));
        }

        protected async virtual Task<OUT> MapInToOut(IN value)
        {
            return _mapperOutIn.Transform(value);
        }

        [HttpPut]
        public async Task<ActionResult<OUT>> BasePut([FromBody] OUT value)
        {
            try
            {
                var added = await _repo.Update(_mapperTOut.FromDto(value));
                return CreatedAtAction(nameof(BaseGet), new { uuid = new Guid(added.Uuid).ToString(), version = "v1" }, _mapperTOut.ToDto(added));
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
        public async Task<ActionResult<OUT>> BaseDelete(string id)
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
