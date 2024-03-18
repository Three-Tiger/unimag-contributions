using AutoMapper;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Dto.Auth;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Dto.User;
using UniMagContributions.Dto.Feedback;
using UniMagContributions.Models;
using UniMagContributions.Dto.ImageDetail;

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

            CreateMap<CreateContributionDto, Contribution>();
            CreateMap<UpdateContributionDto, Contribution>();
            CreateMap<Contribution, ContributionDto>();

            CreateMap<FileDetails, FileDetailDto>();
            CreateMap<ImageDetails, ImageDetailDto>();
            CreateMap<Role, RoleDto>();

            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            /*CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());*/

            CreateMap<CreateContributionDto, Contribution>();
            CreateMap<UpdateContributionDto, Contribution>();
            CreateMap<Contribution, ContributionDto>();

            CreateMap<CreateFeedbackDto, Feedback>();
            CreateMap<UpdateFeedbackDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();
        }
    }
}
