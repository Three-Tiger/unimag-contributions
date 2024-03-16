using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Dto.ImageDetail;

namespace UniMagContributions.Services.Interface
{
    public interface IImageDetailService
    {
        FileContentResult GetImage(Guid id);
        string AddImageDetail(CreateImageDetailDto imageDetailDto);
        FileContentResult DownloadFileById(Guid id);
        string AddMultipleImageDetail(List<CreateImageDetailDto> imageDetailDtos);
        FileContentResult DownloadMultipleImage(Guid contributionId);
    }
}
