using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace CosmicGameAPI.Utility.Mapper
{
    public class BaseAutoMapper<Source, Destination> : IBaseAutoMapper<Source, Destination>
    {
        List<Destination> list = new();

        public List<Destination> ToList(IEnumerable<Source> sources)
        {
            list.Clear();
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
            IMapper iMapper = config.CreateMapper();
            foreach (Source source in sources)
            {
                list.Add(iMapper.Map<Source, Destination>(source));
            }
            return list;
        }

        public Destination ToSingle(Source source)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<Source, Destination>(source);
        }
    }
}
