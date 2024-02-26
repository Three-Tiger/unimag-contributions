using AutoMapper;
using UniMagContributions.Dto.Auth;
using UniMagContributions.Models;

namespace UniMagContributions.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
