using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers.DTO
{
    public class MapperOutIn<OUT, IN>
        where OUT : class, IDtoEntity
        where IN : class, IDtoEntity
    {
        private IMapper _mapper;

        public MapperOutIn()
        {
            _mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public OUT Transform(IN entity)
        {
            return _mapper.Map<OUT>(entity);
        }
    }
}
