using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class FeedbackRepository : IFeedbackRepository
	{
		private readonly ApplicationDbContext _context;

		public FeedbackRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void CreateFeedback(Feedback feedback)
		{
			try
			{
				_context.Feedbacks.Add(feedback);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error creating feedback");
			}
		}

		public void DeleteFeedback(Feedback feedback)
		{
			try
			{
				_context.Feedbacks.Remove(feedback);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error deleting feedback");
			}
		}

		public List<Feedback> GetAllFeedback(Guid contributionId)
		{
			try
			{
				return _context.Feedbacks.ToList();
			}
			catch (Exception)
			{
				throw new Exception("Error fetch feedback");
			}
		}

		public Feedback GetFeedbackById(Guid id)
		{
			try
			{
				return _context.Feedbacks
					.Where(u => u.FeedBackId == id)
					.AsNoTracking()
					.Include(f => f.User)
					.FirstOrDefault();
			}
			catch (Exception)
			{
				throw new Exception("Error getting feedback");
			}
		}

		public void UpdateFeedback(Feedback feedback)
		{
			try
			{
				_context.Entry<Feedback>(feedback).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error updating feedback");
			}
		}
	}
}
