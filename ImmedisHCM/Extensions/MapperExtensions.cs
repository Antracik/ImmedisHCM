using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(
                this IMapper mapper, TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }
    }
}
