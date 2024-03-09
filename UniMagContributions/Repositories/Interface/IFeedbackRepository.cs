using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IFeedbackRepository
	{
		void CreateFeedback(Feedback feedback);
		Feedback GetFeedbackById(Guid id);
		List<Feedback> GetAllFeedback(Guid contributionId);
		void UpdateFeedback(Feedback feedback);
		void DeleteFeedback(Feedback feedback);
	}
}
