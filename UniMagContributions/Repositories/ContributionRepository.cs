using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public List<Contribution> GetContributionIsPublished(int limit)
        {
            try
            {
                if (limit == 0)
                {
                    return _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(i => i.ImageDetails)
                    .Where(u => u.IsPublished == true)
                    .OrderByDescending(c => c.SubmissionDate)
                    .ToList();
                }
                else
                {
                    return _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(i => i.ImageDetails)
                    .Where(u => u.IsPublished == true)
                    .OrderByDescending(c => c.SubmissionDate)
                    .Take(limit)
                    .ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error getting Contribution");
            }
        }

        public List<Contribution> GetContributionByMagazineIdAndFacultyId(QueryDto queryDto)
        {
            try
            {
                var query = _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(a => a.ImageDetails)
                    .Where(u => u.AnnualMagazineId == queryDto.AnnualMagazineId)
                    .AsQueryable();

                List<Contribution> contributions = query.ToList();

                if (queryDto.FacultyId.HasValue)
                {
                    query = query.Where(a => a.User.FacultyId == queryDto.FacultyId);
                }

                if (queryDto.UserId.HasValue)
                {
                    query = query.Where(a => a.UserId == queryDto.UserId);
                }

                contributions = query.ToList();

                return contributions;
            }
            catch (Exception)
            {
                throw new Exception("Error getting Contribution");
            }
        }

        public List<Contribution> GetContributionByFilter(FilterDto filterDto)
        {
            try
            {
                var query = _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(i => i.ImageDetails)
                    .Include(a => a.AnnualMagazine)
                    .OrderByDescending(c => c.SubmissionDate)
                    .AsQueryable();

                if (filterDto.FacultyId.HasValue)
                {
                    query = query.Where(u => u.User.FacultyId == filterDto.FacultyId);
                }

                if (filterDto.AnnualMagazineId.HasValue)
                {
                    query = query.Where(u => u.AnnualMagazineId == filterDto.AnnualMagazineId);
                }

                if (filterDto.Status.HasValue)
                {
                    query = query.Where(u => u.Status == filterDto.Status);
                }

                if (filterDto.IsPublished.HasValue)
                {
                    query = query.Where(u => u.IsPublished == filterDto.IsPublished);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error getting Contribution");
            }
        }

        public List<Contribution> GetContributionByUserId(Guid userId)
        {
            try
            {
                return _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(i => i.ImageDetails)
                    .Include(a => a.AnnualMagazine)
                    .Where(u => u.UserId == userId)
                    .OrderByDescending(c => c.SubmissionDate)
                    .ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error getting Contribution");
            }
        }

        public Contribution GetContributionById(Guid id)
        {
            try
            {
                return _context.Contributions
                    .Include(u => u.User).ThenInclude(u => u.Faculty)
                    .Include(f => f.FileDetails)
                    .Include(i => i.ImageDetails)
                    .Include(f => f.Feedbacks).ThenInclude(u => u.User)
                    .Include(a => a.AnnualMagazine)
                    .Where(u => u.ContributionId == id)
                    .AsNoTracking()
                    .FirstOrDefault();
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

        public List<Contribution> GetContributionByMagazineIdAndUserId(Guid userId, Guid annualMagazineId)
        {
            try
            {
                return _context.Contributions
                    .Where(u => u.UserId == userId && u.AnnualMagazineId == annualMagazineId)
                    .ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error checking Contribution");
            }
        }
    }
}
