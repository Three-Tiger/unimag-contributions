using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class StatisticRepository : IStatisticRepository
	{
		private readonly ApplicationDbContext _context;

		public StatisticRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndYear = _context.Contributions
			.GroupBy(c => new { c.User.Faculty.Name, c.AnnualMagazine.AcademicYear })
			.Select(g => new
			{
				Faculty = g.Key.Name,
				AcademicYear = g.Key.AcademicYear,
				Count = g.Count()
			})
			.ToList();

			var result = new Dictionary<string, Dictionary<string, int>>();

			foreach (var contribution in contributionsByFacultyAndYear)
			{
				if (!result.ContainsKey(contribution.Faculty))
				{
					result[contribution.Faculty] = new Dictionary<string, int>();
				}

				result[contribution.Faculty][contribution.AcademicYear] = contribution.Count;
			}

			return result;
		}

		public Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear()
		{
			var academicYears = _context.AnnualMagazines.Select(a => a.AcademicYear).ToList();

			var result = new Dictionary<string, Dictionary<string, double>>();

			foreach (var academicYear in academicYears)
			{
				var contributionsByFacultyAndYear = _context.Contributions
					.Where(c => c.AnnualMagazine.AcademicYear == academicYear)
					.GroupBy(c => c.User.Faculty.Name)
					.Select(g => new
					{
						Faculty = g.Key,
						Count = g.Count()
					})
					.ToList();

				var totalContributionsForYear = contributionsByFacultyAndYear.Sum(c => c.Count);

				foreach (var contribution in contributionsByFacultyAndYear)
				{
					var percentage = (contribution.Count / (double)totalContributionsForYear) * 100;

					if (!result.ContainsKey(contribution.Faculty))
					{
						result[contribution.Faculty] = new Dictionary<string, double>();
					}

					result[contribution.Faculty][academicYear] = percentage;
				}
			}

			return result;
		}

		public Dictionary<string, Dictionary<string, int>> GetNumberOfContributorsByFacultyAndAcademicYear()
		{
			var contributorsByFacultyAndYear = _context.Contributions
				.GroupBy(c => new { c.User.Faculty.Name, c.AnnualMagazine.AcademicYear })
				.Select(g => new
				{
					Faculty = g.Key.Name,
					AcademicYear = g.Key.AcademicYear,
					ContributorsCount = g.Select(c => c.UserId).Distinct().Count()
				})
				.ToList();

			var result = new Dictionary<string, Dictionary<string, int>>();

			foreach (var contributor in contributorsByFacultyAndYear)
			{
				if (!result.ContainsKey(contributor.Faculty))
				{
					result[contributor.Faculty] = new Dictionary<string, int>();
				}

				result[contributor.Faculty][contributor.AcademicYear] = contributor.ContributorsCount;
			}

			return result;
		}

	}
}
