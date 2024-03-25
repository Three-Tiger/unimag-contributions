using UniMagContributions.Dto.Feedback;

namespace UniMagContributions.Services.Interface
{
	public interface IFeedbackService
	{
		Task<FeedbackDto> AddFeedback(CreateFeedbackDto feedbackDto);
		FeedbackDto UpdateFeedback(Guid id, UpdateFeedbackDto feedbackDto);
		string DeleteFeedback(Guid id);
		FeedbackDto GetFeedbackById(Guid id);
		List<FeedbackDto> GetAllFeedback(Guid contributionId);
	}
}
