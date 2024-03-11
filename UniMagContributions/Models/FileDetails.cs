using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniMagContributions.Constraints;

namespace UniMagContributions.Models
{
	public class FileDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid FileId { get; set; }
		
		[StringLength(255)]
		public string FileName {  get; set; }
		
		public string FilePath { get; set; }

		public EFileType FileType { get; set; }

		public Guid ContributionId { get; set; }

		[ForeignKey("ContributionId")]
		public virtual Contribution? Contribution { get; set; }
	}
}
