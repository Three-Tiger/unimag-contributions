using AutoMapper;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Feedback;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _feedbackRepository;

		public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository)
		{
			_mapper = mapper;
			_feedbackRepository = feedbackRepository;
		}

		public FeedbackDto AddFeedback(CreateFeedbackDto feedbackDto)
		{
			Feedback feedback = _mapper.Map<Feedback>(feedbackDto);
			_feedbackRepository.CreateFeedback(feedback);

			return _mapper.Map<FeedbackDto>(feedback);
		}

		public string DeleteFeedback(Guid id)
		{
			Feedback feedback = _feedbackRepository.GetFeedbackById(id)??throw new NotFoundException("Feedback does not exists");
			_feedbackRepository.DeleteFeedback(feedback);

			return "Delete successful";
		}

		public List<FeedbackDto> GetAllFeedback(Guid contributionId)
		{
			List<Feedback> feedbackList = _feedbackRepository.GetAllFeedback(contributionId);
			return _mapper.Map<List<FeedbackDto>>(feedbackList);
		}

		public FeedbackDto GetFeedbackById(Guid id)
		{
			Feedback feedback = _feedbackRepository.GetFeedbackById(id) ?? throw new NotFoundException("Feedback does not exists"); ;

			return _mapper.Map<FeedbackDto>(feedback);
		}

		public FeedbackDto UpdateFeedback(Guid id, UpdateFeedbackDto feedbackDto)
		{
			_ = _feedbackRepository.GetFeedbackById(id)??throw new NotFoundException("Feedback does not exists");

			feedbackDto.FeedbackId = id;

			Feedback feedbackToUpdate = _mapper.Map<Feedback>(feedbackDto);
			_feedbackRepository.UpdateFeedback(feedbackToUpdate);

			return _mapper.Map<FeedbackDto>(feedbackToUpdate);
		}
	}
}
