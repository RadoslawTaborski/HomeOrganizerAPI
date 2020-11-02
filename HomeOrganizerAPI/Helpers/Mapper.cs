using AutoMapper;
using HomeOrganizerAPI.Helpers;
using HomeOrganizerAPI.Helpers.DTO;
using HomeOrganizerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers
{
    public class Mapper<T, DTO>
        where T : class, IEntity
        where DTO : class, IDtoEntity
    {
        private IMapper _mapper;

        public Mapper()
        {
            _mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public DTO ToDto(T entity)
        {
            return _mapper.Map<DTO>(entity);
        }

        public T FromDto(DTO dto)
        {
            return _mapper.Map<T>(dto);
        }
    }
}
