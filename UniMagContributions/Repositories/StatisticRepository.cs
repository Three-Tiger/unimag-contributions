using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniMagContributions.Constraints;
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

        public Dictionary<string, double> GetAcceptanceRejectionRate()
        {
            var result = new Dictionary<string, double>();

            var totalContributions = _context.Contributions.Count();

            var waitingContributions = _context.Contributions.Where(c => c.Status == EStatus.Waiting).Count();
            var acceptedContributions = _context.Contributions.Where(c => c.Status == EStatus.Approved).Count();
            var rejectedContributions = _context.Contributions.Where(c => c.Status == EStatus.Rejected).Count();

            result["Waiting"] = Math.Round((waitingContributions / (double)totalContributions) * 100, 2);
            result["Accepted"] = Math.Round((acceptedContributions / (double)totalContributions) * 100, 2);
            result["Rejected"] = Math.Round((rejectedContributions / (double)totalContributions) * 100, 2);

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
    }
}
