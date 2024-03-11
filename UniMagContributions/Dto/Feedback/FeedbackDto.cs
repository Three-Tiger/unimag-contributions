namespace UniMagContributions.Dto.Feedback
{
	public class FeedbackDto
	{
		public Guid FeedBackId { get; set; }

		public string Content { get; set; }

		public DateTime FeedbackDate { get; set; }

		public Guid UserId { get; set; }

		public Guid ContributionId { get; set; }
	}
}
