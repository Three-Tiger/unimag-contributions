using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class ContributionRepository : IContributionRepository
	{
		private readonly ApplicationDbContext _context;

		public ContributionRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public void CreateContribution(Contribution contribution)
		{
			try
			{
				_context.Contributions.Add(contribution);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error creating Contribution");
			}
		}

		public void DeleteContribution(Contribution contribution)
		{
			try
			{
				_context.Contributions.Remove(contribution);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error deleting Contribution");
			}
		}

		public List<Contribution> GetAllContribution()
		{
			try
			{
				return _context.Contributions.ToList();
			}
			catch (Exception)
			{
				throw new Exception("Error fetch Contribution");
			}
		}

		public Contribution GetContributionById(Guid id)
		{
			try
			{
				return _context.Contributions.Where(u => u.ContributionId == id).AsNoTracking().FirstOrDefault();
			}
			catch (Exception)
			{
				throw new Exception("Error getting Contribution");
			}
		}

		public Contribution GetContributionByTitle(string title)
		{
			try
			{
				Contribution contribution = _context.Contributions.FirstOrDefault(u => u.Title == title);
				return contribution;
			}
			catch (Exception)
			{
				throw new Exception("Error getting Contribution");
			}
		}

		public void UpdateContribution(Contribution contribution)
		{
			try
			{
				_context.Entry<Contribution>(contribution).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error updating Contribution");
			}
		}
	}
}
