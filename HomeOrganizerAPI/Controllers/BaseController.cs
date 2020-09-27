﻿using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Models;
using HomeOrganizerAPI.Repositories;
using HomeOrganizerAPI.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Controllers
{
    public abstract class BaseController<T,V, DTO> : Controller
        where T: Model
        where V: Model
        where DTO: Model
    {
        private readonly Repository<T,V> _repo;

        public BaseController(Repository<T, V> repository)
        {
            _repo = repository;
        }

        protected abstract DTO FromObject(T obj);
        protected abstract T ToObject(DTO obj);

        public async Task<ActionResult<ResponseData<DTO>>> BaseGet(Parameters resourceParameters)
        {
            var data = (await _repo.Get(resourceParameters)).Select(i => FromObject(i)).ToArray();
            return Ok(ControllerHelper.GenerateResponse(data, data.Length));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO>> BaseGet(int id)
        {
            var entity = await _repo.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(FromObject(entity));
        }

        [HttpPost]
        public async Task<ActionResult<DTO>> BasePost([FromBody] DTO value)
        {
            await _repo.Add(ToObject(value));

            return CreatedAtAction(nameof(T), new { id = value.Id }, value);
        }

        [HttpPut]
        public async Task<ActionResult<DTO>> BasePut([FromBody] DTO value)
        {
            try
            {
                await _repo.Update(ToObject(value));
                return Ok();
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
    }
}
