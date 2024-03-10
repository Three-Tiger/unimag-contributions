using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.FileDetails
{
	public class FileDetailDto
	{
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public Guid ContributionId { get; set; }
    }
}
