using AutoMapper;
using PusherServer;
using UniMagContributions.Dto.Feedback;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
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

        public async Task<FeedbackDto> AddFeedback(CreateFeedbackDto feedbackDto)
        {
            Feedback feedback = _mapper.Map<Feedback>(feedbackDto);
            _feedbackRepository.CreateFeedback(feedback);

            var options = new PusherOptions
            {
                Cluster = "ap1",
                Encrypted = true
            };

            var pusher = new Pusher(
              Environment.GetEnvironmentVariable("PUSHER_APP_ID"),
              Environment.GetEnvironmentVariable("PUSHER_APP_KEY"),
              Environment.GetEnvironmentVariable("PUSHER_APP_SECRET"),
              options);

            await pusher.TriggerAsync(
               Environment.GetEnvironmentVariable("CHANNEL_NAME"),
               Environment.GetEnvironmentVariable("EVENT_NAME"),
               new
               {
                   feedbackId = feedback.FeedBackId,
                   content = feedback.Content,
                   feedbackDate = feedback.FeedbackDate,
                   userId = feedback.UserId,
                   contributionId = feedback.ContributionId
               });

            return _mapper.Map<FeedbackDto>(_feedbackRepository.GetFeedbackById(feedback.FeedBackId));
        }

        public string DeleteFeedback(Guid id)
        {
            Feedback feedback = _feedbackRepository.GetFeedbackById(id) ?? throw new NotFoundException("Feedback does not exists");
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
            _ = _feedbackRepository.GetFeedbackById(id) ?? throw new NotFoundException("Feedback does not exists");

            feedbackDto.FeedbackId = id;

            Feedback feedbackToUpdate = _mapper.Map<Feedback>(feedbackDto);
            _feedbackRepository.UpdateFeedback(feedbackToUpdate);

            return _mapper.Map<FeedbackDto>(feedbackToUpdate);
        }
    }
}
