using AutoMapper;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class AnnualMagazineService : IAnnualMagazineService
    {
        private readonly IMapper _mapper;
        private readonly IAnnualMagazineRepository _annualMagazineRepository;

        public AnnualMagazineService(IMapper mapper, IAnnualMagazineRepository annualMagazineRepository)
        {
            _mapper = mapper;
            _annualMagazineRepository = annualMagazineRepository;
        }
        public AnnualMagazineDto AddAnnualMagazine(CreateAnnualMagazineDto createAnnualMagazine)
        {
            AnnualMagazine annualMagazine = _annualMagazineRepository.GetAnnualMagazineByAcademicYear(createAnnualMagazine.AcademicYear);

            if (annualMagazine != null)
            {
                throw new ConflictException("Annual Magazine already exists");
            }

            annualMagazine = _mapper.Map<AnnualMagazine>(createAnnualMagazine);
            _annualMagazineRepository.CreateAnnualMagazine(annualMagazine);

            return _mapper.Map<AnnualMagazineDto>(annualMagazine);
        }

        public string DeleteAnnualMagazine(Guid id)
        {
            AnnualMagazine annualMagazine = _annualMagazineRepository.GetAnnualMagazineById(id) ?? throw new NotFoundException("Annual Magazine does not exists");
            _annualMagazineRepository.DeleteAnnualMagazine(annualMagazine);

            return "Delete successful";
        }

        public List<AnnualMagazineDto> GetAllAnnualMagazine()
        {
            List<AnnualMagazine> annualMagazineList = _annualMagazineRepository.GetAllAnnualMagazine();
            return _mapper.Map<List<AnnualMagazineDto>>(annualMagazineList);
        }

        public AnnualMagazineDto GetAnnualMagazineById(Guid id)
        {
            AnnualMagazine annualMagazine = _annualMagazineRepository.GetAnnualMagazineById(id) ?? throw new NotFoundException("Annual Magazine does not exists"); ;

            return _mapper.Map<AnnualMagazineDto>(annualMagazine);
        }

        public AnnualMagazineDto UpdateAnnualMagazine(Guid id, UpdateAnnualMagazineDto updateAMDto)
        {
            _ = _annualMagazineRepository.GetAnnualMagazineById(id) ?? throw new NotFoundException("Annual Magazine does not exists");

            if (_annualMagazineRepository.CheckUpdateAcademicYear(updateAMDto.AcademicYear, id))
            {
                throw new ConflictException("Academic Year already exists");
            }

            updateAMDto.AnnualMagazineId = id;

            AnnualMagazine aMToUpdate = _mapper.Map<AnnualMagazine>(updateAMDto);
            _annualMagazineRepository.UpdateAnnualMagazine(aMToUpdate);

            return _mapper.Map<AnnualMagazineDto>(aMToUpdate);
        }
    }
}
