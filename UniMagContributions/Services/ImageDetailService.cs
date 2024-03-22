using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Dto.ImageDetail;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class ImageDetailService : IImageDetailService
    {
        private readonly IFileService _fileService;
        private readonly IImageDetailRepository _imageDetailRepository;

        public ImageDetailService(IFileService fileService, IImageDetailRepository imageDetailRepository)
        {
            _fileService = fileService;
            _imageDetailRepository = imageDetailRepository;
        }

        public FileContentResult GetImage(Guid id)
        {
            ImageDetails imageDetails = _imageDetailRepository.GetImageDetailById(id);

            if (imageDetails == null)
            {
                throw new NotFoundException("Image not found");
            }

            return _fileService.GetFile(imageDetails.ImagePath);
        }

        public string AddImageDetail(CreateImageDetailDto imageDetailDto)
        {
            var result = _fileService.SaveFile(imageDetailDto.FileUpload, EFolder.ContributionImage);

            // if it is not right format
            if (result.Item1 == 0)
            {
                throw new InvalidException(result.Item2);
            }

            var imageDetail = new ImageDetails()
            {
                ImageName = imageDetailDto.FileUpload.FileName,
                ImagePath = result.Item2,
                ContributionId = imageDetailDto.ContributionId,
            };

            _imageDetailRepository.CreateImageDetail(imageDetail);

            return "Upload Success!";
        }

        public string AddMultipleImageDetail(List<CreateImageDetailDto> imageDetailDtos)
        {
            var listImageDetails = new List<ImageDetails>();
            foreach (var imageDetailDto in imageDetailDtos)
            {
                var result = _fileService.SaveFile(imageDetailDto.FileUpload, EFolder.ContributionImage);

                // if it is not right format
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }

                var imageDetail = new ImageDetails()
                {
                    ImageName = imageDetailDto.FileUpload.FileName,
                    ImagePath = result.Item2,
                    ContributionId = imageDetailDto.ContributionId,
                };

                listImageDetails.Add(imageDetail);
            }

            foreach (var item in listImageDetails)
            {
                _imageDetailRepository.CreateImageDetail(item);
            }

            return "Upload Success!";
        }

        public FileContentResult DownloadFileById(Guid id)
        {
            ImageDetails imageDetails = _imageDetailRepository.GetImageDetailById(id);
            return _fileService.DownloadFileById(imageDetails);
        }

        public FileContentResult DownloadMultipleImage(Guid contributionId)
        {
            List<ImageDetails> imageDetails = _imageDetailRepository.GetImageDetailByContributionId(contributionId);
            return _fileService.DownloadMultipleFile(imageDetails, EFolder.ContributionImage);
        }

        public string DeleteImageByContributionId(Guid contributionId)
        {
            List<ImageDetails> imageDetails = _imageDetailRepository.GetImageDetailByContributionId(contributionId);
            foreach (var image in imageDetails)
            {
                _fileService.DeleteFile(image.ImagePath);
                _imageDetailRepository.DeleteImageDetail(image);
            }
            return "Delete successful";
        }
    }
}
