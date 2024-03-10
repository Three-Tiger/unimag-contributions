using AutoMapper;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class ContributionService : IContributionService
    {
        private readonly IMapper _mapper;
        private readonly IContributionRepository _contributionRepository;
        private readonly IAnnualMagazineRepository _annualMagazineRepository;

        public ContributionService(IMapper mapper, IContributionRepository contributionRepository, IAnnualMagazineRepository annualMagazineRepository)
        {
            _mapper = mapper;
            _contributionRepository = contributionRepository;
            _annualMagazineRepository = annualMagazineRepository;
        }

        public ContributionDto AddContribution(CreateContributionDto contributionDto)
        {
            Contribution contribution = _contributionRepository.GetContributionByTitle(contributionDto.Title);

            if (contribution != null)
            {
                throw new ConflictException("Contribution already exists");
            }

            contribution = _mapper.Map<Contribution>(contributionDto);
            _contributionRepository.CreateContribution(contribution);

            return _mapper.Map<ContributionDto>(contribution);
        }

        public string DeleteContribution(Guid id)
        {
            Contribution contribution = _contributionRepository.GetContributionById(id) ?? throw new NotFoundException("Contribution does not exists");
            _contributionRepository.DeleteContribution(contribution);

            return "Delete successful";
        }

        public List<ContributionDto> GetAllContribution()
        {
            List<Contribution> contributionList = _contributionRepository.GetAllContribution();
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }

        public ContributionDto GetContributionById(Guid id)
        {
            Contribution contribution = _contributionRepository.GetContributionById(id) ?? throw new NotFoundException("Contribution does not exists"); ;
            return _mapper.Map<ContributionDto>(contribution);
        }

        public List<ContributionDto> GetContributionByMagazineId(Guid annualManagazinId)
        {
            _ = _annualMagazineRepository.GetAnnualMagazineById(annualManagazinId) ?? throw new NotFoundException("Annual Magazine does not exists");

            List<Contribution> contributionList = _contributionRepository.GetContributionByMagazineId(annualManagazinId);
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }


        public ContributionDto UpdateContribution(Guid id, UpdateContributionDto updateContributionDto)
        {
            _ = _contributionRepository.GetContributionById(id) ?? throw new NotFoundException("Contribution does not exists");

            updateContributionDto.ContributionId = id;

            Contribution contributionToUpdate = _mapper.Map<Contribution>(updateContributionDto);
            _contributionRepository.UpdateContribution(contributionToUpdate);

            return _mapper.Map<ContributionDto>(contributionToUpdate);
        }
    }
}
