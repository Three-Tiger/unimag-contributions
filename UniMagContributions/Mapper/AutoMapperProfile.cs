using AutoMapper;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Dto.Auth;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Models;

namespace UniMagContributions.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<CreateFacultyDto, Faculty>();
            CreateMap<UpdateFacultyDto, Faculty>();
            CreateMap<Faculty, FacultyDto>();

			CreateMap<CreateAnnualMagazineDto, AnnualMagazine>();
			CreateMap<UpdateAnnualMagazineDto, AnnualMagazine>();
			CreateMap<AnnualMagazine, AnnualMagazineDto>();
		}
    }
}
