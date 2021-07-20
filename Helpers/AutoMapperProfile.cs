using AutoMapper;
using Dksh.ePOD.Entities;
using Dksh.ePOD.Models;

namespace Dksh.ePOD.Helpers
{
    /// <summary>
    /// A class that setup the automapper mapping configuration.
    /// More detail can be obtained from https://code-maze.com/automapper-net-core/
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddressTypeBO, AddressTypeModel>();
        }
    }
}
