using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.Statistic;
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
			var academicYears = _context.AnnualMagazines.OrderBy(a => a.FinalClosureDate).Select(a => a.AcademicYear).ToList();
			var faculties = _context.Faculties.Where(f => f.Name != "Admin").Select(f => f.Name).Distinct().ToList();

			var result = new Dictionary<string, Dictionary<string, int>>();

			foreach (var academicYear in academicYears)
			{
				// Initialize a dictionary for the current academic year
				result[academicYear] = new Dictionary<string, int>();

				// Get contributions for the current academic year
				var contributionsByFacultyAndYear = _context.Contributions
					.Where(c => c.AnnualMagazine.AcademicYear == academicYear)
					.GroupBy(c => c.User.Faculty.Name)
					.Select(g => new
					{
						Faculty = g.Key,
						Count = g.Count()
					})
					.ToDictionary(c => c.Faculty, c => c.Count);

				// Add contributions for faculties with 0 count
				foreach (var faculty in faculties)
				{
					int count = 0;
					if (contributionsByFacultyAndYear.ContainsKey(faculty))
						count = contributionsByFacultyAndYear[faculty];
					result[academicYear][faculty] = count;
				}
			}

			return result;
		}

		public Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear()
		{
			var academicYears = _context.AnnualMagazines.OrderBy(a => a.FinalClosureDate).Select(a => a.AcademicYear).ToList();

			var result = new Dictionary<string, Dictionary<string, double>>();

			foreach (var academicYear in academicYears)
			{
				// Initialize dictionary for current academic year
				result[academicYear] = new Dictionary<string, double>();

				// Get contributions for the current academic year
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
					var percentage = totalContributionsForYear != 0 ? (contribution.Count / (double)totalContributionsForYear) * 100 : 0;

					// Populate the dictionary with faculty percentage contributions
					result[academicYear][contribution.Faculty] = Math.Round(percentage, 2);
				}

				// Add faculties with 0 contributions
				var faculties = _context.Faculties.Where(f => f.Name != "Admin").Select(f => f.Name).ToList();
				foreach (var faculty in faculties)
				{
					if (!result[academicYear].ContainsKey(faculty))
					{
						result[academicYear][faculty] = 0; // Set percentage to 0 for faculties with no contributions
					}
				}
			}

			return result;
		}

		public Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto)
		{
			var result = new Dictionary<string, double>();

			var totalContributions = _context.Contributions.Count();

			if (totalContributions == 0)
			{
				totalContributions = 1;
			}

			if (!statisticDto.FacultyId.HasValue)
			{
				var waitingContributions = _context.Contributions.Where(c => c.Status == EStatus.Waiting).Count();
				var acceptedContributions = _context.Contributions.Where(c => c.Status == EStatus.Approved).Count();
				var rejectedContributions = _context.Contributions.Where(c => c.Status == EStatus.Rejected).Count();

				result["Waiting"] = Math.Round((waitingContributions / (double)totalContributions) * 100, 2);
				result["Accepted"] = Math.Round((acceptedContributions / (double)totalContributions) * 100, 2);
				result["Rejected"] = Math.Round((rejectedContributions / (double)totalContributions) * 100, 2);
			}
			else
			{
				var acceptedContributions = _context.Contributions.Where(f => f.User.FacultyId == statisticDto.FacultyId && f.Status == EStatus.Approved).Count();
				var rejectedContributions = _context.Contributions.Where(f => f.User.FacultyId == statisticDto.FacultyId && f.Status == EStatus.Approved).Count();

				result["Accepted"] = Math.Round((acceptedContributions / (double)totalContributions) * 100, 2);
				result["Rejected"] = Math.Round((rejectedContributions / (double)totalContributions) * 100, 2);
			}

			return result;
		}

		public Dictionary<string, int> NumberOfAccountsCreated()
		{
			var result = new Dictionary<string, int>();

			var roles = _context.Roles.Select(r => r.Name).ToList();
			var totalAccounts = _context.Users.Count();

			foreach (var item in roles)
			{
				var user = _context.Users.Where(u => u.Role.Name == item);
				result[item] = user.Count();
			}

			return result;
		}

		public List<Contribution> GetTop6Contribution()
		{
			try
			{
				return _context.Contributions
					.Include(u => u.User).ThenInclude(u => u.Faculty)
					.Include(f => f.FileDetails)
					.Include(i => i.ImageDetails)
					.OrderByDescending(c => c.SubmissionDate)
					.Take(6)
					.ToList();
			}
			catch (Exception)
			{
				throw new Exception("Error getting Contribution");
			}
		}

		//get contribution belong to the account's faculty id 
		public Dictionary<string, int> TotalPublicContributionsByFacultyId(Guid facultyId)
		{
			var facultyName = _context.Faculties
				.Where(f => f.FacultyId == facultyId)
				.Select(f => f.Name)
				.FirstOrDefault();

			if (facultyName == null)
			{
				return null; // Faculty not found
			}

			int totalPublicContributions = _context.Contributions
				.Count(c => c.User.FacultyId == facultyId && c.IsPublished);

			var result = new Dictionary<string, int>();
			result["Total"] = totalPublicContributions;

			return result;
		}

		public Dictionary<string, int> GetNumberOfContributionsWithoutFeedback(Guid annualMagazineId)
		{
			var magazineName = _context.AnnualMagazines
				.Where(a => a.AnnualMagazineId == annualMagazineId)
				.FirstOrDefault();

			if (magazineName == null)
			{
				return null;
			}

			int contributionsWithoutFeedbackCount = _context.Contributions
				.Count(c => c.AnnualMagazineId == annualMagazineId && (c.Feedbacks == null || !c.Feedbacks.Any()));

			var result = new Dictionary<string, int>();
			result["Total"] = contributionsWithoutFeedbackCount;

			return result;
		}


		public Dictionary<string, double> GetPercentageOfContributionsWithFeedback(Guid annualMagazineId)
		{
			Dictionary<string, double> result = new Dictionary<string, double>();

			int totalContributions = _context.Contributions.Count(c => c.AnnualMagazineId == annualMagazineId);

			if (totalContributions == 0)
			{
				result["Percentage"] = 0; // To avoid division by zero
				return result;
			}

			// Get the number of contributions with feedback for the specified annualMagazineId
			var contributionsWithoutFeedback = GetNumberOfContributionsWithoutFeedback(annualMagazineId);
			int contributionsWithFeedback = totalContributions - contributionsWithoutFeedback["Total"];

			// Calculate the percentage
			double percentage = (double)contributionsWithFeedback / totalContributions * 100;

			result["Percentage"] = percentage;
			return result;
		}

		public Dictionary<string, double> GetPercentageOfContributionsWithFeedbackAfter14days(Guid annualMagazineId)
		{
			Dictionary<string, double> result = new Dictionary<string, double>();

			// Get the total number of contributions for the specified annualMagazineId
			int totalContributions = _context.Contributions.Count(c => c.AnnualMagazineId == annualMagazineId);

			if (totalContributions == 0)
			{
				result["Percentage"] = 0; // To avoid division by zero
				return result;
			}

			// Get the number of contributions with feedback after 14 days of submission for the specified annualMagazineId
			int contributionsWithFeedbackAfter14days = _context.Contributions
				.Count(c => c.AnnualMagazineId == annualMagazineId &&
							c.Feedbacks != null &&
							c.Feedbacks.Any(f => f.FeedbackDate < f.Contribution.SubmissionDate.AddDays(14)));

			// Calculate the percentage
			double percentage = (double)contributionsWithFeedbackAfter14days / totalContributions * 100;

			result["Percentage"] = percentage;
			return result;
		}

	}
}
