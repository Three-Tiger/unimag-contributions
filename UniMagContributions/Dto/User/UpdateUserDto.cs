using System.ComponentModel.DataAnnotations;
using UniMagContributions.Dto.Faculty;

namespace UniMagContributions.Dto.User
{
    public class UpdateUserDto : CreateUserDto
    {
        public Guid UserId { get; set; }
    }
}
