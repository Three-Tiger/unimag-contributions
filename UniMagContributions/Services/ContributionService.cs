using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFileService _fileService;
        private readonly IContributionRepository _contributionRepository;
        private readonly IAnnualMagazineRepository _annualMagazineRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public ContributionService(IMapper mapper, IContributionRepository contributionRepository, IAnnualMagazineRepository annualMagazineRepository, IFileService fileService, IFeedbackRepository feedbackRepository)
        {
            _mapper = mapper;
            _contributionRepository = contributionRepository;
            _annualMagazineRepository = annualMagazineRepository;
            _fileService = fileService;
            _feedbackRepository = feedbackRepository;
        }

        public ContributionDto AddContribution(CreateContributionDto contributionDto)
        {
            Contribution isContributionExist = _contributionRepository.IsContributionExist(contributionDto.UserId, contributionDto.AnnualMagazineId);
            if (isContributionExist != null)
            {
                Contribution result = _contributionRepository.GetContributionById(isContributionExist.ContributionId);
                return _mapper.Map<ContributionDto>(result);
            }

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

            if (contribution.FileDetails.Count > 0)
            {
                foreach(var file in contribution.FileDetails)
                {
                    _fileService.DeleteFile(file.FilePath);
                }
            }

            if (contribution.ImageDetails.Count > 0)
            {
                foreach (var image in contribution.ImageDetails)
                {
                    _fileService.DeleteFile(image.ImagePath);
                }
            }

            foreach (var feedback in contribution.Feedbacks)
            {
                _feedbackRepository.DeleteFeedback(feedback);
            }

            _contributionRepository.DeleteContribution(contribution);

            return "Delete successful";
        }

        public List<ContributionDto> GetAllContribution()
        {
            List<Contribution> contributionList = _contributionRepository.GetAllContribution();
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }

        public List<ContributionDto> GetTop6Contribution()
        {
            List<Contribution> contributionList = _contributionRepository.GetTop6Contribution();
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }

        public List<ContributionDto> GetContributionIsPublished(int limit)
        {
            List<Contribution> contributionList = _contributionRepository.GetContributionIsPublished(limit);
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }

        public ContributionDto GetContributionById(Guid id)
        {
            Contribution contribution = _contributionRepository.GetContributionById(id) ?? throw new NotFoundException("Contribution does not exists"); ;
            return _mapper.Map<ContributionDto>(contribution);
        }

        public FileContentResult GetContributionPicture(Guid id)
        {
            Contribution contribution = _contributionRepository.GetContributionById(id) ?? throw new NotFoundException("Contribution does not exists");

            if (contribution.ImageDetails.Count == 0)
            {
                throw new NotFoundException("Contribution does not have any picture");
            }

            List<ImageDetails> imageDetails = contribution.ImageDetails.ToList();
            return _fileService.GetFile(imageDetails[0].ImagePath);
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

        public ContributionDto GetContributionByMagazineIdAndUserId(Guid annualManagazinId, Guid userId)
        {
            Contribution isContributionExist = _contributionRepository.IsContributionExist(userId, annualManagazinId);
            if (isContributionExist == null)
            {
                throw new NotFoundException("Contribution does not exists");
            }

            Contribution result = _contributionRepository.GetContributionById(isContributionExist.ContributionId);
            return _mapper.Map<ContributionDto>(result);
        }

        public List<ContributionDto> GetContributionByUserId(Guid userId)
        {
            List<Contribution> contributionList = _contributionRepository.GetContributionByUserId(userId);
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }
    }
}
