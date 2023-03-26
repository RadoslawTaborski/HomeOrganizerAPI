using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Helpers.DTO;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Controllers;

public abstract class BaseController<T, V, DTO, P> : Controller
    where T : Model
    where V : Model
    where DTO : DtoModel
    where P: Parameters
{
    protected readonly Repository<T, V, DTO> _repo;
    protected Mapper<T, DTO> _mapper;

    public BaseController(Repository<T, V, DTO> repository)
    {
        _repo = repository;
        _mapper = new Mapper<T, DTO>();
    }

    protected virtual Task<bool> HasAccessGet(ClaimsPrincipal user, P parameters)
    {
        return Task.FromResult(false);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseData<DTO>>> BaseGet([FromQuery] P resourceParameters)
    {
        if (!User.IsInRole("admin") && ! await HasAccessGet(User, resourceParameters))
        {
            return Forbid();
        }
        var (Collection, Lenght) = await _repo.Get(resourceParameters);
        var collection = Collection.Select(i => _mapper.ToDto(i)).ToArray();
        return Ok(ControllerHelper.GenerateResponse(collection, Lenght));
    }

    protected virtual Task<bool> HasAccessGet(ClaimsPrincipal user, T entity)
    {
        return Task.FromResult(false);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<DTO>> BaseGet(string id)
    {
        var byteUuid = Guid.Parse(id).ToByteArray();
        var entity = await _repo.Get(byteUuid);
        if (!User.IsInRole("admin") && ! await HasAccessGet(User, entity))
        {
            return Forbid();
        }
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.ToDto(entity));
    }

    protected virtual Task<bool> HasAccessPost(ClaimsPrincipal user, DTO entity)
    {
        return Task.FromResult(false);
    }

    [HttpPost]
    public async Task<ActionResult<DTO>> BasePost([FromBody] DTO value)
    {
        if (!User.IsInRole("admin") && ! await HasAccessPost(User, value))
        {
            return Forbid();
        }
        var added = await _repo.Add(_mapper.FromDto(value));

        return CreatedAtAction(nameof(BaseGet), new { uuid = new Guid(added.Uuid).ToString(), version = "v1" }, _mapper.ToDto(added));
    }

    protected virtual Task<bool> HasAccessPut(ClaimsPrincipal user, DTO entity)
    {
        return Task.FromResult(false);
    }

    [HttpPut]
    public async Task<ActionResult<DTO>> BasePut([FromBody] DTO value)
    {
        try
        {
            if (!User.IsInRole("admin") && ! await HasAccessPut(User, value))
            {
                return Forbid();
            }
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

    protected virtual Task<bool> HasAccessDelete(ClaimsPrincipal user, T entity)
    {
        return Task.FromResult(false);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DTO>> BaseDelete(string id)
    {
        var byteUuid = Guid.Parse(id).ToByteArray();
        var entity = await _repo.Get(byteUuid);
        if (entity == null)
        {
            throw new InvalidOperationException("entity not found");
        }
        if (!User.IsInRole("admin") && ! await HasAccessDelete(User, entity))
        {
            return Forbid();
        }
        var deleted = await _repo.DeleteItem(entity);
        if (deleted)
        {
            return Ok();
        }
        return NotFound();
    }
}
